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
        public string AssemblyPath { get; }
        public string ActiveConfig { get; }
        public DateTime SaveDate { get; }
        public int NumberOfComponents { get; }
        public List<Part> PartList { get; }

        public List<string> ConfigurationList { get; }

        public Assembly(Model assembly)
        {
            NumberOfComponents = assembly.GetNbComponents();
            List<Model> modelList = assembly.GetComponents();

            AssemblyPath = assembly.GetFullPath();
            ActiveConfig = assembly.GetActiveConfiguration();

            SaveDate = assembly.GetSaveDateTime();

            ConfigurationList = assembly.GetConfigurations();

            PartList = new List<Part>();

            foreach (Model m in modelList)
            {
                PartList.Add(new Part(m));
            }
        }

        public override string ToString()
        {
            string assemblyResults = string.Empty;

            int padding = 40;

            string header = "\n" + "------------------------------------------------------------------------------------------------------------------------" +
                 "\n" + "Nom de la pièce".PadRight(padding) + "|" + "Masse".PadRight(padding + 1) + "|" + "Volume".PadRight(padding) + "|" + "Couleur".PadRight(padding) + "|" + "Matériau".PadRight(padding)
                + "\n" + "------------------------------------------------------------------------------------------------------------------------"
                ;


            foreach (Part p in PartList)
            {


                assemblyResults += "\n" + String.Format("{0,-30}||{1,-30}|{2,-30}|{3,-30}|{4,-30}|", p.PartName, p.Mass.ToString(), p.Volume.ToString(), p.Color, p.Material);
                //char pad = '=';


                //assemblyResults += p.Mass.ToString().PadRight(padding );
                //assemblyResults += " | ";


                //assemblyResults += p.Volume.ToString().PadRight(padding );
                //assemblyResults += " | ";



                ////assemblyResults += p.Color.PadRight(padding - p.Volume.ToString().Length);
                //assemblyResults += p.Material.PadRight(padding );
                //assemblyResults += " | ";

                //assemblyResults += p.PartName.PadRight(padding ) + " | ";
                //assemblyResults += " | " + "\n"; ;


            }

            return header + assemblyResults;
        }
    }
}
