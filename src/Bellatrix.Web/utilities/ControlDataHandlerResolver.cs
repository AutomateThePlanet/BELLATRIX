using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix.Web;

public static class ControlDataHandlerResolver
{
    public static dynamic ResolveReadonlyDataHandler(Type controlType)
    {
        Type controlDataHandler = typeof(IReadonlyControlDataHandler<>);
        Type typeSpecificControlDataHandler = controlDataHandler.MakeGenericType(controlType);
        dynamic resolvedControlDataHandler = ServicesCollection.Current.Resolve(typeSpecificControlDataHandler);

        if (resolvedControlDataHandler == null)
        {
            resolvedControlDataHandler = ResolveEditableDataHandler(controlType);
        }

        if (resolvedControlDataHandler == null)
        {
            throw new Exception($"Cannot find proper IControlDataHandler for type: {controlType.Name}. Make sure it is registered in the ServiceContainer");
        }

        return resolvedControlDataHandler;
    }

    public static dynamic ResolveEditableDataHandler(Type controlType)
    {
        Type editableControlDataHandler = typeof(IEditableControlDataHandler<>);
        Type typeSpecificEditableControlDataHandler = editableControlDataHandler.MakeGenericType(controlType);
        dynamic resolvedControlDataHandler = ServicesCollection.Current.Resolve(typeSpecificEditableControlDataHandler);

        return resolvedControlDataHandler;
    }
}
