using B4.PE3.RodriguezA.Domain.Services;
using B4.PE3.RodriguezA.ViewModels;
using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.RodriguezA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUyNzA0QDMxMzcyZTMzMmUzME5PemVVY2JiQWtqSHZnLzZtRHRVdVRVc3pxVjU5R1kyN2pGNjNyUnBLSGM9");

            //Register dependencies
            FreshIOC.Container.Register<ILocationUserRepository>(new JsonLocationRepository());

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());
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
