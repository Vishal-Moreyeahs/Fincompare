using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Domain.Entities.Common
{
    public class DateBase : ActionBase
    {
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }
    }

    public class ActionBase
    { 
        public bool IsDeleted { get; set; }  = false;
    }    
}
