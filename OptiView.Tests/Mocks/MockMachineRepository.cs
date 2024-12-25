using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OptiView.Domain.Entities;
using OptiView.Domain.Interfaces;
using Xunit.Abstractions;

namespace OptiView.Tests.Mocks
{
    public class MockMachineRepository : IMachineRepository
    {
        private readonly List<Machine> _machines = new()
        {
            new Machine { Id = "1", Name = "SuperSkySchorcher-01", Status =  MachineStatus.Online, LastUpdated = DateTime.UtcNow,  Description = "Test Machine 1" },
            new Machine { Id = "2", Name = "Drill-02", Status = MachineStatus.Maintenance, LastUpdated = DateTime.UtcNow, Description = "Test Machine 2" },
            new Machine { Id = "3", Name = "MechX92-01", Status =  MachineStatus.Online, LastUpdated = DateTime.UtcNow,  Description = "Test Machine 3" },
        };
        public Task<IEnumerable<Machine>> GetAllAsync() => Task.FromResult(_machines.AsEnumerable());

        public Task<Machine> GetByIdAsync(string id)
        {
            var machine = _machines.FirstOrDefault(m => m.Id == id);
            if (machine == null)
                throw new KeyNotFoundException($"Machine with id {id} not found");
            return Task.FromResult(machine);
        }

        public Task<Machine> AddAsync(Machine machine)
        {
            _machines.Add(machine);
            return Task.FromResult(machine);
        }

        public Task<Machine> UpdateAsync(Machine machine)
        {
            var existing = _machines.FirstOrDefault(m => m.Id == machine.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Machine with id {machine.Id} not found");
            
            existing.Name = machine.Name;
            existing.Status = machine.Status;
            existing.LastUpdated = machine.LastUpdated;
            existing.Description = machine.Description;
            return Task.FromResult(existing);
        }

        public Task<bool> DeleteAsync(string id)
        {
            var machine = _machines.FirstOrDefault(m => m.Id == id);
            if (machine != null)
            {
                _machines.Remove(machine);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task SendDataToMachineAsync(string id, object data)
        {
            var machine = _machines.FirstOrDefault(m => m.Id == id);
            if (machine != null)
            {
                machine.LastUpdated = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }
    }
}