using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion_tax_calculator.Application.CommonResponse
{
    public class BaseCommandResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
