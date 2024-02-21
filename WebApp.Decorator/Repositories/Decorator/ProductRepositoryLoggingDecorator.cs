using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator
{
    public class ProductRepositoryLoggingDecorator : BaseProductRepositoryDecorator
    {
        private readonly ILogger _logger;
        public ProductRepositoryLoggingDecorator(IProductRepository repository, ILogger log) : base(repository)
        {
            _logger = log;
        }
        public override Task<List<Product>> GetAll()
        {
            _logger.LogInformation("GetAll() çalıştı");
            
            return base.GetAll();
        }
        public override Task<List<Product>> GetAll(string userId)
        {
            _logger.LogInformation("GetAll(userId) çalıştı");
            return base.GetAll(userId);
        }
        public override Task<Product> Save(Product product)
        {
            _logger.LogInformation("Save() çalıştı");
            return base.Save(product);
        }
        public override Task Update(Product product)
        {
            _logger.LogInformation("Update() çalıştı");
            return base.Update(product);
        }
        public override Task Remove(Product product)
        {
            _logger.LogInformation("Remove() çalıştı");
            return base.Remove(product);
        }
    }
}
