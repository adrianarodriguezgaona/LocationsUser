﻿using System;
using System.Collections.Generic;
using System.Text;

namespace B4.PE3.RodriguezA.Domain.Models
{
    public  class LocationItem
    {
        public Guid Id { get; set; }
        public Guid LocationUserId { get; set; }
        public LocationUser ParentLocation { get; set; }
        public string ItemName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Photo { get; set; }
       
        public DateTime? VisitDate { get; set; }
    }
}
