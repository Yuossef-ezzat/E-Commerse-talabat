using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DelieveryMethodNotFoundException(int id) 
        : NotFoundException($"Delievery Method With Id: {id} Not Found")
    {
    }
}
