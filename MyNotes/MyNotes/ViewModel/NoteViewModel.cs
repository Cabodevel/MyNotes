using MyNotesCore.Common;
using Xamarin.Forms;

namespace MyNotes.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private string _Title;
        public string Title 
        { 
            get => _Title;
            set { _Title = value; RaisePropertyChanged(() => Title); }
        }
        public string Text { get; set; }
        public string Color { get; set; }
        public PriorityEnum Priority { get; set; }
        public Command CreateCommand { get; set; }
        public Command UpdateCommand { get; set; }
    }
}
