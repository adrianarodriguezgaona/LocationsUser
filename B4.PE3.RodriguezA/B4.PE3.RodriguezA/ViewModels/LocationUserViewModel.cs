using B4.PE3.RodriguezA.Constants;
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
    public  class LocationUserViewModel : FreshBasePageModel

    {
        private ILocationUserRepository _locationUserRepository;

        private LocationUser _currentLocationUser;
        private bool isNew = true;


        public LocationUserViewModel()
        {
            Colors = new List<string>();
            LoadElements();
            _locationUserRepository = new JsonLocationRepository();
 
        }
        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(pageTitle));
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(nameof(Name)); }
        }


   
        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; RaisePropertyChanged(nameof(Color)); }

        }


        private ObservableCollection<LocationItem> locationItems;
        public ObservableCollection<LocationItem> LocationItems
        {
            get { return locationItems; }
            set { locationItems = value; RaisePropertyChanged(nameof(LocationItems)); }
        }

        public System.Collections.IList Colors { get; set; }
       

        private void LoadElements()
        {
            Colors.Add("lime"); 
            Colors.Add("lightBlue");
            Colors.Add("lightGreen");
            Colors.Add("orange");
            Colors.Add("aqua");
        }

        /// Called whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>
        public async override void Init(object initData)
        {
            base.Init(initData);

            _currentLocationUser = initData as LocationUser;


            await RefreshLocationUser();
        }

        /// <summary>
        /// Called when returning to this Model from a previous model
        /// </summary>
        /// <param name="returnedData"></param>
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is LocationItem)
            {
                //refresh list, to update this item visually
                LoadLocationUserState();
            }
        }

        /// <summary>
        /// Refreshes the currentBucket (to edit) or initializes a new one (to add)
        /// </summary>
        /// <returns></returns>
        private async Task RefreshLocationUser()
        {
            if (_currentLocationUser != null)
            {
                //editing existing bucketlist
                isNew = false;
                PageTitle = "Edit Location List";
                _currentLocationUser = await _locationUserRepository.GetLocationUserList(_currentLocationUser.Id);
            }
            else
            {
                //editing brand new bucketlist
                isNew = true;
                PageTitle = "New Location List";
                _currentLocationUser = new LocationUser();
                _currentLocationUser.Id = Guid.NewGuid();
               _currentLocationUser.Items = new List<LocationItem>();
            }
            LoadLocationUserState();
        }


        public ICommand SaveLocationUserCommand => new Command(
            async () => {
                SaveLocationUserState();
             
                    IsBusy = true;

                    if (isNew)
                    {
                        await _locationUserRepository.AddLocationUserList(_currentLocationUser);
                    }
                    else
                    {
                        await _locationUserRepository.UpdateLocationUserList(_currentLocationUser);
                    }
                    IsBusy = false;

                MessagingCenter.Send(this,MessageNames.LocationUserSaved, _currentLocationUser);

                await CoreMethods.PopPageModel(false, true);

            }
        );

        public ICommand OpenItemPageCommand => new Command<LocationItem>(
            async (LocationItem item) => {

                SaveLocationUserState();

                if (item == null)
                {
                    //new BucketList Item requested, let's make sure to
                    //pass a reference to the parent Bucket to which the new item will belong
                    item = new LocationItem
                    {
                        LocationUserId = _currentLocationUser.Id,
                        ParentLocation = _currentLocationUser
                    };
                }
                await CoreMethods.PushPageModel<LocationItemViewModel>(item, false, true);
            }
        );
        

        public ICommand DeleteItemCommand => new Command<LocationItem>(
            (LocationItem item) => {
                _currentLocationUser.Items.Remove(item);
                LoadLocationUserState();
            }
        );


        /// <summary>
        /// Loads the currentBucket list properties into the VM properties for display in UI
        /// </summary>
        private void LoadLocationUserState()
        {
            Name = _currentLocationUser.Name;
            Color = _currentLocationUser.Color;
            LocationItems = new ObservableCollection<LocationItem>(_currentLocationUser.Items.OrderBy(e => e.ItemName));
        }

       
        private void SaveLocationUserState()
        {
            _currentLocationUser.Name = Name;
            _currentLocationUser.Color = Color;
           
        }
    }
}
