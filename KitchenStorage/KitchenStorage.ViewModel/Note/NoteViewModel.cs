namespace KitchenStorage.ViewModel
{
    public record NoteViewModel
        (Guid? Id, string Title, byte Importance, string Description, byte Status, string CreateDate);

    public record UpsertNoteViewModel
        (Guid? Id, string Title, byte Importance, string Description);

    public enum NoteActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }
    public enum Importance
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
}

