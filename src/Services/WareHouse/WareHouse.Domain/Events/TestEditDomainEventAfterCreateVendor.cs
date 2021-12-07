using System;
using System.Collections.Generic;
using MediatR;

#nullable disable

namespace WareHouse.Domain
{
    // tạo events
    public partial class TestEditDomainEventAfterCreateVendor:INotification
    {
        public string _TextEditToEntity { get; set; }
        public string _Id { get; set; }

        public TestEditDomainEventAfterCreateVendor(string TextEditToEntity, string Id)
        {
            _TextEditToEntity = TextEditToEntity;
            _Id = Id;
        }
    }
}