using System;
using System.Collections.Generic;
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

namespace Путешествуй_по_России.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();

        }

        private void Button_Hotel_List_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HotelList());
        }
        private void Button_Add_Hotel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditHotel());
        }
        private void Button_Tour_List_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourList());
        }   
    }
}
