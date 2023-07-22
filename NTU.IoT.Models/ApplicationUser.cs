using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NTU.IoT.Models
{
	public class ApplicationUser:IdentityUser
	{
		public bool IsActive { get; set; }
		public ICollection<Device> Devices { get; set; }
	}
}

