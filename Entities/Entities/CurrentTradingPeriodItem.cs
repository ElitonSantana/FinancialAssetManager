using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class CurrentTradingPeriodItem
    {
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("start")]
        public long End { get; set; }

        [JsonProperty("end")]
        public long Start { get; set; }

        [JsonProperty("gmtoffset")]
        public int GmtOffset { get; set; }
    }
}
