using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public enum TaskType{
        StepsCount,
        ColletItem,
    }
    [CreateAssetMenu(fileName = "New Task",menuName = "Scriptable Objects/Task System/Tasks")]
    public class Task : ScriptableObject {
        public TaskType taskType;
                
        [TextArea(3,2)]
        
        public string taskDescriptsion;
        public bool isTaskCompleted;
    }
    

    

}