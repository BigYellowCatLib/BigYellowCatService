using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySvc.Extensions
{
    public class Injection
    {
        private RequestDelegate _next;
        public Injection(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IdentityDB db)
        {
            await _next(context);
        }
    }
}
