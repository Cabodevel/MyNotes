using Autofac;
using MyNotes.Services.Abstract;
using MyNotesCore.Abstract;
using MyNotesCore.Common;
using MyNotesCore.Entities;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyNotes.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly INotesService _notesService;

        public override Task Init()
        {
            return Task.CompletedTask;
        }

        public NoteViewModel(INavService navService) : base(navService)
        {

            using (var scope = App.container.BeginLifetimeScope())
                _notesService = scope.Resolve<INotesService>();

            CreateCommand = new Command(async () => await Create());
            UpdateCommand = new Command(async () => await Update());
        }

        private string _Title;
        public string Title 
        { 
            get => _Title;
            set { _Title = value; SetProperty(ref _Title, value); }
        }
        private string _Text;
        public string Text 
        {
            get => _Text;
            set {
                _Text = value; SetProperty(ref _Text, value); 
            }
        }

        private string _Color;
        public string Color 
        {
            get => _Color;
            set { _Color = value; SetProperty(ref _Color, value); }
        }

        private PriorityEnum _Priority;
        public PriorityEnum Priority 
        {
            get => _Priority;
            set { _Priority = value; SetProperty(ref _Priority, value); }
        }

        public Command CreateCommand { get; set; } 
        public Command UpdateCommand { get; set; }

        private async Task Create()
        {
            await _notesService.AddNote(new Note { Priority = Priority, Title = Title, Text = Text });
            await NavService.GoBack();
        }
        private async Task Update() => await _notesService.UpdateNote(new Note());

       
    }
}
