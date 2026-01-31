using UnityEngine;

[CreateAssetMenu]
public class Customer : ScriptableObject
{
    public Sprite customerSprite;
    public Sprite customerSpriteAngry;
    public string customerName;
    public int rounds;
    [SerializeField]
    public Difficulty difficulty;
    public string drinkOrder;

 
    [System.Serializable]
    public enum Difficulty
    {
        Super_Easy=5,
        Easy=4,
        Medium=3,
        Hard=2,
        Super_Hard=1
    }
}
