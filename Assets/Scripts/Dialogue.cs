using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private static Dialogue _instance;

    private bool inDialogue;

    public static Dialogue Instance {  get { return _instance; } }

    public GameObject DialogueContainerBox;
    public GameObject DialogueImage;

    private GameObject _dialogueContainerBox;
    private TextMeshProUGUI _dialogueTMP;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {

        inDialogue = false;

        _dialogueContainerBox = DialogueContainerBox;
        _dialogueTMP = DialogueImage.GetComponent<TextMeshProUGUI>();

        _dialogueContainerBox.SetActive(false);
    }

    public void PushNewDialogue(string reaction, float liveTime)
    {
        if (inDialogue) { StopCoroutine(RunDialogue(reaction ,liveTime));}

        StartCoroutine(RunDialogue(reaction, liveTime));
    }

    private IEnumerator RunDialogue(string reaction, float liveTime) 
    {
        inDialogue = true;
        
        _dialogueTMP.text = reaction;


        _dialogueContainerBox.SetActive(true);
        yield return new WaitForSeconds(liveTime * Time.time);
        _dialogueContainerBox.SetActive(false);

        inDialogue = false;
    }
}
