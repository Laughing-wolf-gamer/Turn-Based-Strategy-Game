using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class TaskSystemUI : MonoBehaviour {
        
        public List<Task> tasks = new List<Task>();

        public Transform[] tasksCompletedUI;
        public List<Toggle> taskCompleteToggle;
        
        private void InitTaskUI(){
            
            for (int i = 0; i < tasksCompletedUI.Length; i++){
                tasksCompletedUI[i].transform.Find("Image").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().SetText(tasks[i].taskDescriptsion);
                Toggle toggle = tasksCompletedUI[i].Find("is Complted Toggle").GetComponent<Toggle>();
                if(toggle != null){
                    taskCompleteToggle.Add(toggle);
                }
            }
        }
        private void Start(){
            InitTaskUI();
        }
        public void SetToggle(){
            if(taskCompleteToggle.Count > 0){
                for (int i = 0; i < taskCompleteToggle.Count; i++){
                    taskCompleteToggle[i].isOn = tasks[i].isTaskCompleted;
                }
            }
        }

        

    }
}
