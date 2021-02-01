using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Models;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.AssigmentTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    public class AssigmentTagService : IAssigmentTagService
    {
        private readonly IDataAccessRepository<AssigmentTag> _dal;
        private readonly IMapper _mapper;

        public AssigmentTagService(IDataAccessRepository<AssigmentTag> dal,IMapper mapper)
        {
            _dal = dal;

            _mapper = mapper;
        }

        [IsAdminAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }

        [IsAdminAspect]
        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }

        [SecurityAspect]
        public async Task<IEnumerable<AssigmentTagModel>> GetAllAsync()
        {
            return _mapper.Map<List<AssigmentTagModel>>(await _dal.TableNoTracking.ToListAsync());
        }

        [SecurityAspect]
        public async Task<AssigmentTagModel> GetAsync(int id)
        {
            return _mapper.Map<AssigmentTagModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x=>x.Id==id));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(AssigmentTagValidator))]
        public async Task<IDataResponse<int>> InsertAsync(AssigmentTagModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<AssigmentTag>(model));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(AssigmentTagValidator))]
        public async Task<IResponse> UpdateAsync(AssigmentTagModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<AssigmentTag>(model));
        }

        [IsAdminAspect]
        public async Task<IEnumerable<DropdownModel>> SelectListAsync()
        {
            var entities = await _dal.TableNoTracking.OrderBy(x => x.Name).ToListAsync();

            return entities.Select(x => new DropdownModel
            {
                Id = x.Id,
                Description = x.Name
            });
        }

    }
}
