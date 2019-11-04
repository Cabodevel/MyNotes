using Autofac;
using Autofac.Core;
using Infrastructure.Repositories;
using MyNotes.Services;
using MyNotes.Services.Abstract;
using MyNotes.ViewModel;
using MyNotes.Views;
using MyNotesCore.Abstract;
using MyNotesCore.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MyNotes
{
    public partial class App : Application
    {
        public static IContainer container;
        public static readonly ContainerBuilder builder = new ContainerBuilder();

        public App()
        {
            InitializeComponent();
            DependencyResolver.ResolveUsing(type => container.IsRegistered(type) ? container.Resolve(type) : null);
            RegisterTypes();
            var mainPage = new NavigationPage(new NotesListView());

            var navService = DependencyService.Get<INavService>() as NavService;

            navService.Navigator = mainPage.Navigation;

            navService.RegisterViewMapping(typeof(NoteViewModel),
                                           typeof(NoteFormView));
            navService.RegisterViewMapping(typeof(NoteListViewModel),
                                           typeof(NotesListView));

            MainPage = mainPage;
        }

        void RegisterTypes()
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            RegisterType<INotesService, NotesService>();
            BuildContainer();
        }

        public static void RegisterType<T>() where T : class
        {
            builder.RegisterType<T>();
        }

        public static void RegisterType<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            builder.RegisterType<T>().As<TInterface>();
        }

        public static void RegisterTypeWithParameters<T>(Type param1Type, object param1Value, Type param2Type, string param2Name) where T : class
        {
            builder.RegisterType<T>()
                   .WithParameters(new List<Parameter>()
            {
            new TypedParameter(param1Type, param1Value),
            new ResolvedParameter(
                (pi, ctx) => pi.ParameterType == param2Type && pi.Name == param2Name,
                (pi, ctx) => ctx.Resolve(param2Type))
            });
        }

        public static void RegisterTypeWithParameters<TInterface, T>(Type param1Type, object param1Value, Type param2Type, string param2Name) where TInterface : class where T : class, TInterface
        {
            builder.RegisterType<T>()
                   .WithParameters(new List<Parameter>()
            {
            new TypedParameter(param1Type, param1Value),
            new ResolvedParameter(
                (pi, ctx) => pi.ParameterType == param2Type && pi.Name == param2Name,
                (pi, ctx) => ctx.Resolve(param2Type))
            }).As<TInterface>();
        }

        public static void BuildContainer()
        {
            if(container == null)
            container = builder.Build();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
