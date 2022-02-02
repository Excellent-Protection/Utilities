using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Enums
{
    public enum PromotionType
    {
        LoyaltyPromotion = 3
    }
    public enum VisitShift
    {
        Morning = 1,
        Evening = 2,
        FullDay = 3
    }
    public enum DayShifts
    {
        Morning = 0,
        Evening = 1,
    }
    public enum AttachmentTypes
    {
        FinancialRequest = 100000008,
        Other = 5,

        Contract = 4,
        DrivingLicense = 279640003,
        passport = 1,
        IdCard = 279640002,
        MedicalInsuranceAttachment = 22,
        ATMCardAttachment = 23,


    }
    public enum SectorsTypeEnum
    {
        Business = 2,
        Individuals = 3,
        HeadOffice = 1,
        Hourly = 4
    }
    public enum ApplyToShiftEnum
    {
        All = 4,
        Morning = 1,
        Evening = 2,
        FullDay = 3
    }

    public enum ServiceContractPerHourStatus
    {
        Canceled = 100000007,
        PaymentIsPendingConfirmation = 100000008,
        ConfirmedByFinance = 100000009,
        ConfirmedNotPaid = 100000006,
        Finished = 100000000,
        ConfirmedPaymentWasMade = 100000002,
        WaitingConfirmation = 100000004,
        FullRefundHasbeenMade = 100000015,
        RefundIsUnderWay = 100000014,
        PostPonded=	100000003,
        Running=	100000005,
        NoAvailable=	100000013


//Part of the amount has been refunded	100000016
    }

    public enum CustomerTicketSectorType
    {
        Individual = 2,
        Hourly = 4,
        Business = 1,
        HO = 3
    }

    public enum CustomerTicketStatus
    {
        PendingService = 100000000,
        SendtoCSforclose = 100000012,
        Servicehadstopped = 100000008,
        Servicehaddonesuccessfully = 100000009,
        SendToResponsable = 100000010,
        Rejected = 100000013,
        SendtoCallCenterSupervisor = 100000014

    }

    public enum Who
    {
        CRM = 1,
        Web = 3,
        Mobile = 2
    }
    public enum TransactionType
    {
        ServiceContractPerHour = 1,
        DomesticInvoice = 2
    }

    public enum EmployeeStatus
    {
        New = 1,
        BackLabor = 279640000,
        Remepolize = 279640012,
        InHousing = 279640001,
        InHousingReserved = 279640020
    }

    public enum GenderEnum
    {
        Male = 1,
        Female = 2,
        both = 3
    }
    public enum PaymentType
    {
        HourlyContract = 1,
        FlexibleService = 2,
        IndividualContractRequest = 3,
        IndividualContract = 4,
        RenewIndividualContract = 5,
        FinancialRequest = 6 ,
        Enterprise = 7 ,
        IndvProcedure = 8,
        Points = 20
    }

    public enum EvaluationSkills
    {
        general = 0,
        cooking = 1,
        laundry = 2,
        dealWithChild = 3
    }

    public enum ContractStepsEnum
    {
        FirstStep = 1,
        SecondStep = 2,
        ThirdStep = 3,
        ForthStep = 4,
        FifthStep = 5,
        SixthStep = 6,
        SeventhStep = 7,
        EighthStep = 8
    }

    public enum StepTypeEnum
    {
        Next = 1,
        Previous = 0
    }

    public enum WorkerSkillEnum
    {
        NoSkill = -1,
        Worker = 1,
        Cooker = 2,
        BabySitter = 3,
        Supervisor = 4,
        CaringForElderly = 5
    }


    public enum HourlyProblemType
    {
        ProblemTypeOthers = 100000005,
        AskEnquiry = 100000006,
        NewSuggestion = 100000007,
        CancellationRequest = 100000008,
        ComplainOnEmployee = 100000009,
        UpdateContract = 100000010,
        PermanentWorkerRequest = 100000011
    }

    public enum ArabicDayOfWeek
    {
        //
        // Summary:
        //     Indicates Sunday.
        الأحد = 1,
        //
        // Summary:
        //     Indicates Monday.
        الإثنين = 2,
        //
        // Summary:
        //     Indicates Tuesday.
        الثلاثاء = 3,
        //
        // Summary:
        //     Indicates Wednesday.
        الأربعاء = 4,
        //
        // Summary:
        //     Indicates Thursday.
        الخميس = 5,
        //
        // Summary:
        //     Indicates Friday.
        الجمعة = 6,
        //
        // Summary:
        //     Indicates Saturday.
        السبت = 0
    }
    public enum EnglishDayOfWeek
    {
        //
        // Summary:
        //     Indicates Sunday.
        Sunday = 1,
        //
        // Summary:
        //     Indicates Monday.
        Monday = 2,
        //
        // Summary:
        //     Indicates Tuesday.
        Tuesday = 3,
        //
        // Summary:
        //     Indicates Wednesday.
        Wednesday = 4,
        //
        // Summary:
        //     Indicates Thursday.
        Thursday = 5,
        //
        // Summary:
        //     Indicates Friday.
        Friday = 6,
        //
        // Summary:
        //     Indicates Saturday.
        Saturday = 0
    }
    public enum ContractTypeEnum
    {
        Systemic = 0,
        Network = 1
    }

    public enum NetTypeEnum
    {
        Branch = 0,
        OnDelivery = 1
    }

    //for employees
    public enum IndividualRequestStatus
    {
        Approved = 100000000,
        Cancelled = 100000001,
        ConfirmedByFinance = 100000004,
        PaymentIsPendingConfirmation = 100000006,
        New = 1
    }
    public enum LandingPage
    {
        No = 0,
        Yes = 2
    }

    public enum IndividualContractStatus
    {
        Cancelled = 279640008,
        UnderTransferKafala = 279640020,
        New = 1,
        WaitingToCollectAmountFromCustomer = 279640019,
        WaitingApprovalFromIndividualsSectorManager = 279640009,
        WaitingCEOApproval = 279640010,
        WaitingVicePresidentApproval = 279640011,
        WaitingAdvancedPayment = 279640012,
        WaitingSendLaborToBranch = 279640013,
        WaitingDeliveryLaborToClient = 279640014,
        ActiveLaborDelivered = 279640001,
        RequestRejected = 279640015,
        Replacement = 279640006,
        ContractCancellationRequest = 279640017,
        ContractCloseRequest = 279640004,
        CancellationRejected = 279640005,
        ClosedNotRenewedYet = 279640007,
        Reserved = 279640000,
        Temp = 279640003,
        Escape = 279640016,
        SponsorshipTransferred = 279640018,
        TransferredVisa = 279640021,
        WaitingRenewalCollection = 279640022,
        RenewedWaitingApproval = 279640023,
        FinishedAndrenewedAgain = 279640024,
    }
    public enum MaritalStatus
    {
        Single = 1,
        Married = 2,
        Divorcee = 3,
        Widow = 4
    }
    public enum EmployeeExperience
    {
        New = 1,
        HasExperience = 2,
        Donotknow = -1
    }
    public enum EmployeeReligion
    {
        Muslim = 1,
        Christian = 2,
        Donotknow = -1,
        Other = 5
    }
    public enum CollectionPaymentType
    {
        GauranteeAmount = 2,
        AdvancePayment = 3
    }
    public enum CollectionPaymentMethod
    {
        TransferFromWallet = 10,
        Points=20,
        TamayouzSystem=100
    }
    public enum CustomerRequestEnum
    {
        RenewContract = 4
    }
    public enum IndividualProcedureStatus
    {
        New = 1,
        Completed = 100000005,
        Rejected = 100000006,
        WaitingStartDate = 100000010,
        PaymentIsPendingConfirmation = 100000011
    }
    public enum ProcedureClosingReason
    {
        CancelledContract = 1,
        Renew = 4,
    }

    public enum IndividualContractRequestStatus
    {
        New = 1,
        Approved = 100000000,
        Cancelled = 100000001,
        ConfirmedByFinance = 100000004,
        PaymentIsPendingConfirmation = 100000006
    }
    public enum HousingType
    {
        RealHouse = 100000000
    }
    public enum RecieptVoucherPaymentType
    {
        Raghi = 100000000,
        BankTransfer = 2,
        LocalBank = 100000001,
        Visa = 100000002,
        HyperPay = 9,
        Cash = 3,
        STCPay = 100000005,
        Check = 1,
        TransferFromWallet = 10
    }
    public enum SectorType
    {
        individual = 2,
        hourly = 4
    }
    #region New PromotionSystem Enums
    public enum PromotionTypeEnum
    {
        Broadcast = 1,
        Personal = 2,
        Loyalty = 3,
        Parent = 4
    }
    public enum ApplyToPromotionEnum
    {
        CRM = 1,
        WebandMobile = 2,
        All = 3
    }
    public enum ContactClassEnum
    {
        Any = 1,
        CompanyEmployee = 2,
        BusinessUnit = 3,
        VIP = 4,
        Normal = 5
    }

    public enum PromotionStatusEnum
    {
        NoPromotion = 1,
        Valid = 2,
        InValid = 3,
        InCorrect = 4,
        InValidWithFriday = 5,
        InValidNoPromotion = 6,
        InvalidToTheCodeOwner = 7
    }

    public enum PointsSourceStatus
    {
        New = 1,
        Confirmed = 100000000,
        Converted = 100000001,
        Canceled = 100000002
    }

    public enum PointsConsumingStatus
    {
        New = 1,
        Confirmed = 100000000,
        Canceled = 100000001
    }
    #endregion
    public enum RecieptVoucherPaymentPosting
    {
        INVPayment = 1,
        GuaranteeAmount = 2,
        AdvancePayment = 3,
        AdvDepreciation = 5,
        CreditNote = 6,
        Settlment = 7,
        TransferredBalance = 100000001,
        GuaranteesOfExitAndReturn = 100000000,
        LaborEscapedPenalty = 100000002,
        Others = 4,
        TransferOfSponsorship = 100000004,
        TravelInsurance = 100000003
    }
    public enum VoucherStatus
    {
        UnderPreparation = 279640005,
        WaitingForTheSupplyOfFinance = 279640004,
        WaitingForConfirmationOfFinancial = 279640001,
        PendingApprovalByTheDirectorOfFinance = 279640008,
        ConfirmedByDirectorOfFinance = 279640003,
        VoucherRejectedFromFinance = 279640007,
        Canceled = 279640009
    }
    public enum VoucherPrint
    {
        VoucherNotPrintedYet = 0,
        VoucherPrinted = 1
    }
    public enum InvoiceTypeEnum
    {
        ProjectInvoice = 100000000,
        PayrollOnly = 100000001
    }
    public enum WorkFlowInvoiceEnum
    {
        SendToPayroll = 1,
        SendToKeyAccount = 2,
        SendToFinanace = 3,
        SendToDuptyCEO = 4,
        AcceptedInvoice = 5,
        RejectInvoice = 6

    }
    public enum ProjectInvoiceSectorEnum
    {
        Corporate = 1,
        Enterprise = 2,
        Individual = 3,
        Hourly = 100000000
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.359")]
    public enum CrmEntityState
    {

        Active = 0,
        Inactive = 1,
    }
    public enum EmployeeAttachmentTyp
    {
        Contract = 1,
        Iqama = 2,
        Visa = 3,
        MedicalInsurance = 4,
        ATMCard = 5,
        DrivingLicense = 6
    }
    public enum TicketStatus
    {
        UnderPreparation = 1,
        WaitingApproval = 100000007,
        SendToResponsible = 100000006,
        SendToResponsibe2 = 100000008,
        SendToResponsibe3 = 100000010,
        DoneWaitingConfirmation = 100000000,
        Done = 100000003,
        Rejected = 100000009,
        ClosedByClient = 100000011,
        Cancelled = 100000012
    }
    public enum InvoiceStatus
    {

        UnderPreparation = 1,
        SendToFinance = 279640000,
        UnderFinanceReview = 279640001,
        ReviewedByFinance = 279640005,
        SendToCustomerToApprove = 279640002,
        IsPartiallyCollected = 279640003,
        IsFullyCollected = 279640004,
        SendToProjectSupervisor = 279640006,
        Cancelled = 279640007

    }
    public enum WhoWillPayProjectInvoice
    {
        RecruitmentCompany = 1,
        Customer = 2,
        ActiveUserNotActiveCustomer = 3,
        PartByPart = 4
    }
    public enum TicketType
    {
        Group = 1,
        Item = 2
    }
    public enum CostInvoiceType
    {
        CostInvoice = 5,
        FinancialClaim = 1,
        FinalExit = 100000000,
        InvoiceRemaining = 2,
        DebitNote = 3,
        Gurantee = 4,
        IssuanceofVisas = 100000001,
        VacationAllowance = 100000002,
        Other = 6
    }

    public enum CollectionsPaymentMethod
    {
        STCPay = 100000005,
        HyperPay = 9,
        Network = 6,
        AlRajhiNetwork = 100000000,
        LocalBanksNetwork = 100000001,
        Visa = 100000002,
        MasterCard = 100000003,
        CashMoney = 3,
        Check = 1,
        Banktransfer = 2,
        RemainingBalanceOfClient = 7,
        DiscountToCustomer = 279640000,
        Pay = 4,
        BankGuarantee = 5,
        SalaryClient = 279640001,
        CreditNote = 8,
        Other = 279640003,
        SalariesPaidByClient = 279640002,
        InsuranceTransfered = 100000004,
        TransferToWallet = 100000006
    }
    public enum EmployeePickSourceEnum
    {
        Company = 1,
        Website = 2,
        Housing = 3
    }
    public enum InsurancePaymentMethod
    {
        Insurance = 1,
        Sanad = 2
    }
    public enum IndividualPricingType
    {
        Month = 1,
        TwoMonths = 2,
        ThreeMonths = 3,
        FourMonths = 4,
        FiveMonths = 5,
        SixMonths = 6,
        Year = 12,
        EighteenMonths = 17,
        TwoYears = 24,
        NineMonths = 9,
        ThirteenMonths = 13,
        SixteenMonths = 15,
        NineteenMonths = 18,
        TwentyOneMonths = 20

    }
    public enum DisplayPricingFor
    {
        Web = 1,
        Mobile = 2,
        WebAndMobile = 3,
        CRMNewPortal = 4,
        All = 5
    }
    public enum ApplyToOrDisplayFor
    {
        Web = 1,
        Mobile = 2,
        WebAndMobile = 3,
        CRMNewPortal = 4,
        All = 5
    }
    public enum CandidateSkillRating
    {
        Poor = 100000000,
        Good = 100000002,
        Medium = 100000001
    }
    public enum WorkSector
    {
        governmental = 1,
        PrivateSector = 2
    }

    public enum HowToRecieveWorker
    {
        Delivery = 1,
        FromBranch = 2
    }
    public enum PromotionOfferType
    {
        Discount = 1,
        HourPrice = 2,
        FreeVisits = 3,
        None=4
    
    }
    public enum IndividualContractProcedureStatus
    {
        New = 1,
        RequestCompleted = 100000005,
        RequestRejected = 100000006,
        WaitingNewContractStartDate = 100000010,
    }
    public enum RecieveWorkerType
    {
        DeliveryOnly = 0,
        FromHousingOnly = 1,
        DeliveryAndFromHousing = 2
    }
    public enum CancelStatus
    {
        UserCancelledTheContractFromCRM = 100000001,
        ClientCancelledTheContractFromWeb = 100000002,
        ClientCancelledTheContractFromMobile = 100000003,
        ClientDidnotPayBeforeTimeout = 100000004
    }
    public enum HourlyAppointmentStatus
    {
        New = 0,
        Start = 1,
        FinishedDone = 2,
        FinishedCustomerNotReply = 3,
        Postponded = 4,
        Reparation = 5,
        FinishedWrongLocation = 6,
        NotFinishedInternalProblem = 7,
        AmountWasRefunded = 8,
        RefundsInProgress = 9,
        FinishedNoWomanFound = 10,
        FinishedRefuseToRecieve = 11,
        FreeVisitCancelled = 12,
        Arrived = 13,
        ArriveToDeliver = 14,
        DedicatedVisit = 17,
    }
    public enum ReservedStatus
    {
        Available = 0,
        Reserved = 1
    }

    public enum PaymentPosting
    {
        AdvancedPayment = 3
    }
    public enum CollectionCreated
    {
        No = 1,
        Yes = 2
    }


    public enum LoyaltyCustomerAction_PointBy
    {
        FixedPoints = 100000000,
        VisitAmountRatio = 100000001,
        GiFtPoint = 100000002
    }
    public enum ExcSettingsType
    {
        Number = 1,
        Date = 2,
        String = 3,
        Bool = 4
    }
    public enum ContactProcedureLoggerType
    {
        LoyaltyOwner = 100000000,
        ChangeLocation = 100000001,
        ChangeResourceGroup = 100000002,
        ChangeVisitShiftEvening = 100000003,
        VisitFinish = 100000004,
        FixEmployeeOnVisit = 100000005,
        PostpondVisit = 100000006,
        Register = 100000007,
        Contract = 100000008,
        ChangeVisitShiftMorning = 100000009,
        PostpondAndChangeVisitShiftEvening = 100000010,
        PostpondAndChangeVisitShiftMorning = 100000011,
        UpgradePackage = 100000013,
        GiFtPoint = 100000012
    }
    public enum CollectionsPaymentType
    {
        TransferToWallet = 100000006,
        INVPayment = 1,
        GuaranteeAmount = 2,
        AdvancePayment = 3,
        AdvDepreciation = 5,
        CreditNote = 6,
        SponsorshipTransfer = 100000004,
        Settlment = 7,
        TransferredBalance = 100000001,
        GuaranteesOfExitAndReturn = 100000000,
        LaborEscapedPenalty = 100000002,
        TravelInsurance = 100000003,
        Others = 4
    }
    public enum PointGiftType
    {
        Visit = 100000000,
        Point = 100000001

    }

    public enum GiftStatus
    {
        New = 1,
        DedicatedSent = 3,
        WithDrawn = 2,
        Cancel = 4,
    }
    public enum LoyaltyCustomerActionsSetting_Active
    {
        Yes = 1,
        No = 0
    }
    public enum ProjectTimeSheetStatus
    {
        Active = 1,
        SubmittedWaitingKeyAccountApproval = 100000000,
        WaitingOperationManager = 100000001,
        ApprovedSendToPayroll = 100000002,
        PayrollInvoiceStarted = 100000004,
        Rejected = 100000003,
        SubmittedWaitingINDVManagerApproval = 100000006,
        SendToIndividualSectorResponsible = 100000007,
        SendToBusinessSectorResponsible = 100000008,
    }

    public enum FinancialRequestBillStatus
    {
        BillNew = 0 ,
        BillUpdated = 1 ,
        BillExpired = 2 ,
        BillCreate = 3
    }

    public enum FinancialRequestStatus
    {
        New = 1,
        SmsSent = 2,
        paid = 3 ,
        Cancelled = 4,
        PaymentIsPendingConfirmation = 5
    }

    public enum FinancialRequestType
    {
        Contract = 1,
        VatClaim = 4,
        Installment = 6
    }
    public enum SearchByType

    {
        FreeText=1,
        DropDownList=2,
    }

    public enum SortType
    {
        Ascending =0,
        Descending=1
    }

    public enum ServiceType
    {
        Individual =1,
        Hourly=2,
        Renew=3
    }

    public enum ContactLocationType
    {
        Main=1,
        Sub=2
    }

    public enum DynamicStepType
    {
        Stratigy=1,
        PageUi=2,
        PostAction=3
    }
    public enum AvailableForRenew
    {
        Yes=1,
        SamePackage= 2

    }
    public enum AvailableForNew
    {
        Yes=1,
        No=2
    }

    public enum RenewOption
    {
        SameContract = 1,
        NewContract = 2
    }
    public enum FlexContractStatus
    {
        Confirmed = 100000009,
        ConfirmedNotPaid = 100000006,
        Cancelled = 100000007,
        Finished = 100000000,
        PaymentIsPendingConfirmation = 100000008,
        ConfirmedAndPaymentWasMade = 100000002
    }

    public enum PricingType
    {
        Weekly = 11111
    }

    public enum ContractRestrict
    {
        OnlyOneUnpaidContractForSameService = 1,
        OnlyOneUnpaidContractForAnyService=2,
        UnResrtricted=3
    }
    public enum DisplayCitiesForService
    {
        All = 1,
        onlyServiceCities = 2,
        AvailableForHourly = 3

    }

    public enum DisplayDistrictForService
    {
        All = 1,
        OnlyServiceDistricts = 2,
    }


    public enum ChangedAttributesforSelectedHourlyPricing
    {
        //same name in ChangedAttributesWithSelectedHourlyPricing setting and SelectedHourlyPricingVm model
        EmployeeNumber = 1,
        HoursNumber= 2,
        VisitShift = 3,
        WeeklyVisits = 4,
        ContractDuration=5,
        ResourceGroupId=6
    }


    public enum VisitType
    {
        Ordinary=1,
        Gift=2,
        Vacation=3
    }

    public enum EvaluationBy
    {
        Customer = 100000000,
        Labor = 100000001
    }

}