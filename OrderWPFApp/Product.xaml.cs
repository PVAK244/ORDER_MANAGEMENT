using System;
using DataAccess.Repository;
using System.Windows;
using DataAccess;

namespace OrderWPFApp
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        private IProductRepository productRepository = new ProductRepository();


        private static Product instance = null;
        private static readonly object iLock = new object();

        public Product()
        {
            InitializeComponent();
        }

        public static Product Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new Product();
                    }
                    return instance;
                }
            }
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            loadList();
            btnAdd.IsEnabled = true;
        }

        private string checkInput()
        {
            string id = txtId.Text;
            string des = txtDescription.Text;
            string finish = txtFinish.Text;
            string price = txtPrice.Text;

            string error = string.Empty;

            if (id.Equals(String.Empty) || des.Equals(string.Empty) || finish.Equals(string.Empty) || price.Equals(string.Empty))
            {
                error += "Input field must not be empty!!!\n";
            }

            try
            {
                decimal.Parse(price);
            }
            catch (Exception e)
            {
                error += "Price is wrong format!!!\n";
            }

            return error;
        }

        private void loadList()
        {
            lvProducts.ItemsSource = productRepository.GetProducts();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string err = checkInput();

            if (err.Equals(string.Empty))
            {
                ProductObject product = new ProductObject
                {
                    ProductId = int.Parse(txtId.Text),
                    ProductDescription = txtDescription.Text,
                    ProductFinish = txtFinish.Text,
                    StandardPrice = decimal.Parse(txtPrice.Text)
                };

                try
                {
                    productRepository.InsertProduct(product);
                    loadList();
                    MessageBox.Show("Add success!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(err);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string err = checkInput();

            if (err.Equals(string.Empty))
            {
                ProductObject product = new ProductObject
                {
                    ProductId = int.Parse(txtId.Text),
                    ProductDescription = txtDescription.Text,
                    ProductFinish = txtFinish.Text,
                    StandardPrice = decimal.Parse(txtPrice.Text)
                };

                try
                {
                    productRepository.UpdateProduct(product);
                    loadList();
                    MessageBox.Show("Update success!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(err);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ProductObject product = new ProductObject
            {
                ProductId = int.Parse(txtId.Text),
                ProductDescription = txtDescription.Text,
                ProductFinish = txtFinish.Text,
                StandardPrice = decimal.Parse(txtPrice.Text)
            };

            try
            {
                productRepository.DeleteProduct(product);
                loadList();
                MessageBox.Show("Delete success!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
