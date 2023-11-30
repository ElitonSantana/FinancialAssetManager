using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{

    public class Chart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [JsonProperty("result")]
        public List<FinanceChartResult> Result { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("modifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }
}
