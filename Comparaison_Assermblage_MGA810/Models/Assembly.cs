using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comparaison_Assemblage_MGA810.CAD_Software;

namespace Comparaison_Assemblage_MGA810.Models
{
    internal class Assembly 
    {

        private int _numberOfComponents { get; set; }

        public Assembly(Model Assembly)
        {
            _numberOfComponents = Assembly.GetNbComponents(Assembly);
        }
    }
}
