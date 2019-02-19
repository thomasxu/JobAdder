using System.Collections.Generic;
using JobAdder.Domain.ApiClients.Jobs.Response;
using JobMatcher.Application.Dtos.Job;

namespace JobMatcher.Application.Interfaces.ApiClients
{
    public interface ICandidateClient
    {
        IList<CandidateResponse> GetAll();
    }
}
