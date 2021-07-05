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
            taskCompleteToggle = new List<Toggle>();
            
            for (int i = 0; i < levelData.currentLevelTasks.Count; i++){
                string taskDescription = levelData.currentLevelTasks[i].task.taskDescription;
                tasksCompletedUI[i].gameObject.SetActive(true);
                tasksCompletedUI[i].transform.Find("Image").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().SetText(taskDescription);
                Toggle toggle = tasksCompletedUI[i].Find("is Complted Toggle").GetComponent<Toggle>();
                taskCompleteToggle.Add(toggle);
            }
            
        }
        private void Awake(){
            InitTaskUI();
        }
        public void SetToggle(){
            for (int i = 0; i < taskCompleteToggle.Count; i++){
                taskCompleteToggle[i].isOn = levelData.currentLevelTasks[i].task.isTaskCompleted;
            }
        }

        

    }
}
