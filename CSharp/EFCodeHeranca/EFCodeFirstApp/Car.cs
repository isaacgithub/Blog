using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstApp
{
    //[Table("Car")]
    public class Car : Vehicle
    {
        public int DoorsNumber { get; set; }

        public bool PowerSteering { get; set; }
    }
}
