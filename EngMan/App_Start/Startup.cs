using Owin;
using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using EngMan.Providers;

namespace EngMan
{
    public partial class Startup
    {
        public const string TokenPath = "/api/token";

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(TokenPath),
                Provider = new OAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}