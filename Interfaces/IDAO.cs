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

        IEnumerable<IShoe> GetAllShoes();

        IProducer CreateNewProducer();

        IShoe CreateNewShoe();

        void AddShoe(IShoe shoe);

        void RemoveShoe(IShoe shoe);

        void AddProducer(IProducer producer);

        void RemoveProducer(IProducer producer);

        void UpdateShoe(IShoe shoe);

        void SaveChanges();

        // nowe
        void UndoChanges();

        void UpdateProducer(IProducer producer);

    }

}
