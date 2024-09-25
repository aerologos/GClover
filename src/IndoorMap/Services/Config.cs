using IndoorMap.Services.Abstract;
using IndoorMap.Helpers;
using Newtonsoft.Json; 

namespace IndoorMap.Services;

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
    
    public string DroneAddress { get; set; }

    public int DronePort { get; set; }
    
    public string DroneUsername { get; set; }

    public string DronePassword { get; set; }

    public string DroneFileStorage { get; set; }
}
