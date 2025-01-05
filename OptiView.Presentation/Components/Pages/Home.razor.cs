using OptiView.Application.DTOs;
using OptiView.Domain.Entities;
using System.Linq;

namespace OptiView.Presentation.Components.Pages
{
    public partial class Home
    {
        private List<MachineDto> machines;

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

        private void NavigateToMachines()
        {
            NavigationManager.NavigateTo("/machines");
        }

        private string GetPercentage(Func<MachineDto, bool> predicate)
        {
            if (machines == null || !machines.Any())
                return "0";

            return Math.Round((double)machines.Count(predicate) / machines.Count * 100, 1).ToString("0.#");
        }
    }
}
