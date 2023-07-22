using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NTU.IoT.Models;
using NTU.IoT.DataAccess;
using RestSharp;
using NTU.IoT.Utility.ChainOfResponsibility;
using NTU.IoT.Utility;
using Microsoft.AspNetCore.Authorization;

namespace NTU.IoT.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DeviceTypeController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IConfiguration _configuration;
        private readonly InfluxDBService _influxService;

        public DeviceTypeController(ApplicationDBContext db, IConfiguration configuration, InfluxDBService influxDBService) {
            _db = db;
            _configuration = configuration;
            _influxService = influxDBService;
        }

        public IActionResult Index(string success)
        {
            if(success!=null && success=="success")
                TempData["Notification"] = "Success";



            var list = _db.device_types.ToList();

            return View("Index", list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DeviceTypeVM deviceTypeVM)
        {
            

            if (ModelState.IsValid)
            {
                

                var deviceTypes=_db.device_types.ToList();
                if (deviceTypes.Where(dt => dt.name.ToLower() == deviceTypeVM.deviceType.name.ToLower()).SingleOrDefault() != null)
                    ModelState.AddModelError("name", "Name is already exist");

                if (deviceTypes.Where(dt => dt.table_name.ToLower() == deviceTypeVM.deviceType.table_name.ToLower()).SingleOrDefault() != null)
                    ModelState.AddModelError("table_name", "Table Name is already exist");


                if (deviceTypes.Where(dt => dt.topic_name.ToLower() == deviceTypeVM.deviceType.topic_name.ToLower()).SingleOrDefault() != null)
                    ModelState.AddModelError("topic_name", "Topic Name is already exist");

                if(!Regex.IsMatch(deviceTypeVM.deviceType.name, "^[a-zA-Z0-9]*$"))
                    ModelState.AddModelError("name", "Name must be alphanumeric");

                if (!Regex.IsMatch(deviceTypeVM.deviceType.topic_name, "^[a-zA-Z0-9]*$"))
                    ModelState.AddModelError("topic_name", "Topic Name must be alphanumeric");

                if (!Regex.IsMatch(deviceTypeVM.deviceType.table_name, "^[a-zA-Z0-9]*$"))
                    ModelState.AddModelError("table_name", "Table Name must be alphanumeric");

                if (!ModelState.IsValid)
                    return View();

                try
                {

                    FlowHandlerCreation flowHandler = new FlowHandlerCreation();
                    NoSqlMeasurementCreation noSqlMeasurement = new NoSqlMeasurementCreation(_influxService);
                    DeviceTypeCreation deviceTypeCreation = new DeviceTypeCreation(_db);

                     flowHandler.SetNext(noSqlMeasurement).SetNext(deviceTypeCreation);
                    TempData["Notification"] = "Success";
                    

                     flowHandler.Handle(deviceTypeVM.deviceType);

                    return RedirectToAction("Index", "DeviceType");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            return View();

        }

        [HttpDelete]
        public IActionResult Delete(Guid Id) {

            var deviceType=_db.device_types.Find(Id);

            _db.device_types.Remove(deviceType);
            _db.SaveChanges();

           
            return RedirectToAction("Index");
        }

        
    }
}