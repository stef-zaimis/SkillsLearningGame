using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;
        public static bool resetMap = true;

        public Map CurrentMap { get; private set; }

        // Generate a new map, or load an old one
        private void Start()
        {
            if (resetMap)
            {
                GenerateNewMap();
                SaveMap();
                resetMap = false;
                return;
            }
            if (PlayerPrefs.HasKey("Map"))
            {
                var mapJson = PlayerPrefs.GetString("Map");
                var map = JsonConvert.DeserializeObject<Map>(mapJson);
                // Using this instead of .Contains()
                if (map.path.Any(p => p.Equals(map.GetBossNode().point))) // Assume spaces around '=>' are correct; hard to pinpoint without char indices
                {
                    // Player has already reached the boss, generate a new map
                    GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                    // Player has not reached the boss yet, load the current map
                    view.ShowMap(map);
                }
            }
            else
            {
                GenerateNewMap();
            }
        }

        // Genenrate a new map
        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            view.ShowMap(map);
        }

        // Save the map to persist
        public void SaveMap()
        {
            if (CurrentMap == null) return;

            var json = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }); // Assuming potential fixes here without specific indices
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        // Save the map when quitting, so we can implement full game persistence
        private void OnApplicationQuit()
        {
            SaveMap();
        }
    }
}