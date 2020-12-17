using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WinbondNetCoreTemplate.MiddleWares
{
    /// <summary>
    /// 當地語系化 provider
    /// </summary>
    public class CultureMiddleware
    {
        private static readonly List<CultureInfo> _supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("zh-TW"),
            new CultureInfo("en-US"),
            new CultureInfo("ja-JP")
        };

        private static readonly RequestLocalizationOptions _localizationOptions = new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture(_supportedCultures.First()),
            SupportedCultures = _supportedCultures,
            SupportedUICultures = _supportedCultures,          
            RequestCultureProviders = new[]
            {               
                new CookieRequestCultureProvider() { Options = _localizationOptions }
            }
        };

        public void Configure(IApplicationBuilder app)
        {
            app.UseRequestLocalization(_localizationOptions);
        }
    }
}
