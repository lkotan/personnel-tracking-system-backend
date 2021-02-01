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
using PTS.Models.EmailParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    [IsAdminAspect]
    public class EmailParameterService : IEmailParameterService
    {
        private readonly IDataAccessRepository<EmailParameter> _dal;
        private readonly IMapper _mapper;

        public EmailParameterService(IDataAccessRepository<EmailParameter> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmailParameterModel>> GetAllAsync()
        {
            return _mapper.Map<List<EmailParameterModel>>(await _dal.TableNoTracking.ToListAsync());
        }

        public async Task<EmailParameterModel> GetAsync(int id)
        {
            return _mapper.Map<EmailParameterModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [ValidationAspect(typeof(EmailParameterValidator))]
        public async Task<IDataResponse<int>> InsertAsync(EmailParameterModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<EmailParameter>(model));
        }

        [ValidationAspect(typeof(EmailParameterValidator))]
        public async Task<IResponse> UpdateAsync(EmailParameterModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<EmailParameter>(model));
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
