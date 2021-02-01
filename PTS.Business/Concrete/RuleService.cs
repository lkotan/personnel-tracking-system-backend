using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Enums;
using PTS.Core.Helpers;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    [IsAdminAspect]
    public class RuleService:IRuleService
    {
        private readonly IDataAccessRepository<Rule> _dal;
        private readonly IMapper _mapper;

        public RuleService(IDataAccessRepository<Rule> dal, IMapper mapper)
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

        public async Task<IEnumerable<RuleListModel>> GetAllAsync(int roleId)
        {
            var entities = await _dal.TableNoTracking.Where(x => x.RoleId == roleId).ToListAsync();

            var applicationModules = EnumHelper.List<ApplicationModule>().Where(x => x.Id > 0);

            var result =
                from m in applicationModules
                join x in entities on m.Id equals (int)x.ApplicationModule into ps
                from x in ps.DefaultIfEmpty()
                select new RuleListModel
                {
                    Id = x?.Id ?? 0,
                    ApplicationModule = (ApplicationModule)m.Id,
                    ModuleName = EnumHelper.GetDisplayValue((ApplicationModule)m.Id),
                    View = x?.View ?? false,
                    Insert = x?.Insert ?? false,
                    Update = x?.Update ?? false,
                    Delete = x?.Delete ?? false,
                };
            return result;
        }

        public async Task<RuleModel> GetAsync(int id)
        {
            return _mapper.Map<RuleModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<IResponse>> SaveRangeAsync(IEnumerable<RuleModel> models)
        {
            var result = new List<IResponse>();
            foreach (var model in models)
            {
                var entity = await _dal.GetAsync(model.Id);
                if (entity == null)
                {
                    entity = new Rule
                    {
                        ApplicationModule = model.ApplicationModule,
                        RoleId = model.RoleId,
                        View = model.View,
                        Insert = model.Insert,
                        Update = model.Update,
                        Delete = model.Delete
                    };
                    result.Add(await _dal.InsertAsync(entity));
                }
                else
                {
                    entity.View = model.View;
                    entity.Insert = model.Insert;
                    entity.Update = model.Update;
                    entity.Delete = model.Delete;
                    result.Add(await _dal.UpdateAsync(entity));
                }

            }
            return result;
        }

        [ValidationAspect(typeof(RuleValidator))]
        public async Task<IDataResponse<int>> InsertAsync(RuleModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<Rule>(model));
        }
        [ValidationAspect(typeof(RuleValidator))]
        public async Task<IResponse> UpdateAsync(RuleModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Rule>(model));
        }
    }
}
