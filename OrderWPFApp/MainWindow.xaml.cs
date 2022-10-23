using System.Windows;

namespace OrderWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            Product productPage = Product.Instance;
            productPage.Show();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Search searchPage = new Search();
            searchPage.Show();
        }
    }
}
