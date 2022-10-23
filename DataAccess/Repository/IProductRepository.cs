using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProducts();
        ProductObject GetProductByID(int productId);
        void InsertProduct(ProductObject product);
        void DeleteProduct(ProductObject product);
        void UpdateProduct(ProductObject product);
        IEnumerable<ProductObject> GetProductsByDescription(string search);
    }
}
