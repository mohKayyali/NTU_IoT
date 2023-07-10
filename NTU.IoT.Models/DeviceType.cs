using System.ComponentModel.DataAnnotations;

namespace NTU.IoT.Models;
public class DeviceType
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(20)]
    [Display(Name = "Name")]
    public string name { get; set; }

    [Required]
    [MaxLength(20)]
    [Display(Name ="Kafka & MQTT Topic Name") ]
    public string topic_name { get; set; }

    [Required]
    [MaxLength(20)]
    [Display(Name = "Database Table Name")]
    public string table_name { get; set; } 

}

