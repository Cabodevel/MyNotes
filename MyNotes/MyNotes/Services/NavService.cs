using MyNotes.Services;
using MyNotes.Services.Abstract;
using MyNotes.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavService))]
namespace MyNotes.Services
{
    public class NavService : INavService
    {
        public INavigation Navigator { get; set; }

        readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        public bool CanGoBack
        {
            get
            {
                return Navigator.NavigationStack != null
                    && Navigator.NavigationStack.Count > 0;
            }
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await Navigator.PopAsync(true);
            }

            OnCanGoBackChanged();
        }

        public async Task NavigateTo<TVM>() where TVM : ViewModelBase
        {
            await NavigateToView(typeof(TVM));

            if (Navigator.NavigationStack.Last().BindingContext is ViewModelBase)
            {
                await ((ViewModelBase)(Navigator
                    .NavigationStack.Last().BindingContext)).Init();
            }
        }

        public async Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : ViewModelBase
        {
            await NavigateToView(typeof(TVM));

            if (Navigator.NavigationStack.Last().BindingContext is ViewModelBase<TParameter>)
            {
                await ((ViewModelBase<TParameter>)(Navigator
                    .NavigationStack.Last().BindingContext)).Init(parameter);
            }
        }

        async Task NavigateToView(Type viewModelType)
        {
            Type viewType;

            if (!_map.TryGetValue(viewModelType, out viewType))
            {
                throw new ArgumentException("No view found in View Mapping for " + viewModelType.FullName + ".");
            }

            var constructor = viewType.GetTypeInfo()
                                      .DeclaredConstructors
                                      .FirstOrDefault(dc => dc.GetParameters().Count() <= 0);

            var view = constructor.Invoke(null) as Page;
            await Navigator.PushAsync(view, true);
        }

        public async Task RemoveLastView()
        {
            if (Navigator.NavigationStack.Any())
            {
                var lastView = Navigator
                    .NavigationStack[Navigator.NavigationStack.Count - 2];
                Navigator.RemovePage(lastView);
            }
        }

        public async Task ClearBackStack()
        {
            if (Navigator.NavigationStack.Count <= 1)
            {
                return;
            }

            for (var i = 0; i < Navigator.NavigationStack.Count - 1; i++)
            {
                Navigator.RemovePage(Navigator.NavigationStack[i]);
            }
        }

        public async Task NavigateToUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Invalid URI");
            }

           await Launcher.OpenAsync(uri);
        }

        public event PropertyChangedEventHandler CanGoBackChanged;

        void OnCanGoBackChanged()
        {
            CanGoBackChanged?.Invoke(this, new
                PropertyChangedEventArgs("CanGoBack"));
        }
    }
}
