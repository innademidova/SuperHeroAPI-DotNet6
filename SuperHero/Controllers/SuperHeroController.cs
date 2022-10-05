using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero {
                    Id=1,
                    Name= "Iron Man",
                    FirstName="Robert",
                    LastName="Downie",
                    Place="Kaliphornia"
                }
            };

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
         
            return Ok(await _context.SuperHeroes.ToListAsync()); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero is not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero hero)
        {
            var updatedHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if(updatedHero == null)
            {
                return BadRequest("Hero is not found");
            }
            updatedHero.Name = hero.Name;
            updatedHero.FirstName = hero.FirstName;
            updatedHero.LastName = hero.LastName;
            updatedHero.Place = hero.Place;

            await _context.SaveChangesAsync();
         
            return Ok(updatedHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero is not found");
            }
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }



    }
}
