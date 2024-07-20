using System.Globalization;

namespace CashFlow.API.CultureMiddleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var suportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();



        var cultureInfo = new CultureInfo("en");
        
        if(string.IsNullOrWhiteSpace(requestedCulture) == false && suportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
        {
           cultureInfo = new CultureInfo(requestedCulture);
        
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
