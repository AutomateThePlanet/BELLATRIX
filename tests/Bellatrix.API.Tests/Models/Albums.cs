// <copyright file="Albums.cs" company="Automate The Planet Ltd.">
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
using Newtonsoft.Json;

namespace MediaStore.Demo.API.Models;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class Albums : IEquatable<Albums>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public Albums() => Tracks = new HashSet<Tracks>();

    [JsonProperty(Required = Required.Always)]
    public long AlbumId { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public string Title { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public long ArtistId { get; set; }

    [JsonProperty(Required = Required.AllowNull)]
    public Artists Artist { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public ICollection<Tracks> Tracks { get; set; }

    public bool Equals(Albums other) => AlbumId.Equals(other.AlbumId);

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object obj) => Equals(obj as Albums);
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
}