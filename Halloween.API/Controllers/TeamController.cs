using Halloween.Repo.DTO;
using Halloween.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Halloween.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamRepo TeamRepo { get; set; }
        public TeamController(ITeamRepo teamRepo)
        {
            TeamRepo = teamRepo;
        }

        // GET: api/<TeamController>
        [HttpGet]
        public List<Team> Get()
        {
            return TeamRepo.GetAll();
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public Team GetById(int id)
        {
            return TeamRepo.GetById(id);
        }

        //[HttpGet("{name}")]
        //public Team GetByName(string name)
        //{
        //    return TeamRepo.GetByName(name);
        //}

        // POST api/<TeamController>
        [HttpPost("Create")]
        public async Task<ActionResult<Team>> Post([FromBody] Team team)
        {
            try
            {
                var createTeam = await TeamRepo.Create(team);

                if (team is null)
                {
                    return BadRequest("Team not found or not created");
                }
                return CreatedAtAction("PostTeam", new { teamId = createTeam.Id }, createTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured while creating the team");
            }
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TeamRepo.Delete(id);
        }
    }
}
