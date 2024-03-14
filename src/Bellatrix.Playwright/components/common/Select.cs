// <copyright file="Select.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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

using System.Diagnostics;
using System.Globalization;
using System.Text;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.SyncPlaywright;

namespace Bellatrix.Playwright;

public class Select : Component, IComponentDisabled, IComponentRequired, IComponentReadonly
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Selecting;
    public static event EventHandler<ComponentActionEventArgs> Selected;

    public override Type ComponentType => GetType();

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    public virtual Option GetSelected()
    {
        return DefaultSelectedValue(this);
    }

    public virtual List<Option> GetAllOptions()
    {
        return DefaultGetAllOptions(this);
    }

    public virtual void SelectByText(string text)
    {
        DefaultSelectByText(this, text);
    }

    public virtual void SelectByIndex(int index)
    {
        DefaultSelectByIndex(this, index);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsRequired => GetRequiredAttribute();

    public virtual bool IsReadonly => GetReadonlyAttribute();

    protected virtual List<Option> DefaultGetAllOptions(Select select)
    {
        var nativeSelect = new selectComponent(WrappedElement);
        var options = new List<Option>();
        foreach (var option in nativeSelect.Options)
        {
            var optionNativeElement = new Option
            {
                By = select.By,
                WrappedElement = option,
                ShouldCacheElement = false,
                ComponentName = ComponentName,
                PageName = PageName,
            };

            options.Add(optionNativeElement);
        }

        return options;
    }

    protected virtual Option DefaultSelectedValue(Select select)
    {
        var nativeSelect = new selectComponent(WrappedElement);
        var optionNativeElement = new Option
        {
            By = select.By,
            WrappedElement = nativeSelect.SelectedOption,
            ShouldCacheElement = true,
        };
        return optionNativeElement;
    }

    protected virtual void DefaultSelectByText(Select select, string value)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(select, value));

        var nativeSelect = new selectComponent(WrappedElement);
        nativeSelect.SelectByText(value);
        WrappedElement = null;
        ShouldCacheElement = false;
        Selected?.Invoke(this, new ComponentActionEventArgs(select, value));
    }

    protected virtual void DefaultSelectByIndex(Select select, int index)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(select, index.ToString()));

        var nativeSelect = new selectComponent(WrappedElement);
        nativeSelect.SelectByIndex(index);
        WrappedElement = null;
        ShouldCacheElement = false;
        Selected?.Invoke(this, new ComponentActionEventArgs(select, nativeSelect.SelectedOption.InnerText()));
    }
}

public class selectComponent
{
    private readonly WebElement _element;

    /// <summary>
    /// Initializes a new instance of the <see cref="selectComponent"/> class.
    /// </summary>
    /// <param name="element">The _element to be wrapped.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <see cref="WebElement"/> object is <see langword="null"/>.</exception>
    /// <exception cref="UnexpectedTagNameException">Thrown when the _element wrapped is not a &lt;select&gt; _element.</exception>
    public selectComponent(WebElement element)
    {
        _element = element ?? throw new ArgumentNullException("_element", "_element cannot be null");

        // let check if it's a multiple
        string attribute = element.GetAttribute("multiple");
        IsMultiple = attribute != null && attribute.ToLowerInvariant() != "false";
    }

    /// <summary>
    /// Gets the <see cref="WebElement"/> wrapped by this object.
    /// </summary>
    public WebElement WrappedElement => _element;

    /// <summary>
    /// Gets a value indicating whether the parent _element supports multiple selections.
    /// </summary>
    public bool IsMultiple { get; private set; }

    /// <summary>
    /// Gets the list of options for the select _element.
    /// </summary>
    public IList<WebElement> Options
    {
        get
        {
            return _element.Locate(".//option").All().ToList();
        }
    }

    /// <summary>
    /// Gets the selected item within the select _element.
    /// </summary>
    /// <remarks>If more than one item is selected this will return the first item.</remarks>
    /// <exception cref="ArgumentException">Thrown if no option is selected.</exception>
    public WebElement SelectedOption => GetSelectedOption();

    /// <summary>
    /// Gets all of the selected options within the select _element.
    /// </summary>
    public IList<WebElement> AllSelectedOptions
    {
        get
        {
            List<WebElement> returnValue = new List<WebElement>();
            foreach (WebElement option in Options)
            {
                if (IsSelected(option))
                {
                    returnValue.Add(option);
                }
            }

            return returnValue;
        }
    }

    /// <summary>
    /// Select all options by the text displayed.
    /// </summary>
    /// <param name="text">The text of the option to be selected.</param>
    /// <remarks>When given "Bar" this method would select an option like:
    /// <para>
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown if there is no _element with the given text present.</exception>
    public void SelectByText(string text, bool partialMatch = false)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text), "text must not be null");
        }

        bool matched = false;
        IList<WebElement> options;

        if (!partialMatch)
        {
            // try to find the option via XPATH ...
            options = _element.Locate(".//option[normalize-space(.) = " + EscapeQuotes(text) + "]").All().ToList();
        }
        else
        {
            options = _element.Locate(".//option[contains(normalize-space(.),  " + EscapeQuotes(text) + ")]").All().ToList();
        }

        foreach (WebElement option in options)
        {
            SetSelected(option, true);
            if (!IsMultiple)
            {
                return;
            }

            matched = true;
        }

        if (options.Count == 0 && text.Contains(" "))
        {
            string substringWithoutSpace = GetLongestSubstringWithoutSpace(text);
            IList<WebElement> candidates;
            if (string.IsNullOrEmpty(substringWithoutSpace))
            {
                // hmm, text is either empty or contains only spaces - get all options ...
                candidates = _element.Locate(".//option").All().ToList();
            }
            else
            {
                // get candidates via XPATH ...
                candidates = _element.Locate(".//option[contains(., " + EscapeQuotes(substringWithoutSpace) + ")]").All().ToList();
            }

            foreach (WebElement option in candidates)
            {
                if (text == option.InnerText())
                {
                    SetSelected(option, true);
                    if (!IsMultiple)
                    {
                        return;
                    }

                    matched = true;
                }
            }
        }

        if (!matched)
        {
            throw new ArgumentException("Cannot locate _element with text: " + text);
        }
    }

    /// <summary>
    /// Select an option by the value.
    /// </summary>
    /// <param name="value">The value of the option to be selected.</param>
    /// <remarks>When given "foo" this method will select an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when no _element with the specified value is found.</exception>
    public void SelectByValue(string value)
    {
        StringBuilder builder = new StringBuilder(".//option[@value = ");
        builder.Append(EscapeQuotes(value));
        builder.Append(']');
        IList<WebElement> options = _element.Locate(builder.ToString()).All().ToList();

        bool matched = false;
        foreach (WebElement option in options)
        {
            SetSelected(option, true);
            if (!IsMultiple)
            {
                return;
            }

            matched = true;
        }

        if (!matched)
        {
            throw new ArgumentException("Cannot locate option with value: " + value);
        }
    }

    /// <summary>
    /// Select the option by the index, as determined by the "index" attribute of the _element.
    /// </summary>
    /// <param name="index">The value of the index attribute of the option to be selected.</param>
    /// <exception cref="ArgumentException">Thrown when no _element exists with the specified index attribute.</exception>
    public void SelectByIndex(int index)
    {
        string match = index.ToString(CultureInfo.InvariantCulture);

        foreach (WebElement option in Options)
        {
            if (option.GetAttribute("index") == match)
            {
                SetSelected(option, true);
                return;
            }
        }

        throw new ArgumentException("Cannot locate option with index: " + index);
    }

    /// <summary>
    /// Clear all selected entries. This is only valid when the SELECT supports multiple selections.
    /// </summary>
    /// <exception cref="WebDriverException">Thrown when attempting to deselect all options from a SELECT
    /// that does not support multiple selections.</exception>
    public void DeselectAll()
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect all options if multi-select is supported");
        }

        foreach (WebElement option in Options)
        {
            SetSelected(option, false);
        }
    }

    /// <summary>
    /// Deselect the option by the text displayed.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to deselect option from a SELECT
    /// that does not support multiple selections.</exception>
    /// <exception cref="ArgumentException">Thrown when no _element exists with the specified test attribute.</exception>
    /// <param name="text">The text of the option to be deselected.</param>
    /// <remarks>When given "Bar" this method would deselect an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;.
    /// </para>
    /// </remarks>
    public void DeselectByText(string text)
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect option if multi-select is supported");
        }

        bool matched = false;
        StringBuilder builder = new StringBuilder(".//option[normalize-space(.) = ");
        builder.Append(EscapeQuotes(text));
        builder.Append(']');
        IList<WebElement> options = _element.Locate(builder.ToString()).All().ToList();
        foreach (WebElement option in options)
        {
            SetSelected(option, false);
            matched = true;
        }

        if (!matched)
        {
            throw new ArgumentException("Cannot locate option with text: " + text);
        }
    }

    /// <summary>
    /// Deselect the option having value matching the specified text.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to deselect option from a SELECT
    /// that does not support multiple selections.</exception>
    /// <exception cref="ArgumentException">Thrown when no _element exists with the specified value attribute.</exception>
    /// <param name="value">The value of the option to deselect.</param>
    /// <remarks>When given "foo" this method will deselect an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;.
    /// </para>
    /// </remarks>
    public void DeselectByValue(string value)
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect option if multi-select is supported");
        }

        bool matched = false;
        StringBuilder builder = new StringBuilder(".//option[@value = ");
        builder.Append(EscapeQuotes(value));
        builder.Append(']');
        IList<WebElement> options = _element.Locate(builder.ToString()).All().ToList();
        foreach (WebElement option in options)
        {
            SetSelected(option, false);
            matched = true;
        }

        if (!matched)
        {
            throw new ArgumentException("Cannot locate option with value: " + value);
        }
    }

    /// <summary>
    /// Deselect the option by the index, as determined by the "index" attribute of the _element.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to deselect option from a SELECT
    /// that does not support multiple selections.</exception>
    /// <exception cref="ArgumentException">Thrown when no _element exists with the specified index attribute.</exception>
    /// <param name="index">The value of the index attribute of the option to deselect.</param>
    public void DeselectByIndex(int index)
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect option if multi-select is supported");
        }

        string match = index.ToString(CultureInfo.InvariantCulture);
        foreach (WebElement option in Options)
        {
            if (match == option.GetAttribute("index"))
            {
                SetSelected(option, false);
                return;
            }
        }

        throw new ArgumentException("Cannot locate option with index: " + index);
    }

    private static string EscapeQuotes(string toEscape)
    {
        // Convert strings with both quotes and ticks into: foo'"bar -> concat("foo'", '"', "bar")
        if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) > -1 && toEscape.IndexOf("'", StringComparison.OrdinalIgnoreCase) > -1)
        {
            bool quoteIsLast = false;
            if (toEscape.LastIndexOf("\"", StringComparison.OrdinalIgnoreCase) == toEscape.Length - 1)
            {
                quoteIsLast = true;
            }

            List<string> substrings = new List<string>(toEscape.Split('\"'));
            if (quoteIsLast && string.IsNullOrEmpty(substrings[substrings.Count - 1]))
            {
                // If the last character is a quote ('"'), we end up with an empty entry
                // at the end of the list, which is unnecessary. We don't want to split
                // ignoring *all* empty entries, since that might mask legitimate empty
                // strings. Instead, just remove the empty ending entry.
                substrings.RemoveAt(substrings.Count - 1);
            }

            StringBuilder quoted = new StringBuilder("concat(");
            for (int i = 0; i < substrings.Count; i++)
            {
                quoted.Append('"').Append(substrings[i]).Append('"');
                if (i == substrings.Count - 1)
                {
                    if (quoteIsLast)
                    {
                        quoted.Append(", '\"')");
                    }
                    else
                    {
                        quoted.Append(')');
                    }
                }
                else
                {
                    quoted.Append(", '\"', ");
                }
            }

            return quoted.ToString();
        }

        // Escape string with just a quote into being single quoted: f"oo -> 'f"oo'
        if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) > -1)
        {
            return string.Format(CultureInfo.InvariantCulture, "'{0}'", toEscape);
        }

        // Otherwise return the quoted string
        return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", toEscape);
    }

    private static string GetLongestSubstringWithoutSpace(string s)
    {
        string result = string.Empty;
        string[] substrings = s.Split(' ');
        foreach (string substring in substrings)
        {
            if (substring.Length > result.Length)
            {
                result = substring;
            }
        }

        return result;
    }

    private void SetSelected(WebElement option, bool select)
    {
        bool isSelected = IsSelected(option);
        if ((!isSelected && select) || (isSelected && !select))
        {
            option.Click();
        }
    }

    private WebElement GetSelectedOption()
    {
        string optionValue = WrappedElement.Evaluate("selectElement => {" +
            "    const selectedOption = selectElement.options[selectElement.selectedIndex];" +
            "    return selectedOption.getAttribute('value');" +
            "}").GetValueOrDefault().GetString();

        try
        {
            return WrappedElement.Locate($".//option[@value='{optionValue}']");
        }
        catch
        {
            throw new ArgumentException("No option is selected");
        }
    }

    private static bool IsSelected(WebElement option)
    {
        return option.Evaluate("el => { el.selected; }").GetValueOrDefault().GetBoolean();
    }
}