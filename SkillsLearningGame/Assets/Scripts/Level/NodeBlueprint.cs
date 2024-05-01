using UnityEngine;

namespace Map
{
    // Types of nodes that we might use
    public enum NodeType
    {
        MinorEnemy,
        EliteEnemy,
        RestSite,
        Treasure,
        Store,
        Boss,
        Mystery,
        QuizBattle
    }
}

namespace Map
{
    // This is the basic node UI
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodeType;
    }
}