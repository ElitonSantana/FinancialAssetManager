using FinancialAssetManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAssetManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetManagerController : ControllerBase
    {
        private readonly ILogger<AssetManagerController> _logger;
        private readonly IAssetManagerService _assetManagerService;

        public AssetManagerController(ILogger<AssetManagerController> logger, IAssetManagerService assetManagerService)
        {
            _logger = logger;
            _assetManagerService = assetManagerService;
        }

        /// <summary>
        /// Busca de informações online pelo symbol e atualização na base.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChartOnline")]
        public async Task<IActionResult> GetChartOnlineAsync(string Symbol)
        {
            var result = await _assetManagerService.GetChartOnlineBySymbol(Symbol.ToUpper());
            if (!result.isSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retorna informaçõões
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

        [HttpGet]
        [Route("GetChartVariationWithRange")]
        public async Task<IActionResult> GetChartVariationWithRange(string Symbol, int Days = 30, bool AllDays = true)
        {
            var result = await _assetManagerService.GetVariationBySimbol(Symbol.ToUpper(), Days, AllDays);

            if (!result.isSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }

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
