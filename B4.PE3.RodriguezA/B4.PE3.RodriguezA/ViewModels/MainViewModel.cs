using B4.PE3.RodriguezA.Domain.Models;
using B4.PE3.RodriguezA.Domain.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace B4.PE3.RodriguezA.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private ILocationUserRepository _locationUserRepository;

        private ObservableCollection<LocationUser> locationsUser;
        public ObservableCollection<LocationUser> LocationsUser
        {
            get { return locationsUser; }
            set { locationsUser = value; RaisePropertyChanged(nameof(LocationsUser)); }
        }

        public MainViewModel()
        {
            _locationUserRepository = new JsonLocationRepository();
            LocationsUser = new ObservableCollection<LocationUser>(_locationUserRepository.GetAll().Result);
        }

        
    }
}
