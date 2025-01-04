using Microsoft.JSInterop;
using OptiView.Application.DTOs;
using OptiView.Domain.Entities;

namespace OptiView.Presentation.Components.Pages
{
    public partial class Machines
    {
        private List<MachineDto> machines = new();
        private bool showAddDialog = false;
        private MachineDto newMachine = new();
        private HashSet<string> isSending = new();
        private Dictionary<string, DateTime> lastSentTime = new();
        private Random random = new Random();

        protected override async Task OnInitializedAsync()
        {
            var machineEntities = await MachineService.GetAllMachinesAsync();
            machines = machineEntities.Select(machine => new MachineDto
            {
                Id = machine.Id,
                Name = machine.Name,
                Status = machine.Status,
                LastUpdated = machine.LastUpdated,
                Description = machine.Description
            }).ToList();
        }
        private async Task UpdateMachineStatus(string machineId, MachineStatus newStatus)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Attempting to update status for machine {machineId}");
                await MachineService.UpdateMachineStatusAsync(machineId, newStatus);
                await OnInitializedAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Error: {ex.Message}");
            }
        }
        private MachineStatus GetNextStatus(MachineStatus currentStatus)
        {
            return currentStatus switch
            {
                MachineStatus.Online => MachineStatus.Offline,
                MachineStatus.Offline => MachineStatus.Maintenance,
                MachineStatus.Maintenance => MachineStatus.Online,
                _ => MachineStatus.Offline
            };
        }
        private string GetStatusClass(MachineStatus status)
        {
            return status switch
            {
                MachineStatus.Online => "status-online",
                MachineStatus.Offline => "status-offline",
                MachineStatus.Maintenance => "status-maintenance",
                _ => "status-offline"
            };
        }
        private string GetButtonClass(MachineStatus status)
        {
            return status switch
            {
                MachineStatus.Online => "online",
                MachineStatus.Offline => "offline",
                MachineStatus.Maintenance => "maintenance",
                _ => "offline"
            };
        }
        private string GetRandomTemp() => random.Next(35, 85).ToString();
        private string GetRandomPressure() => (random.NextDouble() * 10 + 2).ToString("F1");
        private string GetRandomSpeed() => (random.Next(1000, 3000)).ToString();
        private string GetRandomPower() => (random.NextDouble() * 100 + 50).ToString("F1");
        private async Task SendDataToMachine(string machineId)
        {
            try
            {
                isSending.Add(machineId);
                StateHasChanged();

                var machineData = new MachineDataDto
                {
                    Temperature = double.Parse(GetRandomTemp()),
                    Pressure = double.Parse(GetRandomPressure()),
                    Speed = double.Parse(GetRandomSpeed()),
                    PowerUsage = double.Parse(GetRandomPower()),
                    ProductionCount = random.Next(100, 1000),
                    Timestamp = DateTime.Now
                };

                await Task.Delay(2000); // Simulera nätverksfördröjning
                await MachineService.SendDataToMachineAsync(machineId, machineData);
                lastSentTime[machineId] = DateTime.Now;
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Error sending telemetry: {ex.Message}");
            }
            finally
            {
                isSending.Remove(machineId);
                StateHasChanged();
            }
        }
        private async Task DeleteMachine(string machineId)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Attempting to delete machine {machineId}");
                await MachineService.DeleteMachineAsync(machineId);
                await OnInitializedAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Error: {ex.Message}");
            }
        }
        private void OpenAddMachineDialog()
        {
            showAddDialog = true;
            newMachine = new MachineDto
            {
                Id = Guid.NewGuid().ToString(),
                Status = MachineStatus.Offline,
                LastUpdated = DateTime.UtcNow
            };
        }
        private void CloseDialog()
        {
            showAddDialog = false;
        }
        private async Task HandleValidSubmit(MachineDto machineDto)
        {
            var machine = new Machine
            {
                Id = machineDto.Id,
                Name = machineDto.Name,
                Status = machineDto.Status,
                LastUpdated = machineDto.LastUpdated,
                Description = machineDto.Description
            };
            await MachineService.AddMachineAsync(machine);
            await OnInitializedAsync();
            showAddDialog = false;
        }
        private async Task AddNewMachine()
        {
            var newMachine = new MachineDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "New Machine",
                Status = MachineStatus.Offline,
                LastUpdated = DateTime.UtcNow
            };

            await MachineService.AddMachineAsync(new Machine
            {
                Id = newMachine.Id,
                Name = newMachine.Name,
                Status = newMachine.Status,
                LastUpdated = newMachine.LastUpdated
            });

            await OnInitializedAsync();
        }
        private string GetPercentage(Func<MachineDto, bool> predicate)
        {
            if (machines == null || !machines.Any())
                return "0";

            return Math.Round((double)machines.Count(predicate) / machines.Count * 100, 1).ToString("0.#");
        }
    }
}
