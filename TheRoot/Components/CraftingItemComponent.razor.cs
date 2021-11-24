using Microsoft.AspNetCore.Components;
using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Components
{
    public partial class CraftingItemComponent
    {
        [Inject]
        public StateContainer StateContainer { get; init; }

        [Parameter]
        public int CraftingItemId { get; init; }

        /*private CraftingItemModel CraftingItem =>
            StateContainer.State.CraftingItems[CraftingItemId];

        private string ItemImage =>
            CraftingItem.ItemType switch
            {
                CraftingItemType.Bag => "bag",
                CraftingItemType.Boot => "boot",
                CraftingItemType.Crossbow => "crossbow",
                CraftingItemType.Hammer => "hammer",
                CraftingItemType.Sword => "sword",
                CraftingItemType.Teapot => "teapot",
                CraftingItemType.Money => "money",
                _ => string.Empty
            };*/
    }
}
