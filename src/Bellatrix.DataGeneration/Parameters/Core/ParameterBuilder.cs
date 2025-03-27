using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration;

public class ParameterBuilder<TDataParameter> where TDataParameter : IInputParameter
{
    private readonly List<TestValue> _values = new();

    public ParameterBuilder<TDataParameter> Valid(object val)
    {
        _values.Add(new TestValue(val, TestValueCategory.Valid));
        return this;
    }

    public ParameterBuilder<TDataParameter> BoundaryValid(object val)
    {
        _values.Add(new TestValue(val, TestValueCategory.BoundaryValid));
        return this;
    }

    public TestValueBuilder<TDataParameter> BoundaryInvalid(object val)
    {
        return new TestValueBuilder<TDataParameter>(this, val, TestValueCategory.BoundaryInvalid);
    }

    public TestValueBuilder<TDataParameter> Invalid(object val)
    {
        return new TestValueBuilder<TDataParameter>(this, val, TestValueCategory.Invalid);
    }

    public TDataParameter Build()
    {
        var parameter = (TDataParameter)Activator.CreateInstance(
            typeof(TDataParameter),
            true,  // preciseMode
            false, // allowValidEquivalenceClasses
            false, // allowInvalidEquivalenceClasses
            false, // includeBoundaryValues
            _values.ToArray() // preciseTestValues
        );

        return parameter;
    }

    internal void Add(TestValue value) => _values.Add(value);
}
