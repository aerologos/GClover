namespace ElevationMap.Services.Abstract;

public interface IConfig
{
    string FlightMissionFile { get; }

    string DroneScriptFile { get; }

    int MonitorDowntimeInMs { get; }
}
