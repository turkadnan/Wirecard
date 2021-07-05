using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.CommandFramework
{
    [DataContract]
    public class CommandResult<TOutput> where TOutput : class
    {
        [DataMember]
        public int Result { get; set; }
        [DataMember]
        public string ErrorCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ReferenceId { get; set; }
        [DataMember]
        public TOutput OutputObject { get; set; }
    }
}
