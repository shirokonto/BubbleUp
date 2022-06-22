using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewVector2Variable", menuName = "Utils/Variables/Vector2Variable")]
    public class Vector2Variable : AbstractVariable<Vector2>
    {
        public void Add(Vector2 value)
        {
            runtimeValue += value;
            onValueChanged.Raise();
        }

        public void Add(Vector2Variable value)
        {
            runtimeValue += value.runtimeValue;
            onValueChanged.Raise();
        }
    }
}

