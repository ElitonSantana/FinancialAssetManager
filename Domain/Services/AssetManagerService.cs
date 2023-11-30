using FinancialAssetManager.Domain.Interfaces.Repository;
using FinancialAssetManager.Domain.Interfaces.Services;
using FinancialAssetManager.Domain.Options;
using FinancialAssetManager.Entities.Entities;
using FinancialAssetManager.Entities.Entities.VM;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Domain.Services
{
    public class AssetManagerService : IAssetManagerService
    {
        private readonly CredentialsOptions _credentialsOptions;
        private readonly IAssetManagerRepository _assetManagerRepository;
        public AssetManagerService(
            IOptions<CredentialsOptions> credentialsOptions,
            IAssetManagerRepository assetManagerRepository
            )
        {
            _credentialsOptions = credentialsOptions.Value;
            _assetManagerRepository = assetManagerRepository;
        }

        #region :: Public Methods ::

        /// <summary>
        /// Retorno de informações pelo Symbol.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        public async Task<MessageResponse<Chart>> GetChartBySymbol(string Symbol)
        {
            MessageResponse<Chart> response = new MessageResponse<Chart>();
            try
            {
                if (String.IsNullOrEmpty(Symbol))
                {
                    response.Message = "Symbol is Empty!";
                    return Task.FromResult(response).Result;
                }

                response.Value = await _assetManagerRepository.GetAsync(Symbol);
                if (response.Value == null)
                {
                    response.Message = "Value for Symbol is NotFound!";
                    return Task.FromResult(response).Result;
                }

                return Task.FromResult(response).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(GetChartBySymbol)} exception {ex.Message} Symbol {Symbol}");
                throw ex;
            }
        }

        /// <summary>
        /// Retorno de informações com informação de percentual de variação dos valores, com um parâmetro de data ou base toda.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <param name="Days"></param>
        /// <param name="AllDays"></param>
        /// <returns></returns>
        public async Task<MessageResponse<List<VMValueVariation>>> GetVariationBySimbol(string Symbol, int Days, bool AllDays)
        {
            MessageResponse<List<VMValueVariation>> response = new MessageResponse<List<VMValueVariation>>();

            try
            {
                if (String.IsNullOrEmpty(Symbol))
                {
                    response.Message = "Symbol is Empty!";
                    return Task.FromResult(response).Result;
                }

                var chart = await _assetManagerRepository.GetAsync(Symbol.ToUpper());
                if (chart == null)
                {
                    response.Message = "Value for Symbol is NotFound!";
                    return Task.FromResult(response).Result;
                }

                var tradingDates = chart.Result?.FirstOrDefault()?.Timestamp;
                var assetValues = chart.Result?.FirstOrDefault()?.Indicators?.Quote?.FirstOrDefault()?.Open;

                //Validação das datas de pregões e valores de ativos.
                if (tradingDates == null
                        ||
                    assetValues == null
                        ||
                    tradingDates?.Count != assetValues?.Count())
                {
                    response.Message = "Error, values invalid to trading dates and asset values.";
                    return Task.FromResult(response).Result;
                }

                if (tradingDates.Count < Days
                        ||
                    assetValues.Count < Days)
                {
                    response.Message = "Error, the days reported are smaller than total.";
                    return Task.FromResult(response).Result;
                }

                response.Value = await GetListWithVariation(tradingDates, assetValues, Days, AllDays);
                response.isSuccessful = true;

                return Task.FromResult(response).Result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(GetVariationBySimbol)} exception {ex.Message} Symbol {Symbol.ToUpper()} Days {Days} AllDays {AllDays}");
                throw ex;
            }
        }

        /// <summary>
        /// Criação de um registro chart a partir de um parâmetro symbol consultando no Yahoo.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        public async Task<MessageResponse<Chart>> CreateChartOnlineBySymbol(string Symbol)
        {
            MessageResponse<Chart> response = new MessageResponse<Chart>();
            try
            {
                if (String.IsNullOrEmpty(Symbol))
                {
                    response.Message = "Symbol is Empty!";
                    return Task.FromResult(response).Result;
                }

                var value = _assetManagerRepository.GetAsync(Symbol).Result;

                if (value != null)
                {
                    response.Message = "Value for Symbol are existent on the base";
                    response.isSuccessful = false;
                    return Task.FromResult(response).Result;
                }
                else
                {
                    response.Value = await GetChartToSiteBySymbol(Symbol);
                    if (response.Value == null)
                    {
                        response.Message = "Value for Symbol is NotFound!";
                        return Task.FromResult(response).Result;
                    }

                    await _assetManagerRepository.CreateAsync(response.Value);
                    response.isSuccessful = true;
                }

                return Task.FromResult(response).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em {nameof(CreateChartOnlineBySymbol)} exception {ex.Message}");
                throw ex;
            }
        }

        /// <summary>
        /// Atualização de um registro chart a partir de um parâmetro symbol consultando no Yahoo.
        /// </summary>
        /// <param name="Symbol"></param>
        /// <returns></returns>
        public async Task<MessageResponse<Chart>> UpdateChartOnlineBySymbol(string Symbol)
        {
            MessageResponse<Chart> response = new MessageResponse<Chart>();
            try
            {
                if (String.IsNullOrEmpty(Symbol))
                {
                    response.Message = "Symbol is Empty!";
                    return Task.FromResult(response).Result;
                }

                var value = _assetManagerRepository.GetAsync(Symbol).Result;

                if (value != null)
                {
                    response.Value = await GetChartToSiteBySymbol(Symbol);
                    if (response.Value == null)
                    {
                        response.Message = "Value for Symbol is NotFound!";
                        return Task.FromResult(response).Result;
                    }

                    response.Value.CreationDate = value.CreationDate;
                    await _assetManagerRepository.UpdateAsync(value._id, response.Value);
                    response.isSuccessful = true;
                }
                else
                {
                    response.Message = "Value for Symbol are not existent on the base";
                    response.isSuccessful = false;
                }

                return Task.FromResult(response).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em {nameof(UpdateChartOnlineBySymbol)} exception {ex.Message}");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MessageResponse<bool>> DeleteChartById(string _id)
        {
            try
            {
                if (string.IsNullOrEmpty(_id))
                {
                    return new MessageResponse<bool>
                    {
                        isSuccessful = false,
                        Message = "_id is null or empty"
                    };
                }

                if (_assetManagerRepository.GetAsyncById(_id).Result != null)
                    await _assetManagerRepository.DeleteAsync(_id);
                else
                {
                    return new MessageResponse<bool>
                    {
                        isSuccessful = false,
                        Message = "Chart with _id not found"
                    };
                }
                
                return new MessageResponse<bool> { isSuccessful = true, Value = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em {nameof(DeleteChartById)} exception {ex.Message} _id {_id}");
                throw ex;
            }
        }

        /// <summary>
        /// Retorna todas os ativos da base.
        /// </summary>
        /// <returns></returns>
        public async Task<MessageResponse<List<Chart>>> GetList()
        {
            try
            {
                MessageResponse<List<Chart>> response = new MessageResponse<List<Chart>>();
                response.Value = await _assetManagerRepository.GetAsync();

                if (response.Value == null)
                {
                    response.Message = "Error on get list all documents.";
                    return response;
                }


                response.isSuccessful = true;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em {nameof(GetList)} exception {ex.Message}");
                throw;
            }
        }

        #endregion

        #region :: Private Methods ::

        /// <summary>
        /// Retorna de lista já com os calculos da variação de percentual.
        /// </summary>
        /// <param name="tradingDates"> Data dos pregões.</param>
        /// <param name="assetValues"> Valores dos ativos.</param>
        /// <returns></returns>
        private async Task<List<VMValueVariation>> GetListWithVariation(List<long> tradingDates, List<double> assetValues, int days, bool allDays)
        {
            try
            {
                List<VMValueVariation> valuesVariation = new List<VMValueVariation>();

                // Caso não desejar todos os dias, inverter a ordem da lista para pegar da maior data para menor ( últimos dias );
                if (!allDays)
                {
                    tradingDates.Reverse();
                    assetValues.Reverse();
                }

                int count = allDays ? tradingDates.Count : days;

                for (int i = 0; i < count; i++)
                {
                    valuesVariation.Add(
                        new VMValueVariation
                        {
                            Id = i,
                            TradingDate = ConvertTimeStampToDateTime(tradingDates[i]),
                            AssetValue = Double.Parse(assetValues[i].ToString("0.00")),
                            FirstDayVariationValue = i == 0 ? "-" : GetValueOfVariation(assetValues[i], assetValues.First()),
                            ValueOfYesterdayChange = i == 0 ? "-" : GetValueOfVariation(assetValues[i], assetValues[i - 1])
                        });
                }

                return valuesVariation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(GetListWithVariation)} exception {ex.Message} tradingDates {JsonConvert.SerializeObject(tradingDates)} assetValues {JsonConvert.SerializeObject(assetValues)}");
                throw ex;
            }
        }

        /// <summary>
        /// Método para retorno de calcula do percentual com relação a um valor X.
        /// </summary>
        /// <param name="currentAssetValue">Valor atual</param>
        /// <param name="assetValueToCompare">Valor para comparar percentual</param>
        /// <returns></returns>
        private string GetValueOfVariation(double currentAssetValue, double assetValueToCompare)
        {
            try
            {
                return $"{Double.Parse(((currentAssetValue - assetValueToCompare) / currentAssetValue * 100).ToString("0.00"))}%";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(GetValueOfVariation)} exception {ex.Message} currentAssetValue {currentAssetValue} assetValueToCompare {assetValueToCompare}");
                throw ex;
            }
        }

        /// <summary>
        /// Método para realizar a conversão de um timestamp para DateTime utilizando o Timezone Local.
        /// </summary>
        /// <param name="timeStaamp">Valor em segundos</param>
        /// <returns></returns>
        private DateTime ConvertTimeStampToDateTime(long timeStamp)
        {
            //Fuso horário local da aplicação.
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
            DateTime dateTimeLocal = TimeZoneInfo.ConvertTime(dateTimeOffset.DateTime, localTimeZone);

            return dateTimeLocal;
        }

        /// <summary>
        /// Retorno de informações da chamada da url do Yahoo
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private async Task<Chart> GetChartToSiteBySymbol(string symbol)
        {
            try
            {
                string chartUrl = $"{_credentialsOptions.ChartUrl}{symbol.ToUpper()}";

                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(chartUrl);

                        //Valida StatusCode 200
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"chartUrl {chartUrl} content {content}");
                            FinanceChart result = JsonConvert.DeserializeObject<FinanceChart>(content);
                            return result.Chart;
                        }
                        else
                            Console.WriteLine($"Erro: {response.StatusCode} response {JsonConvert.SerializeObject(response)}");

                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro 1 em {nameof(GetChartToSiteBySymbol)} exception {ex.Message}");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro 2 em {nameof(GetChartToSiteBySymbol)} exception {ex.Message}");
                throw ex;
            }
        }

        #endregion

    }
}
