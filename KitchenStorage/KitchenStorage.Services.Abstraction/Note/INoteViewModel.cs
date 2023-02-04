namespace KitchenStorage.Services.Abstraction
{
    public interface INoteViewModel
    {
        NoteViewModel CreateNoteViewModel(Note note);

        IEnumerable<NoteViewModel> CreateNoteViewModel(IEnumerable<Note> inventories);
    }
}
