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
    public class TeamRepoTests
    {
        public Dbcontext context { get; set; }
        public DbContextOptions<Dbcontext> options { get; set; }

        private readonly TeamRepo teamRepo;
        public TeamRepoTests()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
            .UseInMemoryDatabase("Lørdag").Options;

            context = new Dbcontext(options);
            teamRepo = new TeamRepo(context);
        }

        [Fact]
        public async void TeamRepo_CreateNewTeam_OnSuccess()
        {
            //Arrange
            //Dbcontext context = new Dbcontext(options);
            //TeamRepo teamRepo = new TeamRepo(context);
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetTeamData(7));
            context.Add(MockDataMethods.GetTeamData(8));
            context.SaveChanges();

            int newTeamid = 9;
            string newTeamName = "C-Team";

            //Act
            Team team = new Team()
            {
                Id = newTeamid,
                TeamName = newTeamName,
            };
            await teamRepo.Create(team);
            context.SaveChanges();

            var result = teamRepo.GetAll();
            int amount = result.Count();

            //Assert
            Assert.Equal(newTeamid, team.Id);
            Assert.Equal(newTeamName, team.TeamName);

            Assert.Equal(3, amount);

        }

        [Fact]
        public void TeamRepo_GetListOfTeams_OnSuccess()
        {
            //Arrange
            //Dbcontext context = new Dbcontext(options);
            //TeamRepo teamRepo = new TeamRepo(context);
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetTeamData(1));
            context.Add(MockDataMethods.GetTeamData(2));
            context.SaveChanges();

            //Act
            var result = teamRepo.GetAll();
            int amount = result.Count();
            //Assert

            Assert.NotNull(result);
            Assert.IsType<List<Team>>(result);
            Assert.Equal(2, amount);
        }

        [Fact]
        public void TeamRepo_GetOneTeamById_OnSuccess()
        {
            //Arrange
            //Dbcontext context = new Dbcontext(options);
            //TeamRepo teamRepo = new TeamRepo(context);
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetTeamData(3));
            context.Add(MockDataMethods.GetTeamData(4));
            context.SaveChanges();

            int id = 3;
            int wrongId = 10;
            string errorMesssage = "Sequence contains no elements";

            //Act
            var result1 = teamRepo.GetById(id);
            Action result2 = () => teamRepo.GetById(wrongId);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(result2);

            //Assert
            Assert.Equal(id, result1.Id);
            Assert.Equal(errorMesssage, exception.Message);
        }

        [Fact]
        public void TeamRepo_DeleteOneTeam_OnSuccess()
        {
            //Arrange
            //Dbcontext context = new Dbcontext(options);
            //TeamRepo teamRepo = new TeamRepo(context);
            context.Database.EnsureDeleted();
            context.Add(MockDataMethods.GetTeamData(5));
            context.Add(MockDataMethods.GetTeamData(6));
            context.SaveChanges();

            int id = 5;

            //Act
            var beforeDelete = teamRepo.GetAll().Count();
            teamRepo.Delete(id);
            var afterDelete = teamRepo.GetAll().Count();

            //Assert
            Assert.NotEqual(beforeDelete, afterDelete);
            Assert.Equal(1, afterDelete);
        }


    }
}
