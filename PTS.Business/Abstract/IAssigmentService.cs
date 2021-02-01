using PTS.Core.Enums;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.Result;
using PTS.Models.Assigment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Abstract
{
    public interface IAssigmentService : IServiceRepository<AssigmentModel>
    {
        Task<List<AssigmentListModel>> GetAllAsync(int? personnelId, AssigmentStatus? statusId, string keyword);
        Task<IResponse> ChangePriorityAsync(int assigmentId, short priority);
        Task<List<AssigmentListModel>> GetAllMyAssigmentAsync();
        Task<IResponse> ChangeMyStatusAsync(int assigmentId,AssigmentStatus status);
    }
}
