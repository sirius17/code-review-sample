using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApiRelay
{
    [DataContract]
    [Serializable]
    public class RelayRequest
    {
        private string _method;

        [DataMember(Name = "method")]
        public string Method
        {
            get { return _method ?? "GET"; }
            set { _method = value?.ToUpper(); }
        }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "body")]
        public string body { get; set; }

        [DataMember(Name = "httpHeaders")]
        public List<Header> Headers { get; set; }
    }
}
