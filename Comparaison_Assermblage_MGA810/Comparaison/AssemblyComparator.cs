using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparaison_Assemblage_MGA810.Models;

namespace Comparaison_Assemblage_MGA810.Comparaison
{
    class AssemblyComparator
    {
        public AssemblyComparator()
        {
            _elementsToCompare = new ElementsToCompare();
        }

        public AssemblyComparator(bool compareNbParts, bool compareMaterial, bool compareMass,
                                    bool compareVolume, bool compareSurfaceArea, bool compareNbFaces,
                                    bool compareNbEdges, bool compareColors, bool compareCenterOfMass,
                                    bool comparePrincipalAxes, bool compareInertia, bool compareInertiaTersorAtCM,
                                    bool compareInertiaTensorAtFOR, bool compareConstraints, bool compareCustomProperties)
        {
            _elementsToCompare = new ElementsToCompare(compareNbParts, compareMaterial, compareMass,
                                                        compareVolume, compareSurfaceArea, compareNbFaces,
                                                        compareNbEdges, compareColors, compareCenterOfMass,
                                                        comparePrincipalAxes, compareInertia, compareInertiaTersorAtCM,
                                                        compareInertiaTensorAtFOR, compareConstraints, compareCustomProperties);
        }

        public AssemblyComparator(ElementsToCompare elementsToCompare)
        {
            _elementsToCompare = new ElementsToCompare(elementsToCompare);
        }

        public AssemblyComparator(AssemblyComparator assemblyComparator)
        {
            _elementsToCompare = new ElementsToCompare(assemblyComparator._elementsToCompare);
        }

        public class ElementsToCompare
        {
            public ElementsToCompare()
            {

            }

            public ElementsToCompare(bool compareNbParts, bool compareMaterial, bool compareMass,
                                        bool compareVolume, bool compareSurfaceArea, bool compareNbFaces,
                                        bool compareNbEdges, bool compareColors, bool compareCenterOfMass,
                                        bool comparePrincipalAxes, bool compareInertia, bool compareInertiaTersorAtCM,
                                        bool compareInertiaTensorAtFOR, bool compareConstraints, bool compareCustomProperties)
            {
                CompareNbParts = compareNbParts;
                CompareMaterial = compareMaterial;
                CompareMass = compareMass;
                CompareVolume = compareVolume;
                CompareSurfaceArea = compareSurfaceArea;
                CompareNbFaces = compareNbFaces;
                CompareNbEdges = compareNbEdges;
                CompareColors = compareColors;
                CompareCenterOfMass = compareCenterOfMass;
                ComparePrincipalAxes = comparePrincipalAxes;
                CompareInertia = compareInertia;
                CompareInertiaTersorAtCM = compareInertiaTersorAtCM;
                CompareInertiaTensorAtFOR = compareInertiaTensorAtFOR;
                CompareConstraints = compareConstraints;
                CompareCustomProperties = compareCustomProperties;
            }

            public ElementsToCompare(ElementsToCompare elementsToCompare)
            {
                CompareNbParts = elementsToCompare.CompareNbParts;
                CompareMaterial = elementsToCompare.CompareMaterial;
                CompareMass = elementsToCompare.CompareMass;
                CompareVolume = elementsToCompare.CompareVolume;
                CompareSurfaceArea = elementsToCompare.CompareSurfaceArea;
                CompareNbFaces = elementsToCompare.CompareNbFaces;
                CompareNbEdges = elementsToCompare.CompareNbEdges;
                CompareColors = elementsToCompare.CompareColors;
                CompareCenterOfMass = elementsToCompare.CompareCenterOfMass;
                ComparePrincipalAxes = elementsToCompare.ComparePrincipalAxes;
                CompareInertia = elementsToCompare.CompareInertia;
                CompareInertiaTersorAtCM = elementsToCompare.CompareInertiaTersorAtCM;
                CompareInertiaTensorAtFOR = elementsToCompare.CompareInertiaTensorAtFOR;
                CompareConstraints = elementsToCompare.CompareConstraints;
                CompareCustomProperties = elementsToCompare.CompareCustomProperties;
            }

            public bool CompareNbParts { get; set; }

            public bool CompareMaterial { get; set; }

            public bool CompareMass { get; set; }

            public bool CompareVolume { get; set; }

            public bool CompareSurfaceArea { get; set; }

            public bool CompareNbFaces { get; set; }

            public bool CompareNbEdges { get; set; }

            public bool CompareColors { get; set; }

            public bool CompareCenterOfMass { get; set; }

            public bool ComparePrincipalAxes { get; set; }

            public bool CompareInertia { get; set; }

            public bool CompareInertiaTersorAtCM { get; set; }

            public bool CompareInertiaTensorAtFOR { get; set; }

            public bool CompareConstraints { get; set; }

            public bool CompareCustomProperties { get; set; }
        }

        public bool CompareAssemblies(Assembly assembly1, Assembly assembly2)
        {
            bool isSame = true;



            return isSame;
        }

        private ElementsToCompare _elementsToCompare;
    }
}
