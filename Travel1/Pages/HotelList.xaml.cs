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
    /// Interaction logic for HotelList.xaml
    /// </summary>
    public partial class HotelList : Page
    {
        public HotelList()
        {
            InitializeComponent();

        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditHotel((sender as Button).DataContext as Hotel));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = GridHotels.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    App.DatabaseContext.Hotels.RemoveRange(hotelsForRemoving);
                    App.DatabaseContext.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    GridHotels.ItemsSource = App.DatabaseContext.Hotels.ToList();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditHotel(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.DatabaseContext.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                GridHotels.ItemsSource = App.DatabaseContext.Tours.ToList();                
            }
        }
    }
}
