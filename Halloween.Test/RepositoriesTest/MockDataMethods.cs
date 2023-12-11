using Halloween.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Test.RepositoriesTest
{
    internal class MockDataMethods
    {
        public static Team GetTeamData(int id)
        {
            Team team = new Team()
            {
                Id = id,
                TeamName = $"Team-{id}"
            };
            return team;
        }

        public static SuperHero GetSuperHeroData(int id)
        {
            SuperHero superHero = new SuperHero()
            {
                Id = id,
                HeroName = $"Hero-{id}",
                RealName = $"Name-{id}",
                DebutYear = DateTime.Now
            };
            return superHero;
        }
    }
}
