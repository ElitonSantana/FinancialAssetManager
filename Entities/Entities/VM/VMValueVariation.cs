using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities.VM
{
    public class VMValueVariation
    {
        public int Id { get; set; }
        /// <summary>
        /// Data do pregão
        /// </summary>
        public DateTime TradingDate {get;set;}
        /// <summary>
        /// Valor do ativo
        /// </summary>
        public double AssetValue { get; set; }
        /// <summary>
        /// Valor da variação D-1
        /// </summary>
        public string ValueOfYesterdayChange { get; set; }
        /// <summary>
        /// Valodar da variação comparado ao primeiro dia.
        /// </summary>
        public string FirstDayVariationValue { get; set; }
    }
}
