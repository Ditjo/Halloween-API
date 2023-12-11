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
    public class SuperHeroRepo : ISuperHeroRepo
    {
        Dbcontext context;
        public SuperHeroRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Delete(int id)
        {
            //context.SuperHero.Where(x => x.Id == id)?.ExecuteDelete();
            context.SuperHero.Remove(GetById(id));
            context.SaveChanges();
        }

        public SuperHero Create(SuperHero hero)
        {

            context.SuperHero.Add(hero);
            context.SaveChanges();
            return hero;
        }

        public List<SuperHero> GetAll()
        {
            return context.SuperHero.ToList();
        }

        public SuperHero GetById(int id)
        {
            return context.SuperHero.First(x => x.Id == id);
        }
        public SuperHero GetByName(string name)
        {
            return context.SuperHero.First(x => x.HeroName == name);
        }

        public SuperHero Update(SuperHero hero)
        {

            context.SuperHero.Update(hero);
            context.SaveChanges();
            return hero;
            //context.SuperHero.Where(x => x.Id == hero.Id).ExecuteUpdate(
            //    setters => setters
            //    .SetProperty(b => b.HeroName, hero.HeroName)
            //    .SetProperty(b => b.RealName, hero.RealName)
            //    .SetProperty(b => b.DebutYear, hero.DebutYear));
            //return context.SuperHero.Where(x => x.Id == hero.Id);
        }
    }
}
