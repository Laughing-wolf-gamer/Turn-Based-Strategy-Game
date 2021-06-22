using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.Utilitys{
    public class MobileInputs : MonoBehaviour {
        

        private static MobileInputs Instance;
        [SerializeField] private LayerMask playerLayer;
        private Camera cam;
        private Vector2 startPos;
        private Vector2 endPos;
        private Vector3 swipeDirecionWithCollider;
        private float swipAmount;
        private Vector3 dragDirection;
        private Collider hitColliders;
        
        private Vector2 startCameraPos;
        private Vector2 swipStart = Vector2.zero;
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance.gameObject);
                Debug.LogWarning("MOBIEL INPUT: More Than one Mobiel Input Object is Found");
            }
            if(cam == null){
                cam = Camera.main;
            }
        }
        private void Update(){
            GetMouseSwipDirectionWithCollider();
            DragSwipe();
        }
        private void DragSwipe(){
            if(hitColliders != null){
                swipAmount = 0f;
                return;
            }
            Vector2 swipEnd = Vector2.zero;
            if(Input.GetMouseButtonDown(0)){
                swipStart = GetMousePos();
                
            }
            else if(Input.GetMouseButton(0)){
                swipEnd = GetMousePos();
                Vector2 DirectionDefernece = (swipStart - swipEnd).normalized;
                if(Mathf.Abs(DirectionDefernece.x) > Mathf.Abs(DirectionDefernece.y)){
                    DirectionDefernece.y = 0f;
                }else{
                    DirectionDefernece.x = 0f;
                }
                if(DirectionDefernece.x > 0){
                    swipAmount = -Vector2.Distance(swipStart,GetMousePos());

                }else{
                    swipAmount = Vector2.Distance(swipStart,GetMousePos());
                }
                dragDirection = new Vector3(DirectionDefernece.x,0f,DirectionDefernece.y) * swipAmount;
                
                
                
            }
            else if(Input.GetMouseButtonUp(0)){
                Debug.Log("Swip Amount " + swipAmount);
                swipAmount = 0f;
                swipStart = Vector2.zero;
                Debug.Log("Swip Amount " + swipAmount);
            }
        }
        private void GetMouseSwipDirectionWithCollider(){
            if(Input.GetMouseButtonDown(0)){
                GetMouseHitCollider();
                if(hitColliders != null){
                    startPos = GetMousePos();
                }
                
            }
            else if(Input.GetMouseButtonUp(0)){
                if(hitColliders != null){
                    endPos = GetMousePos();
                    SwipeIfCollider();
                    hitColliders = null;

                }else{
                    swipeDirecionWithCollider = Vector3.zero;
                }
                
            }else{
                swipeDirecionWithCollider = Vector3.zero;
            }

        }
        
        
        private void GetMouseHitCollider(){

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,playerLayer)){
                if(hit.collider != null){
                    hitColliders = hit.collider;
                }
            }
        }
        
        
        
        public static Vector3 GetMousePos(){
            return Instance.cam.ScreenToViewportPoint(Input.mousePosition);
        }
        private void SwipeIfCollider(){
                
            swipeDirecionWithCollider = Vector3.zero;
            Vector2 DirectionDefernece = (endPos - startPos).normalized;
            if(Mathf.Abs(DirectionDefernece.x) > Mathf.Abs(DirectionDefernece.y)){
                DirectionDefernece.y = 0f;
            }else{
                DirectionDefernece.x = 0f;
            }
            swipeDirecionWithCollider = new Vector3(DirectionDefernece.x,0f,DirectionDefernece.y);

            Debug.Log("Swipe Direction with Collider is " + GetSwipDirectionWithCollider());
            
        }
        public static Vector3 GetSwipDirectionWithCollider(){
            return Instance.swipeDirecionWithCollider;
        }
        public static float GetSwipAmount(){
            return Instance.swipAmount;
            
        }
        public static Vector3 GetDragDirection(){
            return Instance.dragDirection;
        }
        

        


    }

}
