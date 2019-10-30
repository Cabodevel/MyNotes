using Autofac;
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

        public NotesListView()
        {
            Device.BeginInvokeOnMainThread(async() =>
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
            });

           
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
