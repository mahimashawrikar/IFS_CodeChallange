using CandidateSelection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSelection
{ 
        public interface IRecruit
        {
            Task<IEnumerable<String>> GetTechnologyNames();
            IEnumerable<CandidateDetailsDTO> GetCandidateDetails(string technology, string yrsOfExperience);
            List<CandidateDetailsDTO> GetSelectedCandidates(string ids);
            void RemoveCandidates(string selectedId, string rejectedId);

        }    
}
