using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Travel1;
using Travel1.Model;

namespace Путешествуй_по_России.Pages
{
    /// <summary>
    /// Interaction logic for EditHotel.xaml
    /// </summary>
    public partial class EditHotel : Page
    {
        public Hotel currentHotel { get; set; } = new Hotel();

        public EditHotel(Hotel selectedHotel)
        {
            if (selectedHotel == null)
            {
                currentHotel = selectedHotel;
            }
            InitializeComponent();
            DataContext = currentHotel;
            ComboCountries.ItemsSource = App.DatabaseContext.Countries.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //check errors
            var errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentHotel.Name))
            {
                errors.AppendLine("Need input name");
            }
            if (currentHotel.CountOfStars < 1 || currentHotel.CountOfStars > 5)
            {
                errors.AppendLine("Count stars need > 0 and < 6");

            }
            if (currentHotel.Country == null)
            {
                errors.AppendLine("Need set country");
                
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (currentHotel.Id == 0)
            {
                App.DatabaseContext.Hotels.Add(currentHotel);
            }
            try
            {
                App.DatabaseContext.SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
