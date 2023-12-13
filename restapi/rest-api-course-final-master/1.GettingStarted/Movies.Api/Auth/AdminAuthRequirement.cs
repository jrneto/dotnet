using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Movies.Api.Auth
{
    public class AdminAuthRequirement : IAuthorizationHandler, IAuthorizationRequirement
    {
        private readonly string _apiKey;

        public AdminAuthRequirement(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            // Se o usuário possuir a claim de admin então está valido e
            // já retorna sucesso. Nem verifica a APIKey
            if (context.User.HasClaim(AuthConstants.AdminUserClaimName, "true"))
            {
                context.Succeed(this);
                return Task.CompletedTask;
            }

            var httpContext = context.Resource as HttpContext;

            if (httpContext is null)
            {
                return Task.CompletedTask;
            }

            if (!httpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName,
                out var extractedApiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (_apiKey != extractedApiKey)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var identity = (ClaimsIdentity)httpContext.User.Identity!;
            //o userid está hardcoded apenas como exemplo. É usuário criado na API Identity
            identity.AddClaim(new Claim("userid", Guid.Parse("d8566de3-b1a6-4a9b-b842-8e3887a82e41").ToString()));
            context.Succeed(this);
            return Task.CompletedTask;
        }
    }
}
