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
            SalonDBEntities db = new SalonDBEntities();
            var docs = from d in db.Car
                       select d;
            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Marka);
                Console.WriteLine(doc.Model);
            }
            this.gridCars.ItemsSource = docs.ToList();

        }
    }
}
