using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore
{
    public class BaseSsoConfig
    {

        public const string Sso = "Sso";

        public string Realm { get; set; }

        public string Authority { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ResponseType { get; set; }

        public string RedirectUri { get; set; }

        public string CallbackPath { get; set; }

        public string RemoteSignOutPath { get; set; }

        public string SignedOutRedirectUri { get; set; }

        public string SignedOutCallbackPath { get; set; }

        public string AccessDeniedPath { get; set; }

        public bool GetClaimsFromUserInfoEndpoint { get; set; }

        public bool SaveTokens { get; set; }

        public ServiceAccount ServiceAccount { get; set; }

        public IList<string> Scopes { get; set; }

        public BaseSsoConfig()
        {
            this.RequireHttpsMetadata = false;
            this.ResponseType = "code";
            this.SignedOutRedirectUri = "/";
            this.AccessDeniedPath = "/MvcError/Forbidden";
            this.GetClaimsFromUserInfoEndpoint = true;
            this.SaveTokens = true;
            this.Scopes = (IList<string>)new List<string>()
      {
        "openid",
        "email",
        "profile",
        "offline_access",
        "roles"
      };
        }
    }
    public class ServiceAccount
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
