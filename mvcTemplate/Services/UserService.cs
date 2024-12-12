using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.Models;
using System.Threading.Tasks;

namespace mvc.Services
{
    public class UserService
    {
        private readonly UserManager<Teacher> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<Teacher> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> IsTeacherAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return false;
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == user.Id);
            return account?.IsTeacher ?? false;
        }
    }
}