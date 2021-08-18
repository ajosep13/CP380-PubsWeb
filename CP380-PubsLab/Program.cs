using System;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many practice
                //



                var employee = dbcontext.Employee
                    .Select(emp => new {
                        emp_id = emp.emp_id,
                        fname = emp.fname,
                        lname = emp.lname, 
                        job_desc = emp.Job.job_desc 
                    })
                    .ToList();                          //Take the content of the employee table

                Console.WriteLine("EMPLOYEE WITH THEIR JOB DESCRIPTION");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("ID \t\t Name  \t\t\t Job Description");
                Console.WriteLine("------------------------------------");
                foreach (var emp in employee)           //Loop through each employee
                {
                    Console.WriteLine(emp.emp_id +". \t " + emp.fname + " " + emp.lname + "\t\t" + emp.job_desc); //For each employee, list their job description (job_desc, in the jobs table)
                }
               
                var job_list = dbcontext.Jobs.ToList();
                Console.WriteLine("\n\nLIST OF EMPLOYEES BASED ON JOB\n");
                Console.WriteLine("------------------------------------");
                foreach (var job in job_list)                   //Loop through all of the jobs
                {
                    Console.WriteLine("\nEmployee list for "+ job.job_desc);
                    Console.WriteLine("------------------------------------");
                    var job_emp = dbcontext.Employee
                                  .Where(emp => emp.job_id == job.job_id)
                                  .ToList();
                    foreach (var emp in job_emp)    //For each job, list the employees (first name, last name) that have that job
                    {
                        Console.WriteLine(emp.fname +" "+emp.lname);
                    }
                }

                // Many:many practice

                var stores = dbcontext.Stores.ToList();
                var titles = dbcontext.Titles.ToList();
                var sales = dbcontext.Sales.ToList();
                // e.g.
                // Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("\n\n LIST OF BOOKS SOLD AT EACH STORE\n");
                Console.WriteLine("------------------------------------");
                foreach (var store in stores)               //Loop through each Store
                {
                    Console.Write(store.stor_name + " -> ");
                    var sale_list = sales.Where(str => str.stor_id == store.stor_id).ToList(); //For each store, list all the titles sold at that store
                    var i = 0;
                    foreach (var sale in sale_list)
                    {
                        if (i != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(titles.First(t => t.title_id == sale.title_id).title);
                        i++;
                    }
                    Console.WriteLine("\n");
                }
              
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("\n\nLIST OF STORES IN WHICH THE BOOK IS AVAILABLE");
                Console.WriteLine("---------------------------------------------");
                foreach (var title in titles)                               //Loop through each Title
                {
                    Console.Write(title.title + " -> ");
                    var sale_list = sales.Where(str => str.title_id == title.title_id).ToList(); //For each title, list all the stores it was sold at
                    var i= 0;
                    foreach (var sale in sale_list)
                    {
                        if (i != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(stores.First(t => t.stor_id == sale.stor_id).stor_name);
                        i++;
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
