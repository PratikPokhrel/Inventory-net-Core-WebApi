using Inventory.Application.ViewModels;
using Inventory.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public interface IProductService
    {
        Task<PagedDataInquiryResponse<ProductViewModel>> GetListAsync(string searchText, int page, int pageSize, string orderDir, string orderBy);
        Task<ProductViewModel> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ProductViewModel> AddAsync(ProductViewModel model);
        Task<ProductViewModel> UpdateAsync(ProductViewModel model);
    }
}
