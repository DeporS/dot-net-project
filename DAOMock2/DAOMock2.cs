using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock2
{
    public class DAOMock2:Interfaces.IDAO
    {
        private List<IProducer> producers;

        private List<IShoe> cars;

        public DAOMock2()
        {
            producers = new List<IProducer>()
            {
                new Producer() { Id = 1, Name="KIA"},
                new Producer() { Id = 2, Name="BMW"},
            };

            cars = new List<IShoe>()
            {
                new Car() { Id = 1, Producer = producers[0], Name="Ceed", Transmission= Interfaces.Type.Automatic },
                new Car() { Id = 2, Producer = producers[0], Name="Rio", Transmission= Interfaces.Type.Manual},
                new Car() { Id = 3, Producer = producers[0], Name="Sportage", Transmission= Interfaces.Type.Manual },
                new Car() { Id = 4, Producer = producers[1], Name="5", Transmission= Interfaces.Type.Automatic },
            };
        }

        public void AddCar(IShoe car)
        {
            throw new NotImplementedException();
        }

        public void AddProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public IShoe CreateNewCar()
        {
            throw new NotImplementedException();
        }

        public IProducer CreateNewProducer()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShoe> GetAllCars()
        {
            return cars;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public void RemoveCar(IShoe car)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(IShoe car)
        {
            throw new NotImplementedException();
        }
    }
}
