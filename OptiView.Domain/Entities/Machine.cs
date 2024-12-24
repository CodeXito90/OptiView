using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiView.Domain.Entities
{
    public class Machine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MachineStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Description { get; set; }
    }

    public enum MachineStatus
    {
        Online,
        Offline,
        Maintenance
    }
}
