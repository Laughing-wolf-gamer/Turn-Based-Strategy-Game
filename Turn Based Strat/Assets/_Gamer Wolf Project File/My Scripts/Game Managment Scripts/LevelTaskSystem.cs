using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class LevelTaskSystem : MonoBehaviour {

        private static LevelTaskSystem Instance;
        [SerializeField] private TaskSystemUI taskSystemUI;

        [SerializeField] private LevelData tasks;
        [SerializeField] private int maxStepToCompleteTheLevel;

        
        
        private void Awake(){
            Instance = this;
            
        }
        
        public static void SetCompletedTask(int currentLevelStepCount,bool isLevelCompleted,bool hasPickedUpItem){
            
            
            if(currentLevelStepCount <= Instance.maxStepToCompleteTheLevel){
                Instance.tasks.GetStepTask();
            }
            if(isLevelCompleted){
                Instance.tasks.SetFinsiedTheLevel();
            }
            if(hasPickedUpItem){
                Instance.tasks.SetisColletedItem();
            }
            Instance.taskSystemUI.SetToggle();
        }
        private void OnApplicationQuit(){
            for (int i = 0; i < tasks.currentLevelTasks.Count; i++){
                tasks.currentLevelTasks[i].task.isTaskCompleted = false;
            }
        }
        




    }
    

}
