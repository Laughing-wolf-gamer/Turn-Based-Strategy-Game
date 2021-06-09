using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class LookAt : MonoBehaviour {
        
        [SerializeField] private Transform lookAtTransform;



        [SerializeField] private float rotationSpeed = 5f;
        private float targetRotation;
        private float rotationRef;
         
        private void LateUpdate(){
            Vector3 dir = (lookAtTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
            targetRotation = Mathf.SmoothDamp(targetRotation,angle,ref rotationRef,Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.AngleAxis(targetRotation,Vector3.up);
        }

    }

}