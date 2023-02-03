using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace KitchenStorage.Services.Implementation
{
    public class NoteAction : INoteAction
    {
        private readonly IBaseCud<Note> _noteAction;
        private readonly IBaseQuery<Note> _noteQuery;


        public NoteAction
            (IBaseCud<Note> noteCud,
            IBaseQuery<Note> noteQuery)
        {
            _noteAction = noteCud;
            _noteQuery = noteQuery;
        }


        public async Task<Either<NoteActionStatus, Note>> CreateAsync(UpsertNoteViewModel upsert)
        {
            Note newNote = new()
            {
                Title = upsert.Title,
                Description = upsert.Description,
                Importance = upsert.Importance,
            };

            return await _noteAction.InsertAsync(newNote) ?
                        newNote : NoteActionStatus.Failed;
        }

        public async Task<Either<NoteActionStatus, Note>> UpdateAsync(UpsertNoteViewModel upsert)
        {
            var note = await _noteQuery.GetAsync(upsert.Id);
            if (note is null)
                return NoteActionStatus.NotFound;

            note.Title = upsert.Title;
            note.Importance = upsert.Importance;
            note.Description = upsert.Description;

            return await _noteAction.UpdateAsync(note) ?
            note : NoteActionStatus.Failed;
        }

        public async Task<Either<NoteActionStatus, Note>> UpsertAsync(UpsertNoteViewModel upsert)
                => upsert.Id is null
                ? await CreateAsync(upsert)
                : await UpdateAsync(upsert);

        public async Task<Either<NoteActionStatus, Note>> DeleteAsync(Guid id)
        {
            var note = await _noteQuery.GetAsync(id);

            if (note is null)
                return NoteActionStatus.NotFound;

            note.Status = (byte)EntityState.Deleted;
            return await _noteAction.UpdateAsync(note) ?
                        note : NoteActionStatus.Failed;
        }
    }
}
