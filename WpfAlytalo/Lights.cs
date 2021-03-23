using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlytalo
{
    public class Lights // määritellään luokka public arvoksi että näkyy pääohjelman
    {
        public Boolean Switched { get; set; } // prop + tab x2 tekee ominaisuuden ja get/set
        public String Dimmer { get; set; }  //Tämä on ominaisuus (property), (0=pimeä, 100=täysi valo)

        public void Päällä()        //Tämä on metodi jonka arvo=100
        {
            Switched = true;
            Dimmer = "100";           //"Valot on päällä"
        }
        public void Himmeä()        //metodi, arvo=50
        {
            Switched = true;
            Dimmer = "50";      //"Himmeä"
        }
        public void Pois()
        {
            Switched = false;          // Switched on false ainoastaan, kun valot pannaan pois päältä (arvo=0)
            Dimmer = "0";           //"Valot on sammutettu"
        }
    }
}
