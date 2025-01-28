using Interfaces;
using System.Reflection;
using System.Configuration;

namespace CarApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];

            Interfaces.IDAO dao = new BLC.BLC(libraryName).DAO;

            Console.WriteLine( "** BRANDS ** \n");

            foreach( IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id}: {p.Name}");
                //Console.WriteLine(p);
            }

            Console.WriteLine( "\n** CARS ** \n");

            foreach( ICar c in dao.GetAllCars())
            {
                Console.WriteLine( $"{c.Id}: {c.Producer} {c.Name} {c.Transmission}");
            }

            string linia = Console.ReadLine();

            Console.WriteLine( $"#{linia}#");

        }
    }
}
