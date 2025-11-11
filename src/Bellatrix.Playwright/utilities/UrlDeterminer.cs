// <copyright file="UrlDeterminer.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Linq.Expressions;
using Bellatrix.Utilities;

namespace Bellatrix.Playwright;

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