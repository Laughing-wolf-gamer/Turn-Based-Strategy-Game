using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GamerWolf.Utilitys{

    
    public class DebugController : MonoBehaviour {

        public TextMeshProUGUI text1,text2,text3,text4;

        public static DebugController Instance;

        private void Awake(){
            Instance = this;
        }

        public static void SetTurnText(string _text){
            Instance.text1.SetText(string.Concat("Current Turn : ",_text));
        }
        public static void SetGameOverText(string _text){
            Instance.text2.SetText(string.Concat("Game over : ",_text));
        }
        public static void SetRoutineText(string _text){
            Instance.text3.SetText(string.Concat("Current Routine : ","\n",_text));
        }
        public static void SetDebugTexts(string _text){
            Instance.text4.SetText(string.Concat(_text));
        }

    }

}