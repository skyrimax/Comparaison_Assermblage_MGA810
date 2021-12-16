using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparaison_Assemblage_MGA810.Models;

namespace Comparaison_Assemblage_MGA810
{
    public class AssemblyComparator
    {
        public AssemblyComparator()
        {
            _elementsToCompare = new ElementsToCompare();
        }

        public AssemblyComparator(bool compareFilePath, bool compareActiveConfig, bool compareSameDateTime,
                                    bool compareNbParts, bool compareMaterial, bool compareMass,
                                    bool compareVolume, bool compareSurfaceArea, bool compareNbFaces,
                                    bool compareNbEdges, bool compareColors, bool compareCenterOfMass,
                                    bool compareInertia, bool compareConstraints, bool compareCustomProperties)
        {
            _elementsToCompare = new ElementsToCompare(compareFilePath, compareActiveConfig, compareSameDateTime,
                                                        compareNbParts, compareMaterial, compareMass,
                                                        compareVolume, compareSurfaceArea, compareNbFaces,
                                                        compareNbEdges, compareColors, compareCenterOfMass,
                                                        compareInertia, compareConstraints, compareCustomProperties);
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

            public ElementsToCompare(bool compareFilePath, bool compareActiveConfig, bool compareSaveDateTime,
                                        bool compareNbParts, bool compareMaterial, bool compareMass,
                                        bool compareVolume, bool compareSurfaceArea, bool compareNbFaces,
                                        bool compareNbEdges, bool compareColors, bool compareCenterOfMass,
                                        bool compareInertia, bool compareConstraints, bool compareCustomProperties)
            {
                CompareFilePath = compareFilePath;
                CompareActiveConfig = compareActiveConfig;
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
                CompareInertia = compareInertia;
                CompareConstraints = compareConstraints;
                CompareCustomProperties = compareCustomProperties;
            }

            public ElementsToCompare(ElementsToCompare elementsToCompare)
            {
                CompareFilePath = elementsToCompare.CompareFilePath;
                CompareActiveConfig = elementsToCompare.CompareActiveConfig;
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
                CompareInertia = elementsToCompare.CompareInertia;
                CompareConstraints = elementsToCompare.CompareConstraints;
                CompareCustomProperties = elementsToCompare.CompareCustomProperties;
            }

            public bool CompareFilePath { get; set; }

            public bool CompareActiveConfig { get; set; }

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

            public bool CompareInertia { get; set; }

            public bool CompareConstraints { get; set; }

            public bool CompareCustomProperties { get; set; }
        }

        public bool CompareAssemblies(Assembly assembly1, Assembly assembly2, out int[] associations)
        {
            if (_elementsToCompare.CompareNbParts && (assembly1.NumberOfComponents != assembly2.NumberOfComponents))
            {
                associations = new int[0];

                return false;
            }

            //if(QuickCompare(assembly1.AssemblyPath, assembly2.AssemblyPath,
            //                assembly1.ActiveConfig, assembly2.ActiveConfig,
            //                assembly1.SaveDate, assembly2.SaveDate))
            //{
            //    associations = new int[1];

            //    return true;
            //}

            List<List<int>> repeatedPartsAss1 = GetRepeatedParts(assembly1);
            List<List<int>> repeatedPartsAss2 = GetRepeatedParts(assembly2);

            return AssociateParts(assembly1, assembly2, repeatedPartsAss1, repeatedPartsAss2,
                                    out associations);
        }

        public int CompareParts(Part part1, Part part2)
        {
            int score = 0;
            int scoreMax = 0;

            if (QuickCompare(part1.PartPath, part2.PartPath, part1.ActiveConfig, part2.ActiveConfig, part1.SaveDateTime, part2.SaveDateTime))
            {
                return 100;
            }

            // Discriminating properties
            if (_elementsToCompare.CompareVolume)
            {
                if (part1.Volume == part2.Volume)
                {
                    score += 12;
                }
                else
                {
                    return 0;
                }

                scoreMax += 12;
            }

            if (_elementsToCompare.CompareSurfaceArea)
            {
                if (part1.SurfaceArea == part2.SurfaceArea)
                {
                    score += 12;
                }
                else
                {
                    return 0;
                }

                scoreMax += 12;
            }

            if (_elementsToCompare.CompareNbFaces)
            {
                if (part1.NbFaces == part2.NbFaces)
                {
                    score += 12;
                }
                else
                {
                    return 0;
                }

                scoreMax += 12;
            }

            if (_elementsToCompare.CompareNbEdges)
            {
                if (part1.NbEdges == part2.NbEdges)
                {
                    score += 12;
                }
                else
                {
                    return 0;
                }

                scoreMax += 12;
            }

            if (_elementsToCompare.CompareInertia)
            {
                if (CompareInertia(part1, part2))
                {
                    score += 12;
                }
                else
                {
                    return 0;
                }

                scoreMax += 12;
            }

            // Complementary properties
            if (_elementsToCompare.CompareMaterial)
            {
                if (part1.Material == part2.Material)
                {
                    score += 6;
                }

                scoreMax += 6;
            }

            if (_elementsToCompare.CompareMass)
            {
                if (part1.Mass == part2.Mass)
                {
                    score += 6;
                }

                scoreMax += 6;
            }

            if (_elementsToCompare.CompareColors)
            {
                if (part1.Color == part2.Color)
                {
                    score += 6;
                }

                scoreMax += 6;
            }

            if (_elementsToCompare.CompareCenterOfMass)
            {
                if (part1.CenterOfMass == part2.CenterOfMass)
                {
                    score += 6;
                }

                scoreMax += 6;
            }

            if (_elementsToCompare.CompareConstraints)
            {
                if (CompareConstraints(part1, part2))
                {
                    score += 6;
                }

                scoreMax += 6;
            }

            if (_elementsToCompare.CompareCustomProperties)
            {
                if (CompareCustomProperties(part1, part2))
                {
                    score += 6;
                }

                scoreMax += 6;
            }

            return score * 100 / scoreMax;
        }

        private List<List<int>> GetRepeatedParts(Assembly assembly)
        {
            int nbParts = assembly.PartList.Count;
            List<List<int>> repeatedParts = new List<List<int>>(nbParts);

            List<bool> partAssigned = new List<bool>(nbParts);

            // Loop needed to initialize both lists with values
            for (int i = 0; i < nbParts; ++i)
            {
                repeatedParts.Add(new List<int>());
                repeatedParts[i].Add(i);
                partAssigned.Add(new bool());
            }

            for (int i = 0; i < nbParts; ++i)
            {
                if (!partAssigned[i])
                {
                    for (int j = i + 1; j < nbParts; ++j)
                    {
                        if (!partAssigned[j])
                        {
                            // If 2 parts are judged to be the same
                            if (CompareParts(assembly.PartList[i], assembly.PartList[j]) > _threshold)
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

            for (int i = 0; i < nbElementsAss1; ++i)
            {
                for (int j = 0; j < nbElementsAss2; ++j)
                {
                    results[i, j] = CompareParts(assembly1.PartList[i], assembly2.PartList[j]);
                }
            }

            return results;
        }

        private bool AssociateParts(Assembly assembly1, Assembly assembly2,
            List<List<int>> repeatedParts1, List<List<int>> repeatedParts2,
            out int[] associations)
        {
            if (assembly1.NumberOfComponents != assembly2.NumberOfComponents)
            {
                associations = new int[0];

                return false;
            }

            int[,] assembliesCrossComparaison = IsSame(ComparePartsInAsembly(assembly1, assembly2));

            int nbPieces = assembly1.NumberOfComponents;

            int nbEqPartsRow;
            int nbEqPartsColumn;

            List<Line> matchesRows;
            List<Line> matchesColumns;

            associations = new int[nbPieces];
            bool[] associated = new bool[nbPieces];

            // for each row (parts of assembly 1)
            for (int i = 0; i < nbPieces; ++i)
            {
                //making sure current row hasn't been previously assigned
                if (!associated[i])
                {
                    // Reset lists of lines for both rows (assembly 1) and columns (assembly 2)
                    matchesRows = new List<Line>();
                    matchesColumns = new List<Line>();

                    // Get indices of matches in current row
                    matchesRows.Add(new Line(i, GetMatchesRow(assembliesCrossComparaison, i)));

                    // Store the number of matches found on the current row
                    nbEqPartsRow = matchesRows[0].PartsIndicies.Count;

                    // Checking of there is matches on the current row
                    if (nbEqPartsRow == 0)
                    {
                        // return different state
                        associations = new int[0];

                        return false;
                    }

                    // For each columns that match in current row
                    for (int j = 0; j < nbEqPartsRow; ++j)
                    {
                        // get column index
                        int column = matchesRows[0].PartsIndicies[j];

                        matchesColumns.Add(new Line(column, GetMatchesColumns(assembliesCrossComparaison, column)));
                    }

                    // Check that all indicies of the colums are the same
                    if (!SameIndices(matchesColumns))
                    {
                        // return different state
                        associations = new int[0];

                        return false;
                    }

                    // Check if indices of columns match repeated parts of assembly 1 at current row
                    if (!SameIndices(matchesColumns[0].PartsIndicies, repeatedParts1[i]))
                    {
                        // return different state
                        associations = new int[0];

                        return false;
                    }

                    // Store the number of matches for the columns matched with the current row
                    nbEqPartsColumn = matchesColumns[0].PartsIndicies.Count;

                    // Check if there as the same number of matches on rows as the number of matches on columns
                    if (nbEqPartsRow != nbEqPartsColumn)
                    {
                        // return different state
                        associations = new int[0];

                        return false;
                    }

                    // For each row that match in the matched columns, excluding the fist
                    for (int j = 1; j < nbEqPartsColumn; ++j)
                    {
                        // get row index
                        int row = matchesColumns[0].PartsIndicies[j];

                        matchesRows.Add(new Line(row, GetMatchesRow(assembliesCrossComparaison, row)));
                    }

                    // Check that all indicies of the rows are the same
                    if (!SameIndices(matchesRows))
                    {
                        // return different state
                        associations = new int[0];

                        return false;
                    }

                    // Check if indices of rows match repeated parts of assembly 2 at matched columns
                    if (!SameIndices(matchesRows[0].PartsIndicies, repeatedParts2[matchesColumns[0].Pos]))
                    {
                        // return different state
                        associations = new int[0];

                        return false;
                    }

                    // Make associations between parts of assembly 1 with corresponding parts of assembly 2
                    for (int j = 0; j < nbEqPartsRow; ++j)
                    {
                        // Associate jth index of columns with jth index of rows
                        // Same as associating jth part of assembly 2 with jth part of assembly 1
                        associations[matchesRows[j].Pos] = matchesColumns[j].Pos;

                        // Indicate that the row at the jth column match has been associated
                        associated[matchesRows[j].Pos] = true;
                    }
                }
            }

            return true;
        }

        private int SumRow(int[,] array, int row)
        {
            int sum = 0;

            int nbColumns = array.GetLength(1);

            for (int i = 0; i < nbColumns; ++i)
            {
                sum += array[row, i];
            }

            return sum;
        }

        private int SumColumn(int[,] array, int column)
        {
            int sum = 0;

            int nbRows = array.GetLength(0);

            for (int i = 0; i < nbRows; ++i)
            {
                sum += array[i, column];
            }

            return sum;
        }

        private List<int> GetMatchesRow(int[,] array, int row)
        {
            List<int> resultats = new List<int>();

            int nbColumns = array.GetLength(1);

            for (int i = 0; i < nbColumns; ++i)
            {
                if (array[row, i] != 0)
                {
                    resultats.Add(i);
                }
            }

            return resultats;
        }

        private List<int> GetMatchesColumns(int[,] array, int column)
        {
            List<int> resultats = new List<int>();

            int nbRows = array.GetLength(0);

            for (int i = 0; i < nbRows; ++i)
            {
                if (array[i, column] != 0)
                {
                    resultats.Add(i);
                }
            }

            return resultats;
        }

        private bool SameIndices(List<Line> lines)
        {
            int nbLines = lines.Count;

            for (int i = 1; i < nbLines; ++i)
            {
                if (!SameIndices(lines[0].PartsIndicies, lines[i].PartsIndicies))
                {
                    return false;
                }
            }

            return true;
        }

        private bool SameIndices(List<int> list1, List<int> list2)
        {
            int nb = list1.Count;

            if (nb != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < nb; ++i)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private int[,] IsSame(int[,] comparaisons)
        {
            int nbRows = comparaisons.GetLength(0);
            int nbColumns = comparaisons.GetLength(1);

            int[,] results = new int[nbRows, nbColumns];

            for (int i = 0; i < nbRows; ++i)
            {
                for (int j = 0; j < nbColumns; ++j)
                {
                    results[i, j] = (comparaisons[i, j] >= _threshold) ? 1 : 0;
                }
            }

            return results;
        }

        private bool CompareInertia(Part part1, Part part2)
        {
            return CompareInertia(part1.PrincipalAxes, part1.Inertia, part2.PrincipalAxes, part2.Inertia);
        }

        private bool CompareInertia(System.Numerics.Matrix4x4 primaryAxes1, System.Numerics.Vector3 inertia1,
                                    System.Numerics.Matrix4x4 primaryAxes2, System.Numerics.Vector3 inertia2)
        {
            return true;
        }

        private bool CompareConstraints(Part part1, Part part2)
        {
            return true;
        }

        private bool CompareCustomProperties(Part part1, Part part2)
        {
            return true;
        }

        private bool QuickCompare(string path1, string path2,
                                    string activeConfig1, string activeConfig2,
                                    System.DateTime saveDateTime1, System.DateTime saveDateTime2)
        {
            bool samePath = path1 == path2;
            bool sameActiveConfig = activeConfig1 == activeConfig2;
            bool sameSaveDateTime = saveDateTime1 == saveDateTime2;

            return (!_elementsToCompare.CompareFilePath && !_elementsToCompare.CompareActiveConfig &&
                    _elementsToCompare.CompareSaveDateTime && sameSaveDateTime) ||
                    (!_elementsToCompare.CompareFilePath && _elementsToCompare.CompareActiveConfig &&
                    sameActiveConfig && !_elementsToCompare.CompareSaveDateTime) ||
                    (_elementsToCompare.CompareFilePath && samePath &&
                    !_elementsToCompare.CompareActiveConfig && !_elementsToCompare.CompareSaveDateTime) ||
                    (!_elementsToCompare.CompareFilePath && sameActiveConfig &&
                    _elementsToCompare.CompareSaveDateTime && sameSaveDateTime) ||
                    (samePath && !_elementsToCompare.CompareActiveConfig &&
                    _elementsToCompare.CompareSaveDateTime && sameSaveDateTime) ||
                    (samePath && _elementsToCompare.CompareActiveConfig &&
                    sameActiveConfig && !_elementsToCompare.CompareSaveDateTime) ||
                    (samePath && sameActiveConfig &&
                    _elementsToCompare.CompareSaveDateTime && sameSaveDateTime);
        }

        private class Line
        {
            public Line(int pos)
            {
                Pos = pos;
                PartsIndicies = new List<int>();
            }
            public Line(int pos, List<int> partsIndices)
            {
                Pos = pos;
                PartsIndicies = partsIndices;
            }
            public int Pos { get; }
            public List<int> PartsIndicies { get; }
        }

        private readonly ElementsToCompare _elementsToCompare;
        private readonly int _threshold = 95;
    }
}
