using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class Indicators
    {
        [JsonProperty("quote")]
        public List<Quote> Quote { get; set; }
    }
}
