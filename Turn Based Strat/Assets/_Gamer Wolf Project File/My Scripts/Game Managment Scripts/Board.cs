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

        [Header("Reference Transform Prefabs")]
        [SerializeField] private Transform goalPrefab;
        [SerializeField] private Transform[] itemPrefabs;
        [SerializeField] private Transform playerHideItems;
        

        [Space,Header("Stats")]
        [SerializeField] private float drawGoalNodeTime;
        [SerializeField] private float drawGoalDelayTime;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

        [SerializeField] private Transform[] capturedPositionsArray;
        [SerializeField] private Transform nodeHolder;
        
        
        private List<Node> m_allNodeList = new List<Node>();
        private int m_currentCapturedPosition = 0;
        private Node m_playerNode,m_goalNode;
        private List<Node> m_HideThePlayerNodes,m_itemNodeList,m_DoorNodeList;
        private PlayerMover m_playerMover;
        private List<Interactable> m_pickupItemList;
        
        #endregion


        #region Methods.

        #region Singelton.
        public static Board Instance{get; private set;}

        #endregion
        private void Awake(){
            if(nodeHolder.childCount > 0){
                for(int i = 0; i < nodeHolder.childCount; i++){
                    Node childNodes = nodeHolder.GetChild(i).GetComponent<Node>();
                    if(childNodes != null){
                        m_allNodeList.Add(childNodes);
                    }

                }
            }
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance);
            }
            m_HideThePlayerNodes = new List<Node>();
            m_itemNodeList = new List<Node>();
            m_DoorNodeList = new List<Node>();

            // Set Goal Node.
            m_goalNode = FindGoalNode();
            
            m_HideThePlayerNodes = FindHideThePlayerNode();
            m_itemNodeList = FindItemNodes();
            m_pickupItemList = new List<Interactable>();
        }
        
        public void SetPlayer(PlayerMover _player){
            m_playerMover = _player;
        }
       

        
        // Get all the list of the Nodes in the Games.
        public List<Node> GetAllNodeList(){
            return m_allNodeList;
        }
        
        
        // Return a Node at any Vector3 Positon in the World.
        public Node FindNodeAt(Vector3 positon){
            Vector2 boardCord = Utility.GetVector2Int(new Vector2(positon.x,positon.z));
            return m_allNodeList.Find(n => n.GetCoordinate == boardCord);
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
        public void FindDoorNodes(Node _node){
            m_DoorNodeList.Add(_node);
        }
        public void RemoveDoorNode(Node _node){
            m_DoorNodeList.Remove(_node);
        }
        public void SpawnItem(){
            if(m_HideThePlayerNodes.Count > 0){
                for(int i = 0; i < m_HideThePlayerNodes.Count; i++){
                    Instantiate(playerHideItems,m_HideThePlayerNodes[i].transform.position,Quaternion.identity);
                }
            }
            if(itemPrefabs.Length > 0){
                for (int i = 0; i < itemPrefabs.Length; i++){
                    Transform pickupItemInstance = Instantiate(itemPrefabs[i].transform, m_itemNodeList[i].transform.position,Quaternion.identity) as Transform;
                    Interactable pickUpItem = pickupItemInstance.GetComponent<Interactable>();
                    if( pickUpItem != null){
                        m_pickupItemList.Add(pickUpItem);
                    }
                }
            }
            
            
            
        }
        public ItemType GetSpawnItemType(Interactable _item){
            return _item.GetITemType();
        }
        

        public void InitBoard(){
            if(m_playerNode != null){
                DebugController.SetDebugTexts("Initializeing Board");
                m_playerNode.InitNode();
                // // Need to work on It.
                Invoke(nameof(InvokeCompass),0.4f);
            }
        }
        private void InvokeCompass(){
            m_playerMover.ShowingCompass();
        }
        
        // find the Goal Node.
        private Node FindGoalNode(){
            return m_allNodeList.Find(n => n.GetGoalNode);
        }
        private List<Node> FindHideThePlayerNode(){
            List<Node> hideThePlayerNodeList = new List<Node>();
            
            foreach(Node n in m_allNodeList){
                if(n.GetHideThePlayerNode){
                    hideThePlayerNodeList.Add(n);
                }
                
            }
            return hideThePlayerNodeList;
            
        }
        private List<Node> FindItemNodes(){
            List<Node> itemNodes = new List<Node>();
            foreach(Node n in m_allNodeList){
                if(n.HasItemNode){
                    itemNodes.Add(n);
                }
            }
            return itemNodes;
        }
        
        public void UpdatePlayerNode(){
            
            m_playerNode = FindPlayerNode();
        }
        
        public Node GetGoalNode{
            get{
                return m_goalNode;
            }
        }
        public List<Node> GetHideThePlayerNodesList{
            get{
                return m_HideThePlayerNodes;
            }
        }
        public List<Node> GetItemNodeList{
            get{
                return m_itemNodeList;
            }
        }
        public List<Interactable> GetPickUpItemList{
            get{
                return m_pickupItemList;
            }
        }
        public void RemoveItemFormItemList(Interactable itemToRemove){
            m_pickupItemList.Remove(itemToRemove);
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
        public List<Node> GetDoorNodeList{
            get{
                return m_DoorNodeList;
            }
        }



        #region  Gimzos

        public void AddNodeToNodeList(Node nodeToAdd){
            if(!m_allNodeList.Contains(nodeToAdd)){
                m_allNodeList.Add(nodeToAdd);

            }
        }
        public void RemoveNodeFromList(Node nodeToRemove){
            if(m_allNodeList.Contains(nodeToRemove)){
                m_allNodeList.Remove(nodeToRemove);
            }
        }
        private void OnDrawGizmos(){
            if(m_playerNode != null){
                Gizmos.DrawSphere(m_playerNode.transform.position,1f);
            }
            if(capturedPositionsArray.Length > 0){
                Gizmos.color = Color.blue;
                foreach(Transform capturePos in capturedPositionsArray){
                    Gizmos.DrawCube(capturePos.position,Vector3.one);
                }
            }
            
        }

        #endregion
        


        
        #endregion


    }

}