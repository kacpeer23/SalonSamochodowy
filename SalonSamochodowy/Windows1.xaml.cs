using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalonSamochodowy
{
    /// <summary>
    /// Logika interakcji dla klasy Windows1.xaml
    /// </summary>
    public partial class Windows1 : Window
    {
        public Windows1()
        {
            InitializeComponent();
            SalonDBEntities2 db = new SalonDBEntities2();
            var docs = from d in db.Car
                       select new
                       {
                           MarkaSamochodu = d.Marka,
                           ModelSamochodu = d.Model,
                           CenaSamochodu = d.Cena,
                           SilnikSamochodu = d.Silnik
                       };
            

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.MarkaSamochodu);
                Console.WriteLine(doc.ModelSamochodu);
            }
            this.gridCars.ItemsSource = docs.ToList();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SalonDBEntities2 db = new SalonDBEntities2();x
            Car carObj = new Car()
            {
                Marka = txtMarka.Text,
                Model = txtModel.Text,
                Silnik = txtSilnik.Text,
                Cena = txtCena.Text
            };
            db.Car.Add(carObj);
            db.SaveChanges();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            SalonDBEntities2 db = new SalonDBEntities2();
            this.gridCars.ItemsSource = db.Car.ToList();

        }
    }
}
