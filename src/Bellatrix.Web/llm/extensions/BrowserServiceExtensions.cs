// <copyright file="BrowserServiceExtensions.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
namespace Bellatrix.Web;
public static class BrowserServiceExtensions
{
    public static string GetPageSummaryJson(this BrowserService browser)
    {
        var javaScriptService = new JavaScriptService(browser.WrappedDriver);
        var script = @"
    return JSON.stringify(Array.from(document.querySelectorAll('*')).filter(el => {
        const style = window.getComputedStyle(el);
        const rect = el.getBoundingClientRect();
        return style.display !== 'none' &&
               style.visibility !== 'hidden' &&
               rect.width > 0 && rect.height > 0;
    }).map(el => {
        const tag = el.tagName.toLowerCase();
        const type = el.getAttribute('type');
        const isCheckbox = tag === 'input' && type === 'checkbox';
        return {
            tag: tag,
            id: el.id,
            class: el.className.trim(),
            text: el.innerText.trim(),
            type: type,
            name: el.getAttribute('name'),
            placeholder: el.getAttribute('placeholder'),
            ariaLabel: el.getAttribute('aria-label'),
            role: el.getAttribute('role'),
            href: el.getAttribute('href'),
            for: el.getAttribute('for'),
            value: el.getAttribute('value'),
            disabled: el.disabled,
            checked: isCheckbox ? el.checked : undefined,
            selected: el.selected
        };
    }));
";
        return (string)javaScriptService.Execute(script);
    }
}
