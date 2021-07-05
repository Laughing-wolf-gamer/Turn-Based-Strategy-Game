using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public enum TaskType{
        StepsCount,
        ColletItem,
        FinsishTheLevel,
        No_Kill,

    }
    [CreateAssetMenu(fileName = "New Task",menuName = "Scriptable Objects/Task System/Task")]
    public class Task : ScriptableObject {
        public TaskType taskType;
                
        [TextArea(3,2)]
        
        public string taskDescription;
        public bool isTaskCompleted;
        
    }
    
    

    

}