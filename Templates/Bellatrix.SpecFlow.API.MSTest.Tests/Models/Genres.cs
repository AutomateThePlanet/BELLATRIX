using System.Collections.Generic;

namespace Bellatrix.SpecFlow.API.MSTest.Tests
{
    public class Genres
    {
        public Genres() => Tracks = new HashSet<Tracks>();

        public long GenreId { get; set; }
        public string Name { get; set; }

        public ICollection<Tracks> Tracks { get; set; }
    }
}
