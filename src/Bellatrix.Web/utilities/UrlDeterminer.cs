using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Utilities;

namespace Bellatrix.Web
{
    public static class UrlDeterminer
    {
        public static string GetUrl<TUrlSettings>(Expression<Func<TUrlSettings, object>> expression, string partialUrl = "")
            where TUrlSettings : class, new()
        {
            string propertyName = TypePropertiesNameResolver.GetMemberName(expression);
            var urlSettings = ConfigurationService.GetSection<TUrlSettings>();
            var propertyInfo = typeof(TUrlSettings).GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName));
            return new Uri(new Uri(propertyInfo.GetValue(urlSettings) as string), partialUrl).AbsoluteUri;
        }
    }
}