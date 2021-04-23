using Inventory.Application.Mapper;
using Inventory.Application.ViewModels;
using Inventory.Common;
using Inventory.DAL;
using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly ProductMapper _productMapper;
        public ProductService(DataContext context)
        {
            _productMapper = new ProductMapper();
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<PagedDataInquiryResponse<ProductViewModel>> GetListAsync(string searchText, int page, int pageSize, string orderDir, string orderBy)
        {
            IQueryable<Product> query = _context.Product.Include(e=>e.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(e => e.Name.ToLower().Contains(searchText.ToLower())
                                      || e.Description.ToLower().Contains(searchText.ToLower()));

            int totalItemsCount = query.Count();

            var ProductViewModelList = await query.Select(selectExpression).GetPagedResult(orderBy, orderDir, page, pageSize);

            QueryResult<ProductViewModel> queryResult = new QueryResult<ProductViewModel>(ProductViewModelList, totalItemsCount, pageSize);
            return PagedDataResponse<ProductViewModel>.CreatedPagedDataResponse(ProductViewModelList, totalItemsCount, queryResult.TotalPageCount, page, pageSize);
        }



        public async Task<ProductViewModel> GetAsync(Guid id) =>
             await _context.Product.Where(e => e.Id == id).Select(selectExpression).FirstOrDefaultAsync();

        public async Task<ProductViewModel> UpdateAsync(ProductViewModel ProductViewModel)
        {
            Product productToUpdate = _productMapper.Map(ProductViewModel);
            Product product = await _context.Product.FirstOrDefaultAsync(e => e.Id == ProductViewModel.Id);
            if (product != null)
            {
                product.Name = productToUpdate.Name;
                product.Quantity = productToUpdate.Quantity;
                product.Description = productToUpdate.Description;
                product.CategoryId = productToUpdate.CategoryId;
                product.Price = productToUpdate.Price;


                _context.Product.Update(product);
                await _context.SaveChangesAsync();
                return ProductViewModel;
            }
            throw new Exception("Product not found");

        }

        public async Task<ProductViewModel> AddAsync(ProductViewModel productDTO)
        {
            Product product = _productMapper.Map(productDTO);
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return productDTO;
        }

        public async Task DeleteAsync(Guid id)
        {
            Product product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            if (product != null)
            {
                product.IsDeleted = true;
                _context.Product.Update(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        #region "Entity TO DTO Select"
        private static readonly Expression<Func<Product, ProductViewModel>> selectExpression = e =>
                                                                               new ProductViewModel
                                                                               {
                                                                                   Id = e.Id,
                                                                                   Name = e.Name,
                                                                                   Price = e.Price,
                                                                                   Description = e.Description,
                                                                                   Quantity = e.Quantity,
                                                                                   ImageUrl = e.ImageUrl,
                                                                                   Category =  new CategoryViewModel
                                                                                   {
                                                                                       Id = e.Category.Id,
                                                                                       Name = e.Category.Name
                                                                                   }
                                                                               };
        #endregion
    }
}
