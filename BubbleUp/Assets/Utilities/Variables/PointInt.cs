using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewPointIntVariable", menuName = "Utils/Variables/PointInt")]
    public class PointInt : AbstractVariable<int>
    {
        public void AddPoint(int value)
        {
            runtimeValue += value;
            onValueChanged?.Raise();
        }

        public void Add(PointInt value)
        {
            runtimeValue += value.runtimeValue;
            onValueChanged.Raise();
        }
    }
}