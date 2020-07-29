// <copyright file="ApiAuthenticationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.TestExecutionExtensions.API
{
    [Binding]
    public class ApiAuthenticationSteps
    {
        [Given(@"I use basic authentication username (.*) password (.*)")]
        public void GivenUseBasicAuthentication(string userName, string password)
        {
            ServicesCollection.Current.RegisterInstance(new HttpBasicAuthenticator(userName, password));
        }

        [Given(@"I use JSON web token authentication with access token (.*)")]
        public void GivenUseJwtAuthentication(string accessToken)
        {
            ServicesCollection.Current.RegisterInstance(new JwtAuthenticator(accessToken));
        }

        /// <summary>
        ///     Tries to Authenticate with the credentials of the currently logged in user, or impersonate a user.
        /// </summary>
        [Given(@"I use NTLM authentication")]
        public void GivenUseNtlmAuthentication()
        {
            ServicesCollection.Current.RegisterInstance(new NtlmAuthenticator());
        }

        [Given(@"I use simple authentication username key (.*) username (.*) password key (.*) password (.*)")]
        public void GivenUseSimpleAuthentication(string usernameKey, string username, string passwordKey, string password)
        {
            ServicesCollection.Current.RegisterInstance(new SimpleAuthenticator(usernameKey, username, passwordKey, password));
        }

        /// <summary>
        ///     The OAuth 2 authenticator using the authorization request header field.
        /// </summary>
        /// <remarks>
        ///     Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.1.
        /// </remarks>
        [Given(@"I use OAuth 2 access token (.*)")]
        public void GivenUseOAuth2AuthorizationRequestHeaderAuthentication(string accessToken)
        {
            ServicesCollection.Current.RegisterInstance(new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken));
        }

        /// <summary>
        ///     The OAuth 2 authenticator using URI query parameter.
        /// </summary>
        /// <remarks>
        ///     Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.2.
        /// </remarks>
        [Given(@"I use OAuth 2 URI access token (.*)")]
        public void GivenUseOAuth2UriQueryParameterAuthentication(string accessToken)
        {
            ServicesCollection.Current.RegisterInstance(new OAuth2UriQueryParameterAuthenticator(accessToken));
        }
    }
}