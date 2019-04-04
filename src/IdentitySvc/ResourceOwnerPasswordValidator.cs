using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySvc
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IdentityDB _db;
        public ResourceOwnerPasswordValidator(IdentityDB db)
        {
            _db = db;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_db.User.Count(d => d.UserName == context.UserName && d.PassWord == context.Password) > 0)
            {
                context.Result = new GrantValidationResult();
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "登录失败");
            }
        }
    }
}
