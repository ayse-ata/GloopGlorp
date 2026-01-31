using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    [Header("HealthBar")]
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    [Header("PlayerVisual")]
    public Sprite Stage1;
    public Sprite Stage2;
    public Sprite Stage3;
    public Sprite Stage4;
    public SpriteRenderer spriteRenderer;

    public PlayerHealth playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerHealth.currentHealth;
        maxHealth = playerHealth.maxHealth;
        UpdateVisuals();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
        void UpdateVisuals()
        {
            if (currentHealth == 3)
            {
                spriteRenderer.sprite = Stage1;
            }
            else if (currentHealth == 2)
            {
                spriteRenderer.sprite = Stage2;
            }
            else if (currentHealth == 1)
            {
                spriteRenderer.sprite = Stage3;
            }
            else if (currentHealth == 0)
            {
                spriteRenderer.sprite = Stage4;
            }
        }
    }
}
