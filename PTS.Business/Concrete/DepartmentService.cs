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
using PTS.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDataAccessRepository<Department> _dal;
        private readonly IMapper _mapper;

        public DepartmentService(IDataAccessRepository<Department> dal, IMapper mapper)
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
        public async Task<IEnumerable<DepartmentModel>> GetAllAsync()
        {
            return _mapper.Map<List<DepartmentModel>>(await _dal.TableNoTracking.ToListAsync());
        }

        [SecurityAspect]
        public async Task<DepartmentModel> GetAsync(int id)
        {
            return _mapper.Map<DepartmentModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(DepartmentValidator))]
        public async Task<IDataResponse<int>> InsertAsync(DepartmentModel model)
        {
            return await _dal.InsertAsync(_mapper.Map<Department>(model));
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(DepartmentValidator))]
        public async Task<IResponse> UpdateAsync(DepartmentModel model)
        {
            return await _dal.UpdateAsync(_mapper.Map<Department>(model));
        }

        [SecurityAspect]
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
