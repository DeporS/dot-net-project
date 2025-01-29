using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesGUI.ViewModels
{
    public class ShoeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Interfaces.IShoe shoe;
        public Interfaces.IShoe Shoe => shoe;

        public ShoeViewModel(Interfaces.IShoe shoe)
        {
            this.shoe = shoe;
        }
        private void RaisePropertyChanged( string propertyName)
        {
            if ( PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public Interfaces.ShoeType ShoeType
        {
            get => shoe.ShoeType;
            set
            {
                shoe.ShoeType = value;
                RaisePropertyChanged(nameof(ShoeType));
            }
        }
        [Required]
        [Range(1900,2025, ErrorMessage = "musi być wyprodukowany pomiędzy 1900-2025")]

        public int ReleaseYear
        {
            get => shoe.ReleaseYear;
            set
            {
                shoe.ReleaseYear = value;
                RaisePropertyChanged(nameof(ReleaseYear));
            }
        }
        public Interfaces.IProducer Producer
        {
            get => shoe.Producer;
            set
            {
                shoe.Producer = value;
                RaisePropertyChanged(nameof(Producer));
            }
        }

        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        public string Name
        {
            get { return shoe.Name; }
            set
            {
                shoe.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        [Required(ErrorMessage = "Musi posiadać opis")]
        public string Description
        {
            get { return shoe.Description; }
            set
            {
                shoe.Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public int Id
        {
            get => shoe.Id;
            set
            {
                shoe.Id = value;
                RaisePropertyChanged(nameof(Id));
            }


        }

    }
}
