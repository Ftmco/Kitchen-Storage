namespace KitchenStorage.Services.Implementation;

public class GetNote : IGetNote
{
    private readonly IBaseQuery<Note> _noteQuery;

    public GetNote(IBaseQuery<Note> query)
    {
        _noteQuery = query;
    }


    public async Task<PaginationResult<IEnumerable<Note>>> NotesAsync(int page, int pageCount)
    {
        IEnumerable<Note> notes = await _noteQuery.GetAllAsync(page, pageCount);
        var notesCount = await _noteQuery.CountAsync();

        return notes.GetPaginationResult(notesCount.PageCount(pageCount));
    }
}
