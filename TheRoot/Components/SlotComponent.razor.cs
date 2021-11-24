using Microsoft.AspNetCore.Components;
using System;
using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Components
{
    public partial class SlotComponent: IDisposable
    {
        [Inject]
        public StateContainer StateContainer { get; init; }

        [Parameter]
        public ClearingSlot Slot { get; init; }

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;
        }

        private double TopLocation => Slot.Location.Y;
        private double LeftLocation => Slot.Location.X;
        private string BuildingImage => Slot.SlotPiece switch
        {
            BuildingType.Sawmill => "Marquise/sawmill",
            BuildingType.Recruiter => "Marquise/recruiter",
            BuildingType.Workshop => "Marquise/workshop",
            BuildingType.Nest => "Eyrie/nest",
            BuildingType.AllianseBase => "",
            BuildingType.Ruin => "ruin",
            _ => throw new NotImplementedException()
        };

        private void OnSlotClick()
        {
            StateContainer.ModifyState(new CraftItem
            {
                //Item = Slot.SlotPiece,
                Faction = FactionType.MarquiseDeCat
            });
            /*StateContainer.ModifyState(new Build
            {
                Slot = Slot
            });*/
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }
    }
}
