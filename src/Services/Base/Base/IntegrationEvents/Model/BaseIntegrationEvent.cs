using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Base.IntegrationEvents
{
    public class BaseIntegrationEvent
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public DateTime CreateDate { get; set; }
    }
}
