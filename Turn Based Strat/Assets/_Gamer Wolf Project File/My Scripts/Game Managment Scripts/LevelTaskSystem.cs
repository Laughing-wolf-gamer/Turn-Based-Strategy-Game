using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class LevelTaskSystem : MonoBehaviour {

        public static LevelTaskSystem Instance{get;private set;}
        [SerializeField] private TaskSystemUI taskSystemUI;

        [SerializeField] private LevelData tasks;
        [SerializeField] private int maxStepToCompleteTheLevel;

        
        
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Debug.LogWarning("More Than one LEVEL TASK SYSTEM found");
                Destroy(Instance.gameObject);
            }
        }
        
        public void SetCompletedTask(int currentLevelStepCount = 0,bool isLevelCompleted = false,bool hasPickedUpItem = false){
            if(currentLevelStepCount < maxStepToCompleteTheLevel){
                tasks.GetStepTask();
            }
            if(isLevelCompleted){
                tasks.FinsiedTheLevel();
            }
            if(hasPickedUpItem){
                tasks.isColletedItemTask();
            }
            taskSystemUI.SetToggle();
        }
        




    }
    

}
