// <copyright file="MapperService.cs" company="Automate The Planet Ltd.">
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

using AutoMapper;

namespace Bellatrix.Playwright.SyncPlaywright;

internal static class MapperService
{
    private static MapperConfiguration Config;
    private static IMapper Mapper;

    static MapperService()
    {
        Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<GetByAltTextOptions, LocatorGetByAltTextOptions>();
            cfg.CreateMap<GetByAltTextOptions, FrameLocatorGetByAltTextOptions>();
            cfg.CreateMap<GetByAltTextOptions, PageGetByAltTextOptions>();

            cfg.CreateMap<GetByLabelOptions, LocatorGetByLabelOptions>();
            cfg.CreateMap<GetByLabelOptions, FrameLocatorGetByLabelOptions>();
            cfg.CreateMap<GetByLabelOptions, PageGetByLabelOptions>();

            cfg.CreateMap<GetByPlaceholderOptions, LocatorGetByPlaceholderOptions>();
            cfg.CreateMap<GetByPlaceholderOptions, FrameLocatorGetByPlaceholderOptions>();
            cfg.CreateMap<GetByPlaceholderOptions, PageGetByPlaceholderOptions>();

            cfg.CreateMap<GetByRoleOptions, LocatorGetByRoleOptions>();
            cfg.CreateMap<GetByRoleOptions, FrameLocatorGetByRoleOptions>();
            cfg.CreateMap<GetByRoleOptions, PageGetByRoleOptions>();

            cfg.CreateMap<GetByTextOptions, LocatorGetByTextOptions>();
            cfg.CreateMap<GetByTextOptions, FrameLocatorGetByTextOptions>();
            cfg.CreateMap<GetByTextOptions, PageGetByTextOptions>();

            cfg.CreateMap<GetByTitleOptions, LocatorGetByTitleOptions>();
            cfg.CreateMap<GetByTitleOptions, FrameLocatorGetByTitleOptions>();
            cfg.CreateMap<GetByTitleOptions, PageGetByTitleOptions>();
        });

        Mapper = Config.CreateMapper();
    }

    public static T ConvertTo<T>(this IOptions options)
    {
        return Mapper.Map<T>(options);
    }
}
