using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PotOnStove : MonoBehaviour
{
    public Slider cookingBar;
    public bool isOnStove = false;
    public bool inSomething = false;
    public float cookingTime;
    private Coroutine _coTimer;
    private bool pause = false;
    private bool stateIsCooked = false;
    public Animator potAnim;

    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject IngredientUI;
    [SerializeField] private Sprite[] Icons;
    [SerializeField] private GameObject pfxFire;

    private void Start()
    {
        //potAnim.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOnStove && inSomething && !stateIsCooked)
        {
            UpdateCookingBarPosition();
            UpdateCookingBarValue();
            UpdateisIngredientState();
        }

        if(stateIsCooked)
            cookingBar.gameObject.SetActive(false);
    }

    private void UpdateisIngredientState()
    {
        if (transform.GetChild(2).gameObject != null)
        {
            stateIsCooked = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Ingredient_Net>().isCooked;
        }
    }

    private void UpdateCookingBarPosition()
    {
        cookingBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)); // 적절한 위치 조정
    }

    private void UpdateCookingBarValue()
    {
        cookingBar.value = cookingTime;
    }

    public void StartCooking(UnityAction EndCallBack = null)
    {
        if (_coTimer == null)
        {
            cookingBar.gameObject.SetActive(true);
            ClearTime();
            _coTimer = StartCoroutine(CoStartCooking(EndCallBack));
        }
        else if (pause)
        {
            pause = false;
        }
    }

    private IEnumerator CoStartCooking(UnityAction EndCallBack = null)
    {
        pfxFire.SetActive(true);
        while (cookingTime <= 1)
        {
            while (pause)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.45f);
            cookingTime += 0.25f;
        }
        pfxFire.SetActive(false);
        EndCallBack?.Invoke();
        OffSlider();
        _coTimer = null;
        pause = false;
        cookingTime = 0;
    }

    private void ClearTime()
    {
        if (_coTimer != null)
        {
            StopCoroutine(_coTimer);
            _coTimer = null;
        }
        pause = false;
    }

    public void OffSlider()
    {
        cookingBar.value = 0f;
        cookingBar.gameObject.SetActive(false);
        UpdateIngredientState();
        InstantiateUI();
    }

    private void UpdateIngredientState()
    {
        if (transform.childCount < 3)
            return;
        Ingredient_Net Ingredient = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Ingredient_Net>();
        Ingredient.isCooked = true;

        //여기선 이제 쿡되야함
        //if (Ingredient.type == Ingredient.IngredientType.Meat || Ingredient.type == Ingredient.IngredientType.Chicken)
        //    Ingredient.isCooked = false;

        Ingredient.ChangeMesh(Ingredient.type);
    }

    public void InstantiateUI()
    {
        if (transform.childCount < 3)
            return;

        Ingredient_Net Ingredient = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Ingredient_Net>();
        GameObject madeUI = Instantiate(IngredientUI, Vector3.zero, Quaternion.identity, Canvas.transform);
        madeUI.transform.GetChild(0).gameObject.SetActive(true);
        Image image = madeUI.transform.GetChild(0).GetComponent<Image>();
        image.sprite = GetIcon(Ingredient.type);
        madeUI.GetComponent<IngredientUI_Net>().Target = Ingredient.transform;
    }

    private Sprite GetIcon(Ingredient_Net.IngredientType ingredientType)
    {
        switch (ingredientType)
        {
            case Ingredient_Net.IngredientType.Fish:
                return Icons[0];

            case Ingredient_Net.IngredientType.Shrimp:
                return Icons[1];

            case Ingredient_Net.IngredientType.Tomato:
                return Icons[2];

            case Ingredient_Net.IngredientType.Lettuce:
                return Icons[3];

            case Ingredient_Net.IngredientType.Cucumber:
                return Icons[4];

            case Ingredient_Net.IngredientType.Potato:
                return Icons[5];

            case Ingredient_Net.IngredientType.Chicken:
                return Icons[6];

            case Ingredient_Net.IngredientType.SeaWeed:
                return Icons[7];

            case Ingredient_Net.IngredientType.Tortilla:
                return Icons[8];

            case Ingredient_Net.IngredientType.Rice:
                return Icons[9];

            case Ingredient_Net.IngredientType.Pepperoni:
                return Icons[10];

            case Ingredient_Net.IngredientType.Meat:
                return Icons[11];

            case Ingredient_Net.IngredientType.Dough:
                return Icons[12];

            case Ingredient_Net.IngredientType.Cheese:
                return Icons[13];

            case Ingredient_Net.IngredientType.SushiRice:
                return Icons[9];

            case Ingredient_Net.IngredientType.SushiFish:
                return Icons[0];

            case Ingredient_Net.IngredientType.SushiCucumber:
                return Icons[4];

            case Ingredient_Net.IngredientType.PizzaTomato:
                return Icons[2];

            default:
                // 기본적으로 아무것도 하지 않음
                throw new ArgumentOutOfRangeException();
        }
    }
}
