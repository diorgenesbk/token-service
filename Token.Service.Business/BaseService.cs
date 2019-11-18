using System;
using Token.Domain.Entities;
using Token.Domain.Interfaces;
using Token.Infrastructure.Context;
using Token.Infrastructure.Repository;

namespace Token.Service.Business
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public BaseRepository<T> Repository;

        public BaseService(UserContext context)
        {
            this.Repository = new BaseRepository<T>(context);
        }

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException($"Não foi possível encontrar o registro de id: {id}");

            return this.Repository.Select(id);
        }
    }
}
