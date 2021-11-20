using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparaison_Assemblage_MGA810.Models
{
    class ElementsToCompare
    {
        public bool CompareMaterial { get; set; }

        public bool CompareMass { get; set; }

        public bool CompareVolume { get; set; }

        public bool CompareSurfaceArea { get; set; }

        public bool CompareNbFaces { get; set; }

        public bool CompareNbEdges{ get; set; }

        public bool CompareColors{ get; set; }

        public bool CompareCenterOfMass { get; set; }

        public bool ComparePrincipalAxes { get; set; }

        public bool CompareInteria { get; set; }

    }
}


