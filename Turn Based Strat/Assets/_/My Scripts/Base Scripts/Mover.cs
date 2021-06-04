using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Mover : MonoBehaviour {
        
        #region Variables................................
        [Header("Events....")]
        [SerializeField] protected UnityEvent finishedMovementEvent;
        [Header("Stats")]
        [SerializeField] private float delayTime = 0.1f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] protected float rotateTime = 0.3f;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;
        [SerializeField] protected bool isMoveing;
        protected bool faceTarget = false;
        protected Vector3 destination;



        protected Board m_board;
        protected Node m_currentNode;
        
        
        #endregion


        #region Methods.
        protected virtual void Awake(){
            
        }

        protected virtual void Start(){
            m_board = Board.Instance;
            UpdateCurrentNode();
            
        }
       
        
        public bool GetIsMoveing(){
            return isMoveing;
        }
        public Node GetCurrentNode{
            get{
                return m_currentNode;
            }
        }
        
        // Move the Player Left.
        public void MoveLeft(){
            Move(transform.position + Vector3.left * Board.spacing,delayTime);
        }
        // Move the Player Right.
        public void MoveRight(){
            Move(transform.position + Vector3.right * Board.spacing,delayTime);
        }
        // Move the Player Forward.
        public void MoveFoward(){
            Move(transform.position + Vector3.forward * Board.spacing,delayTime);
        }
        // Move the Player BackWard.
        public void MoveBack(){
            Move(transform.position + Vector3.back * Board.spacing,delayTime);
        }
        public void Move(Vector3 _Destination,float _delayTime = 0.0f){
            if(isMoveing){
                Debug.LogWarning(string.Concat("Already is Moving."));
                return;
            }
            
            // Move to a Destination.
            Node targetNode = m_board.FindNodeAt(_Destination);
            if(targetNode != null){
                if(m_currentNode != null){
                    if(m_currentNode.GetLinkedNodes.Contains(targetNode)){
                        StartCoroutine(MoveCouroutine(_delayTime,_Destination));
                    }
                }
            }
            
            
        }
        

        
        

        protected virtual IEnumerator MoveCouroutine(float _delayTime,Vector3 _destination){
            isMoveing = true;
            Debug.Log( transform.name + " is Moveing");
            destination = _destination;
            if(faceTarget){
                FaceTargetDirection();
            }
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
            UpdateCurrentNode();
        }
        private void UpdateCurrentNode(){
            if(m_board != null){
                m_currentNode = m_board.FindNodeAt(transform.position);
            }
        }
        public void FaceTargetDirection(){
            Vector3 dir = destination - transform.position;
            Quaternion rotDir = Quaternion.LookRotation(dir,Vector3.up);
            float newY = rotDir.eulerAngles.y;
            iTween.RotateTo(gameObject,iTween.Hash(
                "y",newY,
                "delay",0f,
                "easeType",easeType,
                "time",rotateTime
            ));
            
        }



        #endregion


    }

}
