namespace Bellatrix.API.GettingStarted.Models;

public class PlaylistTrack
{
    public long PlaylistId { get; set; }
    public long TrackId { get; set; }

    public Playlists Playlist { get; set; }
    public Tracks Track { get; set; }
}
