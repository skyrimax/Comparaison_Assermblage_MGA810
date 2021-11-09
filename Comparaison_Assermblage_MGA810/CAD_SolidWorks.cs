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
    }
}
