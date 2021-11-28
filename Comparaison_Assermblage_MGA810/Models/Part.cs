using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comparaison_Assemblage_MGA810.CAD_Software;

namespace Comparaison_Assemblage_MGA810
{
    public class Part
    {
        /// <summary>
        /// Classe qui contient toutes les propriétés de pièces à comparer entre deux assemblages.
        /// </summary>


        // PAS de set;
        public string PartName { get; }

        public double Mass { get;  }

        public string Color { get;  }
        public string Material { get;  }

        public double Volume { get;  }

        public Part(Model Model)
        {
            PartName = String.Empty;
            Volume = Model.GetVolume();
        }

    }
}
