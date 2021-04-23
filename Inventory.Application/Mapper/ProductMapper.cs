using Inventory.Application.ViewModels;
using Inventory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Mapper
{
    public class ProductMapper : MapperBase<Product, ProductViewModel>
    {
        public override Product Map(ProductViewModel viewModel)
        {
            var Product = new Product
            {
                Id = viewModel.Id.HasValue ? viewModel.Id.Value : new Guid(),
                Name=viewModel.Name,
                Quantity=viewModel.Quantity,
                Price=viewModel.Price,
                ImageUrl=viewModel.ImageUrl,
                Description=viewModel.Description,
                CategoryId=viewModel.CategoryId
            };

            if (viewModel.Category != null)
            {
                Product.Category = new Category { Id =(Guid) viewModel.Category.Id, Name = viewModel.Category.Name };
            }

            return Product;

        }

        public override ProductViewModel Map(Product entity)
        {
            if (entity == null) return null;
            var ProductDTO = new ProductViewModel
            {

                Id = entity.Id,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                CategoryId = entity.CategoryId
            };
            if (entity.Category != null)
            {
                ProductDTO.Category = new CategoryViewModel { Id = entity.Category.Id, Name = entity.Category.Name };
            }

            return ProductDTO;
        }
    }
}
