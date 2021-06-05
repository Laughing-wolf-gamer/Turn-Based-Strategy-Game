using UnityEngine;
using GamerWolf.Utilitys;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Board : MonoBehaviour {
        
        #region Variables.
        public static float spacing = 6f;
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
        [SerializeField] private Transform[] capturedPositionsArray;

        [Header("Editor")]
        [SerializeField] private Transform nodeParent;
        
        private int m_currentCapturedPosition = 0;

        private Node m_playerNode;
        private Node m_goalNode;
        private Node m_itemNode;
        private PlayerMover m_playerMover;
        
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
            m_goalNode = FindGoalNode();
            m_itemNode = FindItemNode();
            
        }
        public void SetPlayer(PlayerMover _player){
            
            m_playerMover = _player;
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
        



        // Find The Player Node........
        public Node FindPlayerNode(){
            if(m_playerMover != null && !m_playerMover.GetIsMoveing()){
                return FindNodeAt(m_playerMover.transform.position);
            }
            Debug.Log("Board: Find Player Node Error : Player is Not set.");
            return null;
        }
        public List<EnemyBrain> FindEnemiesAt(Node _node){
            List<EnemyBrain> foundEnemies = new List<EnemyBrain>();
            foreach (EnemyBrain enemy in GameHandler.Instance.GetEnemyList){
                Mover enemyMove = enemy.transform.GetComponent<EnemyMover>();
                if(enemyMove.GetCurrentNode == _node){
                    foundEnemies.Add(enemy);
                }
            }

            return foundEnemies;
        }
        
        // Drawing the goal Node...............
        public void DrawGoalNode(){
            if(goalPrefab != null){
                Transform goalInstance = Instantiate(goalPrefab,m_goalNode.transform.position,Quaternion.identity);
                iTween.ScaleFrom(goalInstance.gameObject,iTween.Hash(
                    "scale",Vector3.zero,
                    "time",drawGoalNodeTime,
                    "delay",drawGoalDelayTime,
                    "easeType",easeType
                ));
            }
        }
        public void InitBoard(){
            if(m_playerNode != null){
                Debug.Log("Initializeing Board");
                m_playerNode.InitNode();
                // // Need to work on It.
                // m_playerMover.ShowingCompass(true);
            }
        }
        
        // find the Goal Node.
        private Node FindGoalNode(){
            return allNodeList.Find(n => n.GetGoalNode);
        }
        private Node FindItemNode(){
            return allNodeList.Find(n => n.HasItemNode);
        }
        public void UpdatePlayerNode(){
            
            m_playerNode = FindPlayerNode();
        }
        public Node GetGoalNode{get{
                return m_goalNode;
            }
        }
        public Node GetItemNode{
            get{
                return m_itemNode;
            }
        }
        // Get the reference for the Player Node.
        public Node GetPlayerNode{
            get{
                return m_playerNode;
            }
        }
        public int GetCurrentCapturedPosition{
            get{
                return m_currentCapturedPosition;
            }set{
                
                m_currentCapturedPosition = value;
            }

        }
        public Transform[] GetCapturePositionArray{
            get{
                return capturedPositionsArray;
            }
        }


        private void OnDrawGizmos(){
            if(m_playerNode != null){
                Gizmos.DrawSphere(m_playerNode.transform.position,1f);
            }
            if(capturedPositionsArray.Length > 0){
                Gizmos.color = Color.blue;
                foreach(Transform capturePos in capturedPositionsArray){
                    Gizmos.DrawCube(capturePos.position,Vector3.one * 0.5f);
                }
            }
            
        }
        


        
        #endregion


    }

}