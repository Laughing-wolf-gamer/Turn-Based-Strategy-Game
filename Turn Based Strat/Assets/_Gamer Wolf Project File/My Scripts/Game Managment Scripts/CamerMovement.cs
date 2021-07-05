using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    
    public class CamerMovement : MonoBehaviour {

        public static CamerMovement Instance{get;private set;}
        
        [SerializeField] private float maxRotation = 15f;
        [SerializeField] private float rotationSpeed = 8f;
        [SerializeField] private float roationThreshold = 0.2f;
        private float rotationAmountRef;
        
        
        
        private Vector3 targetAngle = Vector3.zero;
        
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance.gameObject);
            }
            targetAngle = transform.eulerAngles;
        }
        
        public void RotateCamera(float _rotateAmount){
            // Horizontal Rotation;
            if(Mathf.Abs(_rotateAmount) > roationThreshold){
                targetAngle += Vector3.up * _rotateAmount;
                targetAngle = new Vector3(targetAngle.x,Mathf.Clamp(targetAngle.y,-maxRotation,maxRotation),targetAngle.z);
                transform.eulerAngles = targetAngle;
            }else{
                targetAngle = transform.eulerAngles;
                float targetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,0f,ref rotationAmountRef,rotationSpeed * Time.deltaTime);
                transform.eulerAngles = Vector3.up * targetRotation;
            }
            
            
        }
        
        

        
        
        
    }

}