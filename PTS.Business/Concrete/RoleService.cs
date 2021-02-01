using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    [IsAdminAspect]
    public class RoleService : IRoleService
    {
        private readonly IDataAccessRepository<Role> _dal;
        private readonly IMapper _mapper;

        public RoleService(IDataAccessRepository<Role> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }


        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }

        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            return _mapper.Map<List<RoleModel>>(await _dal.TableNoTracking.Where(x => !x.IsBlocked).ToListAsync());
        }

        public async Task<RoleModel> GetAsync(int id)
        {
            return _mapper.Map<RoleModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [ValidationAspect(typeof(RoleValidator))]
        public async Task<IDataResponse<int>> InsertAsync(RoleModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<Role>(model));
        }

        [ValidationAspect(typeof(RoleValidator))]
        public async Task<IResponse> UpdateAsync(RoleModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Role>(model));
        }

        public async Task<IResponse> IsBlockedChangeAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            entity.IsBlocked = !entity.IsBlocked;
            return await _dal.UpdateAsync(entity);
        }
   
    }
}
