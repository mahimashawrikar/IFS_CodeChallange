using System.Runtime.Serialization;
namespace CandidateSelection.Model
{
    [DataContract]
    public class TechnologyDetails
    {

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "guid")]
        public string guid { get; set; }
    }
}