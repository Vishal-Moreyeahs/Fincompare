using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Models
{
    public class ValidationErrorResponse
    {
        public bool Success { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
