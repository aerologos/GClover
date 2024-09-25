using Glover.Services.Abstract;
using Glover.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace Glover.Services;

public class Config : IConfig
{
    private const string _configFile = "config.json";
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
                var configContent = FileSystemHelper.ReadEmbeddedResourceContent(_configFile);
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

    public int MonitorDowntimeInMs { get; set; }

    // regions
    public string TopLeft { get; set; }

    public string TopRight { get; set; }

    public string BottomLeft { get; set; }

    public string BottomRight { get; set; }

    public void Save()
    {
        var str = JsonConvert.SerializeObject(Instance, Formatting.Indented);
        File.WriteAllText(_configFile, str);        
    }
}
