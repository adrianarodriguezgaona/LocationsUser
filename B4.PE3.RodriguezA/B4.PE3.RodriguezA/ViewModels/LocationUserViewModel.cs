using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace B4.PE3.RodriguezA.ViewModels
{
    public  class LocationUserViewModel : FreshBasePageModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(nameof(Name)); }
        }


        private string latitude;
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; RaisePropertyChanged(nameof(Latitude)); }
        }

        private string longitude;
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(nameof(Longitude)); }
        }


        private string photo;
        public string Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(nameof(Photo)); }
        }

        private DateTime visitDate;
        public DateTime VisitDate
        {
            get { return visitDate; }
            set { visitDate = value; RaisePropertyChanged(nameof(VisitDate)); }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; RaisePropertyChanged(nameof(Color)); }
        }

        public System.Collections.IList Colors { get; set; }
        public LocationUserViewModel()
        {
            Colors = new List<string>();
            LoadElements();
        }

        private void LoadElements()
        {
            Colors.Add("red");
            Colors.Add("blue");
            Colors.Add("green");
            Colors.Add("orange");
            Colors.Add("aqua");
        }
    }
}
