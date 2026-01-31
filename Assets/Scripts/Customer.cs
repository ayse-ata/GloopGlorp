using UnityEngine;

[CreateAssetMenu]
public class Customer : ScriptableObject
{
    public Sprite customerSprite;
    public string customerName;
    public int rounds;
    [SerializeField]
    public Difficulty difficulty;
 
    [System.Serializable]
    public enum Difficulty
    {
        Super_Easy=500,
        Easy=300,
        Medium=200,
        Hard=100,
        Super_Hard=50
    }
}
