using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AutoMapper;
using Models.Labor;

namespace Utilities.Mappers
{
    public static class ToViewModelMapper
    {
        #region CRM Entity


        /// <summary>
        /// Convert the CRM Entity to class ViewModel
        /// </summary>
        /// <typeparam name="T">class ViewModel</typeparam>
        /// <param name="model">CRM Entity You need to convert</param>
        /// <returns></returns>
        public static T Toclass<T>(this Entity model)
            where T : class
        {
            return Mapper.Map<Entity,T>(model);
        }
        public static IEnumerable<T> ToModelListData<T>(this EntityCollection model)
            where T : class
        {
            return Mapper.Map<EntityCollection,IEnumerable<T>>(model);
        }
         public static IEnumerable<T> ToModelListData<T>(this IEnumerable<Entity> model)
            where T : class
        {
            return Mapper.Map<IEnumerable<Entity>,IEnumerable<T>>(model);
        }
        public static IEnumerable<T> ToModelListData<T, T1>(this IEnumerable<T1> model)
            where T : class
            where T1 : Entity
        {
            return Mapper.Map<IEnumerable<T1>, IEnumerable<T>>(model);
        }
        #endregion

        #region DataTable 

        /// <summary>
        /// Convert the DataRow to class ViewModel
        /// </summary>
        /// <typeparam name="T">class ViewModel</typeparam>
        /// <param name="dataRow">DataRow You need to convert</param>
        /// <returns></returns>
        public static T Toclass<T>(this DataRow dataRow)
            where T : class
        {
            return Mapper.Map<DataRow,T>(dataRow);
        }


        /// <summary>
        /// Convert the DataTable to list of class ViewModel
        /// </summary>
        /// <typeparam name="T">list of ViewModel</typeparam>
        /// <param name="dataTable">DataTable You need to convert</param>
        /// <returns></returns>
        public static IEnumerable<T> ToclassList<T>(this DataTable dataTable)
            where T : class
        {
            return dataTable.AsEnumerable().Select(row => row.Toclass<T>());
        }

        #endregion

        #region Labor Entity
        /// <summary>
        /// Convert the IEntityBase to class ViewModel
        /// </summary>
        /// <typeparam name="T">class ViewModel</typeparam>
        /// <typeparam name="T1">IEntityBase</typeparam>
        /// <param name="model">IEntityBase You need to convert</param>
        /// <returns></returns>
        public static T Toclass<T, T1>(this T1 model)
            where T : class
            where T1 : IEntityBase
        {
            return Mapper.Map<T1,T>(model);
        }
       
        /// <summary>
        /// Convert the IEntityBase List to class ViewModel List
        /// </summary>
        /// <typeparam name="T">class ViewModel</typeparam>
        /// <typeparam name="T1">IEntityBase</typeparam>
        /// <param name="model">IEntityBase List You need to convert</param>
        /// <returns></returns>

        public static IEnumerable<T> ToclassList<T, T1>(this IEnumerable<T1> model)
           where T : class
           where T1 : IEntityBase
        {

            return model.Select(obj => obj.Toclass<T,T1>());

        }
        
        /// <summary>
        /// Convert the Identity to class ViewModel
        /// </summary>
        /// <typeparam name="T">class ViewModel</typeparam>
        /// <typeparam name="T1">Identity</typeparam>
        /// <param name="model">Identity You need to convert</param>
        /// <returns></returns>

        public static T1 ToApplicationUserVModel<T, T1>(this T model)
           where T1 : class
           where T : ApplicationUser
        {
            return Mapper.Map<T, T1>(model);
        }
        #endregion

        public static T FromVmToVm<T, T1>(this T1 model)
           where T : class
           where T1 : class
        {
            return Mapper.Map<T1, T>(model);
        }

        public static IEnumerable<T> FromVmtoVmList<T, T1>(this List<T1> model)
           where T : class
           where T1 : class
        {

            return model.Select(obj => obj.FromVmToVm<T, T1>());

        }

    }
}
