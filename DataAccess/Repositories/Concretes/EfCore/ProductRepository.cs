namespace DataAccess.Repositories.Concretes.EfCore;

public class ProductRepository : EfBaseRepository<Product, AppDbContext>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
