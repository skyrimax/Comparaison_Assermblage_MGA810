using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comparaison_Assemblage_MGA810.CAD_Software;

namespace Comparaison_Assemblage_MGA810.Models
{
    public class Assembly
    {

        private int _numberOfComponents { get; set; }
        private List<Part> _partList { get; set; }

        private List<Model> _modelList { get; set; }

    public Assembly(Model Assembly)
        {
            _numberOfComponents = Assembly.GetNbComponents(Assembly);
            _modelList = Assembly.GetComponents(Assembly);

            foreach (Model m in _modelList) 
            {
                _partList.Add(new Part(m));
            }
        }
    }
}
