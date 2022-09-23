using System.Runtime.Serialization;
using System.Collections.Generic;

namespace CandidateSelection.Model
{
    [DataContract]
    public class CandidateDetailsDTO
    {
        [DataMember(Name = "candidateId")]
        public string CandidateId { get; set; }

        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "experience")]
        public List<Experience> Experience { get; set; }

    }
    
}
