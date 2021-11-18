using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SldWorks;
using SwConst;

namespace Comparaison_Assemblage_MGA810
{
    /**
     * Abstract class used to interact with a CAD software through a standardized interface
     * 
     * 
     * 
     **/
    public abstract class CAD_Software
    {
        public CAD_Software()
        {

        }

        ~CAD_Software()
        {

        }

        protected interface IModel
        {
            object RefToComponent { get; set; }
            Software SoftwareUsed { get; set; }
            bool IsAssembly { get; set; }
            string Path { get; set; }

            CAD_Software CAD_Software { set; }
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

            string IModel.Path
            {
                get { return _pathToFile; }
                set { _pathToFile = value; }
            }

            public CAD_Software CAD_Software { 
                get { return _refToSoftware; } 
            }

            CAD_Software IModel.CAD_Software
            {
                set { _refToSoftware = value; }
            }

            public List<Model> GetComponents(Model model)
            {
                if (!model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an assembly", nameof(model));
                }

                return model._refToSoftware.GetComponents(model);
            }
            public int GetNbComponents(Model model)
            {
                if (!model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an assembly", nameof(model));
                }

                return model._refToSoftware.GetNbComponents(model);
            }

            public List<Model> GetConfigurations(Model model)
            {
                if (!model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an assembly", nameof(model));
                }

                return model._refToSoftware.GetConfigurations(model);
            }

            public string GetMaterial(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetMaterial(model);
            }
            public double GetMass(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetMass(model);
            }
            public double GetVolume(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetVolume(model);
            }
            public double GetSurfaceArea(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetSurfaceArea(model);
            }
            public int GetNbFaces(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetNbFaces(model);
            }
            public int GetNbEdges(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetNbEdges(model);
            }
            public string GetColors(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetColors(model);
            }

            public System.Numerics.Vector3 GetCenterOfMass(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetCenterOfMass(model);
            }
            public System.Numerics.Matrix4x4 GetPrincipalAxes(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetPrincipalAxes(model);
            }
            public System.Numerics.Vector3 GetInertia(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetInertia(model);
            }
            public System.Numerics.Matrix4x4 GetInertiaTensorAtCM(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetInertiaTensorAtCM(model);
            }
            public System.Numerics.Matrix4x4 GetInertiaTensorAtFOR(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetInertiaTensorAtFOR(model);
            }

            public List<object> GetConstraints(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetConstraints(model);
            }

            public KeyValuePair<string, string> GetCustomProperties(Model model)
            {
                if (model._isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", nameof(model));
                }

                return model._refToSoftware.GetCustomProperties(model);
            }

            private CAD_Software _refToSoftware;
            private object _refToComponent;
            private Software _softwareUsed;
            private bool _isAssembly;
            private string _pathToFile;
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

        protected abstract Model OpenFile(string path);
        protected abstract void CloseModel(Model model);

        protected abstract List<Model> GetComponents(Model model);
        protected abstract int GetNbComponents(Model model);

        protected abstract List<Model> GetConfigurations(Model model);

        protected abstract string GetMaterial(Model model);
        protected abstract double GetMass(Model model);
        protected abstract double GetVolume(Model model);
        protected abstract double GetSurfaceArea(Model model);
        protected abstract int GetNbFaces(Model model);
        protected abstract int GetNbEdges(Model model);
        protected abstract string GetColors(Model model);

        protected abstract System.Numerics.Vector3 GetCenterOfMass(Model model);
        protected abstract System.Numerics.Matrix4x4 GetPrincipalAxes(Model model);
        protected abstract System.Numerics.Vector3 GetInertia(Model model);
        protected abstract System.Numerics.Matrix4x4 GetInertiaTensorAtCM(Model model);
        protected abstract System.Numerics.Matrix4x4 GetInertiaTensorAtFOR(Model model);

        protected abstract List<object> GetConstraints(Model model);

        protected abstract KeyValuePair<string, string> GetCustomProperties(Model model);
    }
}
