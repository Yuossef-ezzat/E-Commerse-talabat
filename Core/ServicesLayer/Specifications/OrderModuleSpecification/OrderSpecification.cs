using DomainLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications.OrderModuleSpecification
{
    public class OrderSpecification : BaseSpecifications<Order,Guid>
    {
        // Get All By Email o=> o.Email == Claim.Email
        public OrderSpecification(string email): base (o=>o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);
            
        }

        // Get By ID

        public OrderSpecification(Guid Id) : base(o => o.Id == Id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);

        }
    }
}
