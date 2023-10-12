using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Infrastructure
{
    public class BaseCommand : BaseEntity
    {
        public BaseCommand()
        {
            Id = Guid.NewGuid().ToString();
            //  CreateDate = DateTime.UtcNow;
            OnDelete = false;
        }
    }
}
