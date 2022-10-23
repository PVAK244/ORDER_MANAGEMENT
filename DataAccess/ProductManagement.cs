using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class ProductManagement
    {
        private static ProductManagement instance = null;
        private static readonly object iLock = new object();
        public ProductManagement()
        {

        }

        public static ProductManagement Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductManagement();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<ProductObject> GetProductList()
        {
            List<ProductObject> Products;
            try
            {
                var db = new OrderManagementContext();
                Products = db.Products.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Products;
        }

        public ProductObject GetProductById(decimal id)
        {
            ProductObject product = null;
            try
            {
                var db = new OrderManagementContext();
                product = db.Products.SingleOrDefault(c => c.ProductId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }

        public void AddNew(ProductObject product)
        {
            try
            {
                ProductObject _product = GetProductById(product.ProductId);
                if (_product == null)
                {
                    var db = new OrderManagementContext();
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(ProductObject p)
        {
            try
            {
                ProductObject _product = GetProductById(p.ProductId);
                if (_product != null)
                {
                    var db = new OrderManagementContext();
                    db.Products.Update(p);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Remove(ProductObject product)
        {
            try
            {
                ProductObject _product = GetProductById(product.ProductId);
                if (_product != null)
                {
                    var db = new OrderManagementContext();
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ProductObject> Search(string searchValue)
        {
            List<ProductObject> Products;
            try
            {
                var db = new OrderManagementContext();
                Products = db.Products.ToList().FindAll(c => c.ProductDescription.ToLower().Contains(searchValue));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Products;
        }
    }
}
