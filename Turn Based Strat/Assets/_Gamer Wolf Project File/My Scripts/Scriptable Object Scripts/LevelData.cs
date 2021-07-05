using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{

    [CreateAssetMenu(fileName = "New Level",menuName = "Scriptable Objects/Task System/Level Data")]
    public class LevelData : ScriptableObject {
        
        public List<CurrentLevelTask> currentLevelTasks;
        public void GetStepTask(){
            for (int i = 0; i < currentLevelTasks.Count; i++){
                if(currentLevelTasks[i].taskType == TaskType.StepsCount){
                    if(!currentLevelTasks[i].task.isTaskCompleted){
                        currentLevelTasks[i].task.isTaskCompleted = true;
                    }
                    break;
                }
                
            }
            // foreach(CurrentLevelTask currentLevel in currentLevelTasks){
            // }
            
        }
        public void SetFinsiedTheLevel(){
            foreach(CurrentLevelTask currentLevel in currentLevelTasks){
                if(currentLevel.taskType == TaskType.FinsishTheLevel){
                    if(!currentLevel.task.isTaskCompleted){
                        currentLevel.task.isTaskCompleted = true;
                    }
                }
            }
            
        }
        public void SetisColletedItem(){
            foreach(CurrentLevelTask currentLevel in currentLevelTasks){
                if(currentLevel.taskType == TaskType.ColletItem){
                    if(!currentLevel.task.isTaskCompleted){
                        currentLevel.task.isTaskCompleted = true;
                    }
                }
            }
            
        }
        
        

        [System.Serializable]
        public class CurrentLevelTask{
            public TaskType taskType;
            public Task task;
        }
    }

}