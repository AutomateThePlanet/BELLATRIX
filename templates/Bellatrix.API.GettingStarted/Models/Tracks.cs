using System.Collections.Generic;

namespace Bellatrix.API.GettingStarted.Models;

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
