﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Models.CRM;

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

/// <summary>
/// Represents a source of entities bound to a CRM service. It tracks and manages changes made to the retrieved entities.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.359")]
public partial class CrmContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
{

    /// <summary>
    /// Constructor.
    /// </summary>
    public CrmContext(Microsoft.Xrm.Sdk.IOrganizationService service) :
            base(service)
    {
    }
    public System.Linq.IQueryable<ExcSettings> ExcSettingsSet
    {
        get
        {
            return this.CreateQuery<ExcSettings>();
        }
    }

}
