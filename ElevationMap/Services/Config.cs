using ElevationMap.Services.Abstract;
using ElevationMap.Helpers;
using Newtonsoft.Json; 

namespace ElevationMap.Services;

public class Config : IConfig
{
    private static Config _instance;
    /// <summary>
    /// Gets the singleton instance of the config.
    /// </summary>
    public static Config Instance
    {
        get
        {
            if (_instance == null)
            {
                var configContent = FileSystemHelper.ReadEmbeddedResourceContent("config.json");
                _instance = JsonConvert.DeserializeObject<Config>(configContent);
            }

            return _instance;
        }
    }

    public string FlightMissionFile { get; set; }

    public string DroneScriptFile { get; set; }

    public int MonitorDowntimeInMs { get; set; }
}
