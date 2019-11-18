using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Token.Domain.Entities;
using Token.Domain.Interfaces;
using Token.Infrastructure.Context;

namespace Token.Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        public UserContext Context;

        public BaseRepository(UserContext context)
        {
            this.Context = context;
        }

        public T Select(int id)
        {
            return this.Context.Set<T>().Find(id);
        }
    }
}
