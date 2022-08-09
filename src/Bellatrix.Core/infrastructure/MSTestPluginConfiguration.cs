using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Assertions;
using Bellatrix.Assertions.MSTest;

namespace Bellatrix;

public static class MSTestPluginConfiguration
{
    public static void Add()
    {
        ServicesCollection.Current.RegisterType<IAssert, MsTestAssert>();
        ServicesCollection.Current.RegisterType<ICollectionAssert, MsTestCollectionAssert>();
    }
}
