using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs
{
    public interface IFaqsService : IDisposable
    {
        Task<Result> CreateOrUpdateFaqs(FaqsSetup request);
        Task<PagedList<FaqsVM>> GetFaqs(GetFaqs request);
        Task<Result> CreateOrUpdateGeneralQuestion(GeneralQuestion request);
        Task<PagedList<GeneralQuestionVM>> GetGeneralQuestions(GetGeneralQuestions request);
    }
}
