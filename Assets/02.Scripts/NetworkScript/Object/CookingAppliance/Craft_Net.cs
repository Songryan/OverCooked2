using Mirror;
using UnityEngine;

public class Craft_Net : MonoBehaviour
{
    [SerializeField] private Animator CraftAnim;
    public enum FoodType { Fish, Shrimp, Lettuce, Cucumber, Tomato, Chicken, Potato, Meat, Tortilla, Rice, SeaWeed, Pepperoni, Cheese, Dough, PizzaTomato, SushiRice, SushiFish, SushiCucumber }
    public FoodType food;
    public GameObject foodPrefabs;

    public void OpenCraft()
    {
        CraftAnim.SetTrigger("Open");
        //GameObject newFood = Instantiate(foodPrefabs, Vector3.zero, Quaternion.identity);
        //player.isHolding = true;
        //newFood.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //newFood.transform.GetChild(0).transform.GetChild(0).GetComponent<Ingredient_Net>().HandleIngredient(player.transform, ConvertFoodTypeToHandleType(food), true);
    }

    public Ingredient_Net.IngredientType ConvertFoodTypeToHandleType(FoodType food)
    {
        switch (food)
        {
            case FoodType.Fish:
                return Ingredient_Net.IngredientType.Fish;
            case FoodType.Shrimp:
                return Ingredient_Net.IngredientType.Shrimp;
            case FoodType.Lettuce:
                return Ingredient_Net.IngredientType.Lettuce;
            case FoodType.Cucumber:
                return Ingredient_Net.IngredientType.Cucumber;
            case FoodType.Tomato:
                return Ingredient_Net.IngredientType.Tomato;
            case FoodType.Chicken:
                return Ingredient_Net.IngredientType.Chicken;
            case FoodType.Potato:
                return Ingredient_Net.IngredientType.Potato;
            case FoodType.Tortilla:
                return Ingredient_Net.IngredientType.Tortilla;
            case FoodType.SeaWeed:
                return Ingredient_Net.IngredientType.SeaWeed;
            case FoodType.Rice:
                return Ingredient_Net.IngredientType.Rice;
            case FoodType.Pepperoni:
                return Ingredient_Net.IngredientType.Pepperoni;
            case FoodType.Cheese:
                return Ingredient_Net.IngredientType.Cheese;
            case FoodType.Dough:
                return Ingredient_Net.IngredientType.Dough;
            case FoodType.Meat:
                return Ingredient_Net.IngredientType.Meat;
            case FoodType.PizzaTomato:
                return Ingredient_Net.IngredientType.PizzaTomato;
            case FoodType.SushiRice:
                return Ingredient_Net.IngredientType.SushiRice;
            case FoodType.SushiFish:
                return Ingredient_Net.IngredientType.SushiFish;
            case FoodType.SushiCucumber:
                return Ingredient_Net.IngredientType.SushiCucumber;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(food), "Invalid food type");
        }
    }
}
