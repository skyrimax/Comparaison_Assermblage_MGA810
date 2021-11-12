using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparaison_Assemblage_MGA810
{
    class CAD_SolidWorks : CAD_Software
    {
        public override Model OpenFile(string path)
        {
            Model openedModel = default(Model);
            SldWorks.DocumentSpecification documentSpecification = default(SldWorks.DocumentSpecification);
            string extension = default(string);

            if(!System.IO.File.Exists(path))
            {
                throw new ArgumentException("Parameter is not a valid path", nameof(path));
            }

            openedModel = new Model();
            extension = System.IO.Path.GetExtension(path);
            documentSpecification = (SldWorks.DocumentSpecification)swApp.GetOpenDocSpec(path);

            switch (extension)
            {
                // Assemblages Solidworks
                case ".asm": case ".sldasm":
                    ((IModel)openedModel).IsAssembly = true;

                    break;
                // Parts solidworks
                case ".prt": case ".sldprt":
                    ((IModel)openedModel).IsAssembly = false;

                    break;
                default:
                    throw new ArgumentException("Parameter is not a valid file type", nameof(path));
            }

            ((IModel)openedModel).CAD_Software = this;
            ((IModel)openedModel).SoftwareUsed = Software.SolidWorks;
            ((IModel)openedModel).Path = path;

            if(!swApp.SetSearchFolders((int) SwConst.swSearchFolderTypes_e.swDocumentType,
                System.IO.Path.GetDirectoryName(path)))
            {
                throw new ArgumentException("Unable to set search folder", nameof(path));
            }

            ((IModel)openedModel).RefToComponent = swApp.OpenDoc7(documentSpecification);

            return openedModel;
        }

        public override void CloseModel(Model model)
        {
            swApp.CloseDoc(((IModel)model).Path);
        }


        public override List<Model> GetComponents(Model model)
        {
            return new List<Model>();
        }


        public override List<Model> GetConfigurations(Model model)
        {
            return new List<Model>();
        }


        public override string GetMaterial(Model model)
        {
            return "";
        }

        public override double GetMass(Model model)
        {
            return 0.0;
        }

        public override double GetVolume(Model model)
        {
            return 0.0;
        }

        public override double GetSurfaceArea(Model model)
        {
            return 0.0;
        }

        public override int GetNbFaces(Model model)
        {
            return 0;
        }

        public override int GetNbEdges(Model model)
        {
            return 0;
        }

        public override string GetColors(Model model)
        {
            return "";
        }


        public override System.Numerics.Vector3 GetCenterOfMass(Model model)
        {
            return new System.Numerics.Vector3();
        }

        public override System.Numerics.Matrix4x4 GetPrincipalAxes(Model model)
        {
            return new System.Numerics.Matrix4x4();
        }

        public override System.Numerics.Vector3 GetInertia(Model model)
        {
            return new System.Numerics.Vector3();
        }

        public override System.Numerics.Matrix4x4 GetInertiaTensorAtCM(Model model)
        {
            return new System.Numerics.Matrix4x4();
        }

        public override System.Numerics.Matrix4x4 GetInertiaTensorAtFOR(Model model)
        {
            return new System.Numerics.Matrix4x4();
        }


        public override List<object> GetConstraints(Model model)
        {
            return new List<object>();
        }


        public override KeyValuePair<string, string> GetCustomProperties(Model model)
        {
            return new KeyValuePair<string, string>();
        }

        private SldWorks.SldWorks swApp;
    }
}
