using Halloween.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Repo.Interfaces
{
    public interface ISuperHeroRepo
    {
        //CRUD
        //TODO: Flere forskellige metoder en disse, så som GetByName
        List<SuperHero> GetAll();
        SuperHero GetById(int id);
        SuperHero GetByName(string name);
        SuperHero Create(SuperHero hero);
        SuperHero Update(SuperHero hero);
        void Delete(int id);
    }
}
