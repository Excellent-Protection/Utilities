using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappers.Resolvers
{
    public class RelatedEntityToListResolver<T> : IMemberValueResolver<Entity, object, RelatedEntityCollection, IEnumerable<T>> where T : Entity
    {
        private string _relationshipName;
        public RelatedEntityToListResolver(string relationShipName)
        {
            _relationshipName = relationShipName;
        }
        public IEnumerable<T> Resolve(Entity source, object destination, RelatedEntityCollection sourceMember, IEnumerable<T> destMember, ResolutionContext context)
        {

            return sourceMember.Keys.Contains(new Relationship(_relationshipName)) ? sourceMember[new Relationship(_relationshipName)].Entities.Select(z => z.ToEntity<T>()) : null;

        }
    }
    public class RelatedEntityToListVmResolver<T, T1> : IMemberValueResolver<Entity, object, RelatedEntityCollection, IEnumerable<T1>> where T : Entity where T1 : class
    {
        private string _relationshipName;
        public RelatedEntityToListVmResolver(string relationShipName)
        {
            _relationshipName = relationShipName;
        }
        public IEnumerable<T1> Resolve(Entity source, object destination, RelatedEntityCollection sourceMember, IEnumerable<T1> destMember, ResolutionContext context)
        {

            return sourceMember.Keys.Contains(new Relationship(_relationshipName)) ? sourceMember[new Relationship(_relationshipName)].Entities.Select(z => z.ToEntity<T>()).ToModelListData<T1, T>().ToList() : null;

        }
    }
}
