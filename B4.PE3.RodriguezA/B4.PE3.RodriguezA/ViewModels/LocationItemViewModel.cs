using B4.PE3.RodriguezA.Domain.Models;
using B4.PE3.RodriguezA.Domain.Services;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.RodriguezA.ViewModels
{
    public class LocationItemViewModel :FreshBasePageModel 
    {
        private LocationItem _locationItem;
        private ILocationUserRepository _locationRepository;

        public LocationItemViewModel()
        {
            _locationRepository = new JsonLocationRepository();
           
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

        private bool itemIsComplete;
        public bool ItemIsComplete
        {
            get { return itemIsComplete; }
            set
            {
                itemIsComplete = value;
                RaisePropertyChanged(nameof(ItemIsComplete));
            }
        }

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


        private string longitude;
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(nameof(Longitude)); }
        }

        private string latitude;
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; RaisePropertyChanged(nameof(Latitude)); }
        }


        private string photo;
        public string Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(nameof(Photo)); }
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
            ItemIsComplete = _locationItem.VisitDate.HasValue;
            VisitDate = _locationItem.VisitDate ?? DateTime.Now;
        }

        private void SaveItemState()
        {
            _locationItem.ItemName = ItemName;
            _locationItem.VisitDate = ItemIsComplete ? new DateTime?(VisitDate) : null;
        }

        public ICommand SaveBucketItemCommand => new Command(
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

    }
}
