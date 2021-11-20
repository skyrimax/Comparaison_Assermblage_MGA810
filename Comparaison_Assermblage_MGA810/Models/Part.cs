using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparaison_Assemblage_MGA810
{
    public class Part
    {
        /// <summary>
        /// Classe qui contient toutes les propriétés de pièces à comparer entre deux assemblages.
        /// </summary>

        public string PartName { get; set; }

        public double Mass { get; set; }

        public string Color { get; set; }

        public Part(CAD_SolidWorks.Model Model){
           
        }

}
}
