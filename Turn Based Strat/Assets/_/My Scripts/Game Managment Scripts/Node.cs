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
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private bool isGoal = false;
        [Header("Stats")]
        [SerializeField] private float scaleTime = 0.1f;
        [SerializeField] private float dealyTime = 0.25f;
        [Header("ITween Variables.")]
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;

        /// Private Variables.

        
        private Vector3 initalSize;
        private Vector2 m_coordinate;
        private List<Node> m_neighboursNodes = new List<Node>();
        private bool isInitialize = false;
        private List<Node> m_LinkedNeighbours = new List<Node>();
        private Board board;

        #endregion


        #region Methods.
        private void Awake(){
            m_coordinate = new Vector2(transform.position.x,transform.position.z);
        }
        private void Start() {
            board = Board.Instance;
            m_neighboursNodes = new List<Node>();
            m_LinkedNeighbours = new List<Node>();
            
            if(nodeViewPrefabs != null){
                initalSize = nodeViewPrefabs.localScale;
                nodeViewPrefabs.localScale = Vector3.zero;
                m_neighboursNodes = FindNeighbours(board.GetAllNodeList());
                
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
            foreach (Node n in m_neighboursNodes){
                if(!m_LinkedNeighbours.Contains(n)){
                    Obstacles obstacle = FindObstacle(n);
                    if(obstacle == null){
                        LinkNode(n);
                        n.InitNode();    
                    }
                }
            }
            
            
        }
        public void LinkNode(Node _targetNode){
            Transform linkInstance = Instantiate(linkPrefab,transform.position,Quaternion.identity);
            linkInstance.transform.SetParent(transform);
            Link link = linkInstance.GetComponent<Link>();
            if(link != null){
                Debug.Log("isIntializing Link");
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
        private List<Node> FindNeighbours(List<Node> nodes){
            List<Node> nList = new List<Node>();
            foreach(Vector2 dir in Board.direction){
                Node neighbourNode = nodes.Find(n => n.GetCoordinate == GetCoordinate + dir);
                if(neighbourNode != null && !nList.Contains(neighbourNode)){
                    nList.Add(neighbourNode);
                }
            }

            return nList;
        }
        private Obstacles FindObstacle(Node _targetNode){
            Vector3 direction = _targetNode.transform.position - transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position,direction,out hit,Board.spacing,obstacleMask,QueryTriggerInteraction.UseGlobal)){
//                Debug.Log("Node Find Obstacel : hit an Obstacle form " + this.name + " to " + _targetNode.name);
                return hit.transform.GetComponent<Obstacles>();
            }
            return null;
        }
        public List<Node> GetNeighbourNodes{
            get{
                return m_neighboursNodes;
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

        #endregion


    }
    
}
