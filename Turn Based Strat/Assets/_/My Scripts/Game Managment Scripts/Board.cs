using UnityEngine;
using GamerWolf.Utilitys;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Board : MonoBehaviour {
        
        #region Variables.
        public static float spacing = 8f;
        public static Vector2[] direction = {
            new Vector2(spacing,0f),
            new Vector2(-spacing,0f),
            new Vector2(0f,spacing),
            new Vector2(0f,-spacing)
        };
        [SerializeField] private Transform goalPrefab;
        [SerializeField] private float drawGoalNodeTime;
        [SerializeField] private float drawGoalDelayTime;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

        [SerializeField] private List<Node> allNodeList = new List<Node>();

        private Node playerNode;
        private Node goalNode;
        private Mover playerMover;
        
        #endregion


        #region Methods.

        #region Singelton.
        public static Board Instance{get; private set;}

        #endregion
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance);
            }
            // Set Goal Node.
            goalNode = FindGoalNode();
        }
        
        public void SetPlayerNode(Mover playerMover){
            this.playerMover = playerMover;
        }

        
        // Get all the list of the Nodes in the Games.
        public List<Node> GetAllNodeList(){
            return allNodeList;
        }
        
        
        // Return a Node at any Vector3 Positon in the World.
        public Node FindNodeAt(Vector3 positon){
            Vector2 boardCord = Utility.GetVector2Int(new Vector2(positon.x,positon.z));
            return allNodeList.Find(n => n.GetCoordinate == boardCord);
        }
        // Find The Player Node
        private Node FindPlayerNode(){
            if(playerMover != null && !playerMover.GetIsMoveing()){
                return FindNodeAt(playerMover.transform.position);
            }
            Debug.Log("Board: Find Player Node Error : Player is Not set.");
            return null;
        }
        public void DrawGoalNode(){
            if(goalPrefab != null){
                Transform goalInstance = Instantiate(goalPrefab,goalNode.transform.position,Quaternion.identity);
                iTween.ScaleFrom(goalInstance.gameObject,iTween.Hash(
                    "scale",Vector3.zero,
                    "time",drawGoalNodeTime,
                    "delay",drawGoalDelayTime,
                    "easeType",easeType
                ));
            }
        }
        // find the Goal Node.
        private Node FindGoalNode(){
            return allNodeList.Find(n => n.GetGoalNode);
        }
        public void UpdatePlayerNode(){
            playerNode = FindPlayerNode();
        }
        public Node GetGoalNode{get{
                return goalNode;
            }
        }
        // Get the reference for the Player Node.
        public Node GetPlayerNode{
            get{
                return playerNode;
            }
        }
        private void OnDrawGizmos(){
            if(playerNode != null){
                Gizmos.DrawSphere(playerNode.transform.position,0.5f);
            }
        }


        
        #endregion


    }

}