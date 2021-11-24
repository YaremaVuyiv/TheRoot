using Microsoft.AspNetCore.Components;
using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Repositories;
using TheRoot.Services;
using TheRoot.Services.Movement;

namespace TheRoot.Components
{
    public partial class ClearingComponent: IDisposable
    {
        [Inject]
        private IClearingsRepository ClearingsRepository { get; init; }

        [Inject]
        private IWarriorsRepository WarriorsRepository { get; init; }

        [Inject]
        private IDominanceService DominanceService { get; init; }

        [Inject]
        private IMovementService MovementService { get; init; }

        [Parameter]
        public Clearing Clearing { get; init; }

        private IEnumerable<ClearingSlot> Slots => ClearingsRepository.GetSlotsByClearingId(Clearing.Id);

        private Dictionary<FactionType, int> Warriors =>
            WarriorsRepository.GetClearingWarriors(Clearing.Id);

        private string ClearingBorder
        {
            get
            {
                var color = Clearing.ClearingType switch
                {
                    ClearingType.Fox => "fox-clearing",
                    ClearingType.Mouse => "mouse-clearing",
                    ClearingType.Rabbit => "rabbit-clearing",
                    _ => string.Empty
                };

                color = Clearing.IsActivated ? "activated-clearing" : color;

                return color;
            }
        }

        protected override void OnInitialized()
        {
            //StateContainer.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            //StateContainer.OnChange -= StateHasChanged;
        }

        private void OnClearingClick()
        {
            /*if (StateContainer.State.FromClearingId == null)
            {
                StateContainer.State.FromClearingId = Clearing.Id;

                StateContainer.ModifyState(new DeactivateClearings());

                StateContainer.ModifyState(new ActivateClearings
                {
                    ClearingIds = MovementService.GetToClearingIds(FactionType.MarquiseDeCat, Clearing.Id)
                });
            }
            else
            {
                StateContainer.ModifyState(new Move
                {
                    ToClearing = Clearing,
                    WarriorsCount = 1,
                    Faction = FactionType.MarquiseDeCat
                });

                StateContainer.ModifyState(new DeactivateClearings());

                StateContainer.State.FromClearingId = null;
            }*/
        }

        private string DominanceColor =>
            DominanceService.GetDominantFactionInClearing(Clearing.Id) switch
            {
                FactionType.MarquiseDeCat => "ff0000",
                FactionType.EyrieDynasties => "0000ff",
                FactionType.WoodlandAllianse => "00ff00",
                _ => "000000"
            };
    }
}
