using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

#if EASYHIDE_PRESENT

    using DizzyMedia.HFPS_EasyHide;

#endif

using HFPS.Systems;
using ThunderWire.Input;

namespace DizzyMedia.Shared {

    [AddComponentMenu("Dizzy Media/Shared/Systems/Action Bar/Action Bar")]
    public class DM_ActionBar : MonoBehaviour {


    //////////////////////////
    //
    //      INSTANCE
    //
    //////////////////////////


        public static DM_ActionBar instance;


    //////////////////////////
    //
    //      CLASSES
    //
    //////////////////////////


        [System.Serializable]
        public class Action {

            [Space]

            public string name;
            public string actionInput;

            [Space]

            [Header("Holders")]

            [Space]

            public GameObject holder;
            public Text actionText;
            public string defaultText;

            [Space]

            [Header("Icon")]

            [Space]

            public GameObject keyboard;
            public GameObject gamepad;

            [Space]

            [Header("Events")]

            [Space]

            public UnityEvent onActionInit;

            [Header("Auto")]

            [Space]

            public bool actionPressed;

        }//Action

        [System.Serializable]
        public class Auto {

            [Space]

            public List<string> actionTexts;
            
            #if EASYHIDE_PRESENT
            
                public List<HideHand> hideHands;

            #endif
            
            [Space]

            public bool pauseKeyPressed;
            public bool pausedLocked;
            public bool paused;
            public bool actionsActive;
            public bool buffInput;
            public float lockWait;
            public bool locked;

        }//Auto


    //////////////////////////
    //
    //      VALUES
    //
    //////////////////////////

    /////////////////
    //
    //   DEBUG
    //
    /////////////////


        public bool useDebug;


    /////////////////
    //
    //   REFERENCES
    //
    /////////////////


        public GameObject holder;
        public List<Action> actions;


    /////////////////
    //
    //   START OPTIONS
    //
    /////////////////


        public bool createInstance;


    /////////////////
    //
    //   INPUT OPTIONS
    //
    /////////////////


        public bool detectPause;
        public string pauseInput;
        public float inputWait;


    /////////////////
    //
    //   AUTO
    //
    /////////////////


        public DM_InternEnums.PlayInput_Type inputType;
        public Auto auto;

        public int actBarTabs;
        public int debugInt;

        public bool startOpts;
        public bool inputOpts;


    //////////////////////////
    //
    //      START ACTIONS
    //
    //////////////////////////


        void Awake(){

            if(createInstance){

                instance = this;

            }//createInstance

        }//Awake

        void Start() {

            StartInit();

        }//Start

        public void StartInit(){

            if(holder.activeSelf){

                ActionBar_State(false);

            }//activeSelf

            ActionTexts_Clear();

            auto.buffInput = false;
            auto.actionsActive = false;
            auto.locked = false;

            for(int i = 0; i < actions.Count; ++i ) {

                actions[i].holder.SetActive(false);
                actions[i].actionText.text = actions[i].defaultText;

            }//for i actions

        }//StartInit


    //////////////////////////
    //
    //      UPDATE ACTIONS
    //
    //////////////////////////


        void Update(){

            if(!auto.locked){

                if(InputHandler.InputIsInitialized) {

                    InputCheck_Type();

                }//InputIsInitialized


    ////////////////
    // PAUSE INPUT CHECK
    ////////////////


                if(detectPause){

                    if(InputHandler.InputIsInitialized) {

                        auto.pauseKeyPressed = InputHandler.ReadButton(pauseInput);

                    }//InputIsInitialized

                    if(auto.actionsActive){

                        if(auto.pauseKeyPressed){

                            if(!auto.pausedLocked){

                                PauseCheck();

                            }//!pausedLocked

                        }//pauseKeyPressed & menuOpen

                        if(detectPause){

                            if(!auto.pauseKeyPressed){

                                auto.pausedLocked = false;

                            }//!pauseKeyPressed

                        }//detectPause

                    }//actionsActive

                }//detectPause


    ////////////////
    // ACTIONS INPUT CHECK
    ////////////////


                if(!auto.paused){

                    if(auto.actionsActive){

                        if(auto.actionTexts.Count > 0){

                            for(int i = 0; i < actions.Count; ++i ) {

                                if(actions[i].actionInput != ""){

                                    if(InputHandler.InputIsInitialized) {

                                        actions[i].actionPressed = InputHandler.ReadButton(actions[i].actionInput);

                                    }//InputIsInitialized

                                    if(actions[i].actionPressed && !auto.buffInput) {

                                        auto.buffInput = true;

                                        Action_Init(i);

                                    }//actionPressed & !buffInput

                                }//actionInput != null

                            }//for i actions

                        }//actionTexts.Count > 0

                    }//actionsActive

                }//!paused

            }//!locked

        }//Update


    //////////////////////////
    //
    //      ACTIONS CHECK ACTIONS
    //
    //////////////////////////


        public void ActionBar_Check(){

            if(!auto.locked){

                InputCheck_Icons();

                if(auto.actionTexts.Count > 0){

                    for(int i = 0; i < auto.actionTexts.Count; ++i ) {

                        actions[i].actionText.text = auto.actionTexts[i];

                        if(actions[i].actionText.text != "" && actions[i].actionText.text != "Null"){

                            actions[i].holder.SetActive(true);

                        //text != null
                        } else {

                            actions[i].holder.SetActive(false);

                        }//text != null

                    }//for i actionTexts

                }//actionTexts.Count > 0

            }//!locked

        }//ActionBar_Check


    //////////////////////////
    //
    //      STATE ACTIONS
    //
    //////////////////////////


        public void ActionBar_State(bool active){

            holder.SetActive(active);

            auto.actionsActive = active;

            if(useDebug){

                Debug.Log("AB State Actions Active = " + auto.actionsActive);

            }//useDebug

        }//ActionBar_State

        public void ActionBar_StateCheck(){

            if(!auto.locked){

                if(useDebug){

                    Debug.Log("AB State Check Action Count = " + auto.actionTexts.Count);

                }//useDebug

                if(auto.actionTexts.Count > 0){

                    holder.SetActive(true);

                    auto.actionsActive = true;

                //actionTexts.Count > 0
                } else {

                    holder.SetActive(false);

                    auto.actionsActive = false;

                }//actionTexts.Count > 0

                if(useDebug){

                    Debug.Log("AB State Check Actions Active = " + auto.actionsActive);

                }//useDebug

            }//!locked

        }//ActionBar_State

        public void ActionBar_StateCheckDelayed(float delay){

            StartCoroutine("StateCheckDelayed", delay);

        }//ActionBar_StateCheckDelayed

        public IEnumerator StateCheckDelayed(float delay){

            yield return new WaitForSeconds(delay);

            if(!auto.locked){

                if(auto.actionTexts.Count > 0){

                    holder.SetActive(true);

                    auto.actionsActive = true;

                //actionTexts.Count > 0
                } else {

                    holder.SetActive(false);

                    auto.actionsActive = false;

                }//actionTexts.Count > 0

                if(useDebug){

                    Debug.Log("AB State Check Delay Actions Active = " + auto.actionsActive);

                }//useDebug

            }//!locked

        }//StateCheckDelayed


    //////////////////////////
    //
    //      INPUT ACTIONS
    //
    //////////////////////////

    /////////////////
    //
    //      INPUT CHECKS
    //
    /////////////////


        public void InputCheck_Type(){

            if(InputHandler.CurrentDevice == InputHandler.Device.MouseKeyboard) {

                inputType = DM_InternEnums.PlayInput_Type.Keyboard;

            //deviceType = keyboard
            } else if(InputHandler.CurrentDevice.IsGamepadDevice() > 0) {

                inputType = DM_InternEnums.PlayInput_Type.Gamepad;

            }//deviceType = gamepad

        }//InputCheck_Type

        public void InputCheck_Icons(){

            if(auto.actionTexts.Count > 0){

                for(int i = 0; i < auto.actionTexts.Count; ++i ) {

                    if(inputType == DM_InternEnums.PlayInput_Type.Keyboard){

                        if(actions[i].keyboard != null){

                            actions[i].keyboard.SetActive(true);

                        }//keyboard != null

                        if(actions[i].gamepad != null){

                            actions[i].gamepad.SetActive(false);

                        }//gamepad != null

                    }//inputType = keyboard

                    if(inputType == DM_InternEnums.PlayInput_Type.Gamepad){

                        if(actions[i].keyboard != null){

                            actions[i].keyboard.SetActive(false);

                        }//keyboard != null

                        if(actions[i].gamepad != null){

                            actions[i].gamepad.SetActive(true);

                        }//gamepad != null

                    }//inputType = gamepad

                }//for i actionTexts

            }//actionTexts.Count > 0

        }//InputCheck_Icons


    /////////////////
    //
    //      INPUT ACTIONS
    //
    /////////////////


        public void Action_Init(int slot){

            actions[slot].onActionInit.Invoke();

            StartCoroutine("BuffInput", slot);

        }//Action_Init


    /////////////////
    //
    //      INPUT BUFFS
    //
    /////////////////


        public IEnumerator BuffInput(int slot){

            yield return new WaitForSeconds(inputWait);

            auto.buffInput = false;
            actions[slot].actionPressed = false;

        }//BuffInput


    //////////////////////////
    //
    //      RESET ACTIONS
    //
    //////////////////////////


        public void ActionBar_Reset(){

            ActionTexts_Clear();

            for(int i = 0; i < actions.Count; ++i ) {

                actions[i].holder.SetActive(false);
                actions[i].actionText.text = actions[i].defaultText;
                actions[i].actionPressed = false;

            }//for i actions

            auto.buffInput = false;
            auto.actionsActive = false;
            auto.locked = false;

        }//ActionBar_Reset
        
        
    //////////////////////////
    //
    //      HIDE HAND ACTIONS
    //
    //////////////////////////
    
    
        #if EASYHIDE_PRESENT

            public void HideHand_Add(HideHand newHideHand){

                if(!auto.hideHands.Contains(newHideHand)){

                    auto.hideHands.Add(newHideHand);

                }//!Contains

            }//HideHand_Add

            public void HideHand_Remove(HideHand newHideHand){

                if(auto.hideHands.Contains(newHideHand)){

                    auto.hideHands.Remove(newHideHand);

                }//Contains

            }//HideHand_Remove

            public void HideHands_Clear(){

                auto.hideHands = new List<HideHand>();

            }//HideHands_Clear
        
        #endif


    //////////////////////////
    //
    //      ACTION TEXTS ACTIONS
    //
    //////////////////////////


        public void ActionTexts_Add(string newAction){

            if(!auto.actionTexts.Contains(newAction)){

                auto.actionTexts.Add(newAction);

            }//!Contains

        }//ActionTexts_Add

        public void ActionTexts_Remove(string newAction){

            if(auto.actionTexts.Contains(newAction)){

                auto.actionTexts.Remove(newAction);

            }//Contains

        }//ActionTexts_Add

        public void ActionTexts_Clear(){

            auto.actionTexts = new List<string>();

        }//ActionTexts_Add


    //////////////////////////
    //
    //      PAUSE ACTIONS
    //
    //////////////////////////


        public void PauseCheck(){

            auto.paused = HFPS_GameManager.Instance.isPaused;

            if(auto.actionsActive){

                if(auto.paused){

                    holder.SetActive(false);

                //paused
                } else {

                    holder.SetActive(true);

                }//paused

            }//actionsActive

        }//PauseCheck


    //////////////////////////
    //
    //      LOCK ACTIONS
    //
    //////////////////////////


        public void Lock_State(bool newLock){

            auto.locked = newLock;

            if(useDebug){

                Debug.Log("Lock State = " + auto.locked);

            }//useDebug

        }//Lock_State

        public void Lock_DelayUpdate(float delay){

            auto.lockWait = delay;

        }//Lock_DelayUpdate

        public void Lock_StateDelay(bool newLock){

            StopCoroutine("LockDelay");
            StartCoroutine("LockDelay", newLock);

        }//Lock_StateDelay

        private IEnumerator LockDelay(bool newLock){

            yield return new WaitForSeconds(auto.lockWait);

            auto.locked = newLock;

            if(useDebug){

                Debug.Log("Lock State Delay = " + auto.locked);

            }//useDebug

        }//LockDelay


    }//DM_ActionBar


}//namespace
