using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstApp
{
    //[Table("Airplane")]
    public class Airplane : Vehicle
    {
        public int PassengersNumber { get; set; }

        public string Company { get; set; }

        public void Fly()
        { 
            //Do something...
        }
    }
}
