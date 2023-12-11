using Halloween.Repo.DTO;
using Halloween.Repo.Interfaces;
using Halloween.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Halloween.API.Controllers
{
    [Route("api/[controller]")] //URL
    [ApiController] //reglesæt
    public class SuperHeroController : ControllerBase
    {
        ISuperHeroRepo SuperHeroRepo { get; set; }
        public SuperHeroController(ISuperHeroRepo superHeroRepo)
        {
            this.SuperHeroRepo = superHeroRepo;
        }
        // GET: api/<SuperHeroController>
        [HttpGet]
        //Task<List<SuperHero>>
        public List<SuperHero> Get()
        {
            //try
            //{
            //    var heroes = await SuperHeroRepo.GetAll();
            //    if (heroes == null)
            //    {
            //        return Problem("Is Null");
            //    }
            //    else if (heroes.Count == 0)
            //    {
            //        return Problem("No Heroes");
            //    }

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            return SuperHeroRepo.GetAll();
        }

        // GET api/<SuperHeroController>/5
        [HttpGet("{id}")]
        public SuperHero Get(int id)
        {
            return SuperHeroRepo.GetById(id);
        }

        // POST api/<SuperHeroController>
        [HttpPost]
        public void Post([FromBody] SuperHero hero)
        {
            SuperHeroRepo.Create(hero);
        }

        // PUT api/<SuperHeroController>/5
        [HttpPut("{id}")]
        public void Put(int id, SuperHero hero)
        {
            var heroOld = SuperHeroRepo.GetById(hero.Id);
            //string.IsNullOrWhiteSpace(hero.HeroName) ? HeroOld.HeroName = HeroOld.HeroName : HeroOld.HeroName = hero.HeroName;
            if (!string.IsNullOrWhiteSpace(hero.HeroName)) heroOld.HeroName = hero.HeroName;
            if (!string.IsNullOrWhiteSpace(hero.RealName)) heroOld.RealName = hero.RealName;
            if (!string.IsNullOrWhiteSpace(hero.Place)) heroOld.Place= hero.Place;
            if (hero.DebutYear is not null) heroOld.DebutYear = hero.DebutYear;
            
            SuperHeroRepo.Update(heroOld);




        }

        // DELETE api/<SuperHeroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SuperHeroRepo.Delete(id);
        }

    }
}
