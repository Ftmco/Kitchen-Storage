using System.Linq.Expressions;

namespace KitchenStorage.Services.Implementation;

public class GetNote : IGetNote
{
    private readonly IBaseQuery<Note> _noteQuery;

    public GetNote(IBaseQuery<Note> query)
    {
        _noteQuery = query;
    }


    public async Task<PaginationResult<IEnumerable<Note>>> NotesAsync(int page, int pageCount, string? q)
    {
        Expression<Func<Note, bool>> query = n => q == null || n.Title.Contains(q);
        IEnumerable<Note> notes = await _noteQuery.GetAllAsync(page, pageCount);
        var notesCount = await _noteQuery.CountAsync(query);

        return notes.GetPaginationResult(notesCount.PageCount(pageCount));
    }
}
