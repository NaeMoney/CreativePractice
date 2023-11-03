using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DizzyMedia.Shared;

using Newtonsoft.Json.Linq;
using HFPS.Player;
using HFPS.Systems;

namespace DizzyMedia.HFPS_Components {

    [AddComponentMenu("Dizzy Media/Components for HFPS/Player/Player Manager")]
    public class HFPS_PlayerMan : MonoBehaviour, ISaveable {


    //////////////////////////////////////
    ///
    ///     INSTANCE
    ///
    ///////////////////////////////////////


        public static HFPS_PlayerMan instance;


    //////////////////////////////////////
    ///
    ///     CLASSES
    ///
    ///////////////////////////////////////


        [System.Serializable]
        public class Melee_Cont {

            [Space]

            public string name;
            public MeleeController meleeCont;

        }//Melee_Cont

        [System.Serializable]
        public class Weapon_Cont {

            [Space]

            public string name;
            public WeaponController weaponCont;

        }//Weapon_Cont
        
        [System.Serializable]
        public class ThrowWeapon_Cont {

            [Space]

            public string name;
            
            #if HFPS_THROWABLES_PRESENT
            
                public HFPS_ThrowWeapon throwWeapCont;

            #endif
            
        }//ThrowWeapon_Cont


    //////////////////////////////////////
    ///
    ///     ENUMS
    ///
    ///////////////////////////////////////


        public enum SlowDown {

            None = 0,
            SlowOnZoom = 1,

        }//SlowDown


    //////////////////////////////////////
    ///
    ///     VALUES
    ///
    ///////////////////////////////////////


        public SlowDown slowDownUse;

        public HFPS_References references;
        public List<Melee_Cont> meleeConts;
        public List<Weapon_Cont> weaponConts;
        
        #if HFPS_THROWABLES_PRESENT
        
            public List<ThrowWeapon_Cont> throwConts;

        #endif
        
        public HFPS_EnemiesHolder enemHold;
        public bool isHiding;

        public int tabs;
        public bool userOpts;


    //////////////////////////////////////
    ///
    ///     START ACTIONS
    ///
    ///////////////////////////////////////


        void Start() {

            StartInit();

        }//Start

        public void StartInit(){

            if(HFPS_EnemiesHolder.instance != null){

                enemHold = HFPS_EnemiesHolder.instance;

            }//instance != null

            #if COMPONENTS_PRESENT

                if(slowDownUse == SlowDown.None){

                    references.playCont.LockZoom_State(true);

                }//slowDownUse = none

                if(slowDownUse == SlowDown.SlowOnZoom){

                    references.playCont.LockZoom_State(false);

                }//slowDownUse = slow on zoom

            #endif

        }//StartInit


    //////////////////////////////////////
    ///
    ///     HIDE ACTIONS
    ///
    ///////////////////////////////////////


        public void Hide_State(bool hide){

            isHiding = hide;

            if(enemHold.zombAI.Count > 0){

                for(int i = 0; i < enemHold.zombAI.Count; ++i ) {

                    enemHold.zombAI[i].behaviourSettings.playerInvisible = isHiding;

                }//for i enemHold

            }//zombAI.Count > 0

        }//Hide_State    


    //////////////////////////////////////
    ///
    ///     MOVE ACTIONS
    ///
    ///////////////////////////////////////


        public void LockMoveX_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LockMoveX_State(state);

            #endif

        }//LockMoveX_State

        public void LimitMoveX_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LimitMoveX_State(state);

            #endif

        }//LimitMoveX_State

        public void LimitMoveX_Set(int direction){

            #if COMPONENTS_PRESENT

                references.playCont.LimitMoveX_Set(direction);

            #endif

        }//LimitMoveX_Set


        public void LockMoveY_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LockMoveY_State(state);

            #endif

        }//LockMoveY_State

        public void LimitMoveY_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LimitMoveY_State(state);

            #endif

        }//LimitMoveY_State

        public void LimitMoveY_Set(int direction){

            #if COMPONENTS_PRESENT

                references.playCont.LimitMoveY_Set(direction);

            #endif

        }//LimitMoveY_Set


        public void LockJump_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LockJump_State(state);

            #endif

        }//LockJump_State


    //////////////////////////////////////
    ///
    ///     SPRINT ACTIONS
    ///
    ///////////////////////////////////////


        public void SprintLock_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LockSprint_State(state);

            #endif

        }//SprintLock_State


    //////////////////////////////////////
    ///
    ///     STATE ACTIONS
    ///
    ///////////////////////////////////////


        public void LockStateInput_State(bool state){

            #if COMPONENTS_PRESENT

                references.playCont.LockStateInput_State(state);

            #endif

        }//LockStateInput_State


    //////////////////////////////////////
    ///
    ///     LEAN ACTIONS
    ///
    ///////////////////////////////////////


        public void LeanLock_State(bool state){

            #if COMPONENTS_PRESENT

                references.playerFunct.LeanLock_State(state);

            #endif

        }//LeanLock_State


    //////////////////////////////////////
    ///
    ///     ZOOM ACTIONS
    ///
    ///////////////////////////////////////


        public void ZoomLock_State(bool state){

            #if COMPONENTS_PRESENT

                references.playerFunct.ZoomLock_State(state);

            #endif

        }//ZoomLock_State


    //////////////////////////////////////
    ///
    ///     WEAPONS ACTIONS
    ///
    ///////////////////////////////////////


        public void WeaponsUseLock_State(bool state){

            #if COMPONENTS_PRESENT

                if(meleeConts.Count > 0){

                    for(int m = 0; m < meleeConts.Count; m++) {

                        meleeConts[m].meleeCont.FireLock_State(state);

                    }//for m meleeConts

                }//meleeConts.Count > 0

                if(weaponConts.Count > 0){

                    for(int w = 0; w < weaponConts.Count; w++) {

                        weaponConts[w].weaponCont.FireLock_State(state);

                    }//for w weaponConts

                }//weaponConts.Count > 0

            #endif
            
            #if HFPS_THROWABLES_PRESENT
            
                if(throwConts.Count > 0){

                    for(int tc = 0; tc < throwConts.Count; tc++) {

                        throwConts[tc].throwWeapCont.FireLock_State(state);

                    }//for tc throwConts

                }//throwConts.Count > 0
            
            #endif

        }//WeaponsUseLock_State

        public void WeaponsZoomLock_State(bool state){

            #if COMPONENTS_PRESENT

                /*

                if(meleeConts.Count > 0){

                    for(int m = 0; m < meleeConts.Count; m++) {



                    }//for m meleeConts

                }//meleeConts.Count > 0

                */

                if(weaponConts.Count > 0){

                    for(int w = 0; w < weaponConts.Count; w++) {

                        weaponConts[w].weaponCont.ZoomLock_State(state);

                    }//for w weaponConts

                }//weaponConts.Count > 0

            #endif
            
            /*
            
            #if HFPS_THROWABLES_PRESENT
            
                if(throwConts.Count > 0){

                    for(int tc = 0; tc < throwConts.Count; tc++) {

                        throwConts[tc].throwWeapCont.ZoomLock_State(state);

                    }//for tc throwConts

                }//weaponConts.Count > 0
                
            #endif
            
            */

        }//WeaponsZoomLock_State


    //////////////////////////////////////
    ///
    ///     SAVE/LOAD ACTIONS
    ///
    ///////////////////////////////////////


        public Dictionary<string, object> OnSave() {

            return new Dictionary<string, object> {

                {"isHiding", isHiding }

            };//Dictionary

        }//OnSave

        public void OnLoad(JToken token) {

            isHiding = (bool)token["isHiding"];

            StartCoroutine("Load_Buff");

        }//OnLoad

        private IEnumerator Load_Buff(){

            yield return new WaitForSeconds(0.4f);

            if(isHiding){

                Hide_State(true);

            }//isHiding

        }//Load_Buff


    }//HFPS_PlayerMan


}//namespace