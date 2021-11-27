using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comparaison_Assemblage_MGA810.CAD_Software;

namespace Comparaison_Assemblage_MGA810.Models
{
    public class Assembly
    {

        private int _numberOfComponents { get; }
        public List<Part> PartList { get; }

        public List<string> ConfigurationList { get; }

        private List<Model> _modelList { get;  }

        public Assembly(Model Assembly)
        {
            _numberOfComponents = Assembly.GetNbComponents();
            _modelList = Assembly.GetComponents();

            ConfigurationList = Assembly.GetConfigurations();

            PartList = new List<Part>();

            foreach (Model m in _modelList)
            {
                PartList.Add(new Part(m));
            }
        }

        public override string ToString()
        {
            string assemblyResults = string.Empty;

            int padding = 55;

            string header = "------------------------------------------------------------------------------------------------------------------------" +
                 "\n" + "      Nom de la pièce".PadRight(padding - 10) + "|" + "      Masse".PadRight(padding-1) + "|" + "      Volume".PadRight(padding-6) + "|" + "      Couleur".PadRight(padding) + "|" + "      Matériau".PadRight(padding)
                + "\n" + "------------------------------------------------------------------------------------------------------------------------"
                + "\n";



            foreach (Part p in PartList)
            {

                

                char pad = '=';
                assemblyResults += p.PartName.PadRight(padding - p.PartName.Length) + " | ";
                assemblyResults += " | ";
           

                assemblyResults += p.Mass.ToString().PadRight(padding  - p.Mass.ToString().Length);
                assemblyResults += " | ";


                assemblyResults += p.Volume.ToString().PadRight(padding - p.Volume.ToString().Length);
                assemblyResults += " | ";

                //assemblyResults += p.Color.PadRight(padding - p.Volume.ToString().Length);
                assemblyResults += p.Material + "\n";

            }

            return header + assemblyResults;
        }
    }
}
