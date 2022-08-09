using System;
using System.Collections.Generic;

namespace Bellatrix.Utilities;

public class Email : IComparer<Email>
{
    public string cc { get; set; }
    public DateTime date { get; set; }
    public List<object> attachments { get; set; }
    public string dkim { get; set; }
    public string envelope_to { get; set; }
    public string subject { get; set; }
    public string SPF { get; set; }
    public string messageId { get; set; }
    public List<ToParsed> to_parsed { get; set; }
    public string oid { get; set; }
    public string envelope_from { get; set; }
    public List<object> cc_parsed { get; set; }
    public string @namespace { get; set; }
    public string from { get; set; }
    public string sender_ip { get; set; }
    public string html { get; set; }
    public string to { get; set; }
    public string tag { get; set; }
    public string text { get; set; }
    public List<FromParsed> from_parsed { get; set; }
    public object timestamp { get; set; }
    public string id { get; set; }
    public string downloadUrl { get; set; }

    public int Compare(Email x, Email y)
    {
        return x.date.CompareTo(y.date);
    }
}
