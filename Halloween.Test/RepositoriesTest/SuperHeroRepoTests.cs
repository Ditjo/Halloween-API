using Halloween.Repo.DTO;
using Halloween.Repo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Test.RepositoriesTest
{
    /// <summary>
    /// We Want to take the DB offline, so we can focus on Repo Layer
    /// To do that we use InMemoryDB
    /// The way we does that is
    /// "copy Dbcontext and Options"
    /// 
    /// </summary>
    public class SuperHeroRepoTests
    {
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private SuperHeroRepo heroRepo;
        //params later...

        public SuperHeroRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase("Torsdag").Options;//TODO: "Torsdag" is a random name. Change it to something else if there are Errors
            // we init our "DB"
            context = new Dbcontext(options);
            heroRepo = new SuperHeroRepo(context);
            //SuperHero hero1 = new SuperHero()
            //{
            //    Id = 1,
            //    HeroName = "Normal Man",
            //    RealName = "John Doe",
            //    Place = "Centralia",
            //    DebutYear = DateTime.Now,
            //};
            //SuperHero hero2 = new SuperHero()
            //{
            //    Id = 2,
            //    HeroName = "Normal Woman",
            //    RealName = "Jane Doe",
            //    Place = "Centralia",
            //    DebutYear = DateTime.Now,
            //}; 
            //SuperHero hero3 = new SuperHero()
            //{
            //    Id = 3,
            //    HeroName = "Good Boy",
            //    RealName = "Fido the Dog",
            //    Place = "Centralia",
            //    DebutYear = DateTime.Now,
            //};
            //context.SuperHero.Add(hero1);
            //context.SuperHero.Add(hero2);
            //context.SuperHero.Add(hero3);
            //context.SaveChanges();
            
        }

        [Fact]
        public void SuperHeroRepo_GetListOfSuperHeroes_OnSucces()
        {
            //Arrange - Create objects / variables / somethings
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetSuperHeroData(1));
            context.Add(MockDataMethods.GetSuperHeroData(2));
            context.Add(MockDataMethods.GetSuperHeroData(3));
            context.SaveChanges();
            //heroRepo = new SuperHeroRepo(context);

            //Act - actions (Handelings)
            //Invoke GetAll.......
            var result = heroRepo.GetAll();
            //Assert - Testing if our results is what we want it to be
            Assert.NotNull(result);
            Assert.IsType<List<SuperHero>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void SuperHeroRepo_GetOneHeroById_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetSuperHeroData(1));
            context.Add(MockDataMethods.GetSuperHeroData(2));
            context.Add(MockDataMethods.GetSuperHeroData(3));
            context.SaveChanges();
            //heroRepo = new SuperHeroRepo(context);
            int id = 1;
            int wrongId = 4;
            string errorMesssage = "Sequence contains no elements";
            //Act
            var result1 = heroRepo.GetById(id);
            Action result = () => heroRepo.GetById(wrongId);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(result);
            //Assert
            Assert.Equal(1, result1.Id);
            Assert.Equal(errorMesssage, exception.Message);
        }
        [Fact]
        public void SuperHeroRepo_DeleteOnHero_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetSuperHeroData(1));
            context.Add(MockDataMethods.GetSuperHeroData(2));
            context.Add(MockDataMethods.GetSuperHeroData(3));
            context.SaveChanges();
            //heroRepo = new SuperHeroRepo(context);
            int id = 1;
            //Act
            var beforeDelete = heroRepo.GetAll();
            heroRepo.Delete(id);
            var afterDelete = heroRepo.GetAll();
            //Assert
            Assert.NotEqual(beforeDelete.Count, afterDelete.Count);
            Assert.Equal(2, afterDelete.Count);

        }
        [Fact]
        public void SuperHeroRepo_CreateNewHero_OnSucces()
        {
            //Arrange
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetSuperHeroData(1));
            context.Add(MockDataMethods.GetSuperHeroData(2));
            context.Add(MockDataMethods.GetSuperHeroData(3));
            context.SaveChanges();
            //heroRepo = new SuperHeroRepo(context);

            int id = 4;
            string heroName = "Good Girl";
            string RealName = "Molly the Cat";
            string place = "London";
            DateTime date = DateTime.Now;

            //Act
            SuperHero newHero = new SuperHero()
            {
                Id = id,
                HeroName = heroName,
                RealName = RealName,
                Place = place,
                DebutYear = date,
            };
            heroRepo.Create(newHero);
            context.SaveChanges();

            var result = heroRepo.GetAll();
            //Assert
            Assert.Equal(id, newHero.Id);
            Assert.Equal(heroName, newHero.HeroName);
            Assert.Equal(RealName, newHero.RealName);
            Assert.Equal(place, newHero.Place);
            Assert.Equal(date, newHero.DebutYear);

            Assert.Equal(4, result.Count);
        }
        
        //Arrange

        //Act

        //Assert
    }
}
