using Comparaison_Assemblage_MGA810;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using UI_Comparaison.Commands;
//using Comparaison_Assemblage_MGA810;

namespace UI_Comparaison
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {



        /// <summary>
        /// Répertoires qui contiennent les chemins d'accès des assemblages
        /// </summary>



        //public string Assembly_1_Directory { get; set; }

        private CAD_Software DriverAssembly1;
        private CAD_Software DriverAssembly2;


        private string _assembly_1_Directory;

        public string Assembly_1_Directory
        {
            get
            {
                return _assembly_1_Directory;
            }
            set
            {
                this._assembly_1_Directory = value;
                OnPropertyChanged(nameof(this.Assembly_1_Directory));
            }

        }



        private string _assembly_2_Directory;

        public string Assembly_2_Directory { 
            get 
            { 
                return _assembly_2_Directory; 
            }
            set
            { 
                this._assembly_2_Directory = value;
                OnPropertyChanged(nameof(this.Assembly_2_Directory));
            }

        }
        public bool IsAssemblyDirectory1Found { get; set; }

        public bool IsAssemblyDirectory2Found { get; set; }


        /// <summary>
        /// Commande pour sauvegarer en CSV
        /// </summary>
     

        private RelayCommand _getAssemblyDirectoryCommand;
        public ICommand GetAssemblyDirectoryCommand
        {
            get
            {
                if (_getAssemblyDirectoryCommand == null)
                {
                    _getAssemblyDirectoryCommand = new RelayCommand(param => this.GetAssemblyDirectory(param)
                        );
                }
                return _getAssemblyDirectoryCommand;
            }
        }

        public void GetAssemblyDirectory(object parameter)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {

                Title = "Rechercher les fichiers CSV",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "CSV files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };


            openFileDialog1.ShowDialog();
                     

            switch (parameter.ToString()) {

                case "Assembly_1_Directory":
                    Assembly_1_Directory = openFileDialog1.FileName;
                    DriverAssembly1 = new CAD_SolidWorks();
                    IsAssemblyDirectory1Found = true;
                    OnPropertyChanged(nameof(IsAssemblyDirectory1Found));
                
                    break;

                case "Assembly_2_Directory":
                    Assembly_2_Directory = openFileDialog1.FileName;
                    DriverAssembly2 = new CAD_SolidWorks();
                    IsAssemblyDirectory2Found = true;
                    OnPropertyChanged(nameof(IsAssemblyDirectory2Found));
            
                    break;
            
            }
                


        }

        /// <summary>
        /// Implantation de la méthode de l'interface INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
          
        }

        /// <summary>
        /// Constructeur de la fenêtre principale. Le DataContext est défini.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        // Ouvrir et créer CAD_Solidworks 
        // (CAD_Solidworks)



        /*
         * Valider extension
         * Si .sldasm ou .asm, extensier CAD_SolidWorks
         * Ouvrir fichier avec Cad_SolidWorks
         * Vérifier extension 2
         * Si même que assemblage 1,
         * Réutiliser Cad_SolidWorks
         * Enumeration Software (pas démarrer 2 fois sld, réutiliser la même si même que 1 SOlidWorks)
         *
         * Construit  objet Assembly à partir de model retourner par Driver (x 2) 
         * Ouvrir les fichiers sur SolidWorks
         * Comparer les deux assembly
         * Retourner les résultats
         */
        // Fonction Comparer
        // Valider extension

    }
}
