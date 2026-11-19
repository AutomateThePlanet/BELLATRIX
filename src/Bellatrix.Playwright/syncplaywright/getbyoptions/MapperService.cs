// <copyright file="MapperService.cs" company="Automate The Planet Ltd.">
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

using Mapster;

namespace Bellatrix.Playwright.SyncPlaywright;

internal static class MapperService
{
    private static readonly TypeAdapterConfig Config;

    static MapperService()
    {
        Config = new TypeAdapterConfig();

        Config.NewConfig<GetByAltTextOptions, LocatorGetByAltTextOptions>();
        Config.NewConfig<GetByAltTextOptions, FrameLocatorGetByAltTextOptions>();
        Config.NewConfig<GetByAltTextOptions, PageGetByAltTextOptions>();

        Config.NewConfig<GetByLabelOptions, LocatorGetByLabelOptions>();
        Config.NewConfig<GetByLabelOptions, FrameLocatorGetByLabelOptions>();
        Config.NewConfig<GetByLabelOptions, PageGetByLabelOptions>();

        Config.NewConfig<GetByPlaceholderOptions, LocatorGetByPlaceholderOptions>();
        Config.NewConfig<GetByPlaceholderOptions, FrameLocatorGetByPlaceholderOptions>();
        Config.NewConfig<GetByPlaceholderOptions, PageGetByPlaceholderOptions>();

        Config.NewConfig<GetByRoleOptions, LocatorGetByRoleOptions>();
        Config.NewConfig<GetByRoleOptions, FrameLocatorGetByRoleOptions>();
        Config.NewConfig<GetByRoleOptions, PageGetByRoleOptions>();

        Config.NewConfig<GetByTextOptions, LocatorGetByTextOptions>();
        Config.NewConfig<GetByTextOptions, FrameLocatorGetByTextOptions>();
        Config.NewConfig<GetByTextOptions, PageGetByTextOptions>();

        Config.NewConfig<GetByTitleOptions, LocatorGetByTitleOptions>();
        Config.NewConfig<GetByTitleOptions, FrameLocatorGetByTitleOptions>();
        Config.NewConfig<GetByTitleOptions, PageGetByTitleOptions>();
    }

    public static T ConvertTo<T>(this IOptions options)
    {
        return options.Adapt<T>(Config);
    }
}
