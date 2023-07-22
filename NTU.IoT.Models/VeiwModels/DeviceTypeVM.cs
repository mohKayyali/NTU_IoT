using System;
using System.Web;
namespace NTU.IoT.Models
{
	public class DeviceTypeVM
	{
		public DeviceType deviceType { get; set; }
		public bool storeInSqlDB { get; set; }
	}
}

