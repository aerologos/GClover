namespace DomainModel;

public class ArucoMarker
{
    public ArucoMarker(int number, float z, string id)
    {
        Number = number;
        Altitude = z;
        Id = id;
    }
    
    public int Number{ get; }
    
    public float Altitude { get; set; }

    public string Id { get; }
}
