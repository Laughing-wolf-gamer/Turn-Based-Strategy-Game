using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace GamerWolf.TurnBasedStratgeyGame{
    public class TaskSystemUI : MonoBehaviour {
        
        public List<Task> tasks = new List<Task>();

        public Toggle[] tasksCompletedToggels;

        private void Update(){
            for(int i = 0; i < tasks.Count; i++){
                tasksCompletedToggels[i].isOn = tasks[i].isTaskCompleted;
            }
        }

    }
}
