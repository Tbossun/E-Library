using E_Library.Data.Context;
using E_Library.Data.Repositories.IRepositories;

namespace E_Library.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _DbContext;

        public IAddressRepo AddressRepo { get; private set; }
        public IAuthorRepo AuthorRepo { get; private set; }
        public IBookRepo BookRepo { get; private set; }
        public ICategoryRepo CategoryRepo { get; private set; }
        public IPublisherRepo PublisherRepo { get; private set; }
        public IReviewsRepo ReviewsRepo { get; private set; }
        public ISubCategoryRepo SubCategoryRepo { get; private set; }
        public ITagRepo TagRepo { get; private set; }


        public UnitOfWork(AppDbContext DbContext)
        {
            _DbContext = DbContext;
            AddressRepo = new AddressRepo(_DbContext);
            AuthorRepo = new AuthorRepo(_DbContext);
            BookRepo = new BookRepo(_DbContext);
            CategoryRepo = new CategoryRepo(_DbContext);
            PublisherRepo = new PublisherRepo(_DbContext);
            SubCategoryRepo = new SubCategoryRepo(_DbContext);
            TagRepo = new TagRepo(_DbContext);
        }


        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
