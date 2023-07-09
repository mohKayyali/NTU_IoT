using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NTU_IOT.DataAccess;

namespace NTU_IoT.Controllers
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
    }
}