using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;

namespace CarsApp2.ViewModels
{
    public class CarListViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<CarViewModel> cars;

        public ObservableCollection<CarViewModel> Cars
        {
            get { return cars; }
            set { cars = value; RaisePropertyChanged(nameof(Cars)); }
        }

        private ObservableCollection<IProducer> producers;
        public ObservableCollection<IProducer> Producers
        {
            get { return producers; }
            set
            {
                producers = value;
                RaisePropertyChanged(nameof(Producers));
            }
        }
        private IDAO dao;
        public CarListViewModel()
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            dao = new BLC.BLC(libraryName).DAO;
            producers = new ObservableCollection<IProducer>(dao.GetAllProducers());
            cars = new ObservableCollection<CarViewModel>();
            //IParking parking = new CarsDB.Parking();
            foreach (var car in dao.GetAllCars())
            {
                Cars.Add(new CarViewModel(car));
            }

            addNewCarCommand = new RelayCommand(param => AddNewCar());
            saveCarCommand = new RelayCommand(param => SaveCar());
        }

        private CarViewModel selectedCar;

        public CarViewModel SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value; RaisePropertyChanged(nameof(SelectedCar));

            }

        }

        private void AddNewCar()
        {
            CarViewModel cvm = new CarViewModel(dao.CreateNewCar());
            
            SelectedCar = cvm;


        }

        private RelayCommand addNewCarCommand;

        public RelayCommand AddNewCarCommand
        {
            get => addNewCarCommand;
        }

        private RelayCommand saveCarCommand;

        public ICommand SaveCarCommand
        {
            get => saveCarCommand;
        }



        private void SaveCar()
        {
            if (SelectedCar.Id == 0)
            {
                cars.Add(selectedCar);
                dao.AddCar(selectedCar.Car);
            }
            dao.SaveChanges();
        }

        private Dictionary<string, ICollection<string>> errorsCollection =
    new Dictionary<string, ICollection<string>>();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => errorsCollection.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !errorsCollection.ContainsKey(propertyName)) return null;
            return errorsCollection[propertyName];
        }
        protected void RaiseErrorChanged( string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();
            //wywołanie walidacji obiektu
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            // usunięcie tych wpisów z kolekcji błędów, dla których już ich nie ma
            foreach (var kv in errorsCollection.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                {
                    errorsCollection.Remove(kv.Key);
                    RaiseErrorChanged(kv.Key);
                }
            }
            var q = from result in validationResults
                    from member in result.MemberNames
                    group result by member into gr
                    select gr;
            foreach (var prop in q)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();

                if (errorsCollection.ContainsKey(prop.Key))
                {
                    errorsCollection.Remove(prop.Key);
                }
                errorsCollection.Add(prop.Key, messages);
                RaiseErrorChanged(prop.Key);
            }
        }

    }
}
