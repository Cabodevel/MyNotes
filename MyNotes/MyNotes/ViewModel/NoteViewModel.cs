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
        private string _Text;
        public string Text 
        {
            get => _Text;
            set { _Text = value; RaisePropertyChanged(() => _Text); }
        }

        private string _Color;
        public string Color 
        {
            get => _Color;
            set { _Color = value; RaisePropertyChanged(() => _Color); }
        }

        private PriorityEnum _Priority;
        public PriorityEnum Priority 
        {
            get => _Priority;
            set { _Priority = value; RaisePropertyChanged(() => Priority); }
        }

        public Command CreateCommand { get; set; }
        public Command UpdateCommand { get; set; }
    }
}
