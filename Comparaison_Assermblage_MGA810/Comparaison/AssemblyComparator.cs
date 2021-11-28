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

        public AssemblyComparator(bool compareFilePath, bool compareSameDateTime,
                                    bool compareNbParts, bool compareMaterial, bool compareMass,
                                    bool compareVolume, bool compareSurfaceArea, bool compareNbFaces,
                                    bool compareNbEdges, bool compareColors, bool compareCenterOfMass,
                                    bool comparePrincipalAxes, bool compareInertia, bool compareInertiaTersorAtCM,
                                    bool compareInertiaTensorAtFOR, bool compareConstraints, bool compareCustomProperties)
        {
            _elementsToCompare = new ElementsToCompare(compareFilePath, compareSameDateTime,
                                                        compareNbParts, compareMaterial, compareMass,
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

            public ElementsToCompare(bool compareFilePath, bool compareSaveDateTime,
                                        bool compareNbParts, bool compareMaterial, bool compareMass,
                                        bool compareVolume, bool compareSurfaceArea, bool compareNbFaces,
                                        bool compareNbEdges, bool compareColors, bool compareCenterOfMass,
                                        bool comparePrincipalAxes, bool compareInertia, bool compareInertiaTersorAtCM,
                                        bool compareInertiaTensorAtFOR, bool compareConstraints, bool compareCustomProperties)
            {
                CompareFilePath = compareFilePath;
                CompareSaveDateTime = compareSaveDateTime;
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
                CompareFilePath = elementsToCompare.CompareFilePath;
                CompareSaveDateTime = elementsToCompare.CompareSaveDateTime;
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

            public bool CompareFilePath { get; set; }

            public bool CompareSaveDateTime { get; set; }

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

            if (_elementsToCompare.CompareFilePath &&
                assembly1.)
            {

            }

            return isSame;
        }

        public int CompareParts(Part part1, Part part2)
        {
            return 0;
        }

        private List<List<int>> GetRepeatedParts(Assembly assembly)
        {
            int nbParts = assembly.PartList.Count;
            List<List<int>> repeatedParts = new List<List<int>>(nbParts);

            List<bool> partAssigned = new List<bool>(nbParts);

            // Loop needed to initialize both lists with values
            for(int i = 0; i < nbParts; ++i)
            {
                repeatedParts[i] = new List<int>();
                partAssigned[i] = new bool();
            }

            for(int i = 0; i < nbParts; ++i)
            {
                if (!partAssigned[i])
                {
                    for(int j = i+1; j < nbParts; ++j)
                    {
                        if (!partAssigned[j])
                        {
                            // If 2 parts are judged to be the same
                            if(CompareParts(assembly.PartList[i], assembly.PartList[j]) > _threshold)
                            {
                                // Assign the same list to the 2nd part as the first for quick access later
                                repeatedParts[j] = repeatedParts[i];

                                // Add the 2nd part to the list
                                repeatedParts[i].Add(j);

                                // Add 2nd part to the list of assigned parts to prevent repetition later
                                partAssigned[j] = true;
                            }
                        }
                    }
                }
            }

            return repeatedParts;
        }

        private int[,] ComparePartsInAsembly(Assembly assembly1, Assembly assembly2)
        {
            int nbElementsAss1 = assembly1.PartList.Count;
            int nbElementsAss2 = assembly2.PartList.Count;

            int[,] results = new int[nbElementsAss1, nbElementsAss2];

            for(int i = 0; i < nbElementsAss1; ++i)
            {
                for(int j = 0; j < nbElementsAss2; ++j)
                {
                    results[i, j] = CompareParts(assembly1.PartList[i], assembly2.PartList[i]);
                }
            }

            return results;
        }

        private ElementsToCompare _elementsToCompare;
        private int _threshold = 95;
    }
}
