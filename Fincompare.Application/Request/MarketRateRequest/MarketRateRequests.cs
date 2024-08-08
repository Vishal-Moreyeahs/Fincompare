using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fincompare.Application.Request.MarketRateRequest
{
    public class MarketRateRequests
    {
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The Send Currency field must be exactly 3 characters long.")]
        public string SendCur { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The Receive Currency field must be exactly 3 characters long.")]
        public string ReceiveCur { get; set; }

        [Required]
        public double Rate { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string RateSource { get; set; } = "Capture";
    }

    public class AddMarketRate : MarketRateRequests { }

    public class UpdateMarketRate : MarketRateRequests
    {
        [Required]
        public int Id { get; set; }
    }

}
