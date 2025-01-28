using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOEF.BO
{
    public class Car : Interfaces.ICar
    {
        public int Id { get; set; }

        public Producer Producer { get; set; }
        public int ProducerId { get; set; }

        [NotMapped]
        IProducer ICar.Producer { 
            get => this.Producer; 
            set
            {
                this.Producer = value as BO.Producer;
                this.ProducerId = value.Id;
            }
        }

        [MaxLength(20)]
        public string Name { get; set; }
        public TransmissionType Transmission { get; set; }

        [Range(1900, 2025, ErrorMessage = "Samochód musi być wyprodukowany pomiędzy 1900-2025")]
        public int ProdYear { get; set; }
    }
}
