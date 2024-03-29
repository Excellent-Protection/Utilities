﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.Defaults
{
    public static class DefaultValues
    {
        public const UserLanguage Language = UserLanguage.Arabic;
        public const string DefaultLanguageRoute = "ar";
        public const string RouteLang_ar = "ar";
        public const string RouteLang_en = "en";
        public const RecordSource Source = RecordSource.Web;
        public const string ServiceContractPerHour_DefaultPromotionCode = "no-promotion";
        public const string Nationalitiy_individualFilterField = "new_isindv";
        public const string AdminRoleName = "Admin";
        public const string UserRoleName = "User";
        public const string MobilePhoneRex = @"^(05)(5|0|3|6|4|9|1|8|7)([0-9]{7})$";
        public const string NameRex = @"(\s*[A-Za-z\u0600-\u06FF]{2,}(\s+[A-Za-z\u0600-\u06FF]{2,})*\s*)";
        public const string EmailRex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string PasswordRex = @".*[a-zA-Z0-9\S\u0600-\u06FF]";
        public const string PrivateDriverId = "49C7F260-292F-E311-B3FD-00155D010303";
        public const string CITContractTypeID = "E71F836B-0A7D-E711-80CF-000D3AB61E51";
        public const string CreateIndivudalContractFromRequestWorkflowId = "62480430-CDD6-4DEC-860F-052D1B300995";
        public const string SaudiNationalityId = "1e0ff838-292f-e311-b3fd-00155d010303";
        public const string HourlyServiceProjectId = "f11e1049-3874-e711-80cf-000d3ab61e51";
        public const string ServiceDefaultIconUrl = "/icon-x_637328467621391593.png";
        public const string SADAD_InvoiceCode = "214";
        public const string AppleCardBrand = "APPLEPAY";
        public const string TomorrowVisitSms = "TomorrowVisitSms";
        public const string AllCountriesId = "c9da5d56-a54a-e311-8887-00155d010303";
        public const string BlockCountryId = "30ce18ed-4afe-e611-80d5-0050568411f9";
        public const string OnlineWebUrl = "OnlineWebUrl";
       


        public const string HourContractShifts_GetShiftsDataForContractSP = "dbo.HourContractShifts_GetShiftsDataForContract"; 
        public const int PostPonddaysAllowedAfterContractEnd = 30;
        public const string FixedEmployeeShiftOffsetHoursSettingName = "FixedEmployeeShiftOffsetHours";
        public const int FixedEmployeeShiftOffsetHours = 8;
        public const string FixEmpOnVisitsSettingName= "FixEmployeeOnVisits";
        public const string BlockEmpOnVisitsSettingName= "BlockEmployee";
        public const bool FixEmpOnVisits = true;
        public const bool BlockEmpOnVisits = true;
        public const string PostPondVisitSettingName= "PostPondVisit";
        public const bool PostPondVisit= false;
        public const bool PostPondVisitByShift= false;
        public const string PostPondVisitByShiftSettingName= "PostPondVisitByShift";
        public const string IsEditContractLocationAvailableSettingName = "IsEditContractLocationAvailable";
        public const bool IsEditContractLocationAvailable =false;
        public const string IsEditResourceGroupAvailableSettingName = "IsEditResourceGroupAvailable";
        public const bool IsEditResourceGroupAvailable = false;
        public const string PostPondVisitsFromOffsetSettingName = "PostPondVisitFromOffSet";
        public const string SelectProfessionsFromPackagesName = "SelectProfessionsFromPackages";
        public const string SelectNationalitiesFromPackagesName = "SelectNationalitiesFromPackages";
        public const string IsTwoFactorAuthenticationAvailableName = "IsTwoFactorAuthAvailable";
        public const string IsOneFactorAuthonticationSettingName = "IsOneFactorAuthontication";
        public const string DealingWithMainAddressName = "DealingWithMainAddress";
        public const string PackagePropertiesSettingName = "PackageProperties";
        public const string RenewPackagePropertiesSettingName = "RenewPackageProperties";
        
        public const string SelectedPackagePropertiesSettingName = "SelectedPackageProperties";
        public const string DefaultSelectedPackagePropertiesSettingName = "DefaultSelectedPackageProperties";
        public const string HourlyPackagePropertiesSettingName = "HourlyPackageProperties";

        public const string ContactDetailsPackagePropertiesSettingName = "ContactDetailsPackageProperties";
        public const string RequestDetailsPackagePropertiesSettingName = "RequestDetailsPackageProperties";
        public const string DaysBeforeEndContractToShowRenewBtnSettingName = "DaysBeforeEndContractToShowRenewBtn";
        public const string IncludInsuranceWithRenewSettingName = "IncludInsuranceWithRenew";
        public const string IsRenewAvailableSettingName = "IsRenewAvailable";
        public const string ProceduresReventRenewSettingname = "ProceduresReventRenew";
        public const string SupportEmailSettingName = "SupportEmail";
        public const string IndividualRequestDocumentCodeSettingName = "IndividualRequestDocumentCode";
        public const string HourlycontractDocumentCodeSettingName = "HourlycontractDocumentCode";
        public const string RenewOptionSettingName = "RenewOption";
        public const string PaymentAvailableSettingName = "IsPaymentAvailable";
        public const string QcTeamEmailSettingName = "QcTeamEmail";
        public const string SystemVersionSettingName = "SystemVersion";
        public const string IndividualRequestPaymentTimeSettingName = "IndividualRequestPaymentTime";
        public const string HourlyRequestPaymentTimeSettingName = "HourlyRequestPaymentTime";
        public const string FlexPaymentTimeSettingName = "FlexPaymentTime";
        public const string whatsappNumberSettingName = "whatsappNumber";

        public const string AutoFillCodeSettingName = "AutoFillCode";
        public const string ResetPasswordLinkSettingName = "ResetPasswordLink";
        public const string IndvPaymentAvailableSettingName = "IsIndvPaymentAvailable";

        //Social Media Setting Name
        public const string FacebookLinkSettingName = "FacebookLink";
        public const string YouTubeLinkSettingName = "YouTubeLink";
        public const string LinkedInLinkSettingName = "LinkedInLink";
        public const string TwitterLinkSettingName = "TwitterLink";
        public const string InstagramLinkSettingName = "InstagramLink";
        public const string CompanyNumberSettingName = "companymobilenumber";
        public const string WebsiteSettingName = "CompanyDomain";
        //Enable Evaluation Edit
        public const string EnableEvaluationEditSettingName = "EnableEditEvaluation";

        public const string IsDeliveryEmployeeAvailableSettingName = "IsDeliveryEmployeeAvailableSetting";
        public const string IsSelectEmployeeFromHouseBuildingAvailableSettingName = "IsSelectEmployeeFromHouseBuildingAvailableSetting";

        public const string ShowIndividualEvaluationButtonSettingName = "ShowIndividualEvaluationButton";

        public static readonly Dictionary<string, int> PostPondVisitsFromOffset = new Dictionary<string, int>() {
            { "PostPondVisitFromOffSetMorning",  -2},
            { "PostPondVisitFromOffSetEvening", 8},
            { "PostPondVisitFromOffSetFullDay",  -2}
        };
        public const string HourlyServicesOffsetSettingName = "HourlyServicesOffset";

        public static readonly Dictionary<string, int> HourlyServicesOffset = new Dictionary<string, int>() {
            { "HourlyServicesOffsetMorning",  -2},
            { "HourlyServicesOffsetEvening", 8},
            { "HourlyServicesOffsetFullDay",  -4}
        };
        public static readonly Dictionary<string, int> ServiceDefaultValues = new Dictionary<string, int>() {
            { "MorningStartTime", 8},
            { "EveningStartTime", 16},
            { "FullDayStartTime",  8}
        };

        public const bool EnableEvaluationEdit = true;
        public const bool SelectProfessionsFromPackages = true;
        public const bool SelectNationalitiesFromPackages = true;
        public const bool IsTwoFactorAuthenticationAvailable = false;
        public const bool IsOneFactorAuthonticationAvailable = false;
        public const bool IsDeliveryEmployeeAvailableSetting = false;
        public const bool IsSelectEmployeeFromHouseBuildingAvailableSetting = false;
        public const bool  DealingWithMainAddress = false;
        public const string WebSettingResult = "";
        public const string MobileSettingResult = "";
        public const int DaysBeforeEndContractToShowRenewBtn = 7;
        public const bool IncludInsuranceWithRenew = false;
        public const bool IsRenewAvailable = false;
        public const bool IndividualSectorEnabled = false;
        public const bool HourlySectorEnabled = false;
        public const bool BusinessSectorEnabled = false;
        public const string ProceduresReventRenew = "1,2,4";
        public const string SupportEmail = "e.shahin@excp.sa";
        public const string IndividualRequestDocumentCode = "SD-11111";
        public const string HourlyContractDocumentCode = "HourlyContractTemplate";
        public const string whatsappNumber = "+9668001242420";

        public const string PackageProperties = "{'professionGroupName':{ 'type':'string'}, 'packagePrice':{ 'type':'currency'},'discount':{ 'type':'currency'},'packagePriceAfterDiscount':{ 'type':'currency'} , 'activationAmount':{ 'type':'currency'},'advancedAmount':{ 'type':'currency'},'vatAmountOfFinalPrice':{ 'type':'currency'},'amountInsurance':{ 'type':'currency'},'finalPriceToPay':{ 'type':'currency'},'packageDisplayName':{ 'type':'string'} }";
        public const string ContactDetailsPackageProperties = "{'packageDisplayName': {'type': 'string'},'finalPriceToPay': {'type': 'currency'},'discount': {'type': 'currency'},'packagePriceAfterDiscount': {'type': 'currency'},'activationAmount':{'type': 'currency'},'advancedAmount': {'type': 'currency'},'vatAmountOfFinalPrice': {'type': 'currency'},'packagePrice': {'type': 'currency'   },'professionGroupName':{'type': 'string'},'amountInsurance': {'type': 'currency'}}";
        public const string RequestDetailsPackageProperties = "{'packageDisplayName': {'type': 'string'},'finalPriceToPay': {'type': 'currency'},'discount': {'type': 'currency'},'packagePriceAfterDiscount': {'type': 'currency'},'activationAmount':{'type': 'currency'},'advancedAmount': {'type': 'currency'},'vatAmountOfFinalPrice': {'type': 'currency'},'packagePrice': {'type': 'currency'   },'professionGroupName':{'type': 'string'},'amountInsurance': {'type': 'currency'}}";
        public const string RenewPackageProperties = "{'professionGroupName':{ 'type':'string'}, 'packagePrice':{ 'type':'currency'},'discount':{ 'type':'currency'},'packagePriceAfterDiscount':{ 'type':'currency'} , 'activationAmount':{ 'type':'currency'},'advancedAmount':{ 'type':'currency'},'vatAmountOfFinalPrice':{ 'type':'currency'},'amountInsurance':{ 'type':'currency'},'finalPriceToPay':{ 'type':'currency'},'packageDisplayName':{ 'type':'string'} }";
        public const int RenewOptionValue = 2;//new contract
        public const string QcTeamEmail = "e.shahin@excp.sa,";

        public const string SelectedPackageProperties = "{'professionGroupName':{ 'type':'string'}, 'packagePrice':{ 'type':'currency'},'discount':{ 'type':'currency'},'packagePriceAfterDiscount':{ 'type':'currency'} , 'activationAmount':{ 'type':'currency'},'advancedAmount':{ 'type':'currency'},'vatAmountOfFinalPrice':{ 'type':'currency'},'amountInsurance':{ 'type':'currency'},'finalPriceToPay':{ 'type':'currency'},'packageDisplayName':{ 'type':'string'} }";
        public const string HourlyPackageProperties = "{'professionGroupName':{ 'type':'string'}, 'packagePrice':{ 'type':'currency'},'discount':{ 'type':'currency'},'packagePriceAfterDiscount':{ 'type':'currency'} , 'activationAmount':{ 'type':'currency'},'advancedAmount':{ 'type':'currency'},'vatAmountOfFinalPrice':{ 'type':'currency'},'amountInsurance':{ 'type':'currency'},'finalPriceToPay':{ 'type':'currency'},'packageDisplayName':{ 'type':'string'} }";

        public const int SystemVersion = 2; //new version


        public static readonly string[] DateFormat = { "dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd","MM/dd/yyyy","M/d/yyyy","dd/MM/yyyy hh:mm:ss","d/M/yyyy h:m:s" , "M/d/yyyy h:m:s tt" }; 
        public static readonly HourlyAppointmentStatus[] PreviousVisitStatus = { HourlyAppointmentStatus.FinishedDone,HourlyAppointmentStatus.FinishedCustomerNotReply,HourlyAppointmentStatus.FinishedNoWomanFound,HourlyAppointmentStatus.FinishedRefuseToRecieve,HourlyAppointmentStatus.FinishedWrongLocation,HourlyAppointmentStatus.AmountWasRefunded };       
        public const string SuccessErrorMessageResource = "SuccessErrorMessageResource";
        public const string ShowCustomerWalletSettingName = "ShowCustomerWallet";
        public const bool ShowCustomerWalletSetting = false;
        public const bool IsPaymentAvailable = false;

        public const bool IsShowIndividualEvaluationButton = false;

        public const bool IndvPaymentAvailable = false;

        public const decimal paymnetTimeDefault = 120;

        public const bool TamayousSystemAvailable = false;
        public const bool CoronaQuestionnaire = false;
        public const string CoronaQuestionnaireQuestions = "[]";
        public const string CoronaQuestionnaireQuestionsAr = "[]";
        public const string ShowTamayousSetting = "TamayousPayment";
        #region LoyaltySettigs

        // =========== Setting Names ===============
        public const string Loyalty_PointsLifeTimeSettingName = "Loyalty_PointsLifeTime";
        public const string Loyalty_PointsProgressFilterStepSettingName = "Loyalty_PointsProgressFilterStep";
        public const string Loyalty_VisitsFilteredViewIdForCustomerLevelSettingName = "Loyalty_VisitsFilteredViewIdForCustomerLevel";
        public const string Loyalty_VisitsFilteredViewIdForFinishVisitSettingName = "Loyalty_VisitsFilteredViewIdForFinishVisit";
        public const string MinLoyalitytPointPaymentSettingName = "MinLoyalitytPointPayment";
        public const string FactorConvertPointSettingName = "FactorConvertPoint";
        public const string CustomerWalletPaymentSettingName = "CustomerWalletPayment";
        public const string ShowLoyalitySettingName = "ShowLoyality";
        public const string LoyalityPaymentSettingName = "LoyalityPayment";
        public const string DedicateVisitSettingName = "DedicateVisit";
        public const string DedicatePointSettingName = "DedicatePoint";
        public const string DedicateVisitOffsetSettingName = "DedicateVisitOffset";
        public const string MinimumPointtodedicateSettingName = "MinimumPointtodedicate";
        public const string AllowPointPaymentWithTamayouzName = "AllowPointPaymentWithTamayouz";
        public const string AllowPointPaymnetWithWalletName = "AllowPointPaymnetWithWallet";
        public const string AllowTamayouzPaymentWithPointsName = "AllowTamayouzPaymentWithPoints";
        public const string AllowTamayouzPaymentWithWalletName = "AllowTamayouzPaymentWithWallet";
        public const string AllowWalletPaymentWithPointsName = "AllowWalletPaymentWithPoints";
        public const string AllowWalletPaymentWithTamayouzName = "AllowWalletPaymentWithTamayouz";

        // =========== Setting Values ============================
        public const string LoyaltyVisitsFilteredViewIdForCustomerLevel = "B84DC0B6-6138-EB11-A81B-0022489A82DC"; //SYSTEM: Loyalty Filtered View
        public const string LoyaltyVisitsFilteredViewIdForFinishVisit = "B84DC0B6-6138-EB11-A81B-0022489A82DC"; //SYSTEM: Loyalty Filtered View
        public const double Loyalty_PointsLifeTime = 365;
        public const int Loyalty_PointsProgressFilterStep = 1;
        public const decimal MinLoyalityPointPayment = 50;
        public const decimal FactorConvertPointSetting = 0.1M;
        public const bool CustomerWalletPaymentSetting = false;
        public const char LoyalityPromotionSplitCharacter = '@';
        public const bool ShowLoyalitySetting = false;
        public const bool LoyalityPaymentSetting = false;
        public const bool DedicateVisit = false;
        public const bool DedicatePoint = false;
        public const int DedicateVisitOffset= -24;
        public const int MinimumPointtodedicate = 10;
        public const bool AllowPointPaymentWithTamayouz = false;
        public const bool AllowPointPaymnetWithWallet = false;
        public const bool AllowTamayouzPaymentWithPoints = false;
        public const bool AllowTamayouzPaymentWithWallet = false;
        public const bool AllowWalletPaymentWithPoints = false;
        public const bool AllowWalletPaymentWithTamayouz = false;


        #endregion


        #region Tamayouze APi Links 
        //------------------  Tamayouze Api Links Setting Name
        public const string TamayouzeAuthAPILink = "TamayouzeAuthAPILink";
        public const string TamayouzeOtpAPILink = "TamayouzeOtpAPILink";
        public const string TamayouzeDiscountAPILink = "TamayouzeDiscountAPILink";

        //------------------  Tamayouze Api Links Setting Default Values 
        public const string TamayouzeAuthAPILinkValue = "http://78.93.37.242:9953/authentication";
        public const string TamayouzeOtpAPILinkValue = "http://78.93.37.242:9953/tamayouz/otp";
        public const string TamayouzeDiscountAPILinkValue = "http://78.93.37.242:9953/tamayouz/discount-by-items";
        //-------------Settings Name -----------------
        public const string TamayouzeAccountIdentityWebName = "TamayouzeAccountIdentityWeb";
        public const string TamayouzePasswordWebName = "TamayouzePasswordWeb";

        public const string TamayouzeAccountIdentityAndroidName = "TamayouzeAccountIdentityAndroid";
        public const string TamayouzePasswordAndroidName = "TamayouzePasswordAndroid";

        public const string TamayouzeAccountIdentityIosName = "TamayouzeAccountIdentityIos";
        public const string TamayouzePasswordIosName = "TamayouzePasswordIos";
        public const string TamayouzeCategoryIdName = "TamayouzeCategoryId";
        public const string TamayouzeAPIVersionName = "TamayouzeAPIVersionName";



        //--------------Settings Values ----------------
        public const string TamayouzeAccountIdentityWeb = "11870001";
        public const string TamayouzePasswordWeb = "12345678";

        public const string TamayouzeAccountIdentityAndroid = "11870002";
        public const string TamayouzePasswordAndroid = "12345678";

        public const string TamayouzeAccountIdentityIos = "11870003";
        public const string TamayouzePasswordIos = "12345678";
        public const int TamayouzeCategoryId = 1;
        public const string TamayouzeAPIVersion = "v1";


        #endregion

        public const string BSAdminRoleName = "ProjectsAdmin";
        public const int TokenLifespan =5;
        public const string IsLaborStockAvailable = "IsLaborStockAvailable";


        public const string OnlinePaymentUrl = "/#/payment?contractId=@id&type=@type";
        
        public static readonly string OnlinePortalUrl = ConfigurationManager.AppSettings["OnlinePortalUrl"].ToString();
        public static readonly string OnlineVisitsUrl = ConfigurationManager.AppSettings["OnlineVisitsUrl"].ToString();
        #region CompleteProfileSetting
        public const string CompleteProfileRequiredFieldsSettingName = "CompleteProfileRequiredFields";
        public const string ContactDetailsFieldsSettingName = "ContactDetailsFields";

        public static string[] completeProfileFieldsDefaultValues = new[] { "new_gender", "new_idnumer", "new_contactnationality" };
        public static string[] ContactDetailsDefaultValues = new[] { "new_gender", "new_idnumer", "new_contactcity", "new_contactnationality", "emailaddress1", "jobtitle" };
        #endregion

        public const string RenewLink = "/#/dashboard/individual-contracts/renew-contract/@id";

        public const string ContractAmount = "new_contractamount";
        public const string ContractPeriod = "new_contractmonths";
        public const int FirstVisitExpiryAfter = 365;
        public const string ChangedAttributesSelectedHourlyPrice = "WeeklyVisits,ContractDuration,VisitShift,EmployeeNumber,ResourceGroupId";
        public const bool ShowOtherRequest = true;
        public const bool BankTransferAvailableForPaymentRequest= false;
        public const bool ActiveBankTransfer = false;
        public const string BankTransferShowStartTime = "";
        public const string BankTransferShowEndTime = "";
        public const string BankTransfareSelectedDays = "";
        public const bool CanCreateLead = true;


        public const bool EnableHourlyLead = true;
        public const bool SendOTPMailToUser = false;
    }
}
