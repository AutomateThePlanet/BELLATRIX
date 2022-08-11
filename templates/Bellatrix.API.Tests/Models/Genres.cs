using System.Collections.Generic;

namespace Bellatrix.API.MSTest.Tests.Models;

public class Genres
{
    public Genres() => Tracks = new HashSet<Tracks>();

    public long GenreId { get; set; }
    public string Name { get; set; }

    public ICollection<Tracks> Tracks { get; set; }
}
