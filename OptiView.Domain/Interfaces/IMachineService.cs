using OptiView.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiView.Domain.Interfaces
{
    public interface IMachineService
    {
        Task<IEnumerable<Machine>> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(string id);
        Task<Machine> UpdateMachineStatusAsync(string id, MachineStatus newStatus);
        Task<Machine> AddMachineAsync(Machine machine);
        Task<bool> DeleteMachineAsync(string id);
        Task SendDataToMachineAsync(string id, object data);
    }
}
