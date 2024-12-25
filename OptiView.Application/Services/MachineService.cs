using OptiView.Domain.Entities;
using OptiView.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Machine = OptiView.Domain.Entities.Machine;

namespace OptiView.Application.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _repository;

        public MachineService(IMachineRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Machine> GetMachineByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Machine> UpdateMachineStatusAsync(string id, MachineStatus newStatus)
        {
            var machine = await _repository.GetByIdAsync(id);
            machine.Status = newStatus;
            machine.LastUpdated = DateTime.UtcNow;
            return await _repository.UpdateAsync(machine);
        }
        public async Task<Machine> AddMachineAsync(Machine machine)
        {
            return await _repository.AddAsync(machine);
        }

        public async Task<bool> DeleteMachineAsync(string id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task SendDataToMachineAsync(string id, object data)
        {
            await _repository.SendDataToMachineAsync(id, data);
        }
    }
}
