using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersonKartotek
{
    public class Person
    {
        [Key]
        public long PersonID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Email { get; set; }

        public List<Address> Addresses { get; set; }
    }

    public class Address
    {
        [Key]
        public long AddressID { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string HouseNumber { get; set; }

        public List<Person> Persons { get; set; }
    }

    public class AddressType
    {
        public long AddressTypeID { get; set; }
        public string Type { get; set; }
    }

    public class PersonIndexContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=PersonKartotek.db");
        }
    }


    public class Program
    {
        public static void Main()
        {
            using (var db = new PersonIndexContext())
            {
                db.Persons.Add(new Person { FirstName = "Kasper", MiddleName = "Juul", LastName = "Hermansen", Email = "KasperjuulHermansen@gmail.com", Gender = "Male"});
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All Persons in database:");
                foreach (var person in db.Persons)
                {
                    Console.WriteLine($" - {person.PersonID} {person.FirstName} {person.MiddleName} {person.LastName}");
                }
            }
        }
    }
}
