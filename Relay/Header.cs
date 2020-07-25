using System;
using System.Runtime.Serialization;

namespace ApiRelay
{
    [Serializable]
    [DataContract]
    public class Header
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
