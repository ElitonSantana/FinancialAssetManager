using FinancialAssetManager.Entities.Entities;
using FinancialAssetManager.Entities.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Domain.Interfaces.Services
{
    public interface IAssetManagerService
    {
        Task<MessageResponse<Chart>> GetChartBySymbol(string Symbol);
        Task<MessageResponse<List<VMValueVariation>>> GetVariationBySimbol(string Symbol, int Days, bool AllDays);
        Task<MessageResponse<bool>> DeleteChartById(string _id);
        Task<MessageResponse<List<Chart>>> GetList();
        Task<MessageResponse<Chart>> CreateChartOnlineBySymbol(string Symbol);
        Task<MessageResponse<Chart>> UpdateChartOnlineBySymbol(string Symbol);

    }
}
