using System.Threading.Tasks;
using MyNotes.Services.Abstract;
using MyNotesCore.Entities;
using Xamarin.Forms;

namespace MyNotes.ViewModel
{
    public class NoteListViewModel : ViewModelBase
    {
        public NoteListViewModel(INavService navService) : base(navService)
        {
        }

        public override Task Init()
        {
            return Task.CompletedTask;
        }

        Command<Note> _viewCommand;
        public Command<Note> ViewCommand
        {
            get
            {
                return _viewCommand
                    ?? (_viewCommand = new Command<Note>(async (entry) => await ExecuteViewCommand(entry)));
            }
        }

        Command _newCommand;
        public Command NewCommand
        {
            get
            {
                return _newCommand
                    ?? (_newCommand = new Command(async () => await ExecuteNewCommand()));
            }
        }

        async Task ExecuteViewCommand(Note entry)
        {
            await NavService.NavigateTo<NoteViewModel, Note>(entry);
        }

        async Task ExecuteNewCommand()
        {
            await NavService.NavigateTo<NoteViewModel>();
        }
    }
}
