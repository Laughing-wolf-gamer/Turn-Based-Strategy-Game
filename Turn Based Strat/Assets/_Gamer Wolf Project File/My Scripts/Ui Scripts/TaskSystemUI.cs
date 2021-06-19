using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class TaskSystemUI : MonoBehaviour {
        
        [SerializeField] private LevelData levelData;

        [SerializeField] private Transform[] tasksCompletedUI;
        [SerializeField] private List<Toggle> taskCompleteToggle;
        
        private void InitTaskUI(){
            for (int i = 0; i < tasksCompletedUI.Length; i++){
                string taskDescription = levelData.currentLevelTasks[i].task.taskDescription;
                tasksCompletedUI[i].transform.Find("Image").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().SetText(taskDescription);
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
                    taskCompleteToggle[i].isOn = levelData.currentLevelTasks[i].task.isTaskCompleted;
                }
            }
        }

        

    }
}
