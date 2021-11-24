using TheRoot.Data.Models;

namespace TheRoot.Data
{
    public class StateContainer
    {
        private readonly State _state;

        public StateContainer()
        {
            var craftingSlotItems = new List<Slot<CraftingItemType>>
            {
                new Slot<CraftingItemType>(new System.Drawing.Point(0, 0), 50)
                {
                    SlotPiece = CraftingItemType.Bag
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(17, 0), 50)
                {
                    SlotPiece = CraftingItemType.Boot
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(34, 0), 50)
                {
                    SlotPiece = CraftingItemType.Crossbow
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(52, 0), 50)
                {
                    SlotPiece = CraftingItemType.Sword
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(69, 0), 50)
                {
                    SlotPiece = CraftingItemType.Teapot
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(86, 0), 50)
                {
                    SlotPiece = CraftingItemType.Money
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(0, 50), 50)
                {
                    SlotPiece = CraftingItemType.Bag
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(17, 50), 50)
                {
                    SlotPiece = CraftingItemType.Boot
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(34, 50), 50)
                {
                    SlotPiece = CraftingItemType.Hammer
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(52, 50), 50)
                {
                    SlotPiece = CraftingItemType.Sword
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(69, 50), 50)
                {
                    SlotPiece = CraftingItemType.Teapot
                },
                new Slot<CraftingItemType>(new System.Drawing.Point(86, 50), 50)
                {
                    SlotPiece = CraftingItemType.Money
                }
            };

            var items = new Dictionary<FactionType, List<CraftingItemType>>();

            var slots = new List<ClearingSlot>
            {
                new ClearingSlot(1, 1, new System.Drawing.Point(20, 32), 25),
                new ClearingSlot(2, 2, new System.Drawing.Point(23, 72), 25),
                new ClearingSlot(3, 2, new System.Drawing.Point(61, 1), 25),
                new ClearingSlot(4, 3, new System.Drawing.Point(71, 26), 25),
                new ClearingSlot(5, 4, new System.Drawing.Point(14, 23), 25),
                new ClearingSlot(6, 4, new System.Drawing.Point(61, 17), 25),
                new ClearingSlot(7, 5, new System.Drawing.Point(7, 13), 25),
                new ClearingSlot(8, 5, new System.Drawing.Point(55, 73), 25),
                new ClearingSlot(9, 6, new System.Drawing.Point(64, 29), 25),
                new ClearingSlot(10, 6, new System.Drawing.Point(29, 67), 25),
                new ClearingSlot(11, 7, new System.Drawing.Point(59, 11), 25),
                new ClearingSlot(12, 7, new System.Drawing.Point(45, 59), 25),
                new ClearingSlot(13, 8, new System.Drawing.Point(25, 10), 25),
                new ClearingSlot(14, 8, new System.Drawing.Point(56, 35), 25),
                new ClearingSlot(15, 8, new System.Drawing.Point(21, 62), 25),
                new ClearingSlot(16, 9, new System.Drawing.Point(37, 8), 25),
                new ClearingSlot(17, 9, new System.Drawing.Point(9, 54), 25),
                new ClearingSlot(18, 10, new System.Drawing.Point(38, 23), 25),
                new ClearingSlot(19, 10, new System.Drawing.Point(58, 62), 25),
                new ClearingSlot(20, 11, new System.Drawing.Point(48, 11), 25),
                new ClearingSlot(21, 11, new System.Drawing.Point(9, 53), 25),
                new ClearingSlot(22, 12, new System.Drawing.Point(19, 34), 25)
            };

            var clearings = new List<Clearing>
            {
                new Clearing(id: 1, new System.Drawing.Point(5, 7), ClearingType.Fox),
                new Clearing(id: 2, new System.Drawing.Point(4, 35), ClearingType.Mouse),
                new Clearing(id: 3, new System.Drawing.Point(5, 76), ClearingType.Rabbit),
                new Clearing(id: 4, new System.Drawing.Point(48, 3), ClearingType.Rabbit),
                new Clearing(id: 5, new System.Drawing.Point(36, 23), ClearingType.Rabbit),
                new Clearing(id: 6, new System.Drawing.Point(26, 49), ClearingType.Fox),
                new Clearing(id: 7, new System.Drawing.Point(31, 81), ClearingType.Fox),
                new Clearing(id: 8, new System.Drawing.Point(57, 42), ClearingType.Mouse),
                new Clearing(id: 9, new System.Drawing.Point(52, 70), ClearingType.Mouse),
                new Clearing(id: 10, new System.Drawing.Point(81, 17), ClearingType.Mouse),
                new Clearing(id: 11, new System.Drawing.Point(84, 45), ClearingType.Fox),
                new Clearing(id: 12, new System.Drawing.Point(77, 80), ClearingType.Rabbit)
            };

            var connectedClearings = new Dictionary<int, int[]>
            {
                { 1, new int[] { 2, 4, 5 } },
                { 2, new int[] { 1, 3, 6 } },
                { 3, new int[] { 2, 6, 7 } },
                { 4, new int[] { 1, 10 } },
                { 5, new int[] { 1, 6, 10 } },
                { 6, new int[] { 2, 3, 5, 8, 9 } },
                { 7, new int[] { 3, 9 } },
                { 8, new int[] { 6, 11, 12 } },
                { 9, new int[] { 6, 7, 12 } },
                { 10, new int[] { 4, 5, 11 } },
                { 11, new int[] { 8, 10, 12 } },
                { 12, new int[] { 8, 9, 11 } },
            };

            _state = new State
            {
                Clearings = clearings,
                FromClearingId = null,
                Items = items,
                CraftingSlotItems = craftingSlotItems,
                ConnectedClearings = connectedClearings,
                ClearingSlots = slots
            };
        }

        public State State => _state;

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void ModifyState(IAction action)
        {
            switch (action)
            {
                case Build build:
                    {
                        //var slot = _state.ClearingSlots[build.SlotId - 1].SlotPiece = build.BuildingPiece;
                        break;
                    }
                case Move move:
                    {
                        /*if (_state.FromClearing.Warriors[move.Faction].Count < move.WarriorsCount)
                        {
                            break;
                        }

                        _state.FromClearing.Warriors[move.Faction].Count -= 1;
                        move.ToClearing.Warriors[move.Faction].Count += 1;*/
                        break;
                    }
                case CraftItem craftItem:
                    {
                        if (_state.Items.TryGetValue(craftItem.Faction, out var pieces))
                        {
                            pieces.Add(craftItem.Item);
                        }
                        else
                        {
                            _state.Items[craftItem.Faction] = new List<CraftingItemType>
                            {
                                craftItem.Item
                            };
                        }
                        
                        break;
                    }
                case ActivateClearings activateClearings:
                    {
                        foreach (var id in activateClearings.ClearingIds)
                        {
                            _state.Clearings[id - 1].IsActivated = true;
                        }

                        break;
                    }
                case DeactivateClearings:
                    {
                        foreach(var clearing in _state.Clearings)
                        {
                            clearing.IsActivated = false;
                        }

                        break;
                    }
                case RecruitWarriors recruitWarriors:
                    {
                        foreach(var id in recruitWarriors.ClearingIds)
                        {
                            _state.Clearings[id - 1].Warriors[FactionType.MarquiseDeCat] = 1;
                        }

                        break;
                    }
            }

            NotifyStateChanged();
        }
    }


    public interface IAction
    {

    }

    public interface IActionWithPayload<T> : IAction where T : class
    {
        T Payload { get; }
    }

    public class Build : IAction
    {
        public int SlotId { get; init; }
        public BuildingType BuildingPiece { get; init; }
    }

    public class Move: IAction
    {
        public Clearing ToClearing { get; init; }

        public int WarriorsCount { get; init; }

        public FactionType Faction { get; init; }
    }

    public class CraftItem: IAction
    {
        public CraftingItemType Item { get; init; }

        public FactionType Faction { get; init; }
    }

    public class ActivateClearings: IAction
    {
        public IEnumerable<int> ClearingIds { get; init; }
    }

    public class DeactivateClearings: IAction
    {

    }

    public class RecruitWarriors: IAction
    {
        public int[] ClearingIds { get; init; }
    }
}