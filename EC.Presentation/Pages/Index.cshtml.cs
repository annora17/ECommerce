using EC.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var crorrr = HttpContext.Request.Headers["X-Correlation-ID"].ToString();
            try
            {
                throw new Exception("Yeni bir hata raporu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "hata");
            }

            _logger.LogInformation("Index requested");
        }

        public void OnPost()
        {
            var t = 5;

        }
    }
}
