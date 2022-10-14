using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
       
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
       
       
    [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            /*var heros = new List<SuperHero>
            {
                new SuperHero {
                    Id = 1, 
                    Name = "Spider Man", 
                    FirstName = "Peter",
                    LastName = "Parker", 
                    place = "New Yory city"
                } 
            };*/
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }
        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();  
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeros.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.place = request.place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _context.SuperHeros.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");
            _context.SuperHeros.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
