using Halloween.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Repo.Interfaces
{
    public interface ITeamRepo
    {
        List<Team> GetAll();
        Team GetById(int id);
        Team GetByName(string name);
        Task<Team> Create(Team team);
        Team Update(Team team);
        void Delete(int id);
    }
}
