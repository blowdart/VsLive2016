using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace MultipleAuthentication.Middleware
{
    public class SimpleBearerOptions : AuthenticationOptions, IOptions<SimpleBearerOptions>
    {
        public SimpleBearerOptions()
        {
            AuthenticationScheme = "Bearer";
            AutomaticAuthenticate = false;
        }

        public SimpleBearerOptions Value
        {
            get
            {
                return this;
            }
        }
    }
}
