using Autofac;
using MyNotes.Services.Abstract;
using MyNotes.ViewModel;
using MyNotesCore.Abstract;
using MyNotesCore.Entities;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListView : ContentPage
    {
        public ObservableCollection<Note> Items { get; set; }
        NoteListViewModel _vm
        {
            get { return BindingContext as NoteListViewModel; }
        }

        public NotesListView()
        {
            BindingContext = new NoteListViewModel(DependencyService.Get<INavService>());
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            
                return;
            var Note = (Note)e.Item;
            _vm.ViewCommand.Execute(Note);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            using (var scope = App.container.BeginLifetimeScope())
            {
                var _notesService = scope.Resolve<INotesService>();
                var notesResult = await _notesService.GetAllNotes();
                if (notesResult.Success)
                {
                    Items = new ObservableCollection<Note>(notesResult.ReturnValue);
                    NotesList.ItemsSource = Items;
                }
                else
                {
                    NotesList.ItemsSource = new ObservableCollection<Note>();
                }

            }
            // Initialize MainViewModel
            if (_vm != null)
                await _vm.Init();
            base.OnAppearing();
        }
    }
}
