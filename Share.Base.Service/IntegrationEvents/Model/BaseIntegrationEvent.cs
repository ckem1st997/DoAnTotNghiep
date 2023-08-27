using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Share.Base.Service.IntegrationEvents.Model
{
    public class BaseIntegrationEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreateDate { get; set; }
    }
}
