using MyNotes.Services.Abstract;
using MyNotes.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteFormView : ContentPage
    {
        public NoteViewModel Note { get; set; }
        public NoteFormView()
        {
            Note = new NoteViewModel(DependencyService.Get<INavService>());
            BindingContext = Note;
            InitializeComponent();
        }
    }
}