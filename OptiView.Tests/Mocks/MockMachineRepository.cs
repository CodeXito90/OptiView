using Moq;
using OptiView.Domain.Entities;
using OptiView.Domain.Interfaces;

namespace OptiView.Tests.Mocks
{
    public class MockMachineRepository : Mock<IMachineRepository>
    {
        public MockMachineRepository()
        {
            var machines = new List<Machine>
            {
                new Machine 
                { 
                    Id = "1", 
                    Name = "Machine 1", 
                    Status = MachineStatus.Online,
                    LastUpdated = DateTime.UtcNow,
                    Description = "Test Machine 1"
                }
            };

            Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(machines);

            Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => machines.FirstOrDefault(m => m.Id == id));

            Setup(repo => repo.AddAsync(It.IsAny<Machine>()))
                .ReturnsAsync((Machine machine) =>
                {
                    machines.Add(machine);
                    return machine;
                });

            Setup(repo => repo.UpdateAsync(It.IsAny<Machine>()))
                .ReturnsAsync((Machine machine) =>
                {
                    var existingMachine = machines.FirstOrDefault(m => m.Id == machine.Id);
                    if (existingMachine != null)
                    {
                        existingMachine.Name = machine.Name;
                        existingMachine.Status = machine.Status;
                        existingMachine.LastUpdated = machine.LastUpdated;
                        existingMachine.Description = machine.Description;
                    }
                    return existingMachine;
                });

            Setup(repo => repo.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) =>
                {
                    var machine = machines.FirstOrDefault(m => m.Id == id);
                    if (machine != null)
                    {
                        machines.Remove(machine);
                        return true;
                    }
                    return false;
                });
        }
    }
}
