using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Mover : MonoBehaviour {
        
        #region Variables.
        private const float moveAmount = 8f;
        [SerializeField] private float delayTime = 0.1f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Vector3 destination;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;


        [SerializeField] private bool isMoveing;


        private Board board;
        #endregion


        #region Methods.
        private void Start(){
            board = Board.Instance;
            board.SetPlayerNode(this);
            UpdateBoard();
            board.GetPlayerNode.InitNode();
        }
        
        public bool GetIsMoveing(){
            return isMoveing;
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
        private void Move(Vector3 _Destination,float _delayTime = 0.0f){
            // Move the player to a Destination.
            Node targetNode = board.FindNodeAt(_Destination);
            if(targetNode != null && board.GetPlayerNode.GetLinkedNodes.Contains(targetNode)){
                StartCoroutine(MoveCouroutine(_delayTime,_Destination));
            }
        }
        private void UpdateBoard(){
            if(board != null){
                // Updating the Board for Player Positon on the Board.
                board.UpdatePlayerNode();
            }
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
            // Updating the Board for Player Positon on the Board.
            UpdateBoard();
        }

        #endregion


    }

}
