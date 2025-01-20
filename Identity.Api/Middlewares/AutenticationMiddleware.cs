using Identity.Application.Contracts;

namespace Identity.Api.Middlewares
{
    public class AutenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AutenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtValidator jwtValidator)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var principal = jwtValidator.ValidateToken(token);

                if (principal != null)
                {
                    context.User = principal;
                }
            }

            await _next(context);
        }
    }
}
