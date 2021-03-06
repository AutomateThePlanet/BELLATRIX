﻿// <copyright file="Albums.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaStore.Demo.API.Models
{
    public class Albums : IEquatable<Albums>
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

        public override bool Equals(object obj) => Equals(obj as Albums);
    }
}
