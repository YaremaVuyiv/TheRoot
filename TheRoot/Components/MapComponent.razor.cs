using Microsoft.AspNetCore.Components;
using TheRoot.Data;
using TheRoot.Services.Movement;

namespace TheRoot.Components
{
    public partial class MapComponent
    {
        [Inject]
        public StateContainer StateContainer { get; init; }

        [Inject]
        public IMovementServiceFactory MovementServiceFactory { get; init; }

        private List<Clearing> Clearings => StateContainer.State.Clearings;

        protected override void OnInitialized()
        {
        }
    }
}
