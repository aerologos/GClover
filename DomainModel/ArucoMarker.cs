using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel;

public class ArucoMarker
{
    public ArucoMarker(int number, float z, string id)
    {
        Number = number;
        Z = z;
        Id = id;
    }
    
    public int Number{ get; }
    
    public float Z { get; }

    public string Id { get; }
}
