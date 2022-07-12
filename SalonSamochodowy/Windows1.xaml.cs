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
            SalonDBEntities2 db = new SalonDBEntities2();
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
        private int updatingCarID = 0;
        private void gridCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridCars.SelectedIndex >= 0)
            {
                if (this.gridCars.SelectedItems.Count >= 0)
                {

                    if (this.gridCars.SelectedItems[0].GetType() == typeof(Car))
                    {
                      Car car = (Car)this.gridCars.SelectedItems[0];
                        this.txtMarka1.Text = car.Marka;
                        this.txtModel1.Text = car.Model;
                        this.txtSilnik1.Text = car.Silnik;
                        this.txtCena1.Text = car.Cena;
                        this.updatingCarID = car.Id;
                    }
                }
            }
        }

        private void btnUpdateCar_Click(object sender, RoutedEventArgs e)
        {
            SalonDBEntities2 db = new SalonDBEntities2();
            var r = from d in db.Car
                    where d.Id == this.updatingCarID
                    select d;
           Car obj = r.SingleOrDefault();
            
            if(obj != null)
            {
                obj.Model = this.txtModel1.Text;
                obj.Marka = this.txtMarka1.Text;
                obj.Silnik = this.txtSilnik1.Text;
                obj.Cena = this.txtCena1.Text;
                db.SaveChanges();
            }
            
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć samochód z bazy danych?", "Usuń samochód", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                SalonDBEntities2 db = new SalonDBEntities2();
                var r = from d in db.Car
                        where d.Id == this.updatingCarID
                        select d;
                Car obj = r.SingleOrDefault();
                
                if (obj != null)
                {
                    db.Car.Remove(obj);
                    db.SaveChanges();

                }
            }

        }
    }
}
