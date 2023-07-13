using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore
{
    /// <summary>
    /// // dùng IAggregateRoot là vì: giả sở mình có entity Order, trong Order có Address, List<Product>
    // như bình thường thì mình sẽ tiến hành thêm Order xong, rồi thêm Address và thêm List<Product> thông qua 3 IRepository khác nhau
    // nếu tiến hành sử dụng IAggregateRoot và IRepository áp dụng cho entity : IAggregateRoot
    // thì việc tiến hành thêm Order xong, rồi thêm Address và thêm List<Product> sẽ được định nghĩa trong lớp Order
    // điều này được hiểu là các hành động sẽ được thực hiện thông qua một AggregateRoot (gốc) 
    // thay vì được thực hiện thông qua từng mối liên hệ
    // chính là bên ngoài chỉ có thể thực hiện các thao tác lưu Order cùng với các nghiệp vụ khác vào database thông qua một entity là gốc (ở đây coi là gốc nếu entity kế thừa một IAggregateRoot)
    // thay vì lưu nghiệp vụ liên quan tới Order ở nhiều nơi
    // nói đơn giản là chỉ có một entity cha xử lý các entity con
    // thay vì entity cha và các entity con xử lý riêng biệt
    /// </summary>
    public class BaseEntity : BaseEntityGeneric<string>
    {
        public bool OnDelete { get; set; }
    }
}
