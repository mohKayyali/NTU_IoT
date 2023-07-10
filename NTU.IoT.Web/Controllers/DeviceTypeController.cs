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

namespace NTU.IoT.Web.Controllers
{
    public class DeviceTypeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public DeviceTypeController(ApplicationDBContext db) {
            _db = db;
        }

        public IActionResult Index()
        {

            var list = _db.device_types.ToList();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                

                var deviceTypes=_db.device_types.ToList();
                if (deviceTypes.Where(dt => dt.name.ToLower() == deviceType.name.ToLower()).SingleOrDefault() != null)
                    ModelState.AddModelError("name", "Name is already exist");

                if (deviceTypes.Where(dt => dt.table_name.ToLower() == deviceType.table_name.ToLower()).SingleOrDefault() != null)
                    ModelState.AddModelError("table_name", "Table Name is already exist");


                if (deviceTypes.Where(dt => dt.topic_name.ToLower() == deviceType.topic_name.ToLower()).SingleOrDefault() != null)
                    ModelState.AddModelError("topic_name", "Topic Name is already exist");

                if(!Regex.IsMatch(deviceType.name, "^[a-zA-Z0-9]*$"))
                    ModelState.AddModelError("name", "Name must be alphanumeric");

                if (!Regex.IsMatch(deviceType.topic_name, "^[a-zA-Z0-9]*$"))
                    ModelState.AddModelError("topic_name", "Topic Name must be alphanumeric");

                if (!Regex.IsMatch(deviceType.table_name, "^[a-zA-Z0-9]*$"))
                    ModelState.AddModelError("table_name", "Table Name must be alphanumeric");

                if (!ModelState.IsValid)
                    return View();

                try
                {

                    FlowHandlerCreation flowHandler = new FlowHandlerCreation();
                    NoSqlMeasurementCreation noSqlMeasurement = new NoSqlMeasurementCreation();
                    DeviceTypeCreation deviceTypeCreation = new DeviceTypeCreation(_db);

                    flowHandler.SetNext(noSqlMeasurement).SetNext(deviceTypeCreation);
                    flowHandler.Handle(deviceType);

                    return RedirectToAction("Index", "DeviceType");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View();

        }

        
    }
}