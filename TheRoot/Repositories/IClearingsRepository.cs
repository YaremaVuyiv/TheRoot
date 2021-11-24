using TheRoot.Data;

namespace TheRoot.Repositories
{
    public interface IClearingsRepository
    {
        List<int> GetAllClearingIds();

        List<BuildingType?> GetSlotPiecesByClearingId(int clearingId);

        List<ClearingSlot> GetSlotsByClearingId(int clearingId);

        List<int> GetAllSlotIds();

        BuildingType? GetSlotPieceById(int slotId);

        List<int> GetConnectedClearings(int clearingId);
    }

    public class ClearingsRepository : IClearingsRepository
    {
        private readonly StateContainer _stateContainer;

        public ClearingsRepository(StateContainer stateContainer)
        {
            _stateContainer = stateContainer;
        }

        public List<int> GetAllClearingIds() =>
                _stateContainer.State.Clearings
                .Select(x => x.Id)
                .ToList();

        public List<int> GetAllSlotIds() =>
                _stateContainer.State.ClearingSlots
                .Select(x => x.Id)
                .ToList();

        public BuildingType? GetSlotPieceById(int slotId) =>
                _stateContainer.State.ClearingSlots
                .First(x => x.Id == slotId)
                .SlotPiece;

        public List<BuildingType?> GetSlotPiecesByClearingId(int clearingId) =>
                _stateContainer.State.ClearingSlots
                .Where(x => x.ClearingId == clearingId)
                .Select(x => x.SlotPiece)
                .ToList();

        public List<ClearingSlot> GetSlotsByClearingId(int clearingId) =>
                _stateContainer.State.ClearingSlots
                .Where(x => x.ClearingId == clearingId)
                .ToList();

        public List<int> GetConnectedClearings(int clearingId) =>
                _stateContainer.State.ConnectedClearings[clearingId]
                .ToList();
    }
}
