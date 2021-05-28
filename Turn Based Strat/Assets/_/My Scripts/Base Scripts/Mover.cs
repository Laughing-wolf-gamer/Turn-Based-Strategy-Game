using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Mover : MonoBehaviour {
        
        #region Variables.
        private const float moveAmount = 2f;
        [SerializeField] private float delayTime = 0.1f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Vector3 destination;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;


        [SerializeField] private bool isMoveing;


        
        #endregion


        #region Methods.
        
        public bool GetIsMoveing(){
            return isMoveing;
        }
        public void MoveLeft(){
            Move(transform.position + Vector3.left * moveAmount,delayTime);
        }
        public void MoveRight(){
            Move(transform.position + Vector3.right * moveAmount,delayTime);
        }
        public void MoveFoward(){
            Move(transform.position + Vector3.forward * moveAmount,delayTime);
        }
        public void MoveBack(){
            Move(transform.position + Vector3.back * moveAmount,delayTime);
        }
        private void Move(Vector3 _Destination,float _delayTime = 0.0f){
            StartCoroutine(MoveCouroutine(_delayTime,_Destination));
        }
        

        private IEnumerator MoveCouroutine(float _delayTime,Vector3 _destination){
            isMoveing = true;
            yield return new WaitForSeconds(delayTime);
            iTween.MoveTo(gameObject,iTween.Hash(
                "x",_destination.x,
                "y",_destination.y,
                "z",_destination.z,
                "delay",_delayTime,
                "easyType",easeType,
                "speed",moveSpeed
            ));
            while (Vector3.Distance(_destination,transform.position) > 0.01f){
                yield return null;
            }
            iTween.Stop(gameObject);
            transform.position = _destination;
            isMoveing = false;
        }

        #endregion


    }

}
