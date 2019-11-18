using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Token.Domain.Entities;
using Token.Infrastructure.Context;

namespace Token.Service.Business
{
    public class UserService : BaseService<User>
    {
        public UserService(UserContext context) : base(context) { }

        public UserDto GetUser(string userName, string password)
        {
            var user = this.Repository.Context.Set<User>()
                .FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if(user != null)
            {
                return new UserDto
                {
                    UserName = user.UserName,
                    AccessKey = user.AccessKey
                };
            }

            return null;
            
        }
    }
}
