using System;
using System.Collections.Generic;
using System.Text;
using Token.Domain.Entities;

namespace Token.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
