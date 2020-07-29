// <copyright file="Image.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Web;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public class Image : Element, IElementSrc, IElementHeight, IElementWidth, IElementLongDesc, IElementAlt, IElementSrcSet, IElementSizes
    {
        public static Action<Image> OverrideHoverGlobally;
        public static Func<Image, string> OverrideSrcGlobally;
        public static Func<Image, int?> OverrideHeightGlobally;
        public static Func<Image, int?> OverrideWidthGlobally;
        public static Func<Image, string> OverrideLongDescGlobally;
        public static Func<Image, string> OverrideAltGlobally;
        public static Func<Image, string> OverrideSrcSetGlobally;
        public static Func<Image, string> OverrideSizesGlobally;

        public static Action<Image> OverrideHoverLocally;
        public static Func<Image, string> OverrideSrcLocally;
        public static Func<Image, int?> OverrideHeightLocally;
        public static Func<Image, int?> OverrideWidthLocally;
        public static Func<Image, string> OverrideLongDescLocally;
        public static Func<Image, string> OverrideAltLocally;
        public static Func<Image, string> OverrideSrcSetLocally;
        public static Func<Image, string> OverrideSizesLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideSrcLocally = null;
            OverrideHeightLocally = null;
            OverrideWidthLocally = null;
            OverrideLongDescLocally = null;
            OverrideAltLocally = null;
            OverrideSrcSetLocally = null;
            OverrideSizesLocally = null;
        }

        public override Type ElementType => GetType();

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Src
        {
            get
            {
                var action = InitializeAction(this, OverrideSrcGlobally, OverrideSrcLocally, DefaultSrc);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string LongDesc
        {
            get
            {
                var action = InitializeAction(this, OverrideLongDescGlobally, OverrideLongDescLocally, DefaultGetLongDesc);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Alt
        {
            get
            {
                var action = InitializeAction(this, OverrideAltGlobally, OverrideAltLocally, DefaultGetAlt);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string SrcSet
        {
            get
            {
                var action = InitializeAction(this, OverrideSrcSetGlobally, OverrideSrcSetLocally, DefaultGetSrcSet);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Sizes
        {
            get
            {
                var action = InitializeAction(this, OverrideSizesGlobally, OverrideSizesLocally, DefaultGetSizes);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Height
        {
            get
            {
                var action = InitializeAction(this, OverrideHeightGlobally, OverrideHeightLocally, DefaultGetHeight);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Width
        {
            get
            {
                var action = InitializeAction(this, OverrideWidthGlobally, OverrideWidthLocally, DefaultGetWidth);
                return action();
            }
        }

        protected virtual void DefaultHover(Image image) => DefaultHover(image, Hovering, Hovered);

        protected virtual string DefaultGetAlt(Image image) => string.IsNullOrEmpty(GetAttribute("alt")) ? null : GetAttribute("alt");

        protected virtual string DefaultGetSrcSet(Image image) => string.IsNullOrEmpty(GetAttribute("srcset")) ? null : GetAttribute("srcset");

        protected virtual string DefaultGetSizes(Image image) => string.IsNullOrEmpty(GetAttribute("sizes")) ? null : GetAttribute("sizes");

        protected virtual string DefaultGetLongDesc(Image image) => string.IsNullOrEmpty(GetAttribute("longdesc")) ? null : GetAttribute("longdesc");

        protected virtual int? DefaultGetHeight(Image image) => base.DefaultGetHeight(image);

        protected virtual int? DefaultGetWidth(Image image) => base.DefaultGetWidth(image);

        protected virtual string DefaultSrc(Image image) => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(GetAttribute("src")));
    }
}