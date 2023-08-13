using System;
using System.Collections.Generic;
using System.Text;

namespace E_Library.Data.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IAddressRepo AddressRepo { get; }
        IAuthorRepo AuthorRepo { get; }
        IBookRepo BookRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        IPublisherRepo PublisherRepo { get; }
        IReviewsRepo ReviewsRepo { get; }
        ISubCategoryRepo SubCategoryRepo { get; }
        ITagRepo TagRepo { get; }
        void Save();
    }
}
