using System;
using System.Collections.Generic;
using System.Text;
using Token.Domain.Entities;

namespace Token.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Select(int id);
    }
}
