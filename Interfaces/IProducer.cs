using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProducer
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
