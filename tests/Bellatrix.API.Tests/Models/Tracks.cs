// <copyright file="Tracks.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;

namespace MediaStore.Demo.API.Models;

public class Tracks
{
    public Tracks()
    {
        InvoiceItems = new HashSet<InvoiceItems>();
        PlaylistTrack = new HashSet<PlaylistTrack>();
    }

    public long TrackId { get; set; }
    public string Name { get; set; }
    public long? AlbumId { get; set; }
    public long MediaTypeId { get; set; }
    public long? GenreId { get; set; }
    public string Composer { get; set; }
    public long Milliseconds { get; set; }
    public long? Bytes { get; set; }
    public string UnitPrice { get; set; }

    public Albums Album { get; set; }
    public Genres Genre { get; set; }
    public MediaTypes MediaType { get; set; }
    public ICollection<InvoiceItems> InvoiceItems { get; set; }
    public ICollection<PlaylistTrack> PlaylistTrack { get; set; }
}
