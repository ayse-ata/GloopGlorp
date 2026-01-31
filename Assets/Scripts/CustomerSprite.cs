using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomerSprite : MonoBehaviour
{
    [SerializeField] Customer customerData;
    private Image _image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
    }
    
    public enum SpriteType { NORMAL, ANGRY };

    public void SetSprite(SpriteType spriteType)
    {
        switch (spriteType)
        {
            case CustomerSprite.SpriteType.NORMAL:
                _image.sprite = customerData.customerSprite;
                break;
            case CustomerSprite.SpriteType.ANGRY:
                _image.sprite = customerData.customerSpriteAngry;
                break;
            default:
                break;
        }
    }
    
}
