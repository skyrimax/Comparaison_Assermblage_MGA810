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

        public string Assembly_1_Directory { get; set; }


        private string _assembly_2_Directory { get; set; }

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


        /// <summary>
        /// Commande pour sauvegarer en CSV
        /// </summary>
        public RelayCommand Save { get; set; }



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
            Assembly_2_Directory = "Allô";
            
        }

       
    }
}
