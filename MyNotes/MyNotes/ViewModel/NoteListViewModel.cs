using System.Threading.Tasks;
using MyNotes.Services.Abstract;

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
    }
}
