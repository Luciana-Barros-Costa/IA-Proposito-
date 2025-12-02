using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IAProposito.Models;
using IAProposito.Data;

namespace IAProposito.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensagemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MensagemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Mensagens.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Mensagem mensagem)
        {
            _context.Mensagens.Add(mensagem);
            await _context.SaveChangesAsync();
            return Ok(mensagem);
        }
    }
}
