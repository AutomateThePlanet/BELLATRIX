using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Assertions;
using Bellatrix.Assertions.NUnit;
using Bellatrix.NUnit;

namespace Bellatrix.Core.infrastructure
{
    public class NUnitPluginConfiguration
    {
        public static void UseNUnitSettings()
        {
            ServicesCollection.Current.RegisterType<IAssert, NUnitAssert>();
            ServicesCollection.Current.RegisterType<ICollectionAssert, NUnitCollectionAssert>();
        }
    }
}
