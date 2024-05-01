using UnityEngine;

namespace Map
{
    // Min max float for ease of use in the map
    [System.Serializable]
    public class FloatMinMax
    {
        public float min;
        public float max;

        public float GetValue()
        {
            return Random.Range(min, max);
        }
    }
}

namespace Map
{
    // Min max int for ease of use in the map
    [System.Serializable]
    public class IntMinMax
    {
        public int min;
        public int max;

        public int GetValue()
        {
            return Random.Range(min, max + 1);
        }
    }
}
