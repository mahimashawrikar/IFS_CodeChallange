
using AutoMapper;
using CandidateSelection.Model;

namespace CandidateSelection.Mapper
{
    public class RecruitMappings : Profile
    {
       public RecruitMappings()
        {
            CreateMap<CandidateDetails, CandidateDetailsDTO>().ReverseMap();           
        }
    }
}
