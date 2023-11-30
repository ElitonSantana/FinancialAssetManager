using FinancialAssetManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAssetManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetManagerController : ControllerBase
    {
        private readonly IAssetManagerService _assetManagerService;

        public AssetManagerController(IAssetManagerService assetManagerService)
        {
            _assetManagerService = assetManagerService;
        }

        /// <summary>
        /// Criação de registro no banco, com base no Symbol.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOnline(string Symbol)
        {
            var result = await _assetManagerService.CreateChartOnlineBySymbol(Symbol.ToUpper());
            if (!result.isSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }

        /// <summary>
        /// Atualização de registro no banco, com base no Symbol.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateOnline(string Symbol)
        {
            var result = await _assetManagerService.UpdateChartOnlineBySymbol(Symbol.ToUpper());
            if (!result.isSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retorna informações da base a partir de um symbol.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChart")]
        public async Task<IActionResult> Get(string Symbol)
        {
            var result = await _assetManagerService.GetChartBySymbol(Symbol.ToUpper());

            if (!result.isSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retorna informação da base com relação a um symbol ( ativo ) e parâmetros de variação.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <param name="Days"></param>
        /// <param name="AllDays"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChartVariationWithRange")]
        public async Task<IActionResult> GetChartVariationWithRange(string Symbol, int Days = 30, bool AllDays = true)
        {
            var result = await _assetManagerService.GetVariationBySimbol(Symbol.ToUpper(), Days, AllDays);

            if (!result.isSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }

        /// <summary>
        /// Remoção de um chart da base a partir de um symbol ( ativo )
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string Symbol)
        {
            var result = await _assetManagerService.GetChartBySymbol(Symbol.ToUpper());
            if (result.Value != null)
                await _assetManagerService.DeleteChartById(result.Value._id);
            else
                return BadRequest(result.Message);

            return Ok(true);
        }

        /// <summary>
        /// Retorno de todos os charts da base
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _assetManagerService.GetList();

            if (result.isSuccessful)
                return Ok(result);
            else 
                return BadRequest(result.Message);
        }

    }
}
