using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bellatrix.Core.utilities.mail.model
{
    public class Root
    {
        public string result { get; set; }
        public object message { get; set; }
        public int count { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<Email> emails { get; set; }
    }
}
