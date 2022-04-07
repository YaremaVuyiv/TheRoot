namespace TheRoot.Domain.Entities;

public abstract record PiecesContainer<T> : BaseEntity where T : BaseEntity
{
    public PiecesContainer()
    {
        _pieces = new List<T>();
    }

    public PiecesContainer(IEnumerable<T> pieces)
    {
        _pieces = pieces.ToList();
    }

    private readonly List<T> _pieces;

    public IEnumerable<T> Pieces => _pieces.AsReadOnly();

    public void AddPieces(params T[] pieces)
    {
        if (pieces == null)
        {
            throw new ArgumentException("pieces are null");
        }

        var intersection = pieces.Intersect(_pieces).ToList();
        if (intersection.Count > 0)
        {
            throw new ArgumentException($"Piece {typeof(T)} with Id:{intersection.First().Id} already exists");
        }

        _pieces.AddRange(pieces);
    }

    public void RemovePiecesRange(params int[] pieceIds)
    {
        var exception = pieceIds.Except(_pieces.Select(x => x.Id)).ToList();
        if (exception.Count > 0)
        {
            throw new ArgumentException($"Piece {typeof(T)} with Id:{exception.First()} is not possible to delete");
        }

        _pieces.RemoveAll(x => pieceIds.Any(i => i == x.Id));
    }
}