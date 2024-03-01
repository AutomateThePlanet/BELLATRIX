// <copyright file="OAuth2AuthorizationRequestHeaderAuthenticationStrategyAttribute.cs" company="Automate The Planet Ltd.">
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
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;

namespace Bellatrix;

/// <summary>
///     The OAuth 2 authenticator using the authorization request header field.
/// </summary>
/// <remarks>
///     Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.1.
/// </remarks>
public class OAuth2AuthorizationRequestHeaderAuthenticationStrategyAttribute : AuthenticationStrategyAttribute
{
    private readonly string _accessToken;

    public OAuth2AuthorizationRequestHeaderAuthenticationStrategyAttribute(string accessToken) => _accessToken = accessToken;

    public override IAuthenticator GetAuthenticator() => new OAuth2AuthorizationRequestHeaderAuthenticator(_accessToken);
}