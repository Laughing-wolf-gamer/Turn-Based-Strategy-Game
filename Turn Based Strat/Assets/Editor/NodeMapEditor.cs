using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame {
    public class NodeMapEditor : EditorWindow {
        
        
        [MenuItem("Tools/NodeMapEditor")]
        private static void Open(){
            GetWindow<NodeMapEditor>();
        }
        
        
        public Transform nodesHolder;
        public Node simpleNode,goalNode,itemNode;


        private Node newNode;
        
        private void OnGUI(){
            SerializedObject obj = new SerializedObject(this);

            EditorGUILayout.PropertyField(obj.FindProperty("simpleNode"));
            EditorGUILayout.PropertyField(obj.FindProperty("goalNode"));
            EditorGUILayout.PropertyField(obj.FindProperty("itemNode"));


            EditorGUILayout.PropertyField(obj.FindProperty("nodesHolder"));
            if(!(simpleNode & goalNode & itemNode)){
                EditorGUILayout.HelpBox("Assigne Node Prefabs",MessageType.Warning);
            }
            else if(nodesHolder == null){
                EditorGUILayout.HelpBox("Assigne a Node Parent GameObject",MessageType.Warning);
            }else{
                EditorGUILayout.BeginVertical("box");
                CreateButtons();
                EditorGUILayout.EndVertical();
            }
            obj.ApplyModifiedProperties();
        }

        private void CreateButtons(){
            if(GUILayout.Button("Create a Node")){
                
                CreateSimpleNode(Vector3.forward);
            }
            if(GUILayout.Button("Create a Goal Node")){
                
                CreateGoalNode();
            }

            if(GUILayout.Button("Create a Item Node")){
                CreateItemNode();
            }
            if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Node>()){
                if(GUILayout.Button("Remove Node")){
                    RemoveNode();
                }
                if(GUILayout.Button("Convert Node To Goal")){
                    ConvertToGoal();
                }
                if(GUILayout.Button("Conver Node to Item Node")){
                    ConvertToItem();
                }
                if(GUILayout.Button("Create Node Forward")){
                    Vector3 dir = Selection.activeGameObject.transform.forward * 1;
                    CreateSimpleNode(dir);
                }
                if(GUILayout.Button("Create Node Back")){
                    Vector3 dir = Selection.activeGameObject.transform.forward * -1;
                    CreateSimpleNode(dir);
                }
                if(GUILayout.Button("Create Node Right")){
                    Vector3 dir = Selection.activeGameObject.transform.right * 1;
                    CreateSimpleNode(dir);
                }
                if(GUILayout.Button("Create Node Left")){
                    Vector3 dir = Selection.activeGameObject.transform.right * -1;
                    CreateSimpleNode(dir);
                }
            }
            
        }
        private void CreateSimpleNode(Vector3 diretion){
            if(simpleNode != null){
                newNode = Instantiate(simpleNode);
                newNode.transform.gameObject.name = "Node " + nodesHolder.childCount;
                
                newNode.transform.SetParent(nodesHolder);
                if(nodesHolder.childCount > 1){
                    Node currentNode = Selection.activeGameObject.GetComponent<Node>();
                    if(currentNode != null){
                        newNode.transform.position = currentNode.transform.position + diretion * Board.spacing;
                    }else{
                        newNode.transform.position = nodesHolder.GetChild(newNode.transform.GetSiblingIndex() - 1).transform.position + diretion * Board.spacing;
                    }
                }
                
                Selection.activeGameObject = newNode.gameObject;

            }else{
                EditorGUILayout.HelpBox("Assign a node ",MessageType.Warning);
            }
        }
        private void CreateGoalNode(){
            if(goalNode != null){
                newNode = Instantiate(goalNode);
                newNode.transform.gameObject.name = "Goal Node " + nodesHolder.childCount;
                newNode.transform.SetParent(nodesHolder);
                if(nodesHolder.childCount > 1){
                    Node currentNode = Selection.activeGameObject.GetComponent<Node>();
                    if(currentNode != null){
                        newNode.transform.position = currentNode.transform.position + Vector3.forward * Board.spacing;
                    }else{
                        
                        newNode.transform.position = nodesHolder.GetChild(newNode.transform.GetSiblingIndex() - 1).transform.position + Vector3.forward * Board.spacing;
                    }
                }
                Selection.activeGameObject = newNode.gameObject;

            }else{
                EditorGUILayout.HelpBox("Assigne a Goal node ",MessageType.Warning);
            }
        }
        private void CreateItemNode(){
            if(itemNode != null){
                newNode = Instantiate(itemNode);
                newNode.transform.gameObject.name = "Item Node " + nodesHolder.childCount;
                newNode.transform.SetParent(nodesHolder);
                if(nodesHolder.childCount > 1){
                    Node currentNode = Selection.activeGameObject.GetComponent<Node>();
                    if(currentNode != null){
                        newNode.transform.position = currentNode.transform.position + Vector3.forward * Board.spacing;

                    }else{
                        newNode.transform.position = nodesHolder.GetChild(newNode.transform.GetSiblingIndex() - 1).transform.position + Vector3.forward * Board.spacing;
                    }
                }
                
                Selection.activeGameObject = newNode.gameObject;
            }else{
                EditorGUILayout.HelpBox("Assign a Item node ",MessageType.Warning);
            }
        }
        private void RemoveNode(){
            Node selectedNode = Selection.activeGameObject.transform.GetComponent<Node>();
            
            
            DestroyImmediate(selectedNode.gameObject);
            if(nodesHolder.childCount == 0){
                Selection.activeGameObject = nodesHolder.transform.gameObject;
            }else{
                Selection.activeGameObject = nodesHolder.GetChild(nodesHolder.childCount -1 ).transform.gameObject;
            }
        }
        private void ConvertToGoal(){
            RemoveNode();
            CreateGoalNode();
        }
        private void ConvertToItem(){
            RemoveNode();
            CreateItemNode();
        }

        
    }

}
