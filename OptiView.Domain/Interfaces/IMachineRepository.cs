using OptiView.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiView.Domain.Interfaces
{
    public interface IMachineRepository
    {
        Task<IEnumerable<Machine>> GetAllAsync();
        Task<Machine> GetByIdAsync(string id);
        Task<Machine> UpdateAsync(Machine machine);
        Task<Machine> AddAsync(Machine machine);
        Task<bool> DeleteAsync(string id);
    }
}
