using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//#if (COMPONENTS_PRESENT || HFPS_DURABILITY_PRESENT || EASYHIDE_PRESENT || PUZZLER_PRESENT || HFPS_VENDOR_PRESENT)

    using DizzyMedia.Shared;

//#endif

using DizzyMedia.Utility;

public class DM_Menu : EditorWindow {
    
    
//////////////////////////////////////
///
///     MENU BUTTONS
///
///////////////////////////////////////
    
////////////////////////////////
///
///     UTILITIES CREATE
///
////////////////////////////////
    
////////////////////
///
///     EFFECTS
///
////////////////////
    
    
    #if (COMPONENTS_PRESENT || PUZZLER_PRESENT)
    
        [MenuItem("Tools/Dizzy Media/Utilities/Effects/Dissolve Controller", false , 11)]
        public static void Create_DissolveCont() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<DM_DissolveCont>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_DissolveCont 
    
    #endif
        
    #if (COMPONENTS_PRESENT || DM_AD_PRESENT)
    
        [MenuItem("Tools/Dizzy Media/Utilities/Effects/Simple Pulse", false, 11)]
        public static void Create_SimpPulse() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<SimplePulse>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_SimpPulse
    
    #endif
    
    
////////////////////
///
///     GIZMOS
///
////////////////////
    
    
    [MenuItem("Tools/Dizzy Media/Utilities/Gizmos/Simple Icon", false, 11)]
    public static void Create_SimpIcon() {
        
        if(Selection.gameObjects.Length > 0){
            
            Selection.gameObjects[0].AddComponent<SimpleIcon>();
        
        //Selection > 0
        } else {
            
            if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}
            
        }//Selection > 0
        
    }//Create_SimpIcon
    
    [MenuItem("Tools/Dizzy Media/Utilities/Gizmos/Transform Forward", false , 11)]
    public static void Create_TransForward() {

        if(Selection.gameObjects.Length > 0){

            Selection.gameObjects[0].AddComponent<TransForward>();

        //Selection > 0
        } else {

            if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

        }//Selection > 0

    }//Create_TransForward
    
    [MenuItem("Tools/Dizzy Media/Utilities/Gizmos/Transform Indicator", false, 11)]
    public static void Create_TransInd() {
        
        if(Selection.gameObjects.Length > 0){
            
            Selection.gameObjects[0].AddComponent<TransInd>();
        
        //Selection > 0
        } else {
            
            if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}
            
        }//Selection > 0
        
    }//Create_TransInd
    
    
////////////////////
///
///     HFPS
///
////////////////////
    
    
    [MenuItem("Tools/Dizzy Media/Utilities/HFPS/Mini Audio", false, 11)]
    public static void Create_MiniAudio() {

        if(Selection.gameObjects.Length > 0){

            Selection.gameObjects[0].AddComponent<HFPS_MiniAudio>();

        //Selection > 0
        } else {

            if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

        }//Selection > 0

    }//Create_MiniAudio
        
    #if (COMPONENTS_PRESENT || EASYHIDE_PRESENT || PUZZLER_PRESENT || HFPS_VENDOR_PRESENT)
        
        [MenuItem("Tools/Dizzy Media/Utilities/HFPS/Scare Handler", false , 11)]
        public static void Create_ScareHand() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<ScareHand>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_ScareHand
    
    #endif
        
        
////////////////////
///
///     WORLD
///
////////////////////
    
        
    #if COMPONENTS_PRESENT
    
        [MenuItem("Tools/Dizzy Media/Utilities/World/Forward Detect", false , 11)]
        public static void Create_ForwardDetect() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<DM_ForwardDetect>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_ForwardDetect
    
    #endif
    
    
////////////////////
///
///     UI
///
////////////////////
    
    
    #if (DM_AD_PRESENT || DM_TD_PRESENT)
    
        [MenuItem("Tools/Dizzy Media/Utilities/UI/Input Hold Handler", false, 11)]
        public static void Create_InputHold() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<InputHold_Handler>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_InputHold
        
    #endif
        
    #if DM_AD_PRESENT

        [MenuItem("Tools/Dizzy Media/Utilities/UI/Simple Dialogue", false, 11)]
        public static void Create_SimpleDialogue() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<SimpleDialogue>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_SimpleDialogue
    
    #endif
    
    
////////////////////////////////
///
///     SHARED CREATE
///
////////////////////////////////
    
/////////////////////////
///
///     ACTION BAR
///
/////////////////////////
    
    
    #if (COMPONENTS_PRESENT || HFPS_DURABILITY_PRESENT || EASYHIDE_PRESENT || PUZZLER_PRESENT || HFPS_VENDOR_PRESENT)
    
        [MenuItem("Tools/Dizzy Media/Shared/Systems/Action Bar/Action Bar", false , 11)]
        public static void Create_ActBar() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<DM_ActionBar>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_ActBar

    #endif
        
        
/////////////////////////
///
///     PLAYER
///
/////////////////////////
        
    
    #if (COMPONENTS_PRESENT || HFPS_DURABILITY_PRESENT || PUZZLER_PRESENT || HFPS_VENDOR_PRESENT)
        
        [MenuItem("Tools/Dizzy Media/Shared/Components/Player/References", false , 0)]
        public static void Create_Refs() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<HFPS_References>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_Refs
    
    #endif
        
    
/////////////////////////
///
///     UI
///
/////////////////////////
    
/////////////////
///
///     DISPLAY
///
/////////////////
    
        
    #if (COMPONENTS_PRESENT || HFPS_DURABILITY_PRESENT || EASYHIDE_PRESENT || PUZZLER_PRESENT || HFPS_VENDOR_PRESENT)
    
        [MenuItem("Tools/Dizzy Media/Shared/Components/UI/Display/Simple Fade", false , 0)]
        public static void Create_SimpleFade() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<SimpleFade>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_SimpleFade
    
    #endif
        
    
/////////////////
///
///     MENU
///
/////////////////

        
    #if (COMPONENTS_PRESENT || HFPS_DURABILITY_PRESENT || PUZZLER_PRESENT || HFPS_VENDOR_PRESENT)

        [MenuItem("Tools/Dizzy Media/Shared/Components/UI/Menu/UI Controller", false , 0)]
        public static void Create_UICont() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<HFPS_UICont>();

                //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_UICont
    
    
/////////////////////////
///
///     WORLD
///
///////////////////////// 
    
///////////////////
///
///     PLAYER
///
///////////////////
    
    
        [MenuItem("Tools/Dizzy Media/Shared/Components/World/Player/Character Action", false , 0)]
        public static void Create_CharAct() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<HFPS_CharacterAction>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_CharAct
    
    #endif
        
        
///////////////////
///
///     SCENE
///
///////////////////
    
    
    [MenuItem("Tools/Dizzy Media/Shared/Components/World/Scene/Timer", false , 0)]
    public static void Create_DMTimer() {

        if(Selection.gameObjects.Length > 0){

            Selection.gameObjects[0].AddComponent<DM_Timer>();

        //Selection > 0
        } else {

            if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

        }//Selection > 0

    }//Create_DMTimer  
    
    
}//DM_Menu
