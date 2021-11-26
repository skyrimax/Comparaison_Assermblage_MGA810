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

        private int _numberOfComponents { get; set; }
        public List<Part> _partList { get; set; }

        private List<Model> _modelList { get; set; }

        public Assembly(Model Assembly)
        {
            _numberOfComponents = Assembly.GetNbComponents();
            _modelList = Assembly.GetComponents();

            _partList = new List<Part>();

            foreach (Model m in _modelList)
            {
                _partList.Add(new Part(m));
            }
        }

        public override string ToString()
        {
            string assemblyResults = string.Empty;

            int padding = 50;

            string header = "      Nom de la pièce".PadRight(padding - 10) + "|" + "      Masse".PadRight(padding-1) + "|" + "      Volume".PadRight(padding-6) + "|" + "      Couleur".PadRight(padding) + "\n" + "------------------------------------------------------------------------------------------------------------------------"+ "\n";



            foreach (Part p in _partList)
            {

                

                char pad = '=';
                assemblyResults += p.PartName.PadRight(padding - p.PartName.Length) + " | ";
                assemblyResults += " | ";
           

                assemblyResults += p.Mass.ToString().PadRight(padding - p.Mass.ToString().Length);
                assemblyResults += " | ";


                assemblyResults += p.Volume.ToString().PadRight(padding - p.Volume.ToString().Length);
                assemblyResults.PadRight(25);
                assemblyResults += " | ";

                assemblyResults += p.Color + "\n";

            }

            return header + assemblyResults;
        }
    }
}
