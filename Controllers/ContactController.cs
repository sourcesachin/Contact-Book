using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApp.Models;
using UserApp.Data;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly HRContext db;

        public ContactController(HRContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<Contact>();
            list = db.Contact.ToList();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = db.Contact.Where(x => x.Id == id).SingleOrDefault();
            var Addressess = db.Addresses.Where(x => x.ContactId == id).ToList();
            foreach (var Addresses in Addressess)
            {
                Addresses.Contact = null;
            }
            contact.Addresses = Addressess;
            return Ok(contact);
        }
        [HttpPost]
        public IActionResult Post([FromBody]Contact contact)
        {
            if (contact.Id == 0)
            {
                db.Contact.Add(contact);
                db.Addresses.AddRange(contact.Addresses);
            }
            else
            {
                var exContact = db.Contact.Where(x => x.Id == contact.Id).SingleOrDefault();
                var existAddressess = db.Addresses.Where(x => x.ContactId == contact.Id).ToList();
                exContact.FirstName = contact.FirstName;
                exContact.LastName = contact.LastName;
                exContact.NickName = contact.NickName;
                exContact.DOB = contact.DOB;
                var newAddressess = contact.Addresses.Where(x => x.Id == 0).ToList();
                db.Addresses.AddRange(newAddressess);
                foreach (var exAddresses in existAddressess)
                {
                    if (!contact.Addresses.Any(x => x.Id == exAddresses.Id))
                    {
                        db.Addresses.Remove(exAddresses);
                    }
                }
            }
            db.SaveChanges();
            return Ok(contact.Id);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var contact = db.Contact.Where(x => x.Id == id).Include(x => x.Addresses).SingleOrDefault();
            db.Contact.Remove(contact);
            db.SaveChanges();
            return Ok();
        }
    }
}
