using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wilson.Web.Areas.Accounting.Services
{
    public interface IPayrollService
    {
        Task<List<SelectListItem>> GetAccountingEmployeeOptions();
    }
}
