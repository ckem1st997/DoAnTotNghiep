using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore
{
    public class BaseEntity : BaseEntityGeneric<string>
    {
        //public BaseEntity()
        //{
        //    Id = Guid.NewGuid().ToString();
        //    SecurityStamp = Guid.NewGuid().ToString();
        //}
       
        public bool OnDelete { get; set; }


        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
     //   public virtual string? SecurityStamp { get; set; }

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store
        /// </summary>
     //   public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();


        //private List<INotification> _domainEvents;
        //public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        //public void AddDomainEvent(INotification eventItem)
        //{
        //    _domainEvents = _domainEvents ?? new List<INotification>();
        //    _domainEvents.Add(eventItem);
        //}

        //public void RemoveDomainEvent(INotification eventItem)
        //{
        //    _domainEvents?.Remove(eventItem);
        //}

        //public void ClearDomainEvents()
        //{
        //    _domainEvents?.Clear();
        //}


    }
}
