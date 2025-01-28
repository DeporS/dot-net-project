using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp2.ViewModels
{
    public class CarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Interfaces.ICar car;
        public Interfaces.ICar Car => car;

        public CarViewModel(Interfaces.ICar car)
        {
            this.car = car;
        }
        private void RaisePropertyChanged( string propertyName)
        {
            if ( PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /*
                     ProdYear = 2000,
                     Transmission = TransmissionType.Manual*/
        public Interfaces.TransmissionType Transmission
        {
            get => car.Transmission;
            set
            {
                car.Transmission = value;
                RaisePropertyChanged(nameof(Transmission));
            }
        }
        [Required]
        [Range(1900,2025, ErrorMessage = "Samochód musi być wyprodukowany pomiędzy 1900-2025")]
        public int ProdYear
        {
            get => car.ProdYear;
            set
            {
                car.ProdYear = value;
                RaisePropertyChanged(nameof(ProdYear));
            }
        }
        public Interfaces.IProducer Producer
        {
            get => car.Producer;
            set
            {
                car.Producer = value;
                RaisePropertyChanged(nameof(Producer));
            }
        }

        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        public string Name
        {
            get { return car.Name; }
            set
            {
                car.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int Id
        {
            get => car.Id;
            set
            {
                car.Id = value;
                RaisePropertyChanged(nameof(Id));
            }


        }
    }
}
