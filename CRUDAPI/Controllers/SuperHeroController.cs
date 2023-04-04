using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers
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
            return Ok(await _context.SuperHeroes.ToListAsync());

        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> Get(int Id)
        {
            var hero = await _context.SuperHeroes.FindAsync(Id);
            if(hero == null)
               return NotFound();
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
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var deHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (deHero == null)
                return NotFound();

            deHero.Name = request.Name;
            deHero.FirstName = request.FirstName;
            deHero.LastName = request.LastName;
            deHero.Place = request.Place;
            deHero.ImageUrl = request.ImageUrl;

            await _context.SaveChangesAsync();


            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int Id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(Id);
            if (dbHero == null)
                return NotFound();

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());

        }
    }
}
