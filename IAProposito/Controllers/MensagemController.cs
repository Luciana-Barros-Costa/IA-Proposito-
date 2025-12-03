using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IAProposito.Data;
using IAProposito.Models;

namespace IAProposito.Controllers
{
    public class MensagemController : Controller
    {
        private readonly AppDbContext _context;

        public MensagemController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Mensagens.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Mensagem mensagem)
        {
            _context.Mensagens.Add(mensagem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var msg = await _context.Mensagens.FindAsync(id);
            return View(msg);
        }
    }
}