using Owin;
using System;
using Microsoft.Owin;
using EngMan.Providers;
using EngMan.Repository;
using EngMan.Service;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;

namespace EngMan
{
    public partial class Startup
    {
        private readonly static OAuthProvider provider = new OAuthProvider(new UserService(new UserRepository(new EFDbContext())));

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = provider,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}