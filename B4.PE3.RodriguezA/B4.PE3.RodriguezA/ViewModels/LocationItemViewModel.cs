using B4.PE3.RodriguezA.Domain.Models;
using B4.PE3.RodriguezA.Domain.Services;
using FreshMvvm;
using Plugin.Geolocator;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;

namespace B4.PE3.RodriguezA.ViewModels
{
    public class LocationItemViewModel :FreshBasePageModel 
    {
        private LocationItem _locationItem;
        private readonly ILocationUserRepository locationRepository;

        public LocationItemViewModel(ILocationUserRepository locationRepository)
        {

            this.locationRepository = locationRepository;           
        }

        #region Properties


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

        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                RaisePropertyChanged(nameof(ItemName));
            }
        }

        //private string itemDescriptionError;
        //public string ItemDescriptionError
        //{
        //    get { return itemDescriptionError; }
        //    set
        //    {
        //        itemDescriptionError = value;
        //        RaisePropertyChanged(nameof(ItemDescriptionError));
        //        RaisePropertyChanged(nameof(ItemDescriptionErrorVisible));
        //    }
        //}

        //public bool ItemDescriptionErrorVisible
        //{
        //    get { return !string.IsNullOrWhiteSpace(ItemDescriptionError); }
        //}

        //private bool itemIsComplete;
        //public bool ItemIsComplete
        //{
        //    get { return itemIsComplete; }
        //    set
        //    {
        //        itemIsComplete = value;
        //        RaisePropertyChanged(nameof(ItemIsComplete));
        //    }
        //}

        private DateTime visitDate;
        public DateTime VisitDate
        {
            get { return visitDate; }
            set
            {
                visitDate = value;
                RaisePropertyChanged(nameof(VisitDate));
            }
        }


        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(nameof(Longitude)); }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; RaisePropertyChanged(nameof(Latitude)); }
        }


        private string myLocation;
        public string MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; RaisePropertyChanged(nameof(MyLocation)); }
        }

        private string photoSource;
        public string PhotoSource
        {
            get { return photoSource; }
            set { photoSource = value; RaisePropertyChanged(nameof(PhotoSource)); }
        }

        #endregion

        public override void Init(object initData)
        {
            LocationItem item = initData as LocationItem;
            _locationItem = item;
            if (item.Id == Guid.Empty)
            {
                PageTitle = "New Item";
            }
            else
            {
                PageTitle = "Edit Item";
            }

            LoadItemState();
            base.Init(initData);
        }

        private void LoadItemState()
        {
            ItemName = _locationItem.ItemName;
            VisitDate = _locationItem.VisitDate ?? DateTime.Now;
            PhotoSource = _locationItem.PhotoSource;
            Latitude = _locationItem.Latitude;
            Longitude = _locationItem.Longitude;
            MyLocation = _locationItem.MyLocation;
        }

       

        public ICommand SaveGeolocationItemCommand => new Command(
            async () =>
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));

                Longitude = position.Longitude;
                Latitude = position.Latitude;
                                
                MyLocation = $"{Latitude} . {Longitude}";

                //var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                //    position.Timestamp, position.Latitude, position.Longitude,
                //    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

                Debug.WriteLine(Longitude);

            });

        public ICommand TakeAPictureCommand => new Command(
            async () =>

        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = $"{ItemName}.jpg",
                PhotoSize = PhotoSize.Small
            });

            if (file == null)
                return;

            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");

            PhotoSource = file.Path;

            var source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

           
        });


        private void SaveItemState()
        {
            _locationItem.ItemName = ItemName;
            _locationItem.VisitDate = new DateTime?(VisitDate);
            _locationItem.Latitude = Latitude;
            _locationItem.Longitude = Longitude;
            _locationItem.MyLocation = MyLocation;
            _locationItem.PhotoSource = PhotoSource;
        }

        public ICommand SaveLocationUserItemCommand => new Command(
            async () => {
                try
                {
                    SaveItemState(); 
      
                        if (_locationItem.Id == Guid.Empty)
                        {
                            _locationItem.Id = Guid.NewGuid();
                            _locationItem.ParentLocation.Items.Add(_locationItem);
                        }
                        //use coremethodes to Pop pages in FreshMvvm!
                        await CoreMethods.PopPageModel(_locationItem, false, true);
                   
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        );

        public ICommand GoToCommand => new Command(
            async () =>
            {
                await Map.OpenAsync(Latitude, Longitude, new MapLaunchOptions
                {
                    Name = ItemName,
                    NavigationMode = NavigationMode.None
                });
            });

    }
}
