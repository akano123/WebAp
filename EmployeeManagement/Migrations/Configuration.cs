namespace EmployeeManagement.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManagement.Models.EmployeeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        string[] names =
        {
            "JAMES", "JOHN", "ROBERT", "MICHAEL", "WILLIAM", "DAVID", "RICHARD",
            "CHARLES", "JOSEPH", "THOMAS", "CHRISTOPHER", "DANIEL", "PAUL", "MARK",
            "DONALD", "GEORGE", "KENNETH", "STEVEN", "EDWARD", "BRIAN", "RONALD",
            "ANTHONY", "KEVIN", "JASON", "MATTHEW", "GARY", "TIMOTHY", "JOSE",
            "LARRY", "JEFFREY", "FRANK", "SCOTT", "ERIC", "STEPHEN", "ANDREW",
            "RAYMOND", "GREGORY", "JOSHUA", "JERRY", "DENNIS", "WALTER", "PATRICK",
            "PETER", "HAROLD", "DOUGLAS", "HENRY", "CARL", "ARTHUR", "RYAN", "ROGER",
            "JUSTIN", "TERRY", "GERALD", "KEITH", "SAMUEL", "WILLIE", "RALPH", "LAWRENCE",
            "NICHOLAS", "ROY", "BENJAMIN", "BRUCE", "BRANDON", "ADAM", "HARRY", "FRED", "WAYNE",
            "BILLY", "STEVE", "LOUIS", "JEREMY", "AARON", "RANDY", "HOWARD", "EUGENE", "CARLOS",
            "RUSSELL", "BOBBY", "VICTOR", "MARTIN", "ERNEST", "PHILLIP", "TODD", "JESSE", "CRAIG",
            "ALAN", "SHAWN", "CLARENCE", "SEAN", "PHILIP", "CHRIS", "JOHNNY", "EARL", "JIMMY",
            "ANTONIO", "DANNY", "BRYAN", "TONY", "LUIS", "MIKE"
        };

        string[] mails = { "@gmail.com", "@hotmail.com", "@yahoo.com" };

        protected override void Seed(EmployeeManagement.Models.EmployeeDbContext context)
        {
            Random random = new Random();
            List<Department> departments = new List<Department>()
            {
                new Department()
                {
                    Name = "IT",
                    Description = "For IT"
                },
                new Department()
                {
                    Name = "HR",
                    Description = "For HR"
                },
                new Department()
                {
                    Name = "Marketing",
                    Description = "For Marketing"
                }
            };

            departments.ForEach(dep => context.Departments.AddOrUpdate(x => x.Name, dep));
            context.SaveChanges();

            var genderValues = (int[])Enum.GetValues(typeof(Gender)); //[0,1]
            var skillValues = (int[])Enum.GetValues(typeof(Skill));
            DateTime startDate = new DateTime(1970, 1, 1);
            DateTime endDate = new DateTime(2000, 12, 31);
            int diffDays = (endDate - startDate).Days;
            for (int i = 0; i < 100; i++)
            {
                string firstName = names[random.Next(names.Length)];
                Employee employee = new Employee()
                {
                    EmployeeId = i + 1,
                    FirstName = firstName,
                    LastName = names[random.Next(names.Length)],
                    Gender = (Gender)genderValues[random.Next(genderValues.Length)],
                    DateOfBirth = startDate.AddDays(random.Next(diffDays)),
                    IsMarried = random.Next(0, 10) % 2 == 0,
                    Email = $"{firstName.ToLower()}{mails[random.Next(mails.Length)]}",
                    PhoneNumber = $"09{random.Next(1000,9999)}{random.Next(1000,9999)}",
                    Address = $"{random.Next(10,99)}/{random.Next(10,99)}, {names[random.Next(names.Length)]} Street, District {random.Next(1, 12)}",
                    Salary = (decimal)random.NextDouble()*(10000m - 1000m) + 1000m,
                    Skills = string.Join(",", skillValues.OrderBy(x => random.Next()).Take(random.Next(3,5))),
                    DepartmentId = departments[random.Next(departments.Count)].DepartmentId
                };
                context.Employees.AddOrUpdate(x => x.EmployeeId, employee);
            }
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
