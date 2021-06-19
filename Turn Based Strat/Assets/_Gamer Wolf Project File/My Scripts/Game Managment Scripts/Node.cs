using System;
using UnityEngine;
using GamerWolf.Utilitys;
using System.Collections;
using System.Collections.Generic;
namespace GamerWolf.TurnBasedStratgeyGame {
    
    public class Node : MonoBehaviour {
        
        #region Variables.
        [Header("External Referances")]
        [SerializeField] private Transform linkPrefab;
        [SerializeField] private Transform nodeViewPrefabs;
        [SerializeField] private LayerMask obstacleMask,doorMask;
        [SerializeField] private bool isGoal = false;
        [SerializeField] private bool hasItem = false,hasKey = false;
        [SerializeField] private bool canHideThePlayer = false;
        [SerializeField] private bool isDoorNode = false;
        [Header("Stats")]
        [SerializeField] private float scaleTime = 0.1f;
        [SerializeField] private float dealyTime = 0.25f;
        [Header("ITween Variables.")]
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;

        /// Private Variables.
        private Vector3 initalSize;
        private Vector2 m_coordinate;
        private bool isInitialize = false;
        private List<Node> m_neighboursNodesList = new List<Node>();
        private List<Node> m_LinkedNeighbours = new List<Node>();
        private Dictionary<Key.KeyType , Node> doorNodeList = new Dictionary<Key.KeyType, Node>();
        private Key.KeyType neighbourDoorKey;
        private Board board;
        

        #endregion


        #region Methods.
        private void Awake(){
            m_coordinate = new Vector2(transform.position.x,transform.position.z);
        }
        private void Start() {
            board = Board.Instance;
            m_neighboursNodesList = new List<Node>();
            m_LinkedNeighbours = new List<Node>();
            
            if(nodeViewPrefabs != null){
                initalSize = nodeViewPrefabs.localScale;
                nodeViewPrefabs.localScale = Vector3.zero;
                m_neighboursNodesList = FindNeighbours(board.GetAllNodeList());
                
            }
        }
        
        [ContextMenu("Initialize")]
        public void InitNode(){
            if(!isInitialize){
                ScaleNodeView();
                InitNeigboursNodes();
                isInitialize = true;
            }
        }
        public void InitNeigboursNodes(){
            StartCoroutine(InitRoutine());
        }
        
        private IEnumerator InitRoutine(){
            yield return new WaitForSeconds(0.1f);
            foreach (Node n in m_neighboursNodesList){
               
                if(!m_LinkedNeighbours.Contains(n)){
                    Obstacles obstacle = FindObstacle(n);

                    Door door = FindDoor(n ,out neighbourDoorKey);

                    if(obstacle == null){
                        if(door != null){
                            n.isDoorNode = true;
                            Debug.Log("Found A Door at " + n.name);
                            doorNodeList.Add(neighbourDoorKey,n);
                            Board.Instance.FindDoorNodes(n);
                        }else{
                            LinkNode(n);
                            n.InitNode();    

                        }
                    }
                }
            }
            
            
        }
        [ContextMenu("Connect Door Nodes")]
        public void ConnectDoorNode(Key.KeyType _doorKey){
            StartCoroutine(InitConnectDoorNodes(_doorKey));
        }
        private IEnumerator InitConnectDoorNodes(Key.KeyType _doorKey){
            yield return new WaitForSeconds(0.1f);
            if(doorNodeList.ContainsKey(_doorKey)){
                if(!m_LinkedNeighbours.Contains(doorNodeList[_doorKey])){
                    LinkNode(doorNodeList[_doorKey]);
                    doorNodeList[_doorKey].InitNode();
                    doorNodeList[_doorKey].isDoorNode = false;
                    Board.Instance.RemoveDoorNode(doorNodeList[_doorKey]);
                    doorNodeList.Remove(_doorKey);
                }
                
            }
        }
        public void LinkNode(Node _targetNode){
            Transform linkInstance = Instantiate(linkPrefab,transform.position,Quaternion.identity);
            linkInstance.transform.SetParent(transform);
            Link link = linkInstance.GetComponent<Link>();
            if(link != null){

                link.DrawLink(transform.position,_targetNode.transform.position);
            }
            if(!m_LinkedNeighbours.Contains(_targetNode)){
                m_LinkedNeighbours.Add(_targetNode);

            }
            if(!_targetNode.GetLinkedNodes.Contains(this)){
                _targetNode.m_LinkedNeighbours.Add(this);
            }
            
            
        }
        

        public void ScaleNodeView(){
            if(nodeViewPrefabs != null){
                iTween.ScaleTo(nodeViewPrefabs.gameObject,iTween.Hash(
                    "time",scaleTime,
                    "scale",initalSize,
                    "easyType",easeType,
                    "dealyTime",dealyTime
                ));
            }
        }
        public List<Node> FindNeighbours(List<Node> nodes){
            List<Node> nList = new List<Node>();
            foreach(Vector2 dir in Board.direction){
                Node neighbourNode = nodes.Find(n => n.GetCoordinate == GetCoordinate + dir);
                if(neighbourNode != null && !nList.Contains(neighbourNode)){
                    nList.Add(neighbourNode);
                }
            }

            return nList;
        }

        // Find Searce for Any Obstacle in Between Nodes..........
        private Obstacles FindObstacle(Node _targetNode){
            Vector3 direction = _targetNode.transform.position - transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position,direction,out hit,Board.spacing,obstacleMask,QueryTriggerInteraction.Collide)){
                return hit.transform.GetComponent<Obstacles>();
            }
            return null;
        }
        private Door FindDoor(Node _targetNode,out Key.KeyType _doorType){
            Vector3 direction = _targetNode.transform.position - transform.position;

            RaycastHit hit;
            if(Physics.Raycast(transform.position,direction,out hit,Board.spacing,doorMask,QueryTriggerInteraction.UseGlobal)){
                Door door = hit.transform.GetComponent<Door>();
                _doorType = door.GetDoorKeyType();
                return door;
            }
            _doorType = default;
            return null;
        }
        public Node FindNeighbourAt(List<Node> nodes,Vector2 direction){
            return nodes.Find( n => n.GetCoordinate == GetCoordinate + direction);
        }
        public Node FindNeighbourAt(Vector2 dir){
            return FindNeighbourAt(m_neighboursNodesList,dir);
        }
        public List<Node> GetNeighbourNodes{
            get{
                return m_neighboursNodesList;
            }
        }
        public List<Node> GetLinkedNodes{
            get{
                return m_LinkedNeighbours;
            }
        }
        public Vector2 GetCoordinate{
            get{
                return Utility.GetVector2Int(m_coordinate);
            }
        }
        public bool GetGoalNode{
            get{
                return isGoal;
            }
        }
        public bool HasItemNode{
            get{
                return hasItem;
            }
            set{
                hasItem = value;
            }
        }
        public bool GetHideThePlayerNode{
            get{
                return canHideThePlayer;
            }
        }
        public bool GetDoorNode{
            get{
                return isDoorNode;
            }
        }
        
        

        #endregion


    }
    
}
