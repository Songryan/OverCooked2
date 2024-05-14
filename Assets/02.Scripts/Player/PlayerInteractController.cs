using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PlayerInteractController : MonoBehaviour
{
    // 상태 변화 패턴
    private IPlayerState currentState;
    private FreeState freeState;
    private HoldState holdState;

    // 애니메이션
    public Animator anim;

    // 상호작용 할 수 있는 오브젝트
    public GameObject interactObject;
    public ObjectHighlight objectHighlight;
    public GameObject nextInteractObject;

    // 변경되는 Bool값
    [SerializeField] public bool isHolding = false;
    // 재료를 제외한 모든 오브젝트 활성화 확인값
    [SerializeField] public bool canActive = false; 

    [Header("Grab Object Control")]
    [SerializeField] private GameObject idleR;
    [SerializeField] private GameObject idleL;
    [SerializeField] private GameObject grabR;
    [SerializeField] private GameObject grabL;
    [SerializeField] private GameObject knife;


    private void Awake()
    {
        freeState = new FreeState(this);
        holdState = new HoldState(this);
        currentState = freeState;  // 초기 상태 설정
    }
    
    #region 인터렉션 부분

    #endregion
    

    #region CatchOrKnockback,CookOrThrow, PickupOrPlace
    public void CatchOrKnockback()
    {
    }

    public void CookOrThrow()
    {
    }
    
    public void PickupOrPlace()
    {
        
    }
    #endregion

    #region OnCookOrThrow
    public void OnCookOrThrow(InputValue inputValue)
    {
        Debug.Log("OnCookOrThrow");
        if (ShouldStartCutting())
        {
            StartCuttingProcess();
        }
    }

    bool ShouldStartCutting()
    {
        return objectHighlight.objectType == ObjectHighlight.ObjectType.Board &&
               interactObject.transform.parent.childCount > 2 &&
               !interactObject.transform.parent.GetChild(2).GetChild(0).GetChild(0).GetComponent<Ingredient>().isCooked &&
               !isHolding;
    }

    void StartCuttingProcess()
    {
        var cuttingBoard = interactObject.transform.GetChild(0).GetComponent<CuttingBoard>();

        if (cuttingBoard._CoTimer == null) // 한번도 실행 안된거면 시작 가능
        {
            anim.SetTrigger("startCut");
            cuttingBoard.Pause = false;
            cuttingBoard.CuttingTime = 0;
            cuttingBoard.StartCutting1();
        }
        else if (cuttingBoard.Pause) // 실행되다 만거라면
        {
            anim.SetTrigger("startCut");
            cuttingBoard.PauseSlider(false);
        }
    }
    #endregion

    #region OnPickupOrPlace
    public void OnPickupOrPlace(InputValue inputValue)
    {
        ProcessInteraction();
        SetHand();
    }

    private void ProcessInteraction()
    {
        if (interactObject == null && !isHolding) return;

        if(isHolding && objectHighlight == null)
        {
            // 뭔가 들고있고 앞에 상호작용 객체가 없을때
            HoldingItemDropObject();
            return;
        }

        Debug.Log($"objectHighlight.objectType : {objectHighlight.objectType}");
        switch (objectHighlight.objectType)
        {
            case ObjectHighlight.ObjectType.CounterTop:
            case ObjectHighlight.ObjectType.Board:
                HandleCounterTopOrBoardInteraction();
                break;
            case ObjectHighlight.ObjectType.Craft:
                HandleCraftInteraction();
                break;
            case ObjectHighlight.ObjectType.Bin:
                HandleBinInteraction();
                break;
            default:
                HandleGeneralObjectInteraction();
                break;
        }
    }
    // CounterTop, Board
    private void HandleCounterTopOrBoardInteraction()
    {
        
        if (canActive && isHolding && !objectHighlight.onSomething)
        {
            // 내가 뭘 들고있고, 테이블이나 찹핑테이블 위에 없을떄
            TablePlaceOrDropObject(false);
        }
        else if (canActive && isHolding && objectHighlight.onSomething)
        {
            // 내가 뭘 들고있고, 테이블이나 찹핑테이블 위에 있을때
            Debug.Log("뭔가 있냐?");
            TablePlaceOrDropObject(true);
        }
        else if (canActive && interactObject.GetComponent<ObjectHighlight>().onSomething)
        {
            GameObject handleThing = interactObject.transform.parent.GetChild(2).gameObject;

            if (handleThing.CompareTag("Ingredient") && objectHighlight.objectType == ObjectHighlight.ObjectType.Board &&
                interactObject.transform.GetChild(0).GetComponent<CuttingBoard>().cookingBar.IsActive())
            {
                // 손질 중인 재료는 집을 수 없음
            }
            else
            {
                Debug.Log($"handleThing.name : {handleThing.name}");
                TryPickupObject(handleThing);
            }
        }
    }
    // Craft
    private void HandleCraftInteraction()
    {
        Debug.Log($"objectHighlight.onSomething : {objectHighlight.onSomething}");
        // Craft 위에 뭔가 없을때
        if (!objectHighlight.onSomething)
        {
            if (!isHolding)
            {
                PickupFromCraft();
            }
            else
            {
                //PlaceOrDropObject(false);
            }
        }
        else if (!isHolding) // 테이블 위에 뭔가 있는데, 손에 든게 아무것도 없을때
        {
            PickupFromCraft();
        }
    }
    // Bin
    private void HandleBinInteraction()
    {
        if (isHolding && transform.GetChild(1).GetComponent<Ingredient>()?.type == Ingredient.IngredientType.Plate)
        {
            DisposePlate();
        }
        else if (transform.childCount > 1)
        {
            DisposeObject();
        }
    }

    private void HandleGeneralObjectInteraction()
    {
        if (!isHolding && interactObject.CompareTag("Ingredient"))
        {
            PickupIngredient();
        }
        else if (!isHolding && interactObject.CompareTag("Plate")) 
        {
            PickupPlate();
        }
        //else if (isHolding)
        //{
        //
        //}
    }

    void HoldingItemDropObject()
    {
        // 떨어트리기 로직 상세 구현 필요
        GameObject handlingThing = transform.GetChild(1).gameObject;
        // 객체를 내려놓을 때의 로직
        Debug.Log($"handlingThing name : {handlingThing.name}");

        if (handlingThing.name.Equals("Plate"))
        {
            // 떨구는 객체가 접시면.
            //Debug.Log("접시 내려");
            handlingThing.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else
        {
            handlingThing.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            handlingThing.transform.GetChild(0).GetComponent<MeshCollider>().isTrigger = false;
        }

        handlingThing.transform.SetParent(null); // 부모 설정 해제
        anim.SetBool("isHolding", false);
        isHolding = false;
    }

    private void TryPickupObject(GameObject handleThing)
    {
        //SoundManager.instance.PlayEffect("take");
        objectHighlight.onSomething = false;
        //isHolding = true;
        //anim.SetBool("isHolding", isHolding);
        HandleObject(handleThing);
    }

    // 내가 뭔가를 들고있을때
    // true 테이블 위에 뭔가 있음 , false 테이블 위에 뭔가 없음
    private void TablePlaceOrDropObject(bool drop)
    {
        //SoundManager.instance.PlayEffect(drop ? "put" : "place");
        if (drop)
        {
            // true 테이블 위에 뭔가 있는데 내가 가진게 접시고, 음식이면 담음
            DropObject();
        }
        else
        {
            // false 테이블 위에 뭔가 없음 => 놓기.
            GameObject handlingThing = transform.GetChild(1).gameObject;
            Debug.Log($"handlingThing.name : {handlingThing.name}");
            HandleObject(handlingThing, false);
        }
    }

    private void HandleObject(GameObject obj, bool isPickingUp = true)
    {
        if (isPickingUp)
        {
            // 객체를 들어 올릴 때의 로직
            Debug.Log("올려");
            if (obj.name.Equals("Plate"))
            {
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                obj.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                obj.transform.GetChild(0).GetChild(0).GetComponent<Ingredient>().HandleIngredient(transform, obj.transform.GetChild(0).GetChild(0).GetComponent<Ingredient>().type, true);
            }
            obj.transform.SetParent(transform); // 플레이어의 하위 객체로 설정
            // 플레이어에서 위치 잡기
            SetPositionbetweenPlayerandObject(obj);
            anim.SetBool("isHolding", true);
            isHolding = true;
        }
        else
        {
            // 객체를 내려놓을 때의 로직
            //Debug.Log("내려");
            //SoundManager.instance.PlayEffect("put");
            //obj.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //obj.transform.SetParent(null); // 부모 설정 해제
            //anim.SetBool("isHolding", false);
            //isHolding = false;
            GameObject handleThing = transform.GetChild(1).gameObject;
            if (handleThing.CompareTag("Ingredient"))
            {
                objectHighlight.onSomething = true;
                isHolding = false;
                handleThing.transform.GetChild(0).transform.GetChild(0).GetComponent<Ingredient>().isOnDesk = true;
                handleThing.transform.GetChild(0).transform.GetChild(0).GetComponent<Ingredient>().
                    IngredientHandleOff(
                    interactObject.transform.parent,
                    interactObject.transform.parent.GetChild(1).localPosition, 
                    handleThing.transform.GetChild(0).GetChild(0).GetComponent<Ingredient>().type);
            }
            else //접시
            {
                if (objectHighlight.objectType != ObjectHighlight.ObjectType.Board)
                {
                    objectHighlight.onSomething = true;
                    isHolding = false;
                    handleThing.GetComponent<Ingredient>().isOnDesk = true;
                    handleThing.GetComponent<Ingredient>().
                        PlayerHandleOff(interactObject.transform.parent,
                        interactObject.transform.parent.GetChild(1).localPosition);
                }

            }
            anim.SetBool("isHolding", isHolding);
        }
    }

    private void PickupFromCraft()
    {
        //SoundManager.instance.PlayEffect("take");
        // Craft에서 아이템 꺼내기 로직 구현
        interactObject.GetComponent<Craft>().OpenCraftPlayer1();
        objectHighlight.onSomething = false;
        isHolding = true;
        anim.SetBool("isHolding", isHolding);
    }

    private void DisposePlate()
    {
        transform.GetChild(1).gameObject.GetComponent<Plates>().ClearIngredient();
    }

    private void DisposeObject()
    {
        Destroy(transform.GetChild(1).gameObject);
        isHolding = false;
        anim.SetBool("isHolding", isHolding);
    }

    private void PickupIngredient()
    {
        //SoundManager.instance.PlayEffect("take");
        isHolding = true;
        anim.SetBool("isHolding", isHolding);
        // 재료 줍기 로직 상세 구현 필요
        GameObject ingredientObj = interactObject.transform.parent.gameObject;
        ingredientObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ingredientObj.transform.GetChild(0).GetComponent<Ingredient>().HandleIngredient(transform, ingredientObj.transform.GetChild(0).GetComponent<Ingredient>().type, true);
    }

    private void PickupPlate() 
    {
        //isHolding = true;
        //anim.SetBool("isHolding", isHolding);
        // 접시인식.
        GameObject plateObject = interactObject.transform.parent.gameObject;
        TryPickupObject(plateObject);
        //plateObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //SetPositionbetweenPlayerandObject(plateObject);
    }

    private void DropObject()
    {
        
    }

    #endregion

    public void ChangeState(IPlayerState newState)
    {
        currentState = newState;
    }

    #region OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("deadZone"))
        //{
        //    HandleDeadZone();
        //    return;
        //}
        
        if (CheckForIngredientHandling(other))
            return;

        HandleActiveObjectInteraction(other);
    }

    private void HandleDeadZone()
    {
        // 사운드 메니져
        //SoundManager.instance.PlayEffect("fall");
        //DieRespawn();
    }
    
    private bool CheckForIngredientHandling(Collider other)
    {
        // 손에 잡고있고, interactObject가 Null이 아니고, 그 태그가 Ingredient일때 True
        // 손에 없거나, interactObject가 Null이거나, 그 태그가 Ingredient가 아니면 False -> HandlePickupIngredient
        if (interactObject != null && interactObject.CompareTag("Ingredient") && isHolding)
        {
            DeactivateObjectHighlight();
            ResetinteractObjects();
            return true;
        }

        return HandlePickupIngredient(other);
    }

    private void ResetinteractObjects()
    {
        interactObject = null;
        objectHighlight = null;
    }

    private void DeactivateObjectHighlight()
    {
        interactObject.GetComponent<ObjectHighlight>().DeactivateHighlight(interactObject.GetComponent<Ingredient>().isCooked);
    }

    private bool HandlePickupIngredient(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            if (interactObject == null && !other.GetComponent<Ingredient>().isOnDesk)
            {
                SetinteractObject(other.gameObject);
                other.GetComponent<ObjectHighlight>().ActivateHighlight(other.GetComponent<Ingredient>().isCooked);
                return true;
            }
        }
        return false;
    }

    private void SetinteractObject(GameObject obj)
    {
        interactObject = obj;
        objectHighlight = obj.GetComponent<ObjectHighlight>();
    }

    private void HandleActiveObjectInteraction(Collider other)
    {
        if (other == interactObject || interactObject != null)
            return;

        if (other.GetComponent<ObjectHighlight>() == null)
            return;

        canActive = true;

        SetinteractObject(other.gameObject);
        other.GetComponent<ObjectHighlight>().activeObject = true;

        if (other.CompareTag("Ingredient"))
        {
            other.GetComponent<ObjectHighlight>().ActivateHighlight(other.GetComponent<Ingredient>().isCooked);
        }
        else
        {
            other.GetComponent<ObjectHighlight>().ActivateHighlight(other.GetComponent<ObjectHighlight>().onSomething);
        }

        if (interactObject.GetComponent<ObjectHighlight>().objectType == ObjectHighlight.ObjectType.Board)
        {
            anim.SetBool("canCut", true);
        }
    }
    #endregion

    #region OnTriggerStay
    private void OnTriggerStay(Collider other)
    {
        if (ShouldReturnEarly(other))
            return;

        if (HandleActiveIngredientSwitch(other))
            return;

        InitializeActiveObjectIfNull(other);
    }

    private bool ShouldReturnEarly(Collider other)
    {
        return (other.CompareTag("Ingredient") || other.CompareTag("Plate")) && isHolding;
    }

    private bool HandleActiveIngredientSwitch(Collider other)
    {
        if (interactObject != null && interactObject.CompareTag("Ingredient") && isHolding)
        {
            if (nextInteractObject != null)
            {
                SwitchActiveObject();
            }
            else
            {
                ClearActiveObjects();
            }
            return true;
        }
        return false;
    }

    private void SwitchActiveObject()
    {
        interactObject.GetComponent<ObjectHighlight>().DeactivateHighlight(interactObject.GetComponent<Ingredient>().isCooked);
        interactObject = nextInteractObject;
        objectHighlight = interactObject.GetComponent<ObjectHighlight>();
        interactObject.GetComponent<ObjectHighlight>().DeactivateHighlight(interactObject.GetComponent<Ingredient>().isCooked);
        nextInteractObject = null;
    }

    private void ClearActiveObjects()
    {
        interactObject.GetComponent<ObjectHighlight>().DeactivateHighlight(interactObject.GetComponent<Ingredient>().isCooked);
        ResetinteractObjects();
    }

    private void InitializeActiveObjectIfNull(Collider other)
    {
        if (interactObject == null)
        {
            canActive = true;
            if (other.GetComponent<ObjectHighlight>() != null)
            {
                SetinteractObject(other.gameObject);
                other.GetComponent<ObjectHighlight>().activeObject = true;
                HighlightBasedOnTag(other);
            }
        }
    }

    private void HighlightBasedOnTag(Collider other)
    {
        bool highlightState = other.CompareTag("Ingredient") ? other.GetComponent<Ingredient>().isCooked : true;
        other.GetComponent<ObjectHighlight>().ActivateHighlight(highlightState);
    }
    #endregion

    #region OnTriggerExit
    private void OnTriggerExit(Collider other)
    {
        if (ShouldDeactivateObjects())
        {
            HandleBoardInteraction();
            DeactivateObjects();
        }
        else if (ShouldSwitchToNextObject(other))
        {
            SwitchActiveToNextObject();
        }
        else
        {
            ClearNextObject();
        }
    }

    private bool ShouldDeactivateObjects()
    {
        return interactObject != null && nextInteractObject == null;
    }

    private bool ShouldSwitchToNextObject(Collider other)
    {
        return other.gameObject == interactObject;
    }

    private void HandleBoardInteraction()
    {
        if (interactObject.GetComponent<ObjectHighlight>().objectType == ObjectHighlight.ObjectType.Board)
        {
            anim.SetBool("canCut", false);
            interactObject.transform.GetChild(0).GetComponent<CuttingBoard>().PauseSlider(true);
        }
    }

    private void DeactivateObjects()
    {
        canActive = false;
        OffHighlightCurrentObject();
        interactObject = null;
        objectHighlight = null;
    }

    private void SwitchActiveToNextObject()
    {
        anim.SetBool("canCut", false);
        OffHighlightCurrentObject();
        interactObject = nextInteractObject;
        objectHighlight = interactObject?.GetComponent<ObjectHighlight>();
        nextInteractObject = null;
        OnHighlightCurrentObject();
    }

    private void ClearNextObject()
    {
        nextInteractObject = null;
    }

    private void OffHighlightCurrentObject()
    {
        if (interactObject != null && interactObject.GetComponent<ObjectHighlight>() != null)
        {
            bool highlightState = interactObject.CompareTag("Ingredient") ? interactObject.GetComponent<Ingredient>().isCooked : true;
            interactObject.GetComponent<ObjectHighlight>().DeactivateHighlight(highlightState);
        }
    }

    private void OnHighlightCurrentObject()
    {
        if (interactObject != null && interactObject.GetComponent<Object>() != null)
        {
            bool highlightState = interactObject.CompareTag("Ingredient") ? interactObject.GetComponent<Ingredient>().isCooked : true;
            interactObject.GetComponent<ObjectHighlight>().DeactivateHighlight(highlightState);
        }
    }
    #endregion

    private void SetHand()
    {
        if (isHolding) //뭘 집었다면 손 접기
        {
            knife.SetActive(false);
            idleL.SetActive(false);
            idleR.SetActive(false);
            grabL.SetActive(true);
            grabR.SetActive(true);
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("New_Chef@Chop")) //다지는 중이면
            {
                knife.SetActive(true);
                idleL.SetActive(false);
                idleR.SetActive(false);
                grabL.SetActive(true);
                grabR.SetActive(true);
            }
            else
            {
                knife.SetActive(false);
                idleL.SetActive(true);
                idleR.SetActive(true);
                grabL.SetActive(false);
                grabR.SetActive(false);
            }
        }
    }

    void SetPositionbetweenPlayerandObject(GameObject obj) 
    {
        string name = obj.name;

        Vector3 localPosition = Vector3.zero;
        Quaternion localRotation = Quaternion.identity;

        switch (name)
        {
            case "Plate":
                //obj.GetComponent<Ingredient>().HandleIngredient(obj.transform, obj.transform.GetComponent<Ingredient>().type, true);
                //Transform parentTransform = obj.transform.parent;
                //parentTransform.localPosition = localPosition;
                //parentTransform.localRotation = localRotation;
                //parentTransform.parent.SetParent(something);
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localPosition = new Vector3(-0.409999996f, 0.4700001f, 1.84000003f);
                break;
            default:
                break;
        }
    }
}
