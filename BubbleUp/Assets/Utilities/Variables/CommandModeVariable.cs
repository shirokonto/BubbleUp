using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewCommandModeVariable", menuName = "Utils/Variables/CommandModeVariable")]
    public class CommandModeVariable : AbstractVariable<CommandMode>
    {
        
    }
    
    public enum CommandMode
    {
        Excavate,
        TransportLine
    }
}
