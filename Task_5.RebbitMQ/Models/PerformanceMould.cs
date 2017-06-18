using System;
using System.Runtime.Serialization;

namespace Task_5.Models
{
    [Serializable]
    [DataContract]
    public class PerformanceMould
    {
        [DataMember]
        public double Cpu { get; set; }

        [DataMember]
        public double Ram { get; set; }
    }
}
