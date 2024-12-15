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
        private readonly UserManager<Student> _userManagerStudent;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<Teacher> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, UserManager<Student> userManagerStudent)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManagerStudent = userManagerStudent;
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

        public async Task<bool> IsUserInscribedAsync(int eventId)
        {
            var user = await _userManagerStudent.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return false;
            }

            return await _context.Inscriptions.AnyAsync(i => i.EventId == eventId && i.StudentId == user.Id);
        }

        public async Task<List<Student>> GetStudentsInscribedAsync(int eventId)
        {
            return await _context.Inscriptions
                .Where(i => i.EventId == eventId)
                .Join(_context.Students,
                      inscription => inscription.StudentId,
                      student => student.Id,
                      (inscription, student) => student)
                .ToListAsync();
        }
    }
}