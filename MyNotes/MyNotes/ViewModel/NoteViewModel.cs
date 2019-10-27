using MyNotesCore.Common;

namespace MyNotes.ViewModel
{
    public class NoteViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public PriorityEnum Priority { get; set; }
    }
}
