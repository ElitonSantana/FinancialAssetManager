using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class MessageResponse<T>
    {
        public bool isSuccessful { get; set; } = false;
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
