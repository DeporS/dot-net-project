using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();

        IEnumerable<ICar> GetAllCars();

        IProducer CreateNewProducer();

        ICar CreateNewCar();

        void AddCar(ICar car);

        void RemoveCar(ICar car);

        void AddProducer(IProducer producer);

        void RemoveProducer(IProducer producer);

        void UpdateCar(ICar car);

        void SaveChanges();

    }

}
