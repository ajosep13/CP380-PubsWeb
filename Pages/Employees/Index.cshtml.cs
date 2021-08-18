using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CP380_PubsLab.Models;
//using Microsoft.EntityFrameworkCore;

namespace CP380_PubsWeb.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly PubsDbContext _db;

        public IndexModel(PubsDbContext db)
        {
            _db = db;
        }

        public List<CP380_PubsLab.Models.Employee> Employees { get; set; }
        public void OnGet(Int16 id)
        {
            var jobs = _db.Jobs.Include(e => e.Employees).First(e => e.job_id == id);

            this.Employees = jobs.Employees.ToList();
        }
    }
}