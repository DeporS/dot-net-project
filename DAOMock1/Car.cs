using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock1
{
    public class Car : Interfaces.ICar
    {
        public int Id { get; set;}
        public IProducer Producer { get; set; }
        public string Name { get; set; }
        public TransmissionType Transmission { get; set; }
        public int ProdYear { get; set; }
    }
}
