// <copyright file="ComponentPromptRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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

// <copyright file="ComponentPromptRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.LLM;

namespace Bellatrix.Web.LLM.Extensions;

public static class ComponentPromptRepositoryExtensions
{
    public static TComponent CreateByPrompt<TComponent>(this ComponentCreateService repository, string instruction, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindByPrompt>(new FindByPrompt(instruction), shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByPrompt<TComponent>(this ComponentCreateService repository, string instruction, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindByPrompt(instruction), null, shouldCacheFoundElements);
}