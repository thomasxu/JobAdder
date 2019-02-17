using System.Collections.Generic;
using JobMatcher.Application.Dtos.Job;

namespace JobMatcher.Application.Interfaces.ApiClients
{
    public interface ICandidateClient
    {
        IList<CandidateDto> GetAll();
    }
}
