namespace KitchenStorage.Services.Abstraction
{
    public interface IGetNote
    {
        Task<PaginationResult<IEnumerable<Note>>> NotesAsync(int page, int pageCount);
        Task<IEnumerable<Note>> NotesAsync();
        Task<Note?> FindByIdAsync(Guid id);
    }
}
