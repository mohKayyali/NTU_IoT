using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTU.IoT.DataAccess;
using NTU.IoT.DataAccess.Migrations;
using NTU.IoT.Models;
using NTU.IoT.Utility;

namespace NTU.IoT.Web.Areas.User.Controllers
{

    [Area("User")]
    [Authorize(Roles = "User,Super User")]
    public class DeviceController : Controller
    {

        private readonly ApplicationDBContext _db;
        private readonly IConfiguration _configuration;
        private readonly InfluxDBService _influxService;
        private readonly UserManager<IdentityUser> _userManager;

        public DeviceController(ApplicationDBContext db, IConfiguration configuration, InfluxDBService influxDBService, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _configuration = configuration;
            _influxService = influxDBService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var deviceTypes=_db.device_types.ToList();

            List<Device> deviceList = new List<Device>();

            foreach (var dt in deviceTypes)
            {

                var reult = _influxService.QueryAsync(async query =>
                {
                    var flux = "from(bucket:\"IOT\") " +
               "|> range(start: 0) " +
               "|> filter(fn: (r) => " +
               "r._measurement == \""+ dt.table_name + "\" and " +
               "r._field == \"device_id\" " +
               ")"
               + "|> distinct()"; ;
                    var loggedInUsr = (ApplicationUser)_userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();
                    
                    var tables = await query.QueryAsync(flux, "NTU");
                    return tables.SelectMany(table =>
                        table.Records.Select(record => {
                            Device dbDevice = _db.devices.Include(dev => dev.Users).SingleOrDefault(dev => dev.Id == record.GetValue().ToString());
                            List<ApplicationUser> usrs = null;
                            if (dbDevice != null && dbDevice.Users!=null)
                                usrs = dbDevice.Users.Where(u => u.Id == loggedInUsr.Id).ToList();

                            

                            var device = new Device
                            {
                                Id = record.GetValue().ToString(),
                                DeviceType = dt,
                                Users = usrs
                                //&&
                                //dev.Users.Any(user => user.Id == loggedInUsr.Id))?.Users
                            };
                            return device;
                            })) ;
                }).GetAwaiter().GetResult();

                deviceList.AddRange(reult.ToList<Device>());
            }
            

            return View(deviceList);

        }

        [HttpPut]
        public IActionResult Update([FromQuery] string id, [FromQuery] Guid devicetype, [FromQuery] bool status)
        {
           
            Device device = _db.devices.Include(u => u.Users).FirstOrDefault(dev=>dev.Id== id);

            if (device == null) {
                var deviceType = _db.device_types.Find(devicetype);
                device = new Device{
                    DeviceType= deviceType,
                    Id= id,
                    IsActive=true
                };

                _db.devices.Add(device);
                _db.SaveChanges();

            }
            ApplicationUser usr = (ApplicationUser)_userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();

                
                if (status)
                {
                if (device.Users != null)
                    device.Users.Add(usr);
                else {
                    device.Users = new List<ApplicationUser>();
                    device.Users.Add(usr);
                }
                }
                else
                    device.Users.Remove(usr);
                    
            _db.SaveChanges();

            

            return Json(new {data= "success" });
        }
    }
}