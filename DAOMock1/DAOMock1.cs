using Interfaces;

namespace DAOMock1
{
    public class DAOMock1 : Interfaces.IDAO
    {

        private List<IProducer> producers;

        private List<ICar> cars;

        public DAOMock1()
        {
            producers = new List<IProducer>()
            {
                new Producer() { Id = 1, Name="VW"},
                new Producer() { Id = 2, Name="BMW"},
            };

            cars = new List<ICar>()
            {
                new Car() { Id = 1, Producer = producers[0], Name="Passat", Transmission=TransmissionType.Automatic },
                new Car() { Id = 2, Producer = producers[0], Name="Golf", Transmission=TransmissionType.Manual},
                new Car() { Id = 3, Producer = producers[0], Name="Polo", Transmission=TransmissionType.Manual },
                new Car() { Id = 4, Producer = producers[1], Name="3", Transmission=TransmissionType.Automatic },
            };
        }

        public void AddCar(ICar car)
        {
            throw new NotImplementedException();
        }

        public void AddProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public ICar CreateNewCar()
        {
            throw new NotImplementedException();
        }

        public IProducer CreateNewProducer()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICar> GetAllCars()
        {
            return cars;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public void RemoveCar(ICar car)
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

        public void UpdateCar(ICar car)
        {
            throw new NotImplementedException();
        }
    }
}
