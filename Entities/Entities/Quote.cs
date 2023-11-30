using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class Quote
    {
        [JsonProperty("open")]
        public List<double> Open { get; set; }

        [JsonProperty("volume")]
        public List<double> Volume { get; set; }

        [JsonProperty("high")]
        public List<double> High { get; set; }

        [JsonProperty("low")]
        public List<double> Low { get; set; }

        [JsonProperty("close")]
        public List<double> Close { get; set; }
    }
}
