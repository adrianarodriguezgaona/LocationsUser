using System;
using System.Collections.Generic;
using System.Text;

namespace B4.PE3.RodriguezA.Domain.Models
{
    public class LocationUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string Foto { get; set; }
        public System.Collections.IList Colors { get; set; }
        public DateTime DateVisit { get; set; }


    }
}
