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
using System.Windows;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Data;


namespace ShoesGUI.ViewModels
{
    public class ShoeListViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool canAddNewShoe;

        public bool CanAddNewShoe
        {
            get { return canAddNewShoe; }
            set
            {
                if (canAddNewShoe != value)
                {
                    canAddNewShoe = value;
                    RaisePropertyChanged(nameof(CanAddNewShoe));
                }
            }
        }

        private bool canSaveShoe;

        public bool CanSaveShoe
        {
            get { return canSaveShoe; }
            set
            {
                if (canSaveShoe != value)
                {
                    canSaveShoe = value;
                    RaisePropertyChanged(nameof(CanSaveShoe));
                }
            }
        }

        private ListCollectionView view;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static ShoeListViewModel _instance;

        public static ShoeListViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShoeListViewModel();
                }
                return _instance;
            }
        }

        private ObservableCollection<ShoeViewModel> shoes;

        public ObservableCollection<ShoeViewModel> Shoes
        {
            get { return shoes; }
            set { shoes = value; RaisePropertyChanged(nameof(Shoes)); }
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
        public ShoeListViewModel()
        {
            canAddNewShoe = true;
            canSaveShoe = false;
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            dao = new BLC.BLC(libraryName).DAO;
            producers = new ObservableCollection<IProducer>(dao.GetAllProducers());
            shoes = new ObservableCollection<ShoeViewModel>();
            //IParking parking = new CarsDB.Parking();
            foreach (var shoe in dao.GetAllShoes())
            {
                Shoes.Add(new ShoeViewModel(shoe));
            }
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(shoes);

            addNewShoeCommand = new RelayCommand(param => AddNewShoe());
            saveShoeCommand = new RelayCommand(param => SaveShoe());
            removeShoeCommand = new RelayCommand(param => RemoveShoe());
            cancelSelectionCommand = new RelayCommand(param => CancelSelection());
            filterDataCommand = new RelayCommand(param => FilterData());
        }

        private ShoeViewModel selectedShoe;

        public ShoeViewModel SelectedShoe
        {
            get { return selectedShoe; }
            set
            {
                CanSaveShoe = true;
                CanAddNewShoe = false;
                selectedShoe = value; RaisePropertyChanged(nameof(SelectedShoe));

            }

        }

        private void AddNewShoe()
        {
            ShoeViewModel cvm = new ShoeViewModel(dao.CreateNewShoe());
            
            SelectedShoe = cvm;

            CanAddNewShoe = false;
            CanSaveShoe = true;
        }

        private RelayCommand addNewShoeCommand;

        public RelayCommand AddNewShoeCommand
        {
            get => addNewShoeCommand;
        }

        private RelayCommand saveShoeCommand;

        public ICommand SaveShoeCommand
        {
            get => saveShoeCommand;
        }



        private void SaveShoe()
        {
            if (string.IsNullOrEmpty(selectedShoe.Name))
            {
                MessageBox.Show("Nazwa nie może być pusta.");
                selectedShoe.Name = selectedShoe.originalName;
                return; 
            }

            if (selectedShoe.Name.Length < 3 || selectedShoe.Name.Length > 20)
            {
                MessageBox.Show("Nazwa musi mieć od 3 do 20 znaków.");
                selectedShoe.Name = selectedShoe.originalName;
                return;
            }

            if (selectedShoe.Producer == null)
            {
                MessageBox.Show("Podaj producenta.");
                return;
            }

            if ((selectedShoe.ReleaseYear > DateTime.Now.Year) || (selectedShoe.ReleaseYear < 1900))
            {
                MessageBox.Show("Podaj poprawny rok.");
                selectedShoe.ReleaseYear = selectedShoe.originalReleaseYear;
                return;
            }

            if (string.IsNullOrEmpty(selectedShoe.Description))
            {
                MessageBox.Show("Opis nie może być pusty.");
                selectedShoe.Description = selectedShoe.originalDescription;
                return;
            }

            

            if (SelectedShoe.Id == 0)
            {
                shoes.Add(selectedShoe);
                dao.AddShoe(selectedShoe.Shoe);
            }
            CanAddNewShoe = true;
            CanSaveShoe = false;
            CancelSelection();
            dao.SaveChanges();
        }

        private RelayCommand removeShoeCommand;

        public ICommand RemoveShoeCommand
        {
            get => removeShoeCommand;
        }

        private void RemoveShoe()
        {
            if (selectedShoe == null)
            {
                MessageBox.Show("Nie wybrano buta do usunięcia.");
                return;
            }

            var result = MessageBox.Show("Czy na pewno chcesz usunąć ten but?", "Potwierdzenie usunięcia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // usuwanie z bazy
                dao.RemoveShoe(selectedShoe.Shoe);

                // usuwanie z kolekcji
                shoes.Remove(selectedShoe);

                dao.SaveChanges();

                SelectedShoe = null;
                CanAddNewShoe = true;
                CanSaveShoe = false;
            }
        }

        private RelayCommand filterDataCommand;
        public RelayCommand FilterDataCommand
        { get => filterDataCommand; }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                RaisePropertyChanged(nameof(filter));
            }
        }

        private void FilterData()
        {
            if (string.IsNullOrEmpty(filter))
            {
                view.Filter = null;
            }
            else
            {
                view.Filter = c => ((ShoeViewModel)c).Name.Contains(filter);
            }
        }

        private RelayCommand cancelSelectionCommand;
        public ICommand CancelSelectionCommand
        {
            get => cancelSelectionCommand;
        }
        private void CancelSelection()
        {
            if (selectedShoe != null)
            {
                selectedShoe = null;
                CanAddNewShoe = true;
                CanSaveShoe = false;
                RaisePropertyChanged(nameof(SelectedShoe));
            }
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
