using AutoMapper;
using CandidateSelection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSelection
{
    public class RecruitRepository : IRecruit
    {

        public ExtractJson getdetails;
        public IEnumerable<CandidateDetailsDTO> allSelectedCandidates;
        public List<CandidateDetailsDTO> selectedCandidates;
        public CandidateDetailsDTO candidateDetailsDTO;
        public string[] listOfId;
        private readonly IMapper _mp;

        public RecruitRepository(IMapper mp)
        {
            _mp = mp;
            getdetails = new ExtractJson();
            selectedCandidates = new List<CandidateDetailsDTO>();          
        }

        public async Task InitializeRecruitRepository()
        {          
            await getdetails.GetDetailsFromJson();
        }

        public async Task<IEnumerable<String>> GetTechnologyNames()
        {
            await InitializeRecruitRepository();
            var result = getdetails.technology.Select(x => x.name);
            return result;
        }
        public IEnumerable<CandidateDetailsDTO> GetCandidateDetails(string technology, string yrsOfExperience)
        {
            try
            {
                string ids = String.Empty;
                var techname = getdetails.technology.FirstOrDefault(x => x.name == technology);

                if (techname == null)
                    return null;

                allSelectedCandidates = _mp.Map<IEnumerable<CandidateDetailsDTO>>(getdetails.candidates.Where(x => x.Experience.Any(y => y.TechnologyId == (techname.guid.ToString()) && y.YearsOfExperience == Convert.ToInt32(yrsOfExperience))));
                using (StreamReader sr = new StreamReader(getdetails.storedDataPath))
                {
                    ids = sr.ReadToEnd();
                }

                listOfId = SplitIds(ids);
                if (listOfId != null)
                {
                    var result = from can in getdetails.candidates
                                 join li in listOfId on can.CandidateId equals li
                                 select new
                                 {
                                     CandidateId = can.CandidateId
                                 };

                    if (result.Count() > 0)
                    {
                        foreach (var id in result)
                        {
                            allSelectedCandidates = allSelectedCandidates.Where(x => x.CandidateId != id.CandidateId.ToString()); //x => id.ToString().Contains(x.candidateId)
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetCandidateDetails : {0}", ex.Message);
            }
            return allSelectedCandidates;
        }

        public List<CandidateDetailsDTO> GetSelectedCandidates(string ids)
        {
            try
            {
                listOfId =SplitIds(ids);
                

                for (int i = 0; i < listOfId.Length; i++)
                {            
                    selectedCandidates.AddRange(_mp.Map<IEnumerable<CandidateDetailsDTO>> (getdetails.candidates.Where(x => x.CandidateId == listOfId[i]).ToList()));
                }
             
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetSelectedCandidates : {0}", ex.Message);
            }

            return selectedCandidates;
        }

        public void RemoveCandidates(string selectedId, string rejectedId)
        {
            try
            {
                listOfId = SplitIds(rejectedId);
                if (listOfId != null)
                {
                    var result = from can in allSelectedCandidates
                                 join li in listOfId on can.CandidateId equals li
                                 select new
                                 {
                                     CandidateId = can.CandidateId
                                 };
                    if (result.Count() > 0)
                    {
                        Console.WriteLine("\nYou have entered same ids in selected and rejected candidates.Please restart the search.");
                        return;
                    }
                }
               

                selectedId = selectedId.Trim() + " " + rejectedId+" ";
                
                using (StreamWriter writer = File.AppendText(getdetails.storedDataPath))
                {
                    writer.WriteLine(selectedId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in RemoveCandidates : {0}", ex.Message);
            }
        }

        public string[] SplitIds(string ids)
        {
            try
            {
                listOfId = null;
                ids = ids.Trim();
                if (!string.IsNullOrWhiteSpace(ids) && ids.Contains(' '))
                {
                    listOfId = ids.Split(' ');
                }
                else if (ids.Length > 0)
                {
                    listOfId = new string[1] { ids };

                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SplitIds : {0}", ex.Message);
            }

            return listOfId;
        }
    }
}

