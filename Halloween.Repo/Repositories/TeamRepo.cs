using Halloween.Repo.DTO;
using Halloween.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Repo.Repositories
{
    public class TeamRepo : ITeamRepo
    {
        Dbcontext context;
        public TeamRepo(Dbcontext temp)
        {
            context = temp;
        }

        public async Task<Team> Create(Team team)
        {
            context.Team.Add(team);
            await context.SaveChangesAsync();
            return team;
        }

        public void Delete(int id)
        {
            //context.Team.Where(x => x.Id == id)?.ExecuteDelete();
            context.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Team> GetAll()
        {
            return context.Team.Include(x => x.SuperHeros).ToList();
        }

        public Team GetById(int id)
        {
            return context.Team.First(x => x.Id == id);
        }

        public Team GetByName(string name)
        {
            return context.Team.First(x => x.TeamName == name);
        }

        public Team Update(Team team)
        {
            context.Team.Update(team);
            context.SaveChanges();
            return team;
        }
    }
}
