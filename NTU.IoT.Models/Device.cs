using System;
using System.ComponentModel.DataAnnotations;

namespace NTU.IoT.Models
{
	public class Device
	{
        [Key]
        public string Id { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public bool IsActive { get; set; }

    }
}

