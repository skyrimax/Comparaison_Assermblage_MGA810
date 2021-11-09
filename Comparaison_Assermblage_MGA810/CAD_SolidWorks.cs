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
    }
}
