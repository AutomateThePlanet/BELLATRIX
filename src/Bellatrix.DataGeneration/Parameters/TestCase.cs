using System;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.Models
{
    public class TestCase
    {
        public List<TestValue> Values { get; set; } = new List<TestValue>();
        public double Score { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not TestCase other) return false;

            // Ensure order consistency when comparing
            return Values.SequenceEqual(other.Values);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var value in Values) // No need for OrderBy() unless order varies
                {
                    hash = hash * 31 + value.GetHashCode();
                }
                return hash;
            }
        }
    }
}
