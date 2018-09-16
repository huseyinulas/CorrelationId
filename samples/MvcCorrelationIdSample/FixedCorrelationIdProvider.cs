using CorrelationId;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorrelationIdSample
{
    public class FixedCorrelationIdProvider : ICorrelationIdProvider
    {
        public string GenerateCorrelationId(HttpContext httpContext)
        {
            return "THISISFIXED";
        }
    }
}
