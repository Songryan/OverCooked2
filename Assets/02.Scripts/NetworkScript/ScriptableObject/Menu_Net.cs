using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu Data Net", menuName = "Scriptable Object Net/Menu data Net", order = int.MaxValue)]

public class Menu_Net : ScriptableObject
{
    // 레벨 스시 3, 부리또 4, 피자 5로 해뒀습니다
    public int Level;
    public string MenuName;
    public List<Ingredient_Net.IngredientType> Ingredient;
    public float LimitTime;
    public Sprite MenuIcon;
    public List<Sprite> IngredientIcon;
    public int Price;
}