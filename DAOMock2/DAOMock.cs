using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock
{
    public class DAOMock:Interfaces.IDAO
    {
        private List<IProducer> producers;
        private List<IShoe> shoes;

        private List<IProducer> oldValuesProducers;
        private List<IShoe> oldValuesShoes;

        public DAOMock()
        {
            producers = new List<IProducer>
            {
                new Producer() { Id = 1, Name="Adidas"},
                new Producer() { Id = 2, Name="Nike"},
            };

            shoes = new List<IShoe>()
            {
                new Shoe() { Id = 1, Producer = producers[1], Name="Air Force 1 Low", Description="Idealne na wiosne.", ShoeType= Interfaces.ShoeType.Lifestyle },
                new Shoe() { Id = 2, Producer = producers[1], Name="Air Jordan 1 High", Description="Idealne na jesień.", ShoeType= Interfaces.ShoeType.Lifestyle}
            };

            oldValuesProducers = producers;
            oldValuesShoes = shoes;
        }

        public void AddShoe(IShoe shoe)
        {
            if (shoe.Id == 0)
            {
                shoe.Id = shoes.Any() ? shoes.Max(p => p.Id) + 1 : 1;
            }
            shoes.Add(shoe);
        }

        public void AddProducer(IProducer producer)
        {
            if (producer.Id == 0)
            {
                producer.Id = producers.Any() ? producers.Max(p => p.Id) + 1 : 1;
            }
            producers.Add(producer);
        }

        public IShoe CreateNewShoe()
        {
            return new Shoe();
        }

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public IEnumerable<IShoe> GetAllShoes()
        {
            return shoes;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {

            return producers;
        }

        public void RemoveShoe(IShoe shoe)
        {
            shoes.Remove(shoe as Shoe);
        }

        public void RemoveProducer(IProducer producer)
        {
            producers.Remove(producer as Producer);
        }

        public void UpdateShoe(IShoe shoe)
        {

        }

        public void UpdateProducer(IProducer producer)
        {
 
        }

        void IDAO.SaveChanges()
        {

        }

        public void UndoChanges()
        {
            producers = oldValuesProducers;
            shoes = oldValuesShoes;
        }
    }

}
