using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.Mappers
{
    public static class MapperConfig
    {
        public static string lang { get; set; }
        static IEnumerable<Type> _profiles { get; set; }
        public static void ConfigureMapping(IEnumerable<Type> Profiles)
        {
            _profiles = Profiles;
            Mapper.Initialize(cfg =>
            {

                foreach (var profile in _profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

        }
    }
}
