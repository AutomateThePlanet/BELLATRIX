using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Bellatrix.Assertions;

namespace Bellatrix.CognitiveServices.services
{
    public class AssertedTableFormLine
    {
        public static event EventHandler<string> TableFormLineAsserted;
        private readonly FormLine _formLine;

        public AssertedTableFormLine(FormLine formLine)
        {
            _formLine = formLine;
        }

        public List<string> Words => _formLine.Words.Select(x => x.Text).ToList();
        public FieldBoundingBox BoundingBox => _formLine.BoundingBox;
        public FormLine WrappedFormLine => _formLine;

        public void AssertWordsCount(int expectedWordsCount)
        {
            TableFormLineAsserted?.Invoke(this, $"Assert line words' count = {expectedWordsCount}");
            Assert.AreEqual(expectedWordsCount, _formLine.Words.Count, $"Line words' count != {expectedWordsCount}");
        }

        public void AssertWordsEqual(params string[] expectedWords)
        {
            TableFormLineAsserted?.Invoke(this, $"Assert line words are {expectedWords}");
            CollectionAssert.AreEqual(expectedWords, Words, "Expected words are different than the actual ones present on the line.");
        }

        public void AssertWordsContain(params string[] expectedWords)
        {
            TableFormLineAsserted?.Invoke(this, $"Assert line words contain the words: {expectedWords}");
            CollectionAssert.IsSubsetOf(expectedWords, Words, "Expected words are different than the actual ones present on the line.");
        }
    }
}
