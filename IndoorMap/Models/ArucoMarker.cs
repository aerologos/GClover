using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndoorMap.Models;

public class ArucoMarker
{
    public ArucoMarker(int number)
    {
        Number = number;
    }
    
    public int Number{ get; }
}
