using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.Contracts;
public interface IInputParameter
{
    List<TestValue> TestValues { get; }
}