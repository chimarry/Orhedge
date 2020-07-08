using AutoMapper;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Orhedge.Enums;
using Orhedge.Helpers;
using Orhedge.ViewModels.Admin;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMapper _mapper;

        public AdminController(IStudentService studentService, IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper)
            => (_studentService, _stringLocalizer, _mapper) = (studentService, stringLocalizer, mapper);


        public async Task<ActionResult> Index(HttpReponseStatusCode statusCode = HttpReponseStatusCode.NoStatus)
        {
            AdminIndexViewModel adminIndexViewModel = await GetStudents();
            ViewBag.InfoMessage = new InfoMessage(_stringLocalizer, statusCode);
            return View(adminIndexViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> SearchSortFilter(int pageNumber, StudentSortingCriteria sortCriteria, string searchFor, StudentPrivilege[] privileges)
        {
            AdminIndexViewModel adminIndexViewModel = await GetStudents(pageNumber, privileges, searchFor, sortCriteria);
            return View("Index", adminIndexViewModel);
        }

        private async Task<AdminIndexViewModel> GetStudents(int pageNumber = 0, StudentPrivilege[] privileges = null, string searchFor = null, StudentSortingCriteria sortCriteria = StudentSortingCriteria.NoSorting)
        {
            List<StudentDTO> students = null;
            if (privileges == null || privileges.Count() == 0)
                privileges = Enum.GetValues(typeof(StudentPrivilege)).Cast<StudentPrivilege>().ToArray();
            Predicate<StudentDTO> filterFunction = (x) => !x.Deleted && privileges.Contains(x.Privilege);
            if (searchFor != null)
                filterFunction = (x) => !x.Deleted && privileges.Contains(x.Privilege)
                                                   && ((x.Name + x.LastName).Contains(searchFor.Trim())
                                                          || ((int)Math.Truncate(x.Rating)).ToString() == searchFor.Trim());
            PageInformation pageInformation = new PageInformation(pageNumber, await _studentService.Count(filterFunction), WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE);
            int offset = pageInformation.PageNumber * WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE;

            switch (sortCriteria)
            {
                case StudentSortingCriteria.NoSorting: students = await _studentService.GetRange<string>(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, asc: true); break;
                case StudentSortingCriteria.NameAsc: students = await _studentService.GetRange(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, sortKeySelector: x => x.Name, asc: true); break;
                case StudentSortingCriteria.NameDesc: students = await _studentService.GetRange(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, sortKeySelector: x => x.Name, asc: false); break;
                case StudentSortingCriteria.RatingAsc: students = await _studentService.GetRange(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, sortKeySelector: x => x.Rating, asc: true); break;
                case StudentSortingCriteria.RatingDesc: students = await _studentService.GetRange(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, sortKeySelector: x => x.Rating, asc: false); break;
                case StudentSortingCriteria.PrivilegeAsc: students = await _studentService.GetRange(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, sortKeySelector: x => x.Privilege, asc: true); break;
                case StudentSortingCriteria.PrivilegeDesc: students = await _studentService.GetRange(offset, WebConstants.MAX_NUMBER_OF_STUDENTS_PER_PAGE, filterFunction, sortKeySelector: x => x.Privilege, asc: false); break;
            }
            if (students == null)
                students = await _studentService.GetAll<NoSorting>(x => !x.Deleted);

            AdminIndexViewModel adminIndexViewModel = new AdminIndexViewModel(_mapper.Map<List<StudentDTO>, List<StudentViewModel>>(students), pageInformation);
            ViewBag.SearchFor = searchFor;
            ViewBag.SortingCriteria = sortCriteria;
            ViewBag.PrivilegeFilters = privileges;
            return adminIndexViewModel;
        }
    }
}