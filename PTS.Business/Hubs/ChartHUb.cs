using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PTS.Core.Repositories;
using PTS.Entities;
using PTS.Models.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Business.Hubs
{
    public class ChartHub:Hub
    {
        private readonly IDataAccessRepository<Personnel> _dalPersonnel;
        public ChartHub(IDataAccessRepository<Personnel> dalPersonnel)
        {
            _dalPersonnel = dalPersonnel;
        }

        public async Task SendChart()
        {
            var chart= await _dalPersonnel.TableNoTracking
                .Where(x => x.Id != 1)
                .Select(x => new DashChartModel
                {
                    PersonnelFullName = $"{x.FirstName} {x.LastName}",
                    AssigmentCount = x.Assigments.Count,
                }).ToListAsync();

            await Clients.All.SendAsync("ReceiveChart", chart);
        }


    }
}
