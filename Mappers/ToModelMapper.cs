using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Mappers
{
    public static class ToModelMapper
    {

        #region CRM Entity
        /// <summary>
        /// Convert the ModelData to CRM Entity
        /// </summary>
        /// <param name="model">ModelData You need to convert</param>
        /// <returns></returns>
        public static T ToCrmEntity<T,T1>(this T1 viewModel)
            where T1 : class
            where T: Entity
        {
            return Mapper.Map<T1,T>(viewModel);
        }
        public static IEnumerable<T> ToCrmEntityList<T,T1>(this IEnumerable<T1> viewModel)
            where T1 : class
            where T: Entity

        {
            return Mapper.Map<IEnumerable<T1>, IEnumerable<T>>(viewModel);
        }


        /// <summary>
        /// Convert the ModelData to Labor IEntityBase
        /// </summary>
        /// <typeparam name="T">IEntityBase</typeparam>
        /// <typeparam name="T1">ModelData View Model</typeparam>
        /// <param name="model">ModelData You need to convert</param>
        /// <returns></returns>
        public static T ToIEntityBase<T, T1>(this T1 viewModel)
            where T : IEntityBase
            where T1 : class
        {
            return Mapper.Map<T>(viewModel);
        }


        #endregion



    }
}
