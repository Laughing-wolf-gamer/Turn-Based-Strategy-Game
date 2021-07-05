using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using GamerWolf.Utilitys;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class EnemyDeath : MonoBehaviour {
        [SerializeField] private Vector3 offScreenOffset = new Vector3(0f, 20f,0f);
        
        [SerializeField] private float deathDelay = 0f;
        [SerializeField] private float offScreenDelay = 3f;


        [SerializeField] private float iTweenDealy = 0.3f;
        [SerializeField] private float moveTime = 0.4f;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

        [SerializeField] private UnityEvent onReachedCapturePosition;

        
        public void Die(){
            StartCoroutine(DeathRoutine());
        }
        private void MoveOffBoard(Vector3 _targetPosition){
            iTween.MoveTo(gameObject,iTween.Hash(
                "x",_targetPosition.x,
                "y",_targetPosition.y,
                "z",_targetPosition.z,
                "delay",iTweenDealy,
                "easyType",easeType,
                "time",moveTime
            ));
            
        }
        private IEnumerator DeathRoutine(){
            yield return new WaitForSeconds (deathDelay);
            Vector3 offsetPos = transform.position + offScreenOffset;
            MoveOffBoard(offsetPos);
            yield return new WaitForSeconds(moveTime + offScreenDelay);
            if(Board.Instance.GetCapturePositionArray.Length > 0){
                Vector3 capturePos = Board.Instance.GetCapturePositionArray[Board.Instance.GetCurrentCapturedPosition].position;
                transform.position = capturePos + offScreenOffset;
                MoveOffBoard(capturePos);
                onReachedCapturePosition?.Invoke();
                DebugController.SetDebugTexts(transform.name + "\n Reached CapturePos");
                transform.eulerAngles = new Vector3(Random.Range(-2f,2f),transform.eulerAngles.y,Random.Range(-2f,2f));
                
                yield return new WaitForSeconds(moveTime);
                Board.Instance.GetCurrentCapturedPosition++;
                Board.Instance.GetCurrentCapturedPosition = Mathf.Clamp(Board.Instance.GetCurrentCapturedPosition,0,Board.Instance.GetCapturePositionArray.Length);

            }
        }
        


    }

}
