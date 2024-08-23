using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincompare.Application.Response.AssetMasterResponse
{
    public class AssetMasterViewModel
    {
        public int Id { get; set; }

        public string AssetName { get; set; } = null!;

        public string AssetDescription { get; set; } = null!;

        public string Country3Iso { get; set; } = null!;

        public DateTime Date { get; set; }

        public bool Status { get; set; }
    }
}
