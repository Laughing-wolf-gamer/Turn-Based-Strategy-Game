using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.Utilitys{
    public class MobileInputs : MonoBehaviour {
        

        public static MobileInputs Instance {get;private set;}
        private Camera cam;
        private Vector2 startPos;
        private Vector2 endPos;
        private Vector3 swipeDirecion;
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance.gameObject);
                Debug.LogWarning("MOBIEL INPUS: More Than one Mobiel Input Object is Found");
            }
            if(cam == null){
                cam = Camera.main;
            }
        }
        private void Update(){
            if(Input.GetMouseButtonDown(0)){
                startPos = GetMousePos();

            }
            else if(Input.GetMouseButtonUp(0)){
                endPos = GetMousePos();
                Swipe();
            }else{
                swipeDirecion = Vector3.zero;
            }
            
        }
        public static Vector3 GetMousePos(){
            return Instance.cam.ScreenToViewportPoint(Input.mousePosition);
        }
        private void Swipe(){
                
            swipeDirecion = Vector3.zero;
            Vector2 DirectionDefernece = (endPos - startPos).normalized;
            if(Mathf.Abs(DirectionDefernece.x) > Mathf.Abs(DirectionDefernece.y)){
                DirectionDefernece.y = 0f;
            }else{
                DirectionDefernece.x = 0f;
            }
            swipeDirecion = new Vector3(DirectionDefernece.x,0f,DirectionDefernece.y);

            Debug.Log("Swipe Direction is " + GetSwipDirection());
            
        }
        public static Vector3 GetSwipDirection(){
            return Instance.swipeDirecion;
        }

        


    }

}
