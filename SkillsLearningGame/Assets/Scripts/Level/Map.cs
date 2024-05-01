﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Map
{
    // The map class contains basic attributes and methods needed for the map itself
    public class Map
    {
        public List<Node> nodes;
        public List<Point> path;
        public string bossNodeName;
        public string configName; // similar to the act name in Slay the Spire

        public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> path)
        {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        // Return the last (boss) node
        public Node GetBossNode()
        {
            return nodes.FirstOrDefault(n => n.nodeType == NodeType.Boss);
        }

        // Return the total map distance
        public float DistanceBetweenFirstAndLastLayers()
        {
            var bossNode = GetBossNode();
            var firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

            if (bossNode == null || firstLayerNode == null)
                return 0f;

            return bossNode.position.y - firstLayerNode.position.y;
        }

        // Return a given node
        public Node GetNode(Point point)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        // Tojson converter to handle newtonsoft
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}