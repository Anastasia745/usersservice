using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UsersService.Models;
using Microsoft.EntityFrameworkCore;

namespace UsersService.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersdbContext db; // БД

        public HomeController(UsersdbContext context)
        {
            db = context;
        }

        // Отображение списка всех пользователей
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            return View(await db.Users.ToListAsync());
        }

        // Создание пользователя
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Редактирование данных о пользователе
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User? user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null) return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Удаление пользователя
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}