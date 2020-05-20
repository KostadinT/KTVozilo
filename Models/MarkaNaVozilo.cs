using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoziloKT.Models
{
    //This method is for MarkaNaVozilo table the usage of [Display(Name="")] is to give each property the wanted name to show in view
    [Display(Name = "Vehicle Models")]
    public partial class MarkaNaVozilo
    {
        public MarkaNaVozilo()
        {
            DataForVehicles = new HashSet<DataForVehicles>();
        }

        public int Id { get; set; }

        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; }

        public virtual ICollection<DataForVehicles> DataForVehicles { get; set; }
    }
}
