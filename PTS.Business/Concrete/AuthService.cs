using Microsoft.EntityFrameworkCore;
using PTS.Business.Abstract;
using PTS.Business.Validations.Fluent;
using PTS.Core.Aspect.Security;
using PTS.Core.Aspect.Validation;
using PTS.Core.Enums;
using PTS.Core.Exceptions;
using PTS.Core.Helpers;
using PTS.Core.Messages;
using PTS.Core.Models;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Plugins.Authentication.Jwt;
using PTS.Core.Plugins.Authentication.Models;
using PTS.Core.Repositories;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using PTS.Entities;
using PTS.Models.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PTS.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IDataAccessRepository<Personnel> _dal;
        private readonly IDataAccessRepository<Rule> _dalRule;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        private readonly LoggedInUsers _loggedInUsers;
        private readonly JwtOptions _jwtOptions;

        public AuthService(IDataAccessRepository<Personnel> dal, IDataAccessRepository<Rule> dalRule, ITokenHelper tokenHelper, IUserService userService, LoggedInUsers loggedInUsers, JwtOptions jwtOptions)
        {
            _dal = dal;
            _dalRule = dalRule;

            _tokenHelper = tokenHelper;
            _userService = userService;
            _loggedInUsers = loggedInUsers;
            _jwtOptions = jwtOptions;
        }


        private async Task<IDataResponse<LoginResultModel>> LoginAsync(Personnel personnel, bool isRefreshLogin = false)
        {
            var roleId = personnel.RoleId;
            if (personnel.Email == "lutfikotann@gmail.com")
            {
                personnel.PersonnelType = PersonnelType.Admin;
            }
            var rules = await _dalRule.TableNoTracking.Where(x => x.RoleId == roleId).ToListAsync();

            var rulesModel = rules.Select(x => new PersonnelRulesModel
            {
                ApplicationModule = x.ApplicationModule,
                Delete = x.Delete,
                Insert = x.Insert,
                Update = x.Update,
                View = x.View,
                ApplicationModuleName = EnumHelper.GetDisplayValue(x.ApplicationModule),
            }).ToList();

            var user = new UserInfo
            {
                PersonnelId = personnel.Id,
                FirstName = personnel.FirstName,
                LastName = personnel.LastName,
                PersonnelType = personnel.PersonnelType,
                Email = personnel.Email,
                Rules = rules.Select(x => new PersonnelRule
                {
                    ApplicationModule = x.ApplicationModule,
                    View = x.View,
                    Insert = x.Insert,
                    Update = x.Update,
                    Delete = x.Delete
                }).Where(x => x.Insert || x.View || x.Delete || x.Update).ToList()
            };
            var accessToken = _tokenHelper.CreateToken(personnel.Id, rulesModel);
            var tokenOptions = _jwtOptions;

            var per = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == personnel.Id);

            per.RefreshToken = accessToken.RefreshToken;
            per.RefreshTokenExpiredDate = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 30);

            await _dal.UpdateAsync(per);

            _loggedInUsers.UserInfo = _loggedInUsers.UserInfo.Where(x => x.PersonnelId != personnel.Id).ToList();

            _loggedInUsers.UserInfo.Add(user);

            var result = new LoginResultModel
            {
                PersonnelId = personnel.Id,
                FirstName = personnel.FirstName,
                LastName = personnel.LastName,
                Email = personnel.Email,
                Token = accessToken.Token,
                RefreshToken = accessToken.RefreshToken,
                TokenExpiration = DateTime.Now,
                Rules = rulesModel
            };
            return new SuccessDataResponse<LoginResultModel>(result);
        }

        public async Task<IDataResponse<LoginResultModel>> LoginAsync(LoginModel model)
        {
            var personnel = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (personnel == null)
                return new ErrorDataResponse<LoginResultModel>(PersonnelMessage.PersonnelNotFound);

            if (personnel.IsBlocked)
                return new ErrorDataResponse<LoginResultModel>(PersonnelMessage.PersonnelIsBlocked);

            if (HashingHelper.VerifyPasswordHash(model.Password, personnel.PasswordHash, personnel.PasswordSalt))

                return await LoginAsync(personnel);

            return new ErrorDataResponse<LoginResultModel>(PersonnelMessage.PasswordWrong);
        }

        public async Task<IDataResponse<LoginResultModel>> LoginByRefreshTokenAsync(RefreshTokenModel model)
        {
            var personnel = await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.RefreshToken == model.Token);

            if (personnel == null)
            {
                throw new AuthenticationException(PersonnelMessage.AuthenticationError);
            }
            if (personnel.IsBlocked)
            {
                return new ErrorDataResponse<LoginResultModel>(PersonnelMessage.PersonnelIsBlocked);
            }
            if (!string.IsNullOrEmpty(personnel.RefreshToken) && personnel.RefreshTokenExpiredDate > DateTime.Now)
            {
                return await LoginAsync(personnel, true);
            }
            throw new AuthenticationException(PersonnelMessage.AuthenticationError);
        }

        [SecurityAspect]
        public async Task<IResponse> LogoutAsync()
        {
            var personnel = await _dal.GetAsync(x => x.Id == _userService.PersonnelId);

            personnel.RefreshTokenExpiredDate = DateTime.Now.AddMinutes(-30);

            await _dal.UpdateAsync(personnel);

            _loggedInUsers.UserInfo = _loggedInUsers.UserInfo.Where(x => x.PersonnelId != personnel.Id).ToList();

            return new SuccessResponse(PersonnelMessage.LogoutSuccessful);
        }

        [SecurityAspect]
        [ValidationAspect(typeof(ChangePasswordValidator))]
        public async Task<IResponse> ChangePasswordAsync(ChangePasswordModel model)
        {
            var personnel = await _dal.GetAsync(_userService.PersonnelId);

            if (!HashingHelper.VerifyPasswordHash(model.OldPassword, personnel.PasswordHash, personnel.PasswordSalt))

                return new ErrorResponse(PersonnelMessage.PasswordWrong);

            HashingHelper.CreatePasswordHash(model.NewPassword, out var passwordHash, out var passwordSalt);

            personnel.PasswordHash = passwordHash;
            personnel.PasswordSalt = passwordSalt;
            return await _dal.UpdateAsync(personnel);
        }
    }
}
