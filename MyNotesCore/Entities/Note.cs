using MyNotesCore.Common;

namespace MyNotesCore.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public PriorityEnum Priority { get; set; }
    }
}
