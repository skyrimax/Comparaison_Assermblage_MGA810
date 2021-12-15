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
        public string PartPath { get; }

        public System.DateTime SaveDateTime { get; }

        public string ActiveConfig { get; }
        public double Mass { get; }

        public string Color { get; }
        public string Material { get; }

        public double Volume { get; }

        public double SurfaceArea { get; }

        public int NbFaces { get; }

        public int NbEdges { get; }

        public System.Numerics.Vector3 CenterOfMass { get; }

        public System.Numerics.Matrix4x4 PrincipalAxes { get; }

        public System.Numerics.Vector3 Inertia { get; }

        public System.Numerics.Matrix4x4 InertiaTensorAtCM { get; }

        public System.Numerics.Matrix4x4 InertiaTensorAtFOR { get; }

        public Part(Model Model)
        {
            PartName = Model.GetFileNameWOExt();
            PartPath = Model.GetFullPath();
            ActiveConfig = Model.GetActiveConfiguration();
            SaveDateTime = Model.GetSaveDateTime();
            Volume = Model.GetVolume();
            Material = Model.GetMaterial();
            Mass = Model.GetMass();
            Volume = Model.GetVolume();
            SurfaceArea = Model.GetSurfaceArea();
            NbFaces = Model.GetNbFaces();
            NbEdges = Model.GetNbEdges();
            Color = Model.GetColors();
            CenterOfMass = Model.GetCenterOfMass();
            PrincipalAxes = Model.GetPrincipalAxes();
            Inertia = Model.GetInertia();
            InertiaTensorAtCM = Model.GetInertiaTensorAtCM();
            InertiaTensorAtFOR = Model.GetInertiaTensorAtFOR();
        }
    }
}
