using OptiView.Domain.Entities;

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
