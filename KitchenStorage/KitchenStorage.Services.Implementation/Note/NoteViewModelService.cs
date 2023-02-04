namespace KitchenStorage.Services.Implementation
{
    public class NoteViewModelService : INoteViewModel
    {
        public NoteViewModel CreateNoteViewModel(Note note)
             => new(Id: note.Id,
                 Title: note.Title,
                 Importance: note.Importance,
                 Description: note.Description,
                 Status: note.Status,
                 CreateDate: note.CreateDate.ToShamsi());

        public IEnumerable<NoteViewModel> CreateNoteViewModel(IEnumerable<Note> notes)
            => notes.Select(CreateNoteViewModel);
    }
}
