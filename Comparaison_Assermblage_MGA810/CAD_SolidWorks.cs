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
                throw e;
            }
        }

        public override Model OpenFile(string path)
        {
            Model openedModel;
            SldWorks.DocumentSpecification documentSpecification;
            string extension;

            if(!System.IO.File.Exists(path))
            {
                throw new ArgumentException("Parameter is not a valid path", nameof(path));
            }

            openedModel = new Model();
            extension = System.IO.Path.GetExtension(path).ToUpper();
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


        protected override System.DateTime GetSaveDateTime(Model model)
        {
            var solidworksSaveTime = ((SldWorks.ModelDoc2)(((IModel)model).RefToComponent)).get_SummaryInfo((int)swSummInfoField_e.swSumInfoSaveDate);
            var assemblySaveTime = DateTime.Parse(solidworksSaveTime);
            return assemblySaveTime;
        }


        protected override List<Model> GetComponents(Model model)
        {
            string path;
            string extension;
            Model component;

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
                extension = System.IO.Path.GetExtension(path).ToUpper();
                if(!(extension == ".SLDPRT" || extension == ".PRT") || swComponent.IsSuppressed())
                {
                    continue;
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

        protected override List<string> GetConfigurations(Model model)
        {
            string[] configurationsArray = (string[])(((SldWorks.ModelDoc2)((IModel)model).RefToComponent)).GetConfigurationNames();

            List<string> configurationsList = new List<string>(configurationsArray);
            return configurationsList;
        }

        protected override string GetActiveConfiguration(Model model)
        {
            string activeConfiguration;
            activeConfiguration = swApp.GetActiveConfigurationName(((IModel)model).Path);
            return activeConfiguration;
        }


        protected override string GetMaterial(Model model)
        {
            if (((IModel)model).IsAssembly)
            {
                throw new ArgumentException("Parameter is not an assembly", nameof(model));
            }

            return (((SldWorks.PartDoc)((IModel)model).RefToComponent).MaterialIdName);
        }

        protected override double GetMass(Model model)
        {
            MassProperty2 allMassInfo = CreateMassProperty2FromModel(model);
            double modelMass = allMassInfo.Mass;
            return modelMass;
        }

        protected override double GetVolume(Model model)
        {
            if (((IModel)model).IsAssembly)
            {
                throw new ArgumentException("Parameter is not an assembly", nameof(model));
            }
            // Using a density of 0 to simply extract volume
            return ((SldWorks.Body2)(((SldWorks.PartDoc)((IModel)model).RefToComponent).GetBodies2((int)swBodyType_e.swSolidBody, true)[0])).GetMassProperties(0)[3];
        }

        protected override double GetSurfaceArea(Model model)
        {
            if (((IModel)model).IsAssembly)
            {
                throw new ArgumentException("Parameter is not an assembly", nameof(model));
            }

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
            if (((IModel)model).IsAssembly)
            {
                throw new ArgumentException("Parameter is not an assembly", nameof(model));
            }
           
            return ((SldWorks.Body2)(((SldWorks.PartDoc)((IModel)model).RefToComponent).GetBodies2((int)swBodyType_e.swSolidBody, true)[0])).GetFaceCount();;
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

            MassProperty2 allMassInfo = CreateMassProperty2FromModel(model);
            double[] modelCOM= allMassInfo.CenterOfMass;
            System.Numerics.Vector3 CtrOfMass;

            CtrOfMass.X = (float)modelCOM[0];
            CtrOfMass.Y = (float)modelCOM[1];
            CtrOfMass.Z = (float)modelCOM[2];

            return CtrOfMass;
        }
        
        protected override System.Numerics.Matrix4x4 GetPrincipalAxes(Model model)
        {
            MassProperty2 allMassInfo = CreateMassProperty2FromModel(model);
            allMassInfo.Recalculate();
            double[] modelPrincipalAxesX = allMassInfo.PrincipalAxesOfInertia[0];
            double[] modelPrincipalAxesY = allMassInfo.PrincipalAxesOfInertia[1];
            double[] modelPrincipalAxesZ = allMassInfo.PrincipalAxesOfInertia[2];
            System.Numerics.Matrix4x4 principalAxis_Matrix;

            principalAxis_Matrix.M11 = (float)modelPrincipalAxesX[0];
            principalAxis_Matrix.M12 = (float)modelPrincipalAxesX[1];
            principalAxis_Matrix.M13 = (float)modelPrincipalAxesX[2];
            principalAxis_Matrix.M14 = 0;
            principalAxis_Matrix.M21 = (float)modelPrincipalAxesY[0];
            principalAxis_Matrix.M22 = (float)modelPrincipalAxesY[1];
            principalAxis_Matrix.M23 = (float)modelPrincipalAxesY[2];
            principalAxis_Matrix.M24 = 0;
            principalAxis_Matrix.M31 = (float)modelPrincipalAxesZ[0];
            principalAxis_Matrix.M32 = (float)modelPrincipalAxesZ[1];
            principalAxis_Matrix.M33 = (float)modelPrincipalAxesZ[2];
            principalAxis_Matrix.M34 = 0;
            principalAxis_Matrix.M41 = 0;
            principalAxis_Matrix.M42 = 0;
            principalAxis_Matrix.M43 = 0;
            principalAxis_Matrix.M44 = 1;

            return principalAxis_Matrix;
            
        }

        protected override System.Numerics.Vector3 GetInertia(Model model)
        {
            MassProperty2 allMassInfo = CreateMassProperty2FromModel(model);
            allMassInfo.Recalculate();
            var PrincipalMomentsofInertiaReturnArray = allMassInfo.PrincipalMomentsOfInertia;
            System.Numerics.Vector3 PrincipalMoments;
            PrincipalMoments.X = (float)PrincipalMomentsofInertiaReturnArray[0];
            PrincipalMoments.Y = (float)PrincipalMomentsofInertiaReturnArray[1];
            PrincipalMoments.Z = (float)PrincipalMomentsofInertiaReturnArray[2];

            return PrincipalMoments;

        }

        protected override System.Numerics.Matrix4x4 GetInertiaTensorAtCM(Model model)
        {
            MassProperty2 allMassInfo = CreateMassProperty2FromModel(model);
            allMassInfo.Recalculate();
            double[] InertiaAroundCOM = allMassInfo.GetMomentOfInertia(0);
         
            System.Numerics.Matrix4x4 InertiaAroundCOM_Matrix;

            InertiaAroundCOM_Matrix.M11 = (float)InertiaAroundCOM[0];
            InertiaAroundCOM_Matrix.M12 = (float)InertiaAroundCOM[1];
            InertiaAroundCOM_Matrix.M13 = (float)InertiaAroundCOM[2];
            InertiaAroundCOM_Matrix.M14 = 0;
            InertiaAroundCOM_Matrix.M21 = (float)InertiaAroundCOM[3];
            InertiaAroundCOM_Matrix.M22 = (float)InertiaAroundCOM[4];
            InertiaAroundCOM_Matrix.M23 = (float)InertiaAroundCOM[5];
            InertiaAroundCOM_Matrix.M24 = 0;
            InertiaAroundCOM_Matrix.M31 = (float)InertiaAroundCOM[6];
            InertiaAroundCOM_Matrix.M32 = (float)InertiaAroundCOM[7];
            InertiaAroundCOM_Matrix.M33 = (float)InertiaAroundCOM[8];
            InertiaAroundCOM_Matrix.M34 = 0;
            InertiaAroundCOM_Matrix.M41 = 0;
            InertiaAroundCOM_Matrix.M42 = 0;
            InertiaAroundCOM_Matrix.M43 = 0;
            InertiaAroundCOM_Matrix.M44 = 1;

            return InertiaAroundCOM_Matrix;
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
        private MassProperty2 CreateMassProperty2FromModel(Model model)
        {

            ModelDocExtension massExtension = ((ModelDoc2)((SldWorks.PartDoc)((IModel)model).RefToComponent)).Extension;
            massExtension.SelectByID2("", "BODYFEATURE", 0, 0, 0, true, 1, null, 1);
            MassProperty2 allMassInfo = (MassProperty2)massExtension.CreateMassProperty2();
            return allMassInfo;
        }

        private readonly SldWorks.SldWorks swApp;
    }
}
