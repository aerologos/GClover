namespace IndoorMap.Models;

public class ArucoMarkerLine
{
    public ArucoMarkerLine(ArucoMarker aruco1, ArucoMarker aruco2, ArucoMarker aruco3)
    {
        Aruco1 = aruco1;
        Aruco2 = aruco2;
        Aruco3 = aruco3;
    }

    public ArucoMarker Aruco1 { get; }

    public ArucoMarker Aruco2 { get; }

    public ArucoMarker Aruco3 { get; }
}
