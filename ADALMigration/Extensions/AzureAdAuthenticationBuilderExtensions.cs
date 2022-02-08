using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Authentication
{
    public static class AzureAdServiceCollectionExtensions
    {
        private static string reportingAudience = "7c5dd291-edd7-4dc4-ad09-25ac006a0a42";

        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder)
            => builder.AddAzureAdBearer(_ => { });

        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder, Action<AzureAdOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer();
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureAdOptions azureOptions;

            public ConfigureAzureOptions(IOptions<AzureAdOptions> azureOptions)
            {
                this.azureOptions = azureOptions.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.Audience = azureOptions.ClientId;
                options.Authority = $"{azureOptions.Instance}{azureOptions.TenantId}";
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = options.Authority,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    // For every application that wants to access Technician app
                    // using Secret Key, we need to add their ClientId here
                    // There must be a better way to do this -- I don't know it yet
                    ValidAudiences = new List<string>{
                        azureOptions.ClientId,
                        reportingAudience
                    }
                };
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}