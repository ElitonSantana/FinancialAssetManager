using FinancialAssetManager.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Domain.Interfaces.Repository
{
    public interface IAssetManagerRepository
    {
        Task<List<Chart>> GetAsync();
        Task<Chart> GetAsync(string Symbol);
        Task<Chart> GetAsyncById(string _id); 
        Task CreateAsync(Chart Chart);
        Task UpdateAsync(string _id, Chart Chart);
        Task DeleteAsync(string _id);
    }
}
