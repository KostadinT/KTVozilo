using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoziloKT.Models
{   //This method is for DataForVehicle table the usage of [Display(Name="")] is to give each property the wanted name to show in view
    [Display(Name = "Vehicles")]
    public partial class DataForVehicles
    {
        public int Id { get; set; }

        [Display(Name = "License Plate Number")]
        public string LicensePlateNumber { get; set; }

        [Display(Name = "VIN")]
        public string Vin { get; set; }

        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; }

        [Display(Name = "Vehicle Model")]
        public virtual MarkaNaVozilo VehicleModelNavigation { get; set; }
    }
}
