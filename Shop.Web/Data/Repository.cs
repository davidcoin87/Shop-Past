

namespace Shop.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        //metodo que nos trae una lista de productos organizado por el nombre
        public IEnumerable<Product> GetProducts()
        {
            return this.context.Products.OrderBy(p => p.Name);
        }

        //metodo que me trae un producto por el ID
        public Product GetProduct(int id)
        {
            return this.context.Products.Find(id);
        }

        //metodo para agregar un producto
        public void AddProduct(Product product)
        {
            this.context.Products.Add(product);
        }

        //metodo para agregar un producto
        public void UpdateProduct(Product product)
        {
            this.context.Products.Update(product);
        }

        //metodo para eliminar un producto
        public void RemoveProduct(Product product)
        {
            this.context.Products.Remove(product);
        }

        //metodo para salvar los cambios en la base de datos
        //retorna un booleano true/false segun como realice el cambio y nos da el total de cambio realizados
        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        //metodo que me dice si el producto ya existe. retorna un booleano
        public bool ProductExists(int id)
        {
            return this.context.Products.Any(p => p.Id == id);
        }

    }
}
