using Bellatrix.DataGeneration.Core.Parameters;

namespace Bellatrix.DataGeneration.Core.Contracts;
public interface IInputParameter
{
    List<TestValue> TestValues { get; }
}