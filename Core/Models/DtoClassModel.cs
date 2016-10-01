using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DtoClassModel
    {
        public string Name { get; set; }

        public IEnumerable<PropertyModel> Properties { get; set; }

    }
}
