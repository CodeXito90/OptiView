using OptiView.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiView.Application.DTOs
{
    public class MachineDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MachineStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Description { get; set; }
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
