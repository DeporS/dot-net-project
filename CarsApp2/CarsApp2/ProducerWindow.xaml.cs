using Interfaces;
using ShoesGUI.ViewModels;
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

namespace ShoesGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProducerWindow : Window
    {
      

        public ProducerWindow()
        {
            InitializeComponent();
            DataContext = ViewModels.ProducerListViewModel.Instance;
        }

        private void SwitchToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            if (AddButton.IsEnabled)
            {
                SwitchToMainWindow();
            }
            else
            {
                MessageBox.Show("Przycisk Dodaj jest zablokowany!");
            }
        }

        public ObservableCollection<ProducerViewModel> getProducerList()
        {
            return ProducerListViewModel.Instance.Producers;
        }

        private void SwitchToMainWindow()
        {
            var mainView = new MainWindow();
            mainView.Show();

            this.Close();
        }
    }
}