using DataAccess.Repository;
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

namespace OrderWPFApp
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {

        private IProductRepository productRepository = new ProductRepository();


        private static Search instance = null;
        private static readonly object iLock = new object();

        public Search()
        {
            InitializeComponent();
        }

        public static Search Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new Search();
                    }
                    return instance;
                }
            }
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            lvProducts.ItemsSource = productRepository.GetProductsByDescription("");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text;

            lvProducts.ItemsSource = productRepository.GetProductsByDescription(search);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
