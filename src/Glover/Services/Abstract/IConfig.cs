namespace Glover.Services.Abstract;

public interface IConfig
{
    string FlightMissionFile { get; }

    string DroneScriptFile { get; }
    
    string DroneAddress { get; }

    int DronePort { get; }
    
    string DroneUsername { get; }

    string DronePassword { get; }

    string DroneFileStorage { get; }

    int MonitorDowntimeInMs { get; }

    // regions
    string TopLeft { get; set; }

    string TopRight { get; set; }

    string BottomLeft { get; set; }

    string BottomRight { get; set; }

    void Save();
}
