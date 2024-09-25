namespace IndoorMap.Services.Abstract;

public interface IConfig
{
    string FlightMissionFile { get; }

    string DroneScriptFile { get; }
    
    string DroneAddress { get; }

    int DronePort { get; }
    
    string DroneUsername { get; }

    string DronePassword { get; }

    string DroneFileStorage { get; }
}
