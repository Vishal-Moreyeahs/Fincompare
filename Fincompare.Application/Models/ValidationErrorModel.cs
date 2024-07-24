using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Models
{
    public class CustomValidationProblemDetails
    {
        public bool Success { get; set; }
        public List<CustomValidationError> Errors { get; set; } = new List<CustomValidationError>();
    }

    public class CustomValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }


}
