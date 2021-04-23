using Inventory.Application.Services;
using Inventory.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Inventory.Web.Areas.Product.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        protected readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        /// <summary>
        /// Get List of Products
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="orderDir"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        [Route("List"), HttpGet]
        public async Task<IActionResult> List([FromQuery] string searchText, [FromQuery] string orderDir, [FromQuery] string orderBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var returnList = await _ProductService.GetListAsync(searchText, page, pageSize, orderDir, orderBy);
                return Ok(returnList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException.Message);
            }
        }
        /// <summary>
        /// Get Single Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Route("[action]/{id}"), HttpGet]
        public async Task<object> Get(Guid id)
        {
            try
            {
                    ProductViewModel product = await _ProductService.GetAsync(id);
                    if (product != null)
                    return new ApiResponse<ProductViewModel>(true, "Successfully retrived product",product);

                return new ApiResponse<ProductViewModel>(false, "Failed to retrive product");

            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException.Message);
            }
        }
        /// <summary>
        /// Add New Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        [Route("[action]"), HttpPost]
        public async Task<object> Add([FromBody] ProductViewModel product)
        {
            try
            {
                    var result = await _ProductService.AddAsync(product);
                    return new ApiResponse<ProductViewModel>(true, "Product added successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductViewModel>(true, "failed to add project,please try again later");
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        [Route("[action]"), HttpPut]
        public async Task<object> Update([FromBody] ProductViewModel product)
        {
            try
            {
                var result = await _ProductService.AddAsync(product);
                return new ApiResponse<ProductViewModel>(true, "Product Updated successfully");

            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductViewModel>(false, "failed to Update project,please try again later");
            }
        }


        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Route("[action]/{id}"), HttpDelete]
        public async Task<object> Delete(Guid id)
        {
            try
            {
                await _ProductService.DeleteAsync(id);
                return new ApiResponse<ProductViewModel>(true, "Product Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductViewModel>(false, "failed to Delete project,please try again later");
            }
        }

    }
}
