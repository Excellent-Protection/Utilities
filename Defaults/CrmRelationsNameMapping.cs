using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Defaults
{
    public static class CrmRelationsNameMapping
    {
        public const string HourlyContract_RecieptVoucher = "new_new_hindvcontract_new_receiptvoucher_contracthourid";
        public const string HourlyContract_Contact = "new_contact_new_hindvcontract_HIndivClintname";
        public const string HourlyContract_Collection = "new_new_hindvcontract_new_payments_hourlycontract";
        public const string HourlyContract_Appointment = "new_new_hindvcontract_new_hourlyappointment_servicecontractperhour";
        public const string HourlyContract_Service = "new_new_service_new_hindvcontract_service";
        public const string HourlyContract_District = "new_new_district_new_hindvcontract_district";
        public const string HourlyContract_City = "new_new_city_new_hindvcontract_city";
        public const string HourlyContract_HourlyPrice = "new_new_hourlypricing_new_hindvcontract_houlrypricing";
        public const string HourlyContract_ResourceGroup = "new_new_resourcegroup_new_hindvcontract_resourcegroup";
        public const string IndividualContractProcedure_IndividualPricing = "new_new_indvprice_new_indvcontractclearance_indvpriceid";
        public const string IndividualContractProcedure_RecieptVoucher = "new_new_indvcontractclearance_new_receiptvoucher_indvcntrctclsid";
        public const string IndividualContractProcedure_Contact = "new_contact_new_indvcontractclearance_indvcustid";
        public const string IndividualContractProcedure_Collection = "new_new_indvcontractclearance_new_payments_indvprocedure";
        public const string Contact_Collection = "new_contact_new_payments_cutomer";
        public const string Contact_ContactPreviousLocation = "new_contact_new_contactpreviouslocation_contact";
        public const string Contactuser_LoyaltyPointSource = "new_contact_new_loyaltypointssource_loyaltyuser";
        public const string ContactOwner_LoyaltyPointSource = "new_contact_new_loyaltypointssource_loyaltyowner";
        public const string Contact_CustomerLoyaltyLevel = "new_contact_new_customerloyaltylevel_customer";
        public const string IndividualContractRequest_Employee = "new_new_employee_new_individualcontractrequest_employeeid";
        public const string IndividualContractRequest_IndividualPricing= "new_new_indvprice_new_individualcontractrequest_pricing";
        public const string IndividualContractRequest_RecieptVoucher= "new_new_individualcontractrequest_new_receiptvoucher_individualcontractrequest";
        public const string IndividualContractRequest_Contact= "new_contact_new_individualcontractrequest_contactid";
        public const string IndividualContractRequest_Collection= "new_new_individualcontractrequest_new_payments_individualcontractrequest";
        public const string IndividualContractPricing_City= "new_new_indvprice_new_city";
        public const string HousingBuilding_City= "new_mw_housing_new_city";
        public const string IndividualContract_Contact = "new_contact_new_indvcontract_customer";
        public const string IndividualContract_Profession = "new_new_profession_new_indvcontract_professionId";
        public const string IndividualContract_Country = "new_new_country_new_indvcontract_reqnationalityid";
        public const string IndividualContract_IndividualPricing = "new_new_indvprice_new_indvcontract_null";
        public const string IndividualContract_Collection = "new_new_indvcontract_new_payments_indcontract";
        public const string IndividualContract_ReceiptVoucher = "new_new_indvcontract_new_receiptvoucher_indv";
        public const string Nationality_AlternativeNationality = "new_new_country_new_country";
        public const string Service_ResourceGroupOneToMany = "new_new_service_new_resourcegroup_service";
        public const string Service_ResourceGroupOneToOne = "new_new_resourcegroup_new_service_resourcegroup";
        public const string Service_ExcSettings = "new_new_service_new_excsettings_service";
        public const string EvaluationCriteria_EvaluationPoint = "new_new_evaluationcriteria_new_evaluationpoint_evaluationcriteria";
        public const string Evaluation_CustomerEvaluationPoint = "new_new_evaluation_new_custevaluation_evaluation";
        public const string Car_District = "new_district_carresource";
        public const string FinanceRequest_Collection = "new_new_finaicalrequest_new_payments_finaicalrequest";
        public const string FinanceRequest_RecieptVoucher = "new_new_finaicalrequest_new_receiptvoucher_financialrequest";
        public const string FinanceRequest_Contact = "new_contact_new_finaicalrequest_Contact";
        public const string Profession_Employee = "new_new_profession_new_employee_professionId";
        public const string Nationality_Employee = "new_new_country_new_employee_nationalityId";

        public const string FlexContract_RecieptVoucher = "new_new_flexibleserviceperhour_new_receiptvoucher_flexibleserviceperhour";
        public const string FlexContract_Appointment = "new_new_flexibleserviceperhour_new_hourlyappointment_flexibleserviceperhour";
    }
}
