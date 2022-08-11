using System;
using System.Collections.Generic;

namespace Bellatrix.Assertions;

public class MultipleAssertException : AggregateException
{
    public MultipleAssertException(List<Exception> exceptions)
        : base(exceptions)
    {
    }

    public override string Message => string.Empty;
}