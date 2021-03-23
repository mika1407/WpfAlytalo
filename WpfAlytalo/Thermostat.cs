using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlytalo
{
    public class Thermostat
    {
        public int Temperature { get; set; }    //ominaisuus

        public void Tavoitelämpö(int NewTemp) //metodi
        {
            Temperature = NewTemp;
        }

    }
}
