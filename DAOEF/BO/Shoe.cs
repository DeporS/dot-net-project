using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOSQL.BO
{
    public class Shoe : Interfaces.IShoe
    {
        //public Shoe()
        //{
        //    Name = "Nowy But"; 
        //    ReleaseYear = DateTime.Now.Year;
        //    Description = "Brak opisu";
        //}

        public int Id { get; set; }

        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public ShoeType ShoeType {  get; set; }

        [NotMapped]
        IProducer IShoe.Producer { 
            get => this.Producer; 
            set
            {
                this.Producer = value as BO.Producer;
                this.ProducerId = value.Id;
            }
        }

        //[MaxLength(20)]
        //public string Name { get; set; }
        //public Interfaces.Type Transmission { get; set; }

        //[Range(1900, 2025, ErrorMessage = "Samochód musi być wyprodukowany pomiędzy 1900-2025")]
        //public int ProdYear { get; set; }
    }
}
