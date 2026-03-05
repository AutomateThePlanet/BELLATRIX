namespace Bellatrix.Desktop.Events;

using System;

public class CapabilityValueResolvingEventArgs : EventArgs
{
    public string RawValue { get; }
    public Type TestClassType { get; }
    public object ResolvedValue { get; set; }
    public bool Handled { get; set; }

    public CapabilityValueResolvingEventArgs(string rawValue, Type testClassType)
    {
        RawValue = rawValue;
        TestClassType = testClassType;
    }
}