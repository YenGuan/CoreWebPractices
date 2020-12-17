using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebPractice.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CoreWebPractice.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IUnitOfWork<WEHQAffairsContext> _unitOfWork;
        private readonly IRepository<Person> _personRepo;
        public PeopleController(IUnitOfWork<WEHQAffairsContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _personRepo = _unitOfWork.GetRepository<Person>();

        }
        public IActionResult Index(int? page, string keyword)
        {
            int pageSize = 5;
            page ??= 1;
            var list = _personRepo.All().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(m => m.FirstName.Contains(keyword)
                || m.LastName.Contains(keyword)).ToList();
            }
            return View(list.ToPagedList(page.Value, pageSize));
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person data)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", data);
            }
            await _personRepo.CreateAsync(data);
            return Json(Url.Action(nameof(Index)));
        }
    }
}
