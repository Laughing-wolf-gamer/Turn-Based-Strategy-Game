using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    
    public class CamerMovement : MonoBehaviour {

        public static CamerMovement Instance{get;private set;}
        [SerializeField] private Transform cameraPivot;
        
        [SerializeField] private float rotationSpeed = 8f;
        private float rotationAmountRef;
        private float currentRotationAmount;
        
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance.gameObject);
            }
            if(cameraPivot == null){
                cameraPivot = this.transform;
            }
        }
        private void LateUpdate(){
            cameraPivot.Rotate(Vector3.up * currentRotationAmount,Space.World);

        }
        public void RotateCamera(float _rotateAmount){
            currentRotationAmount = Mathf.SmoothDamp(currentRotationAmount,_rotateAmount,ref rotationAmountRef,rotationSpeed * Time.deltaTime);
        }

        
        
        
    }

}