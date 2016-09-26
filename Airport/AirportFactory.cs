using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager
{
    public class AirportFactory
    {
        public static IAirportModel Create() => new Airport();
    }
}
