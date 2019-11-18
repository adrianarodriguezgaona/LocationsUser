using B4.PE3.RodriguezA.Domain.Models;
using B4.PE3.RodriguezA.Domain.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.RodriguezA.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private ILocationUserRepository _locationUserRepository;
        public ICommand GoToLocationUserPageCommand; 

        public MainViewModel()
        {
            _locationUserRepository = new JsonLocationRepository();
            //GoToLocationUserPageCommand = new Command(GoToLocationUserPage);

        }

        //private async void GoToLocationUserPage(object obj)
        //{
        //    await CoreMethods.PushPageModel<LocationUserViewModel>(true);
        //}

        private ObservableCollection<LocationUser> locationsUser;
        public ObservableCollection<LocationUser> LocationsUser
        {
            get { return locationsUser; }
            set { locationsUser = value; RaisePropertyChanged(nameof(LocationsUser)); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                // este metodo ya esta en classe FreshBasePageModel

                RaisePropertyChanged(nameof(IsBusy));
            }
        }


        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await RefreshlocationsUserLists();
        }


        public ICommand OpenLocationUserPageCommand => new Command<LocationUser>(
            async (LocationUser location) => {
                await CoreMethods.PushPageModel<LocationUserViewModel>(location, false, true);
            }
        );

       

        public ICommand DeleteLocationUserCommand => new Command<LocationUser>(
            async (LocationUser location) => {
                await _locationUserRepository.DeleteLocationUserList(location.Id);
                await RefreshlocationsUserLists();
            }
        );

        private async Task RefreshlocationsUserLists()
        {
            IsBusy = true;
            //get settings, because we need current user Id
            //var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var locationsUser = await _locationUserRepository.GetMyLocationLists();
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            LocationsUser = null;    //Important! ensure the list is empty first to force refresh!
            LocationsUser = new ObservableCollection<LocationUser>(locationsUser.OrderBy(e => e.Name));
            IsBusy = false;
        }

       
    }
}

