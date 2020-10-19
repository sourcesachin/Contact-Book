using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApp.Data;
using UserApp.Models;
using UserApp.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly HRContext db;

        public SearchController(HRContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult searchContacts([FromBody] SearchVM searchParams)
        {
            var searchedList = db.Contact.ToList();
            if (!(searchParams.FirstName.Equals("")))
            {
                searchParams.FirstName = searchParams.FirstName.ToLower();
                searchedList = searchedList.Where(x => x.FirstName.ToLower().Contains(searchParams.FirstName)).ToList();
            }
            if (!(searchParams.LastName.Equals("")))
            {
                searchParams.LastName = searchParams.LastName.ToLower();
                searchedList = searchedList.Where(x => x.LastName.ToLower().Contains(searchParams.LastName)).ToList();
            }
            return Ok(searchedList);
        }
    }
}
