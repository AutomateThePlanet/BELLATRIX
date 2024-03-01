// <copyright file="AssertExtensionsEventHandlers.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Api;

public abstract class AssertExtensionsEventHandlers
{
    public virtual void SubscribeToAll()
    {
        AssertExtensions.AssertExecutionTimeUnderEvent += AssertExecutionTimeUnderEventHandler;
        AssertExtensions.AssertContentContainsEvent += AssertContentContainsEventHandler;
        AssertExtensions.AssertContentNotContainsEvent += AssertContentNotContainsEventHandler;
        AssertExtensions.AssertContentEqualsEvent += AssertContentEqualsEventHandler;
        AssertExtensions.AssertContentNotEqualsEvent += AssertContentNotEqualsEventHandler;
        AssertExtensions.AssertResultEqualsEvent += AssertResultEqualsEventHandler;
        AssertExtensions.AssertResultNotEqualsEvent += AssertResultNotEqualsEventHandler;
        AssertExtensions.AssertSuccessStatusCodeEvent += AssertSuccessStatusCodeEventHandler;
        AssertExtensions.AssertStatusCodeEvent += AssertStatusCodeEventHandler;
        AssertExtensions.AssertResponseHeaderEvent += AssertResponseHeaderEventHandler;
        AssertExtensions.AssertContentTypeEvent += AssertContentTypeEventHandler;
        AssertExtensions.AssertContentEncodingEvent += AssertContentEncodingEventHandler;
        AssertExtensions.AssertCookieExistsEvent += AssertCookieExistsEventHandler;
        AssertExtensions.AssertCookieEvent += AssertCookieEventHandler;
        AssertExtensions.AssertSchemaEvent += AssertSchemaEventHandler;
    }

    public virtual void UnsubscribeToAll()
    {
        AssertExtensions.AssertExecutionTimeUnderEvent -= AssertExecutionTimeUnderEventHandler;
        AssertExtensions.AssertContentContainsEvent -= AssertContentContainsEventHandler;
        AssertExtensions.AssertContentNotContainsEvent -= AssertContentNotContainsEventHandler;
        AssertExtensions.AssertContentEqualsEvent -= AssertContentEqualsEventHandler;
        AssertExtensions.AssertContentNotEqualsEvent -= AssertContentNotEqualsEventHandler;
        AssertExtensions.AssertResultEqualsEvent -= AssertResultEqualsEventHandler;
        AssertExtensions.AssertResultNotEqualsEvent -= AssertResultNotEqualsEventHandler;
        AssertExtensions.AssertSuccessStatusCodeEvent -= AssertSuccessStatusCodeEventHandler;
        AssertExtensions.AssertStatusCodeEvent -= AssertStatusCodeEventHandler;
        AssertExtensions.AssertResponseHeaderEvent -= AssertResponseHeaderEventHandler;
        AssertExtensions.AssertContentTypeEvent -= AssertContentTypeEventHandler;
        AssertExtensions.AssertContentEncodingEvent -= AssertContentEncodingEventHandler;
        AssertExtensions.AssertCookieExistsEvent -= AssertCookieExistsEventHandler;
        AssertExtensions.AssertCookieEvent -= AssertCookieEventHandler;
        AssertExtensions.AssertSchemaEvent -= AssertSchemaEventHandler;
    }

    protected virtual void AssertExecutionTimeUnderEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentContainsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentNotContainsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentNotEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertResultEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertResultNotEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertSuccessStatusCodeEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertStatusCodeEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertResponseHeaderEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentTypeEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentEncodingEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertCookieExistsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertCookieEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertSchemaEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }
}
