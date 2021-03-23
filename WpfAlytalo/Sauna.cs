using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlytalo
{
    public class Sauna
    {
        

        public Boolean Switched { get; set; } //prop + 2xtab luo ominaisuuden
        public Double Lämpötila { get; set; }

        public void SaunaPäälle(int tila)        //metodi 1
        {
            if (tila == 1)
            {
                Switched = true;
                //Lämpötila = 23;
            }       
        }

        public void SaunaPois(int tila)        //metodi 2
        {
            if (tila == 0)
            {
                Switched = false;
                
            }
        }

    }
}
