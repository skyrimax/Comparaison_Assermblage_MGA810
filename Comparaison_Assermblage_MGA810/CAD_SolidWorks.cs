using SwConst;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparaison_Assemblage_MGA810
{
    public class CAD_SolidWorks : CAD_Software
    {
        public CAD_SolidWorks()
        {
            try
            {
                swApp = new SldWorks.SldWorks();
                if (swApp == null)
                {
                    throw new InvalidOperationException("Unable to connect to SolidWorks");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

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
                case ".ASM": case ".SLDASM":
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


        protected override System.DateTime GetSaveDateTime()
        {
            return new System.DateTime();
        }


        protected override List<Model> GetComponents(Model model)
        {
            string path = default(string);
            string extension = default(string);
            Model component = default(Model);

            if (!((IModel)model).IsAssembly)
            {
                throw new ArgumentException("Parameter is not an assembly", nameof(model));
            }

            int nbComponents = GetNbComponents(model);

            List<Model> components = new List<Model>(nbComponents);

            object[] swComponents = (object[])((SldWorks.AssemblyDoc)((IModel)model).RefToComponent).GetComponents(false);

            foreach(SldWorks.Component2 swComponent in swComponents)
            {
                path = swComponent.GetPathName();
                extension = System.IO.Path.GetExtension(path);
                if(!(extension == ".SLDPRT" || extension == ".PRT") || swComponent.IsSuppressed())
                {
                    break;
                }

                component = new Model();

                ((IModel)component).CAD_Software = this;
                ((IModel)component).IsAssembly = false;
                ((IModel)component).Path = path;
                ((IModel)component).RefToComponent = swComponent.GetModelDoc2(); 
                ((IModel)component).SoftwareUsed = Software.SolidWorks;

                components.Add(component);
            }

            return components;
        }

        protected override int GetNbComponents(Model model)
        {
            if (!((IModel)model).IsAssembly)
            {
                throw new ArgumentException("Parameter is not an assembly", nameof(model));
            }

            return ((SldWorks.AssemblyDoc)((IModel)model).RefToComponent).GetComponentCount(false);
        }


        protected override List<Model> GetConfigurations(Model model)
        {
            return new List<Model>();
        }


        protected override string GetMaterial(Model model)
        {


            return ((SldWorks.Component2)((IModel)model).RefToComponent).GetMaterialIdName();
        }

        protected override double GetMass(Model model)
        {
            return 0.0;
        }

        protected override double GetVolume(Model model)
        {

            return ((SldWorks.Body2)(((SldWorks.PartDoc)((IModel)model).RefToComponent).GetBodies2((int)swBodyType_e.swSolidBody, true)[0])).GetMassProperties(0)[3];
        }

        protected override double GetSurfaceArea(Model model)
        {
            Object[] faces;
            Surface swSurface;
            double PartSurfaceArea = 0.0;

            faces = ((SldWorks.Body2)(((SldWorks.PartDoc)((IModel)model).RefToComponent).GetBodies2((int)swBodyType_e.swSolidBody, true)[0])).GetFaces();
            
            foreach (var face in faces)
            {
                swSurface = (Surface)((Face2)face).GetSurface();
                if (swSurface.IsPlane())
                {
                    PartSurfaceArea += ((Face2)face).GetArea();
                }
            }
            return PartSurfaceArea;
        }

        protected override int GetNbFaces(Model model)
        {
            int Nbfaces;

            Nbfaces = ((SldWorks.Body2)(((SldWorks.PartDoc)((IModel)model).RefToComponent).GetBodies2((int)swBodyType_e.swSolidBody, true)[0])).GetFaceCount();
           
            return Nbfaces;
        }

        protected override int GetNbEdges(Model model)
        {
            return 0;
        }

        protected override string GetColors(Model model)
        {
            return "";
        }


        protected override System.Numerics.Vector3 GetCenterOfMass(Model model)
        {
            return new System.Numerics.Vector3();
        }

        protected override System.Numerics.Matrix4x4 GetPrincipalAxes(Model model)
        {
            return new System.Numerics.Matrix4x4();
        }

        protected override System.Numerics.Vector3 GetInertia(Model model)
        {
            return new System.Numerics.Vector3();
        }

        protected override System.Numerics.Matrix4x4 GetInertiaTensorAtCM(Model model)
        {
            return new System.Numerics.Matrix4x4();
        }

        protected override System.Numerics.Matrix4x4 GetInertiaTensorAtFOR(Model model)
        {
            return new System.Numerics.Matrix4x4();
        }


        protected override List<object> GetConstraints(Model model)
        {
            return new List<object>();
        }


        protected override KeyValuePair<string, string> GetCustomProperties(Model model)
        {
            return new KeyValuePair<string, string>();
        }

        private SldWorks.SldWorks swApp;
    }
}
