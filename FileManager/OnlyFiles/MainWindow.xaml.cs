using OnlyFiles.Class;
using OnlyFiles.DiskDirectory;
using OnlyFiles.DiskDrive;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace OnlyFiles
{
    public partial class MainWindow : Window
    {
        private readonly IDirectoryService _directoryService;
        private readonly IDriveService _driveService;
        public ICommand ButtonClickCommand { get; }
        public MainWindow(IDirectoryService directoryService, IDriveService driveService)
        {
            _directoryService = directoryService;
            _driveService = driveService;
            InitializeComponent();
            FillDriveComboBox();
            ButtonClickCommand = new RelayCommand(ButtonClickExecute, ButtonClickCanExecute);
            DataContext = this;
        }

        private void FillDriveComboBox()
        {
            var readyDrives = _driveService.GetReadyDrives();
            foreach (var driveName in readyDrives)
            {
                comboBoxDrives.Items.Add(driveName);
            }
        }

        private async void ButtonClickExecute(object parameter)
        {
            string folderPath = comboBoxDrives.SelectedItem.ToString();
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            ObservableCollection<DirectoryItem> items = new ObservableCollection<DirectoryItem>();
            progressBar.Visibility = Visibility.Visible;
            DirectoryItem rootItem = await _directoryService.CreateDirectoryItemAsync(directoryInfo);
            items.Add(rootItem);
            treeView1.ItemsSource = items;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private bool ButtonClickCanExecute(object parameter)
        {
            return comboBoxDrives.SelectedItem != null;
        }
    }
}
