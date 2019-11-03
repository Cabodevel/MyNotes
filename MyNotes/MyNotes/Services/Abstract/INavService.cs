using MyNotes.ViewModel;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MyNotes.Services.Abstract
{
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo<TVM>()
            where TVM : ViewModelBase;
        Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : ViewModelBase;
        Task RemoveLastView();
        Task ClearBackStack();
        Task NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
