using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;

namespace WpfAlytalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lights olohuone = new Lights(); //Luodaan Lights-tyyppinen olio, jonka nimi on "olohuone"
        Lights keittiö = new Lights();  //Luodaan Lights-tyyppinen olio, jonka nimi on "keittiö"
        Thermostat lämpöTalossa = new Thermostat();     //Luodaan Thermostat-tyyppinen olio, jonka nimi on "lämpöTalossa"
        Sauna talonSauna = new Sauna();
        DispatcherTimer SaunaTimer = new DispatcherTimer();
        DispatcherTimer SaunaTimerAlas = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            txtVanhaLampo.Text = "23";  //asetetaan talon alkulämmöksi 23 astetta

            SaunaTimer.Tick += Sauna_Tick;
            SaunaTimer.Interval = new TimeSpan(0, 0, 0, 1); //1 sekuntti =(0,0,0,1)

            SaunaTimerAlas.Tick += SaunaAlas_Tick;
            SaunaTimerAlas.Interval = new TimeSpan(0, 0, 1); //1 sekuntti =(0,0,1)

            txtSaunaPaalla.Text = ("");

            lblLampotila.Content = "";
        }

        private void btnValotPaalle_Click(object sender, RoutedEventArgs e) // Olohuoneen valot 100
        {
            olohuone.Päällä();
            txtOlohuone.Text = olohuone.Dimmer;
        }

        private void btnHimmena_Click(object sender, RoutedEventArgs e) //Olohuone 50
        {
            olohuone.Himmeä();
            txtOlohuone.Text = olohuone.Dimmer;
        }

        private void btnValotPois_Click(object sender, RoutedEventArgs e) //Olohuone 0
        {
            olohuone.Pois();
            txtOlohuone.Text = olohuone.Dimmer;
        }

        private void btnValotPaalleKeittio_Click(object sender, RoutedEventArgs e) //Keittiö 100
        {
            keittiö.Päällä();
            txtKeittio.Text = keittiö.Dimmer;
        }

        private void btnHimmenaKeittio_Click(object sender, RoutedEventArgs e)  //Keittiö 50
        {
            keittiö.Himmeä();              //Hakee metodista
            txtKeittio.Text = keittiö.Dimmer;
        }

        private void btnValotPoisKeittio_Click(object sender, RoutedEventArgs e)
        {
            keittiö.Pois();
            txtKeittio.Text = keittiö.Dimmer;
        }

        private void btnUusiLampo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((int.Parse(txtUusiLampo.Text) > 16) && (int.Parse(txtUusiLampo.Text) < 26)) //if-else-ehtolauseella asetetaan että lämpötilan pitää olla 17-25 välillä
                {
                    int NewTemp = int.Parse(txtUusiLampo.Text); //tekstipohjainen lämpötila pitää muuntaa int:ksi, kun sen tallettaa oliolle

                    lämpöTalossa.Tavoitelämpö(NewTemp); //asetetaan uusi lämpötila olioon lämpöTalossa
                    txtVanhaLampo.Text = lämpöTalossa.Temperature.ToString();  //olion int-tyyppinen arvo näytetään käyttöliittymässä ja muutetaan se tekstipohjaiseksi
                    txtUusiLampo.Text = "";  //tavoitelämpötila -kenttä tyhjenee
                    //https://stackoverflow.com/questions/833943/watermark-hint-text-placeholder-textbox txtUusiLampo texboxiin placeholder
                }
                else
                {
                    MessageBox.Show("Sisälämpötilan pitää olla välillä 17-25 astetta");
                    //Normaali sisälämpötila on 19–25 astetta, maukuuhuone viileämpi. Kun makuuhuoneen lämpötila on 17–21 astetta, uni on levollisempaa.
                }
            }
            catch
            {
                MessageBox.Show("Anna lämpötila kokonaislukuna!");

            }
        }

        private void btnSaunaPaalle_Click(object sender, RoutedEventArgs e)
        {
            if (talonSauna.Switched)
            {
                talonSauna.SaunaPois(0);
                txtSaunaPaalla.Text = "";
                SaunaTimer.Stop();
                SaunaTimerAlas.Start();             //Kun sauna on pois päältä laskuri alas käynnistyy
            }
            else
            {
                talonSauna.SaunaPäälle(1);
                talonSauna.Lämpötila = Double.Parse(txtVanhaLampo.Text); // saunan alkulämpö = talonlämpö, haetaan kentästä txtVanhaLämpö
                txtSaunaPaalla.Text = "Kiuas päällä";
                SaunaTimer.Start();
                SaunaTimerAlas.Stop();              //Kun sauna on päällä laskuri alas on pois päältä
            }
        }

        //private void Sauna_Tick(object sender, EventArgs e)
        //{
        //    talonSauna.Lämpötila = talonSauna.Lämpötila + 0.5;
        //    lblLampotila.Content = talonSauna.Lämpötila.ToString();
        //}

        private void Sauna_Tick(object sender, EventArgs e)
        {
            if (talonSauna.Lämpötila >= 98.5)               //saunan lämpeneminen pysähtyy arvoon 99
                SaunaTimer.Stop();
            talonSauna.Lämpötila += 0.5;
            lblLampotila.Content = (talonSauna.Lämpötila.ToString());
        }

        //private void SaunaAlas_Tick(object sender, EventArgs e)
        //{
        //    talonSauna.Lämpötila = talonSauna.Lämpötila - 1;
        //    lblLampotila.Content = talonSauna.Lämpötila.ToString();
        //}

        private void SaunaAlas_Tick(object sender, EventArgs e)
        {
            if (talonSauna.Lämpötila <= (Double.Parse(txtVanhaLampo.Text)+1))    //saunan viilentyminen pysähtyy tekstikentässä txtVanhaLampo olevaan arvoon
                SaunaTimerAlas.Stop();
            talonSauna.Lämpötila -= 1;
            lblLampotila.Content = (talonSauna.Lämpötila.ToString());
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)  //yellow (255,255,0) https://www.youtube.com/watch?v=a_f0CyGVnIY
        {
            if (Slider1.Value == 0)
            {           
             myEllipse.Fill = new SolidColorBrush(new Color() { A = 255, R =244, G =244, B =245 });
            }
            else
            {
                double slider = Slider1.Value;
                double red = 255;
                double green = 255;
                myEllipse.Fill = new SolidColorBrush(new Color() { A = 255, R = (byte)red, G = (byte)green, B = (byte)(100 - slider) });
            }
       
        }
    }
}
