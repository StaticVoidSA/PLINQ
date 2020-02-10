using System;
using System.Linq;

namespace PLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] people =  {
                new Person { Name = "Jonathan", City = "Johannesburg" },
                new Person { Name = "David", City = "Pretoria" },
                new Person { Name = "David", City = "Umtata" },
                new Person { Name = "William", City = "Cape Town" },
                new Person { Name = "William", City = "Petermaritzburg" },
                new Person { Name = "Samuel", City = "Durban" },
                new Person { Name = "Janet", City = "Cape Town" },
                new Person { Name = "Janet", City = "Umtata" },
                new Person { Name = "Janet", City = "Petermaritzburg" },
                new Person { Name = "Charlotte", City = "Witbank" },
                new Person { Name = "Joan", City = "Johannesburg" },
                new Person { Name = "Joan", City = "Petermaritzburg" },
                new Person { Name = "Sean", City = "Pretoria" },
                new Person { Name = "Sean", City = "Umtata" },
                new Person { Name = "Michael", City = "Witbank" },
                new Person { Name = "Manny", City = "Pretoria" },
                new Person { Name = "Manny", City = "Petermaritzburg" },
                new Person { Name = "Darren", City = "Witbank" },
                new Person { Name = "Darren", City = "Umtata" },
                new Person { Name = "Dion", City = "Johannesburg" },
                new Person { Name = "Dion", City = "Petermaritzburg" },
                new Person { Name = "Dion", City = "Johannesburg" }
            };

            // Query all people from Johannesburg
            Console.WriteLine("All people in Johannesburg");
            var johannesburgQuery = from person in people.AsParallel()
                                    where person.City == "Johannesburg"
                                    select person.Name;

            foreach (var person in johannesburgQuery)
            {
                Console.WriteLine(person);
            }


            // Informing Parallelization | Forces parallel LINQ
            Console.WriteLine();
            Console.WriteLine("All people in Pretoria");
            var pretoriaQuery = from person in people.AsParallel().
                                WithDegreeOfParallelism(3).
                                WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                where person.City == "Pretoria"
                                select person;

            foreach (var person in people)
            {
                Console.WriteLine(person.Name);
            }


            // Using the  'AsOrdered()' to preserve the data order
            Console.WriteLine();
            Console.WriteLine("All people in Durban");
            var durbanQuery = from person in people.AsParallel().AsOrdered()
                              where person.City == "Durban"
                              select person;

            foreach (var person in durbanQuery)
            {
                Console.WriteLine(person.Name);
            }


            // Identifying elements of a parallel query as sequential
            Console.WriteLine();
            Console.WriteLine("All people in Cape Town");
            var capetownQuery = (from person in people.AsParallel()
                                 where person.City == "Cape Town"
                                 orderby (person.Name)
                                 select new
                                 {
                                     person.Name
                                 }).AsSequential().Take(2);

            foreach (var person in capetownQuery)
            {
                Console.WriteLine(person.Name);
            }


            // Iterate a query using ForAll
            Console.WriteLine();
            Console.WriteLine("All people in Umtata");
            var umtataQuery = from person in people.AsParallel()
                              where person.City == "Umtata"
                              select person;
            umtataQuery.ForAll(person => Console.WriteLine(person.Name));
        }
    }

    class Person
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
