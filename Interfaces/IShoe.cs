using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IShoe
    {
        int Id { get; set; }

        IProducer Producer { get; set; }

        string Name { get; set; }
        string Description { get; set; }

        ShoeType ShoeType {  get; set; }

        int ReleaseYear { get; set; }
    }
}
