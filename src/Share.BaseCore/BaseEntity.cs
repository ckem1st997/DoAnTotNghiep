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

        public bool OnDelete { get; set; }

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
