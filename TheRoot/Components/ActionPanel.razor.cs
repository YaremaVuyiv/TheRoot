using Microsoft.AspNetCore.Components;
using System;
using TheRoot.Data;
using TheRoot.Services.Movement;

namespace TheRoot.Components
{
    public partial class ActionPanel: IDisposable
    {
        [Inject]
        private StateContainer StateContainer { get; init; }

        [Inject]
        private IMovementService MovementService { get; init; }

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;
        }

        private void OnMoveClick()
        {
            StateContainer.ModifyState(new ActivateClearings
            {
                ClearingIds = MovementService.GetFromClearingIds(Data.Models.FactionType.MarquiseDeCat)
            });
        }

        private void OnRecruitClick()
        {
            StateContainer.ModifyState(new RecruitWarriors
            {
                ClearingIds = new int[]
                {
                    1, 2, 3, 4, 5, 7, 9, 10, 12
                }
            });
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }
    }
}
