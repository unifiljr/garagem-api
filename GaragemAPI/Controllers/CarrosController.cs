namespace GaragemAPI.Controllers
{
    using GaragemAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using GaragemAPI.Models;
    using GaragemAPI.Database;
    using Microsoft.EntityFrameworkCore;
    using GaragemAPI.Models.Dto;

    [ApiController]
    [Route("api/[controller]")]
    public class CarrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarrosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _context.Carros
                .Include(c => c.Status)
                .Select(c => new CarroListaDto
                {
                    Id = c.Id,
                    Marca = c.Marca,
                    Modelo = c.Modelo,
                    Ano = c.Ano,
                    Preco = c.Preco,
                    StatusId = c.StatusId,
                    StatusDescricao = c.Status.Descricao
                })
                .ToList();

            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carro carro)
        {
            _context.Carros.Add(carro);
            _context.SaveChanges();
            return Ok(carro);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carro = _context.Carros.Find(id);
            if (carro == null) return NotFound();

            _context.Carros.Remove(carro);
            _context.SaveChanges();

            return Ok();
        }
    }
}
