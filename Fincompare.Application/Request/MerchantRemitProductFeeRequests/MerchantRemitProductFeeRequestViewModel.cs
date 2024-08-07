﻿using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request.MerchantRemitProductFeeRequests
{
    public class MerchantRemitProductFeeRequestViewModel
    {
        public class CreateMerchantRemitProductFeeRequest
        {
            [Required]
            public int MerchantId { get; set; }

            [Required]
            public string FeesName { get; set; } = null!;

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The Fees Currency field must be exactly 3 characters long.")]
            [Compare("SendCountry3Iso", ErrorMessage = "The Fees Currency must match the SendCountry3Iso.")]
            public string FeesCur { get; set; }

            [Required]
            public double Fees { get; set; }
            public double? PromoFees { get; set; }
            //public int? MerchantProductId { get; set; }

            [Required]
            public int ServiceCategoryId { get; set; }

            [Required]
            public int ProductId { get; set; }

            [Required]
            public int InstrumentId { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The SendCountry3Iso field must be exactly 3 characters long.")]
            public string SendCountry3Iso { get; set; } = null!;

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The ReceiveCurrency3Iso field must be exactly 3 characters long.")]
            public string ReceiveCountry3Iso { get; set; } = null!;

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The SendCurrency3Iso field must be exactly 3 characters long.")]
            public string SendCurrency { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3, ErrorMessage = "The ReceiveCurrency3Iso field must be exactly 3 characters long.")]
            public string ReceiveCurrency { get; set; }

            [Required]
            public double SendMinLimit { get; set; }

            [Required]
            public double SendMaxLimit { get; set; }

            [Required]
            public double ReceiveMinLimit { get; set; }

            [Required]
            public double ReceiveMaxLimit { get; set; }

            [Required]
            public DateTime ValidityExpiry { get; set; }

            [Required]
            public int? PayInInstrumentId { get; set; }
            public double VariableFee { get; set; } = 0;
        }
        public class UpdateMerchantRemitProductFeeRequest : CreateMerchantRemitProductFeeRequest
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
