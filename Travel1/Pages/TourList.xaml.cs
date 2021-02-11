using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TourList.xaml
    /// </summary>
    public partial class TourList : Page
    {


        public decimal PriceTours
        {
            get { return (decimal)GetValue(ChangePrice); }
            set { SetValue(ChangePrice, value); }
        }

       
        public static readonly DependencyProperty ChangePrice =
            DependencyProperty.Register("PriceTours", typeof(decimal), typeof(TourList));


        public bool SortBy { get; set; } = false;
        public ObservableCollection<Tour> Tours { get; set; } = new ObservableCollection<Tour>();
        public ObservableCollection<string> Types { get; set; } = new ObservableCollection<string>();
        public TourList()
        {
            Tours = new ObservableCollection<Tour>(App.DatabaseContext.Tours.ToList());

            InitializeComponent();
            Types.Add("Все типы");
            foreach (var item in App.DatabaseContext.Types.ToList())
            {
                Types.Add(item.Name);
            }
            SetFilter();
        }

        public void SetFilter()
        {
            Tours = new ObservableCollection<Tour>(App.DatabaseContext.Tours.ToList());
            var copyCollection = new ObservableCollection<Tour>(Tours);
            foreach (var item in copyCollection)
            {
                if (!item.Name.ToLower().Contains(FindString.Text.ToLower()) && !string.IsNullOrEmpty(FindString.Text))
                {
                    Tours.Remove(item);
                }
                if (item.Types.Where(a => a.Name == SelectedType.Text).Count() == 0 && !SelectedType.Text.Equals("Все типы"))
                {
                    Tours.Remove(item);
                }
                if (ActualTour.IsChecked.Value && item.IsActual != ActualTour.IsChecked)
                {
                    Tours.Remove(item);
                }
            }
            Sort();
            PriceTours = Tours.Sum(a => a.Price);
            
            Testim.ItemsSource = Tours;
        }

        private void FindString_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilter();
        }

        private void SelectedType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void ActualTour_Checked(object sender, RoutedEventArgs e)
        {
            SetFilter();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SortBy = !SortBy;
            SetFilter();
        }
        public void Sort()
        {
            if (SortBy)
            {
                Tours = new ObservableCollection<Tour>(Tours.OrderByDescending(a => a.Price).ToList());
            }
            else
            {
                Tours = new ObservableCollection<Tour>(Tours.OrderBy(a => a.Price).ToList());
            }
         

        }
    }
}
