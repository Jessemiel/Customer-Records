using BcsExamApp.Interfaces;
using BcsExamApp.Services;
using BcsExamApp.ViewModels;
using BcsExamApp.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace BcsExamApp
{
    //Container used: Unity    
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICustomerService, CustomerService>();
            containerRegistry.Register<IConnectivityService, ConnectivityService>();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectedCustomerPage, SelectedCustomerPageViewModel>();
        }
    }
}
