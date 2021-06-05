using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class LevelTaskSystem : MonoBehaviour {


        public static LevelTaskSystem Instance{get;private set;}
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Debug.LogWarning("More Than one LEVEL TASK SYSTEM found");
                Destroy(Instance.gameObject);
            }
        }
        
        public List<Task> tasks;
        public void SetCompletedTask(TaskType _taskType){
            for (int i = 0; i < tasks.Count; i++){
                if(tasks[i].taskType == _taskType){
                    if(!tasks[i].isTaskCompleted){
                        tasks[i].isTaskCompleted = true;
                    }
                }
            }
        }


    }
    

}
