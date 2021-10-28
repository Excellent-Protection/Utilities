using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.GlobalViewModels.CRM
{
    public class RecieptVoucherCRMVm
    {
        public Guid? Id { get; set; }
        public decimal? Amount { get; set; }

        public string ContactId { get; set; }

        public string ContractId { get; set; }
        public string FlexContractId { get; set; }

        public string Note { get; set; }
        public string PaymentNote { get; set; }

        public int? PaymentType { get; set; }

        public int? PointOfReciept { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public string PaymentCode { get; set; }

        public int? Source { get; set; }

        public string CarSource { get; set; }

        public decimal? FinalPrice { get; set; }

        public string VatAmount { get; set; }

        public decimal? VateRate { get; set; }
        public string VatGroupId { get; set; }
        public string TransactionId { get; set; }
        public string TransactionDesc { get; set; }
        public PaymentType type { get; set; }
        public string PaymentTypeName { get; set; }
        public string IndividualContractProcedureId { get; set; }
        public string IndividualContractProcedureName { get; set; }
        public string OwnerId { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string IBAN { get; set; }
        public bool IsInsuranceTransfer { get; set; }
        public string HousingBuildingId { get; set; }
        public decimal? Discount { get; set; }
        public decimal? AmountBeforeDiscount { get; set; }
        public decimal? CustomerBalance { get; set; }
        public List<RecieptVoucherCRMVm> PaidVouchers { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal? ActivationAmount { get; set; }
        public decimal? UsedBalance { get; set; }
        public decimal? WalletBalance { get; set; }
        public string AccountNumber { get; set; }
        public string CheckNumber { get; set; }
        public string IndividualContractRequestId { get; set; }
        public string IndividualContractId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string CardBrand { get; set; }
        public string CardHolder { get; set; }
        public string CardBinCountry { get; set; }
        public string ReceiptVoucherImageName { get; set; }
        public string EntityName { get; set; }
        //public IndividualContractRequestVm IndividualContractRequest { get; set; }
        public string VoucherNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public string FinancialRequestId { get; set; }
        public decimal? PriceWithoutDiscount { get; set; }
        public int? PaymentPosting { get; set; }
        public int? CollectionCreated { get; set; }
        public int? CardLast4Digit { get; set; }
    }
}
