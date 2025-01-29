using Interfaces;
using System.Reflection;
using System.Configuration;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];

            Interfaces.IDAO dao = new BLC.BLC(libraryName).DAO;

            Console.WriteLine( "** Producers ** \n");

            foreach( IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id}: {p.Name}");
                //Console.WriteLine(p);
            }

            Console.WriteLine( "\n** Shoes ** \n");

            foreach( IShoe c in dao.GetAllShoes())
            {
                Console.WriteLine( $"{c.Id}: {c.Producer} {c.Name} {c.ShoeType}");
            }

            string linia = Console.ReadLine();

            Console.WriteLine( $"#{linia}#");

        }
    }
}
