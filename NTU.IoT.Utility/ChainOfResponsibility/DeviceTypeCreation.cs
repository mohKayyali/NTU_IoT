using System;
using NTU.IoT.Models;
using NTU.IoT.DataAccess;

namespace NTU.IoT.Utility.ChainOfResponsibility
{
	public class DeviceTypeCreation: AbstractHandler
	{
        private readonly ApplicationDBContext _db;

        public DeviceTypeCreation(ApplicationDBContext db)
        {
            _db = db;
        }

        public override object Handle(object obj)
        {
            System.Diagnostics.Debug.WriteLine("DeviceTypeCreation " + ((DeviceType)obj).name);

            var deviceType = (DeviceType)obj;
            deviceType.Id = Guid.NewGuid();
            _db.Add(deviceType);
            _db.SaveChanges();

            return base.Handle(obj);
        }
    }
}

