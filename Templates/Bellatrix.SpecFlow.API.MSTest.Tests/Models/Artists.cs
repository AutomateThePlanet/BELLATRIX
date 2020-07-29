using System;
using System.Collections.Generic;

namespace Bellatrix.SpecFlow.API.MSTest.Tests
{
    public class Artists : IEquatable<Artists>
    {
        public Artists() => Albums = new HashSet<Albums>();

        public long ArtistId { get; set; }
        public string Name { get; set; }

        public ICollection<Albums> Albums { get; set; }

        public bool Equals(Artists other) => ArtistId.Equals(other.ArtistId);

        public override bool Equals(object obj) => Equals(obj as Artists);
    }
}
