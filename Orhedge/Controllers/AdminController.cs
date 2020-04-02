using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orhedge.AutoMapper;
using Orhedge.Enums;
using Orhedge.ViewModels.Admin;
using ServiceLayer.DTO;
using ServiceLayer.Helpers;
using ServiceLayer.Services;

namespace Orhedge.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public AdminController(IStudentService studentService, IMapper mapper) => (_studentService, _mapper) = (studentService, mapper);


        public async Task<ActionResult> Index()
        {
            List<StudentDTO> students = await _studentService.GetAll<NoSorting>(x => !x.Deleted);
            AdminIndexViewModel adminIndexViewModel = new AdminIndexViewModel()
            {
                Students = _mapper.Map<List<StudentDTO>, List<StudentViewModel>>(students)
            };
            ViewBag.SortingCriteria = StudentSortingCriteria.NoSorting;
            ViewBag.SearchFor = null;
            ViewBag.PrivilegeFilters = null;
            return View(adminIndexViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> SearchSortFilter(StudentSortingCriteria sortCriteria, string searchFor, StudentPrivilege[] privileges)
        {
            List<StudentDTO> students = await GetStudents(privileges, searchFor, sortCriteria);
            AdminIndexViewModel adminIndexViewModel = new AdminIndexViewModel()
            {
                Students = _mapper.Map<List<StudentDTO>, List<StudentViewModel>>(students)
            };
            ViewBag.SearchFor = searchFor;
            ViewBag.SortingCriteria = sortCriteria;
            ViewBag.PrivilegeFilters = privileges;
            return View("Index", adminIndexViewModel);
        }

        private async Task<List<StudentDTO>> GetStudents(StudentPrivilege[] privileges = null, string searchFor = null, StudentSortingCriteria sortCriteria = StudentSortingCriteria.NoSorting)
        {
            List<StudentDTO> students = null;
            var arr = Enum.GetValues(typeof(StudentPrivilege));
            if (privileges.Count() == 0)
                privileges = Enum.GetValues(typeof(StudentPrivilege)).Cast<StudentPrivilege>().ToArray();
            Predicate<StudentDTO> filterFunction = (x) => !x.Deleted && privileges.Contains(x.Privilege);
            if (searchFor != null)
                filterFunction = (x) => !x.Deleted && privileges.Contains(x.Privilege) && ((x.Name + x.LastName).Contains(searchFor.Trim()) || ((int)Math.Truncate(x.Rating)).ToString() == searchFor.Trim());
            switch (sortCriteria)
            {
                case StudentSortingCriteria.NoSorting: students = await _studentService.GetAll<string>(filterFunction, asc: true); break;
                case StudentSortingCriteria.NameAsc: students = await _studentService.GetAll<string>(filterFunction, sortKeySelector: x => x.Name, asc: true); break;
                case StudentSortingCriteria.NameDesc: students = await _studentService.GetAll<string>(filterFunction, sortKeySelector: x => x.Name, asc: false); break;
                case StudentSortingCriteria.RatingAsc: students = await _studentService.GetAll<double>(filterFunction, sortKeySelector: x => x.Rating, asc: true); break;
                case StudentSortingCriteria.RatingDesc: students = await _studentService.GetAll<double>(filterFunction, sortKeySelector: x => x.Rating, asc: false); break;
                case StudentSortingCriteria.PrivilegeAsc: students = await _studentService.GetAll<StudentPrivilege>(filterFunction, sortKeySelector: x => x.Privilege, asc: true); break;
                case StudentSortingCriteria.PrivilegeDesc: students = await _studentService.GetAll<StudentPrivilege>(filterFunction, sortKeySelector: x => x.Privilege, asc: false); break;
            }
            if (students == null)
                students = await _studentService.GetAll<NoSorting>(x => !x.Deleted);
            return students;
        }
    }
}