using Microsoft.AspNetCore.Mvc;
using lab1.Models.Services;
using Microsoft.Extensions.Logging;

namespace lab1.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(IContext context, ILogger<AdminController> logger) : base(context)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _context.load();
            _logger.LogWarning("CONTEXT RELOADED");
            return RedirectToAction("Logout", "Identity");
        }
    }
} 