/*
 * ItemSwitcher.cs - by ThunderWire Studio
 * ver. 1.0
*/

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ThunderWire.Utility;
using ThunderWire.Input;
using HFPS.Systems;

using DizzyMedia.HFPS_Components;


namespace HFPS.Player
{
    /// <summary>
    /// Main script for Switching Items.
    /// </summary>
    public class ItemSwitcher : MonoBehaviour
    {
        private Inventory inventory;
        private HFPS_GameManager gameManager;
        private ScriptManager scriptManager;
        private Camera mainCamera;

        public List<GameObject> ItemList = new List<GameObject>();
        public List<HFPS_DualWield> dualWields = new List<HFPS_DualWield>();
        public int currentItem = -1;

        [Header("Wall Detecting")]
        public LayerMask HitMask;
        public bool detectWall;
        public bool showGizmos;
        public float wallHitRange;

        [Header("Wall Hit Position")]
        public Transform WallHitTransform;
        public float WallHitSpeed = 5f;
        public Vector3 WallHitHideWeapon;
        public Vector3 WallHitShowWeapon;

        [Header("Item On Start")]
        public bool startWithCurrentItem;
        public bool startWithoutAnimation;

        [Tooltip("Item ID in the inventory database. Leave -1 unless it is an inventory item.")]
        public int startingItemID = -1;

        [HideInInspector]
        public int weaponItem = -1;

        private int newItem = 0;

        private bool hideWeapon;
        private bool handsFreed;
        private bool antiSpam;
        private bool spam;
        
        [Header("Auto")]
        public bool incomp;
        public int incompInt;
        public int secondItem = -1;

        void Awake()
        {
            scriptManager = ScriptManager.Instance;
            inventory = Inventory.Instance;
            gameManager = HFPS_GameManager.Instance;
            mainCamera = ScriptManager.Instance.MainCamera;
        }

        IEnumerator Start()
        {
            if (startWithCurrentItem)
            {
                yield return new WaitUntil(() => InputHandler.InputIsInitialized);

                if (startingItemID > -1)
                {
                    inventory.AddItem(startingItemID, 1, null, true);
                }

                if (!startWithoutAnimation)
                {
                    SelectSwitcherItem(currentItem);
                }
                else
                {
                    ActivateItem(currentItem);
                }
            }
        }

        void FixedUpdate()
        {
            if (!inventory.CheckSwitcherItemInventory(weaponItem))
            {
                weaponItem = -1;
            }
        }

        public void SelectSwitcherItem(int switchID)
        {
            incomp = false;
            incompInt = -1;
        
            if (IsBusy()) return;

            if (switchID != currentItem)
            {

                if(dualWields.Count > 0){
                
                    if(dualWields[switchID].dualWield){

                        if(currentItem > -1){

                            for(int i = 0; i < dualWields[currentItem].incompIDs.Count; i++) {

                                if(!incomp){

                                    if(switchID == dualWields[currentItem].incompIDs[i]){

                                        incomp = true;
                                        incompInt = currentItem;

                                        //Debug.Log("Incompatible");

                                    }//switchID = incompIDs[i]

                                }//!incomp

                            }//for i incompIDs

                        }//currentItem > -1

                        if(secondItem > -1){

                            for(int i = 0; i < dualWields[secondItem].incompIDs.Count; i++) {

                                if(!incomp){

                                    if(switchID == dualWields[secondItem].incompIDs[i]){

                                        incomp = true;
                                        incompInt = secondItem;

                                        //Debug.Log("Incompatible");

                                    }//switchID = incompIDs[i]

                                }//!incomp

                            }//for i incompIDs

                        }//currentItem > -1

                        if(!incomp){

                            if(currentItem > -1){

                                secondItem = currentItem;

                                //Debug.Log("SelectSwitcher second Item ");

                            }//currentItem > -1

                        }//!incomp

                    //dualWield
                    } else {

                        if(secondItem > 0){

                            DeselectItem(secondItem);

                        }//secondItem > 0

                    }//dualWield
                
                }//dualWields.Count > 0
                newItem = switchID;

                if (IsItemsDeactivated())
                {
                    if (ItemList[newItem].GetComponent<SwitcherBehaviour>() != null)
                    {
                        ItemList[newItem].GetComponent<SwitcherBehaviour>().OnSwitcherSelect();
                        currentItem = newItem;
                    }
                    else
                    {
                        Debug.LogError("[Item Switcher] Object does not contains SwitcherBehaviour subcalss!");
                    }
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(SwitchItem());
                }
            }
            else
            {
                DeselectItems();
            }
        }

        public void DeselectItems()
        {
            if (currentItem == -1) return;

            ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();
            StopAllCoroutines();
            StartCoroutine(DeselectWait());
        }

        public void DeselectItem(int item)
        {
            //if (currentItem == -1) return;

            ItemList[item].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();
            StopAllCoroutines();
            StartCoroutine(DeselectWait());
        }


        public void DeselectItems_Forced() {
        
            if (currentItem == -1) return;

            #if (COMPONENTS_PRESENT || HFPS_DURABILITY_PRESENT)

                ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect_Forced();
            
            #endif
            
            StopAllCoroutines();
            StartCoroutine(DeselectWait());
        
        }//DeselectItems_Forced

        IEnumerator DeselectWait()
        {
        
            if(currentItem > 0 && secondItem < 0){
            
                yield return new WaitUntil(() => IsItemsDeactivated());
            
            //currentItem > 0 & secondItem < 0
            } else {
            
                if(secondItem > -1){
            
                    yield return new WaitUntil(() => IsItemDeactivated(secondItem));
                    
                    //Debug.Log("Deselect second item " + secondItem);
                
                }//secondItem > -1
                
            }//currentItem > 0 & secondItem < 0
            
            if(secondItem < 0){
            
                currentItem = -1;
            
            //secondItem < 0
            } else {
            
                currentItem = secondItem;
                secondItem = -1;
                
                //Debug.Log("Deselect second Item ");
            
            }//secondItem < 0
            
        }//DeselectWait

        public void DisableItems()
        {
            if (currentItem == -1) return;

            ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeactivate();

            if(secondItem < 0){
            
                currentItem = -1;
            
            //secondItem < 0
            } else {
            
                currentItem = secondItem;
                secondItem = -1;
                
                //Debug.Log("Disable second Item ");
            
            }//secondItem < 0
            
        }//DisableItems

        public int GetIDByObject(GameObject switcherObject)
        {
            return ItemList.IndexOf(switcherObject);
        }

        public GameObject GetCurrentItem()
        {
            if (currentItem != -1)
            {
                return ItemList[currentItem];
            }

            return null;
        }

        public bool IsBusy()
        {
            return transform.root.gameObject.GetComponentInChildren<ExamineManager>().isExamining || transform.root.gameObject.GetComponentInChildren<DragRigidbody>().CheckHold();
        }

        bool IsItemsDeactivated()
        {
            return ItemList.All(x => !x.transform.GetChild(0).gameObject.activeSelf);
        }
        
        bool IsItemDeactivated(int slot)
        {
            return ItemList[slot].transform.GetChild(0).gameObject.activeSelf;
        }

        IEnumerator SwitchItem()
        {
            if (currentItem > -1 && newItem > -1)
            {
            
                if(dualWields.Count > 0){
                
                    if(!dualWields[newItem].dualWield){

                        ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();

                        if(secondItem > -1){

                            ItemList[secondItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();

                        }//secondItem > -1

                        yield return new WaitUntil(() => !ItemList[currentItem].transform.GetChild(0).gameObject.activeSelf);

                        if(secondItem > -1){

                            secondItem = -1;

                        }//secondItem > -1

                    //!dualWield
                    } else {

                        if(incomp){

                            if(secondItem > -1){

                                secondItem = currentItem;

                            }//secondItem > -1

                            if(incompInt > -1){

                                ItemList[incompInt].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();

                                yield return new WaitUntil(() => !ItemList[incompInt].transform.GetChild(0).gameObject.activeSelf);

                            //incompInt > -1
                            } else {

                                ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();

                                yield return new WaitUntil(() => !ItemList[currentItem].transform.GetChild(0).gameObject.activeSelf);

                            }//incompInt > -1

                        }//incomp

                    }//!dualWield
                
                //dualWields.Count > 0
                } else {
                
                    ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDeselect();

                    yield return new WaitUntil(() => !ItemList[currentItem].transform.GetChild(0).gameObject.activeSelf);
                
                }//dualWields.Count > 0

                if (ItemList[newItem].GetComponent<SwitcherBehaviour>() != null)
                {
                    ItemList[newItem].GetComponent<SwitcherBehaviour>().OnSwitcherSelect();
                    currentItem = newItem;
                    
                    //Debug.Log("SwitchItem current Item ");
                }
                else
                {
                    Debug.LogError("[Item Switcher] Object does not contains SwitcherBehaviour subcalss!");
                }

                yield return new WaitForSeconds(1f);
            }
        }

        void Update()
        {
            if (WallHitTransform && detectWall && !handsFreed && currentItem != -1)
            {
                if (OnWallHit())
                {
                    if (ItemList[currentItem].GetComponent<SwitcherBehaviour>() != null)
                    {
                        ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherWallHit(true);
                    }

                    hideWeapon = true;
                }
                else
                {
                    if (ItemList[currentItem].GetComponent<SwitcherBehaviour>() != null)
                    {
                        ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherWallHit(false);
                    }

                    hideWeapon = false;
                }
            }

            if (WallHitTransform)
            {
                if (!hideWeapon)
                {
                    WallHitTransform.localPosition = Vector3.MoveTowards(WallHitTransform.localPosition, WallHitShowWeapon, Time.deltaTime * WallHitSpeed);
                }
                else
                {
                    WallHitTransform.localPosition = Vector3.MoveTowards(WallHitTransform.localPosition, WallHitHideWeapon, Time.deltaTime * WallHitSpeed);
                }
            }

            if (!scriptManager.ScriptGlobalState) return;

            if (!gameManager.isGrabbed)
            {
                if (!antiSpam)
                {
                    Vector2 scroll = InputHandler.ReadInput<Vector2>("Scroll", "PlayerExtra");

                    //Mouse ScrollWheel Backward - Deselect Current Item
                    if (scroll.y < 0f)
                    {
                        if (currentItem != -1)
                        {
                            DeselectItems();
                        }
                    }

                    //Mouse ScrollWheel Forward - Select Last Weapon Item
                    if (scroll.y > 0f)
                    {
                        if (weaponItem != -1)
                        {
                            MouseWHSelectWeapon();
                        }
                    }
                }
                else
                {
                    if (!spam)
                    {
                        StartCoroutine(AntiSwitchSpam());
                    }
                }
            }
            else
            {
                antiSpam = true;
            }

            if (currentItem != -1)
            {
                ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherDisable(gameManager.isGrabbed);
            }
        }

        void MouseWHSelectWeapon()
        {
            if (currentItem != weaponItem)
            {
                if (ItemList[weaponItem].GetComponent<WeaponController>() && inventory.CheckSwitcherItemInventory(weaponItem))
                {
                    SelectSwitcherItem(weaponItem);
                }
            }
        }

        IEnumerator AntiSwitchSpam()
        {
            spam = true;
            antiSpam = true;
            yield return new WaitForSeconds(1f);
            antiSpam = false;
            spam = false;
        }

        /// <summary>
        /// Activate Item without playing animation.
        /// </summary>
        public void ActivateItem(int switchID)
        {
            ItemList[switchID].GetComponent<SwitcherBehaviour>().OnSwitcherActivate();
            currentItem = switchID;
            newItem = switchID;
        }

        public void FreeHands(bool free)
        {
            if (currentItem != -1)
            {
                if (free && !handsFreed)
                {
                    if (ItemList[currentItem].GetComponent<SwitcherBehaviour>() != null)
                    {
                        ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherWallHit(true);
                    }

                    hideWeapon = true;
                    handsFreed = true;
                }
                else if (!free && handsFreed)
                {
                    if (ItemList[currentItem].GetComponent<SwitcherBehaviour>() != null)
                    {
                        ItemList[currentItem].GetComponent<SwitcherBehaviour>().OnSwitcherWallHit(false);
                    }

                    hideWeapon = false;
                    handsFreed = false;
                }
            }
        }

        private bool OnWallHit()
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hit, wallHitRange, HitMask))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (detectWall && showGizmos)
            {
                Camera cam;

                if ((cam = Utilities.MainPlayerCamera()) != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward * wallHitRange));
                }
            }
        }
    }
}