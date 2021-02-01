using Microsoft.EntityFrameworkCore;
using PTS.Core.Enums;
using PTS.Core.Helpers;
using PTS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PTS.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder MapConfiguration(this ModelBuilder mb)
        {
            return mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static ModelBuilder SetDataType(this ModelBuilder mb)
        {
            //foreach (var fk in mb.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade))
            //{
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            foreach (var property in mb.Model.GetEntityTypes().SelectMany(x => x.GetProperties().OrderBy(x => x.Name)))
            {
                if (property.ClrType == typeof(bool))
                {
                    property.SetDefaultValue(false);
                }
                else if(property.ClrType == typeof(short))
                {
                    property.SetDefaultValue(0);
                }
                else if (property.ClrType == typeof(string))
                {
                    property.IsNullable = false;
                    property.SetDefaultValueSql("space(0)");
                }
                else if (property.ClrType == typeof(DateTime) && !property.IsNullable)
                {
                    property.SetDefaultValueSql("Convert(Date,GetDate())");
                }
                switch (property.Name)
                {
                    case "Title":
                    case "Name":
                    case "Description":
                        property.SetMaxLength(100);
                        break;

                    case "FirstName":
                    case "LastName":
                        property.SetMaxLength(25);
                        break;

                    case "Email":
                        property.SetMaxLength(75);
                        break;

                    case "Gsm":
                        property.SetMaxLength(11);
                        break;

                    case "ProfilePhoto":
                        property.SetMaxLength(255);
                        break;

                }
            }
            return mb;
        }

        public static ModelBuilder Seed(this ModelBuilder mb)
        {
            HashingHelper.CreatePasswordHash("12345", out var passwordHash, out var passwordSalt);
            mb.Entity<Personnel>().HasData(
               new Personnel
               {
                   Id = 1,
                   RoleId=1,
                   Email = "lutfikotann@gmail.com",
                   FirstName = "Lütfi",
                   LastName = "Kotan",
                   PersonnelType = PersonnelType.Admin,
                   PasswordHash = passwordHash,
                   PasswordSalt = passwordSalt,
                   ProfilePhoto = "",
                   Gsm = "",
                   RefreshToken = Helper.CreateToken(),
                   RefreshTokenExpiredDate = DateTime.Now.AddDays(-1),
               }
            );
            return mb;
        }
    }
}
