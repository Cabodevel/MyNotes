using MyNotes.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes.Views.Note
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteFormView : ContentPage
    {
        public NoteViewModel Note { get; set; }
        public NoteFormView()
        {
            BindingContext = Note;
            InitializeComponent();
        }
    }
}