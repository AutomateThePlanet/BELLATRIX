using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration;

public class TestValueBuilder<T> where T : IInputParameter
{
    private readonly ParameterBuilder<T> _parentBuilder;
    private readonly object _value;
    private readonly TestValueCategory _category;

    public TestValueBuilder(ParameterBuilder<T> parentBuilder, object value, TestValueCategory category)
    {
        _parentBuilder = parentBuilder;
        _value = value;
        _category = category;
    }

    public ParameterBuilder<T> WithExpectedMessage(string message)
    {
        _parentBuilder.Add(new TestValue(_value, _category, message));
        return _parentBuilder;
    }

    public ParameterBuilder<T> WithoutMessage()
    {
        _parentBuilder.Add(new TestValue(_value, _category));
        return _parentBuilder;
    }
}
