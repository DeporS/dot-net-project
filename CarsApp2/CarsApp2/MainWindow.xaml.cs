using Interfaces;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShoesGUI.ViewModels;

namespace ShoesGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ShoeListViewModel();
            // lista.ItemsSource = Cars;// parking.GetAllCars();
            //System.Enum.GetValues(typeof(TransmissionType));
        }

        private void SwitchToProducerWindow_Click(object sender, RoutedEventArgs e)
        {
            if (AddButton.IsEnabled)
            {
                SwitchToProducerWindow();
            }
            else
            {
                MessageBox.Show("Przycisk Dodaj jest zablokowany!");
            }
        }

        public ObservableCollection<ShoeViewModel> getShoesList()
        {
            return ShoeListViewModel.Instance.Shoes;
        }

        private void SwitchToProducerWindow()
        {
            var producerView = new ProducerWindow();
            producerView.Show();

            this.Close();
        }

        public void RefreshProducers()
        {
            var producers = ProducerListViewModel.Instance.dao.GetAllProducers();

            ProducerListViewModel.Instance.Producers = new ObservableCollection<ProducerViewModel>(
                producers.Select(producer => new ProducerViewModel(producer))
            );
        }
    }
}