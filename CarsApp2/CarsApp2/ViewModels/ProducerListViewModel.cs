using Interfaces;
using ShoesGUI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ShoesGUI.ViewModels
{
    public class ProducerListViewModel : INotifyPropertyChanged
    {
        private bool canAddNewProducer;

        public bool CanAddNewProducer
        {
            get { return canAddNewProducer; }
            set
            {
                if (canAddNewProducer != value)
                {
                    canAddNewProducer = value;
                    RaisePropertyChanged(nameof(CanAddNewProducer));
                }
            }
        }

        private bool canSaveProducer;

        public bool CanSaveProducer
        {
            get { return canSaveProducer; }
            set
            {
                if (canSaveProducer != value)
                {
                    canSaveProducer = value;
                    RaisePropertyChanged(nameof(CanSaveProducer));
                }
            }
        }

        #region singletone
        private static ProducerListViewModel _instance;
        private static readonly object _lock = new object();
        public static ProducerListViewModel Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new ProducerListViewModel();
                }
            }
        }
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView view;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ProducerViewModel> producers;

        public ObservableCollection<ProducerViewModel> Producers
        {
            get { return producers; }
            set { producers = value; RaisePropertyChanged(nameof(Producers)); }
        }

        private IDAO dao;
        public ProducerListViewModel()
        {
            canAddNewProducer = true;
            canSaveProducer = false;
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            dao = new BLC.BLC(libraryName).DAO;
            producers = new ObservableCollection<ProducerViewModel>();

            foreach (var producer in dao.GetAllProducers())
            {;
                Producers.Add(new ProducerViewModel(producer));
            }
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(producers);
            addNewProducerCommand = new RelayCommand(param => AddNewProducer());
            deleteProducerCommand = new RelayCommand(param => DeleteProducer());
            saveProducerCommand = new RelayCommand(param => SaveProducer());
            filterDataCommand = new RelayCommand(param => FilterData());
            cancelSelectionCommand = new RelayCommand(param => CancelSelection());
        }

        private ProducerViewModel selectedProducer;

        public ProducerViewModel SelectedProducer
        {
            get { return selectedProducer; }
            set
            {
                selectedProducer = value;

                //if (CanAddNewProducer())
                //{
                //    EditedProducer = SelectedProducer;
                //}
                CanAddNewProducer = false;
                CanSaveProducer = true;
                RaisePropertyChanged(nameof(SelectedProducer));
            }
        }

        private ProducerViewModel editedProducer;
        public ProducerViewModel EditedProducer
        {
            get { return editedProducer; }
            set
            {
                editedProducer = value;
                RaisePropertyChanged(nameof(EditedProducer));
            }
        }
        private void AddNewProducer()
        {
            ProducerViewModel cvm = new ProducerViewModel(dao.CreateNewProducer());
            SelectedProducer = cvm;
            CanAddNewProducer = false;
            CanSaveProducer = true;
            //cvm.IsChanged = true;
            //SelectedProducer = null;
        }


        private void SaveProducer()
        {
            if (selectedProducer == null)
            {
                MessageBox.Show("Nie wybrano Producenta.");
                return;
            }

            if (string.IsNullOrEmpty(selectedProducer.Name))
            {
                MessageBox.Show("Nazwa nie może być pusta.");
                selectedProducer.Name = selectedProducer.originalName;
                return;
            }

            if (SelectedProducer.Id == 0)
            {
                producers.Add(selectedProducer);
                dao.AddProducer(selectedProducer.Producer);
            }
            CanAddNewProducer = true;
            CanSaveProducer = false;
            CancelSelection();
            dao.SaveChanges();
        }
        private void DeleteProducer()
        {

            if (selectedProducer == null)
            {
                MessageBox.Show("Nie wybrano producenta do usunięcia.");
                return;
            }

            var result = MessageBox.Show("Czy na pewno chcesz usunąć tego producenta?", "Potwierdzenie usunięcia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // usuwanie z bazy
                dao.RemoveProducer(selectedProducer.Producer);

                // usuwanie z kolekcji
                producers.Remove(selectedProducer);

                dao.SaveChanges();

                SelectedProducer = null;
                CanAddNewProducer = true;
                CanSaveProducer = false;
            }
        }

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
                view.Filter = c => ((ProducerViewModel)c).Name.Contains(filter);
            }
        }

        private RelayCommand cancelSelectionCommand;
        public ICommand CancelSelectionCommand
        {
            get => cancelSelectionCommand;
        }

        private void CancelSelection()
        {
            if (selectedProducer != null)
            {
                selectedProducer = null;
                CanAddNewProducer = true;
                CanSaveProducer = false;
                RaisePropertyChanged(nameof(SelectedProducer));
            }
        }

        private RelayCommand addNewProducerCommand;

        public RelayCommand AddNewProducerCommand
        {
            get => addNewProducerCommand;
        }

        private RelayCommand saveProducerCommand;

        public RelayCommand SaveProducerCommand
        {
            get => saveProducerCommand;
        }

        private RelayCommand filterDataCommand;
        public RelayCommand FilterDataCommand
        { get => filterDataCommand; }

        private RelayCommand undoChangesCommand;
        public RelayCommand UndoChangesCommand
        {
            get => undoChangesCommand;
        }
        private RelayCommand deleteProducerCommand;
        public RelayCommand DeleteProducerCommand
        {
            get => deleteProducerCommand;
        }
    }
}
