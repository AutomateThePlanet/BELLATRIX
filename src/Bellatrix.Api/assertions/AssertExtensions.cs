// <copyright file="AssertExtensions.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;

namespace Bellatrix.Api;

public static class AssertExtensions
{
    private static List<string> _xmlSchemaValidationErrors;
    public static event EventHandler<ApiAssertEventArgs> AssertExecutionTimeUnderEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertContentContainsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertContentNotContainsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertContentEqualsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertContentNotEqualsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertResultEqualsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertResultNotEqualsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertSuccessStatusCodeEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertStatusCodeEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertResponseHeaderEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertContentTypeEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertContentEncodingEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertCookieExistsEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertCookieEvent;
    public static event EventHandler<ApiAssertEventArgs> AssertSchemaEvent;

    public static void AssertExecutionTimeUnder(this MeasuredResponse response, int seconds)
    {
        AssertExecutionTimeUnderEvent?.Invoke(response, new ApiAssertEventArgs(response, seconds.ToString()));
        if (response.ExecutionTime.TotalSeconds > seconds)
        {
            throw new ApiAssertException($"Request's execution time {response.ExecutionTime.TotalSeconds} was over {seconds}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertContentContains(this MeasuredResponse response, string contentPart)
    {
        AssertContentContainsEvent?.Invoke(response, new ApiAssertEventArgs(response, contentPart));
        if (!response.Content.Contains(contentPart))
        {
            throw new ApiAssertException($"Request's Content did not contain {contentPart}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertContentNotContains(this MeasuredResponse response, string contentPart)
    {
        AssertContentNotContainsEvent?.Invoke(response, new ApiAssertEventArgs(response, contentPart));
        if (response.Content.Contains(contentPart))
        {
            throw new ApiAssertException($"Request's Content contained {contentPart}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertContentEquals(this MeasuredResponse response, string content)
    {
        AssertContentEqualsEvent?.Invoke(response, new ApiAssertEventArgs(response, content));
        if (!response.Content.Equals(content))
        {
            throw new ApiAssertException($"Request's Content was not equal to {content}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertBadRequestStatusCode(this MeasuredResponse response)
    {
        AssertSuccessStatusCodeEvent?.Invoke(response, new ApiAssertEventArgs(response, "2**"));
        if ((int)response.StatusCode < 400 || (int)response.StatusCode > 499)
        {
            throw new ApiAssertException($"Request's status code is not bad - {response.StatusCode}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertContentNotEquals(this MeasuredResponse response, string content)
    {
        AssertContentNotEqualsEvent?.Invoke(response, new ApiAssertEventArgs(response, content));
        if (response.Content.Equals(content))
        {
            throw new ApiAssertException($"Request's Content was equal to {content}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertResultEquals<TResultType>(this MeasuredResponse<TResultType> response, TResultType result)
        where TResultType : IEquatable<TResultType>, new()
    {
        AssertResultEqualsEvent?.Invoke(response, new ApiAssertEventArgs(response, result.ToString()));
        if (!response.Data.Equals(result))
        {
            throw new ApiAssertException($"Request's Data was not equal to {result}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertResultNotEquals<TResultType>(this MeasuredResponse<TResultType> response, TResultType result)
        where TResultType : IEquatable<TResultType>, new()
    {
        AssertResultNotEqualsEvent?.Invoke(response, new ApiAssertEventArgs(response, result.ToString()));
        if (response.Data.Equals(result))
        {
            throw new ApiAssertException($"Request's Data was equal to {result}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertSuccessStatusCode(this MeasuredResponse response)
    {
        AssertSuccessStatusCodeEvent?.Invoke(response, new ApiAssertEventArgs(response, "2**"));
        if ((int)response.StatusCode <= 200 && (int)response.StatusCode >= 299)
        {
            throw new ApiAssertException($"Request's status code was not successful - {response.StatusCode}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertStatusCode(this MeasuredResponse response, HttpStatusCode statusCode)
    {
        AssertStatusCodeEvent?.Invoke(response, new ApiAssertEventArgs(response, statusCode.ToString()));
        if (response.StatusCode != statusCode)
        {
            throw new ApiAssertException($"Request's status code {response.StatusCode} was not equal to {statusCode}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertResponseHeader(this MeasuredResponse response, string headerName, string headerExpectedValue)
    {
        AssertResponseHeaderEvent?.Invoke(response, new ApiAssertEventArgs(response, $"{headerName}"));
        var headerParameter = response.Headers.FirstOrDefault(x => x.Name.ToLower().Equals(headerName.ToLower()));
        if (headerParameter == null)
        {
            throw new ArgumentNullException($"No header was present with name {headerName}");
        }

        if (headerParameter.Value.ToString() != headerExpectedValue)
        {
            throw new ApiAssertException($"Response's header {headerName} with value {headerParameter.Value} was not equal to {headerExpectedValue}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertContentType(this MeasuredResponse response, string expectedContentType)
    {
        AssertContentTypeEvent?.Invoke(response, new ApiAssertEventArgs(response, $"{expectedContentType}"));
        response.AssertResponseHeader("Content-Type", expectedContentType);
    }

    public static void AssertContentEncoding(this MeasuredResponse response, string expectedContentEncoding)
    {
        AssertContentEncodingEvent?.Invoke(response, new ApiAssertEventArgs(response, $"{expectedContentEncoding}"));
        response.AssertResponseHeader("Content-Encoding", expectedContentEncoding);
    }

    public static void AssertCookieExists(this MeasuredResponse response, string cookieName)
    {
        AssertCookieExistsEvent?.Invoke(response, new ApiAssertEventArgs(response, $"{cookieName}"));
        if (!response.Cookies.Any(x => x.Name.Equals(cookieName)))
        {
            throw new ApiAssertException($"Response's cookie with name {cookieName} was not present.", response.ResponseUri.ToString());
        }
    }

    public static void AssertCookie(this MeasuredResponse response, string cookieName, string cookieValue)
    {
        AssertCookieEvent?.Invoke(response, new ApiAssertEventArgs(response, $"{cookieName}={cookieValue}"));
        response.AssertCookieExists(cookieName);
        var cookie = response.Cookies.FirstOrDefault(x => x.Name.Equals(cookieName));
        if (!cookie.Value.Equals(cookieValue))
        {
            throw new ApiAssertException($"Response's cookie with name {cookieName}={cookie.Value} was not equal to {cookieName}={cookieValue}.", response.ResponseUri.ToString());
        }
    }

    public static void AssertSchema(this MeasuredResponse response, string schemaContent)
    {
        AssertSchemaEvent?.Invoke(response, new ApiAssertEventArgs(response, string.Empty));
#pragma warning disable CS0618 // Type or member is obsolete
        if (response.Request.RequestFormat == DataFormat.Json)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            AssertJsonSchema(response, schemaContent);
        }
        else
        {
            AssertXmlSchema(response, schemaContent);
        }
    }

    public static void AssertSchema(this MeasuredResponse response, Uri schemaUri)
    {
        AssertSchemaEvent?.Invoke(response, new ApiAssertEventArgs(response, string.Empty));
#pragma warning disable CS0618 // Type or member is obsolete
        if (response.Request.RequestFormat == DataFormat.Json)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            AssertJsonSchema(response, schemaUri);
        }
        else
        {
            AssertXmlSchema(response, schemaUri);
        }
    }

    private static void AssertJsonSchema(MeasuredResponse response, string schemaContent)
    {
        JSchema jsonSchema;

        try
        {
            jsonSchema = JSchema.Parse(schemaContent);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Schema is not valid schema", ex);
        }

        AssertJsonSchema(response, jsonSchema);
    }

    private static void AssertJsonSchema(MeasuredResponse response, Uri schemaUri)
    {
        var client = new RestClient();
        var schemaResponse = client.Execute(new RestRequest(schemaUri));

        AssertJsonSchema(response, schemaResponse.Content);
    }

    private static void AssertJsonSchema(MeasuredResponse response, JSchema jsonSchema)
    {
        IList<string> messages;

        var trimmedContent = response.Content.TrimStart();

        bool isSchemaValid =
            trimmedContent.StartsWith("{", StringComparison.Ordinal)
                ? JObject.Parse(response.Content).IsValid(jsonSchema, out messages)
                : JArray.Parse(response.Content).IsValid(jsonSchema, out messages);

        if (!isSchemaValid)
        {
            var sb = new StringBuilder();
            sb.AppendLine("JSON Schema is not valid. Error Messages:");
            foreach (var errorMessage in messages)
            {
                sb.AppendLine(errorMessage);
            }

            throw new ApiAssertException(sb.ToString());
        }
    }

    private static void AssertXmlSchema(MeasuredResponse response, Uri schemaUri)
    {
        var schemaSet = new XmlSchemaSet();
        schemaSet.Add(string.Empty, schemaUri.ToString());

        AssertXmlSchema(response, schemaSet);
    }

    private static void AssertXmlSchema(MeasuredResponse response, string schema)
    {
        var schemaSet = new XmlSchemaSet();
        schemaSet.Add(string.Empty, XmlReader.Create(new StringReader(schema)));

        AssertXmlSchema(response, schemaSet);
    }

    private static void AssertXmlSchema(MeasuredResponse response, XmlSchemaSet xmlSchemaSet)
    {
        _xmlSchemaValidationErrors = new List<string>();

        var trimmedContent = response.Content.TrimStart();

        var xml = new XmlDocument();
        xml.LoadXml(trimmedContent);
        xml.Schemas.Add(xmlSchemaSet);

        xml.Validate(ValidationCallBack);

        if (_xmlSchemaValidationErrors.Any())
        {
            var sb = new StringBuilder();
            sb.AppendLine("XML Schema is not valid. Error Messages:");
            foreach (var errorMessage in _xmlSchemaValidationErrors)
            {
                sb.AppendLine(errorMessage);
            }

            throw new ApiAssertException(sb.ToString());
        }
    }

    private static void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs args)
    {
        if (args.Severity == XmlSeverityType.Warning)
        {
            _xmlSchemaValidationErrors.Add($"Warning: Matching schema not found. No validations occurred. {args.Message}");
        }
        else
        {
            _xmlSchemaValidationErrors.Add(args.Message);
        }
    }
}
