using OptiView.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiView.Infrastructure.Repositories
{
    public class MockMachineRepository
    {
        private readonly List<Machine> _machines;

        public MockMachineRepository()
        {
            _machines = new List<Machine>
            {
                new Machine
                {
                    Id = "1",
                    Name = "Lathe-01",
                    Status = MachineStatus.Online,
                    LastUpdated = DateTime.Now,
                    Description = "Primary Lathe Machine"
                },
                new Machine
                {
                    Id = "2",
                    Name = "Drill-02",
                    Status = MachineStatus.Offline,
                    LastUpdated = DateTime.Now.AddMinutes(-35),
                    Description = "Industrial Drill Press"
                }
            };
        }

        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            return await Task.FromResult(_machines);
        }

        public async Task<Machine> GetByIdAsync(string id)
        {
            return await Task.FromResult(_machines.FirstOrDefault(m => m.Id == id));
        }

        public async Task<Machine> UpdateAsync(string id, MachineStatus newStatus)
        {
            var machine = _machines.FirstOrDefault(m => m.Id == id);
            if (machine != null)
            {
                machine.Status = newStatus;
                machine.LastUpdated = DateTime.Now;
            }
            return await Task.FromResult(machine);
        }

        public async Task<Machine> AddAsync(Machine machine)
        {
            machine.Id = (_machines.Count + 1).ToString();
            _machines.Add(machine);
            return await Task.FromResult(machine);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var machine = _machines.FirstOrDefault(m => m.Id == id);
            if (machine != null)
            {
                _machines.Remove(machine);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
