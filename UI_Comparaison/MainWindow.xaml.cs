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
using Comparaison_Assemblage_MGA810.Models;

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

      

        private List<string> _configurations1;

        public List<string> Configurations1
        {
            get
            {
                return _configurations1;
            }
            set
            {
                _configurations1 = value;
                OnPropertyChanged(nameof(Configurations1));
            }

        }

        private List<string> _configurations2;

        public List<string> Configurations2
        {
            get
            {
                return _configurations2;
            }
            set
            {
                _configurations2 = value;
                OnPropertyChanged(nameof(Configurations2));
            }

        }


        private CAD_Software DriverAssembly1;
        private CAD_Software DriverAssembly2;

        private Assembly _assembly1;

        public Assembly Assembly1
        {
            get
            {
                return _assembly1;
            }
            set
            {
                _assembly1 = value;
                OnPropertyChanged(nameof(Assembly1));
            }

        }






        private Assembly _assembly2;

        public Assembly Assembly2
        {
            get
            {
                return _assembly2;
            }
            set
            {
                _assembly2 = value;
                OnPropertyChanged(nameof(Assembly2));
            }

        }


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

        public string Assembly_2_Directory
        {
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


        public bool DefaultState { get; set; }

        private bool _sameSavedDate;
        public bool SameSavedDate
        {
            get
            {
                return _sameSavedDate;
            }
            set
            {
                _sameSavedDate = value;
                OnPropertyChanged(nameof(SameSavedDate));
            }

        }



        private bool _sameNumberOfComponents;
        public bool SameNumberOfComponents
        {
            get
            {
                return _sameNumberOfComponents;
            }
            set
            {
                _sameNumberOfComponents = value;
                OnPropertyChanged(nameof(SameNumberOfComponents));
            }

        }


        public string _assemblyReadResults;



        public string AssemblyReadResults
        {
            get
            {
                return _assemblyReadResults;
            }
            set
            {
                _assemblyReadResults = value;
                OnPropertyChanged(nameof(AssemblyReadResults));
            }
        }

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
                Filter = "SLDASM files (*.SLDASM)|*.SLDASM",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };


            openFileDialog1.ShowDialog();


            switch (parameter.ToString())
            {

                case "Assembly_1_Directory":
                    if (openFileDialog1.FileName != "")
                    {
                        Assembly_1_Directory = openFileDialog1.FileName;
                    }


                    break;

                case "Assembly_2_Directory":
                    if (openFileDialog1.FileName != "")
                    {
                        Assembly_2_Directory = openFileDialog1.FileName;
                    }

                    break;

            }



        }

        private RelayCommand _openAssembly1Command;
        public ICommand OpenAssembly1Command
        {
            get
            {
                if (_openAssembly1Command == null)
                {

                    _openAssembly1Command = new RelayCommand(param => this.OpenAssembly(param), param => Assembly_1_Directory != null
                        );
                }
                return _openAssembly1Command;
            }
        }


        private RelayCommand _openAssembly2Command;
        public ICommand OpenAssembly2Command
        {
            get
            {
                if (_openAssembly2Command == null)
                {

                    _openAssembly2Command = new RelayCommand(param => this.OpenAssembly(param), param => Assembly_2_Directory != null
                        );
                }
                return _openAssembly2Command;
            }
        }


        public void OpenAssembly(object parameter)
        {
            switch (parameter.ToString())
            {

                case "Assembly_1_Directory":

                    DriverAssembly1 = new CAD_SolidWorks();


                    //var theClass = new TheClass(() => TheMethod());

                    Assembly1 = new Assembly(DriverAssembly1.OpenFile(Assembly_1_Directory));


                    AssemblyReadResults += _assembly1.ToString();

                    IsAssemblyDirectory1Found = true;
                    OnPropertyChanged(nameof(IsAssemblyDirectory1Found));

                    Configurations1 = Assembly1.ConfigurationList;
                    // Configurations1.Add("Allô");
                    //Configurations1.Add("Ça va");
 

                    break;

                case "Assembly_2_Directory":

                    DriverAssembly2 = new CAD_SolidWorks();

                    Assembly2 = new Assembly(DriverAssembly2.OpenFile(Assembly_2_Directory));

                    AssemblyReadResults += _assembly2.ToString();

                    Configurations2 = Assembly2.ConfigurationList;

                    IsAssemblyDirectory2Found = true;
                    OnPropertyChanged(nameof(IsAssemblyDirectory2Found));

                    break;

            }



        }


        private RelayCommand _deleteAssemblyDirectoryCommand;
        public ICommand DeleteAssemblyDirectoryCommand
        {
            get
            {
                if (_deleteAssemblyDirectoryCommand == null)
                {
                    _deleteAssemblyDirectoryCommand = new RelayCommand(param => this.DeleteAssemblyDirectory(param)
                        );
                }
                return _deleteAssemblyDirectoryCommand;
            }
        }

        public void DeleteAssemblyDirectory(object parameter)
        {

            Assembly_1_Directory = null;
            IsAssemblyDirectory1Found = false;
            Configurations1 = null;
            OnPropertyChanged("IsAssemblyDirectory1Found");

            Assembly_2_Directory = null;
            IsAssemblyDirectory2Found = false;
            Configurations2 = null;
            OnPropertyChanged("IsAssemblyDirectory2Found");

            AssemblyReadResults = null;
            DefaultState = true;
        }


        private RelayCommand _quickComparisonCommand;
        public ICommand QuickComparisonCommand
        {
            get
            {
                if (_quickComparisonCommand == null)
                {
                    _quickComparisonCommand = new RelayCommand(param => this.QuickComparison(param), param => Assembly1 != null && Assembly2 != null
                        );
                }
                return _quickComparisonCommand;
            }
        }

        public void QuickComparison(object parameter)
        {
            DefaultState = false;

            if (Assembly1.SaveDate == Assembly2.SaveDate)
            {
                SameSavedDate = true;

            }
            else
            {
                SameSavedDate = false;
            }

            if (Assembly1.NumberOfComponents == Assembly2.NumberOfComponents)
            {
                SameNumberOfComponents = true;
            }
            else
            {
                SameNumberOfComponents = false;
            }



        }

        private RelayCommand _launchComparison;
        public ICommand LaunchComparison
        {
            get
            {
                if (_launchComparison == null)
                {
                    _launchComparison = new RelayCommand(param => this.Comparison(param), param => Assembly1 != null && Assembly2 != null
                        );
                }
                return _launchComparison;
            }
        }

        public void Comparison(object parameter)
        {
            DefaultState = false;

            
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
            DefaultState = true;
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
         * Assemblage1 = new Assembly(Model)
         * Ouvrir les fichiers sur SolidWorks
         * Comparer les deux assembly
         * Retourner les résultats
         */
        // Fonction Comparer
        // Valider extension

    }
}

