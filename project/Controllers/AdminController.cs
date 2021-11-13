using Microsoft.AspNetCore.Mvc;
using project.Models.Services;
using Microsoft.Extensions.Logging;

namespace project.Controllers
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