using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICar
    {
        int Id { get; set; }

        IProducer Producer { get; set; }

        string Name { get; set; }

        TransmissionType Transmission {  get; set; }

        int ProdYear { get; set; }
    }
}
