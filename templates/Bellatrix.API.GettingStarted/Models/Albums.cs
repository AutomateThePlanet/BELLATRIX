using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bellatrix.API.GettingStarted.Models;

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