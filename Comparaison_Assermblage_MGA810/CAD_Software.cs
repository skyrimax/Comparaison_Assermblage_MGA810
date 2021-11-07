using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SldWorks;
using SwConst;

namespace Comparaison_Assermblage_MGA810
{
    /**
     * Abstract class used to interact with a CAD software through a standardized interface
     * 
     * 
     * 
     **/
    abstract class CAD_Software
    {
        public CAD_Software()
        {

        }

        ~CAD_Software()
        {

        }

        private interface IModel
        {
            object RefToComponent { get; set; }
            Software SoftwareUsed { get; set; }
            bool IsAssembly { get; set; }
        }

        public class Model : IModel
        {
            object IModel.RefToComponent
            {
                get { return _refToComponent; }
                set { _refToComponent = value; }
            }

            Software IModel.SoftwareUsed
            {
                get { return _softwareUsed; }
                set { _softwareUsed = value; }
            }

            bool IModel.IsAssembly
            {
                get { return _isAssembly; }
                set { _isAssembly = value; }
            }

            private object _refToComponent;
            private Software _softwareUsed;
            bool _isAssembly;
        }

        protected enum Software
        {
            Autodesk_Inventor,
            CATIA,
            Fusion_360,
            Onshape,
            Siemens_NX,
            Solid_Edge,
            SolidWorks,
        }

        public abstract Model OpenFile(string path);
        public abstract void CloseModel(Model model);

        public abstract Model[] GetComponents(Model model);

        public abstract string GetMaterial(Model model);
        public abstract double GetMass(Model model);
        public abstract double GetVolume(Model model);
        public abstract double GetSurfaceArea(Model model);
        public abstract int GetNbFaces(Model model);
        public abstract int GetNbEdges(Model model);
        public abstract string GetColors(Model model);

        public abstract System.Numerics.Vector3 GetCenterOfMass(Model model);
        public abstract System.Numerics.Matrix4x4 GetPrincipalAxes(Model model);
        public abstract System.Numerics.Vector3 GetInertia(Model model);
        public abstract System.Numerics.Matrix4x4 GetInertiaTensorAtCM(Model model);
        public abstract System.Numerics.Matrix4x4 GetInertiaTensorAtFOR(Model model);

        public abstract object[] GetConstraints(Model model);

        public abstract KeyValuePair<string, string> GetCustomProperties(Model model);
    }
}
