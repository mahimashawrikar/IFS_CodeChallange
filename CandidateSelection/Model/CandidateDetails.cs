
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace CandidateSelection.Model
{
    [DataContract]
    public class CandidateDetails
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
    public class Experience
    {
        [DataMember(Name = "technologyId")]
        public string TechnologyId { get; set; }

        [DataMember(Name = "yearsOfExperience")]
        public int YearsOfExperience { get; set; }
    }
}
