using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Components
{
    public partial class CraftingItemsComponent: IDisposable
    {
        [Inject]
        public StateContainer StateContainer { get; init; }

        protected override void OnInitialized()
        {
            StateContainer.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            StateContainer.OnChange -= StateHasChanged;
        }

        /*public IEnumerable<Slot> CraftingItems =>
            StateContainer.State.CraftingSlotItems.Select(x =>
            {
                var slotPiece = x.SlotPiece;
                foreach (var y in StateContainer.State.Items.SelectMany(x => x.Value))
                {
                    if (y == x.SlotPiece)
                    {
                        slotPiece = null;
                    }
                }

                return new Slot(x.Location, x.Height)
                {
                    SlotPiece = slotPiece
                };
            });*/

        public CraftingItemsComponent()
        {
            /*var items = new List<Slot>
            {
                new Slot(new System.Drawing.Point(0, 0), 50)
                {
                    SlotPiece = new CrossbowItem()
                },
                new Slot(new System.Drawing.Point(17, 0), 50){
                    SlotPiece = new CrossbowItem()
                },
                new Slot(new System.Drawing.Point(34, 0), 50)
                {
                    SlotPiece = new CrossbowItem()
                },
                new Slot(new System.Drawing.Point(52, 0), 50)
                {
                    SlotPiece = new CrossbowItem()
                },
                new Slot(new System.Drawing.Point(69, 0), 50)
                {
                    SlotPiece = new CrossbowItem()
                },
                new Slot(new System.Drawing.Point(86, 0), 50)
                {
                    SlotPiece = new CrossbowItem()
                },
                new Slot(new System.Drawing.Point(0, 50), 50),
                new Slot(new System.Drawing.Point(17, 50), 50),
                new Slot(new System.Drawing.Point(34, 50), 50),
                new Slot(new System.Drawing.Point(52, 50), 50),
                new Slot(new System.Drawing.Point(69, 50), 50),
                new Slot(new System.Drawing.Point(86, 50), 50),
            };*/
        }
    }
}
