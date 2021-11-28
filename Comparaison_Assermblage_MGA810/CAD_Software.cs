using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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

            public string GetFullPath()
            {
                return _pathToFile;
            }

            public string GetFileNameWExt()
            {
                return System.IO.Path.GetFileName(_pathToFile);
            }

            public string GetFileNameWOExt()
            {
                return System.IO.Path.GetFileNameWithoutExtension(_pathToFile);
            }

            public System.DateTime GetSaveDateTime()
            {
                return _refToSoftware.GetSaveDateTime(this);
            }


            public List<Model> GetComponents()
            {
                if (!_isAssembly)
                {
                    throw new ArgumentException("Object is not an assembly", _pathToFile);
                }

                return _refToSoftware.GetComponents(this);
            }


            public int GetNbComponents()
            {
                if (!_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an assembly", _pathToFile);
                }

                return _refToSoftware.GetNbComponents(this);
            }

            public List<string> GetConfigurations()
            {
                return _refToSoftware.GetConfigurations(this);
            }

            public string GetMaterial()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetMaterial(this);
            }
            public double GetMass()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetMass(this);
            }
            public double GetVolume()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetVolume(this);
            }
            public double GetSurfaceArea()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetSurfaceArea(this);
            }
            public int GetNbFaces()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetNbFaces(this);
            }
            public int GetNbEdges()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetNbEdges(this);
            }
            public string GetColors()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetColors(this);
            }


            public string GetPartName()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetPartName(this);
            }

            public System.Numerics.Vector3 GetCenterOfMass()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetCenterOfMass(this);
            }
            public System.Numerics.Matrix4x4 GetPrincipalAxes()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetPrincipalAxes(this);
            }
            public System.Numerics.Vector3 GetInertia()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetInertia(this);
            }
            public System.Numerics.Matrix4x4 GetInertiaTensorAtCM()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetInertiaTensorAtCM(this);
            }
            public System.Numerics.Matrix4x4 GetInertiaTensorAtFOR()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetInertiaTensorAtFOR(this);
            }

            public List<object> GetConstraints()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetConstraints(this);
            }

            public KeyValuePair<string, string> GetCustomProperties()
            {
                if (_isAssembly)
                {
                    throw new ArgumentException("Parameter is not an part", _pathToFile);
                }

                return _refToSoftware.GetCustomProperties(this);
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

        public abstract Model OpenFile(string path);
        public abstract void CloseModel(Model model);

        protected abstract System.DateTime GetSaveDateTime(Model model);

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
