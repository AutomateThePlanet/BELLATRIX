// <copyright file="Element.InitializeActions.cs" company="Automate The Planet Ltd.">
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
using System;

namespace Bellatrix.Desktop
{
    public partial class Element
    {
        protected Action InitializeAction<TElement>(
            TElement element,
            Action<TElement> globalAction,
            Action<TElement> localAction,
            Action<TElement> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return () => localAction(element);
            }
            else if (globalAction != null)
            {
                return () => globalAction(element);
            }
            else
            {
                return () => defaultAction(element);
            }
        }

        protected Action<TSecond, TThird> InitializeAction<TElement, TSecond, TThird>(
            TElement element,
            Action<TElement, TSecond, TThird> globalAction,
            Action<TElement, TSecond, TThird> localAction,
            Action<TElement, TSecond, TThird> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return (s1, s2) => localAction(element, s1, s2);
            }
            else if (globalAction != null)
            {
                return (s1, s2) => globalAction(element, s1, s2);
            }
            else
            {
                return (s1, s2) => defaultAction(element, s1, s2);
            }
        }

        protected Func<TReturn> InitializeAction<TElement, TReturn>(
            TElement element,
            Func<TElement, TReturn> globalAction,
            Func<TElement, TReturn> localAction,
            Func<TElement, TReturn> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return () => localAction(element);
            }
            else if (globalAction != null)
            {
                return () => globalAction(element);
            }
            else
            {
                return () => defaultAction(element);
            }
        }

        protected Action<TSecond> InitializeAction<TElement, TSecond>(
            TElement element,
            Action<TElement, TSecond> globalAction,
            Action<TElement, TSecond> localAction,
            Action<TElement, TSecond> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return s => localAction(element, s);
            }
            else if (globalAction != null)
            {
                return s => globalAction(element, s);
            }
            else
            {
                return s => defaultAction(element, s);
            }
        }

        protected Action<TSecond, TThird, TFourth> InitializeAction<TElement, TSecond, TThird, TFourth>(
            TElement element,
            Action<TElement, TSecond, TThird, TFourth> globalAction,
            Action<TElement, TSecond, TThird, TFourth> localAction,
            Action<TElement, TSecond, TThird, TFourth> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return (s1, s2, s3) => localAction(element, s1, s2, s3);
            }
            else if (globalAction != null)
            {
                return (s1, s2, s3) => globalAction(element, s1, s2, s3);
            }
            else
            {
                return (s1, s2, s3) => defaultAction(element, s1, s2, s3);
            }
        }

        protected Func<TReturn> InitializeFunction<TElement, TReturn>(
            TElement element,
            Func<TElement, TReturn> globalAction,
            Func<TElement, TReturn> localAction,
            Func<TElement, TReturn> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return () => localAction(element);
            }
            else if (globalAction != null)
            {
                return () => globalAction(element);
            }
            else
            {
                return () => defaultAction(element);
            }
        }

        protected Func<TReturn, TReturn> InitializeFunction<TElement, TReturn>(
            TElement element,
            Func<TElement, TReturn, TReturn> globalAction,
            Func<TElement, TReturn, TReturn> localAction,
            Func<TElement, TReturn, TReturn> defaultAction)
            where TElement : Element
        {
            if (localAction != null)
            {
                return t => localAction(element, t);
            }
            else if (globalAction != null)
            {
                return t => globalAction(element, t);
            }
            else
            {
                return t => defaultAction(element, t);
            }
        }

        protected Action InitializeAction<TElement>(TElement element, Action<TElement> globalAction, Action<TElement> defaultAction)
            where TElement : Element => globalAction != null ? (Action)(() => globalAction(element)) : (() => defaultAction(element));

        protected Func<TReturn> InitializeAction<TElement, TReturn>(
            TElement element,
            Func<TElement, TReturn> globalAction,
            Func<TElement, TReturn> defaultAction)
            where TElement : Element => globalAction != null ? (Func<TReturn>)(() => globalAction(element)) : (() => defaultAction(element));
    }
}
