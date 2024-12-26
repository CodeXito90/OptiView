using OptiView.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OptiView.Application.DTOs
{
    public class MachineDto
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public MachineStatus Status { get; set; }

        public DateTime LastUpdated { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; } = string.Empty;

        public string StatusDisplay => Status.ToString();
        public string LastUpdatedDisplay => LastUpdated.ToString("dd-MMM-yyyy, HH:mm");
        public string StatusColor => Status switch
        {
            MachineStatus.Online => "text-green-500",
            MachineStatus.Offline => "text-red-500",
            MachineStatus.Maintenance => "text-yellow-500",
            _ => "text-gray-500"
        };
    }
}
