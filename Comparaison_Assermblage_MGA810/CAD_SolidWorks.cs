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
            return new Model();
        }

        public override void CloseModel(Model model)
        {

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
    }
}
