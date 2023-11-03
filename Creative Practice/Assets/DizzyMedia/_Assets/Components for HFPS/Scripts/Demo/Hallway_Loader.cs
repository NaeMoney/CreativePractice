using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_Loader : MonoBehaviour {
    
    
//////////////////////////
//
//      CLASSES
//
//////////////////////////
    
    
    [System.Serializable]
    public class Room {
        
        [Space]
        
        public string name;
        public GameObject show;
        
    }//Room
    

//////////////////////////
//
//      VALUES
//
//////////////////////////
    
    
    public List<Room> rooms;
    
    
//////////////////////////
//
//      START ACTIONS
//
//////////////////////////
    
    
    void Start() {
        
        //Rooms_Hide();
        
    }//start
    
    public void Room_Show(int slot){
        
        Rooms_Hide();
        
        rooms[slot - 1].show.SetActive(true);
        
    }//Room_Show
    
    public void Rooms_Hide(){
        
        for(int i = 0; i < rooms.Count; i++){
            
            rooms[i].show.SetActive(false);
            
        }//for i rooms
        
    }//Rooms_Hide


}//Hallway_Loader
