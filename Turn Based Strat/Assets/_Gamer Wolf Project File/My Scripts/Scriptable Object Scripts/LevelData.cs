using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{

    [CreateAssetMenu(fileName = "New Level",menuName = "Scriptable Objects/Task System/Level Data")]
    public class LevelData : ScriptableObject {
        
        public List<CurrentLevelTask> currentLevelTasks;


        
        public void GetStepTask(){
            foreach(CurrentLevelTask currentLevel in currentLevelTasks){
                if(currentLevel.taskType == TaskType.StepsCount){
                    if(!currentLevel.task.isTaskCompleted){
                        currentLevel.task.isTaskCompleted = true;
                    }
                }
            }
            
        }
        public void FinsiedTheLevel(){
            foreach(CurrentLevelTask currentLevel in currentLevelTasks){
                if(currentLevel.taskType == TaskType.FinsishTheLevel){
                    if(!currentLevel.task.isTaskCompleted){
                        currentLevel.task.isTaskCompleted = true;
                    }
                }
            }
            
        }
        public void isColletedItemTask(){
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