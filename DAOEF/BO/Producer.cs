using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOEF.BO
{
    public class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Nazwa producenta może mieć maksymalnie 20 znaków")]
        public string Name { get; set; }

        public List<Car>? Cars { get; set; }
    }
}
