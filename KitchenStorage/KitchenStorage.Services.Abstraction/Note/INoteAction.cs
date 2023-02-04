namespace KitchenStorage.Services.Abstraction
{
    public interface INoteAction
    {
        Task<Either<NoteActionStatus, Note>> UpsertAsync(UpsertNoteViewModel upsert);

        Task<Either<NoteActionStatus, Note>> CreateAsync(UpsertNoteViewModel upsert);

        Task<Either<NoteActionStatus, Note>> UpdateAsync(UpsertNoteViewModel upsert);

        Task<Either<NoteActionStatus, Note>> DeleteAsync(Guid id);
    }
}
