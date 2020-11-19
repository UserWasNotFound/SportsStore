namespace SportsStore.Models
{
    using System.Collections.Generic;

    public class FakeRepository /*: IProductRepository*/
    {
        public IEnumerable<Product> Products =>
            new List<Product>
                {
                    new Product { Name = "Football", Price = 25 },
                    new Product { Name = "Surf board", Price = 179 },
                    new Product { Name = "Running shoes", Price = 95 }
                };
    }
}