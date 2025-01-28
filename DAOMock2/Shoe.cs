using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock
{
    public class Shoe:Interfaces.IShoe
    {
        public int Id { get; set; }
        public IProducer Producer { get; set; }
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public ShoeType ShoeType { get; set; }

        [NotMapped]
        IProducer IShoe.Producer
        {
            get => this.Producer;
            set
            {
                this.Producer = value as Producer;
                this.ProducerId = value.Id;
            }
        }

    }
}
