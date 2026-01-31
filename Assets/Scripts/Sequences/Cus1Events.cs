using System.Collections;
using UnityEngine;

public class Cus1Events : MonoBehaviour
{

    public GameObject fadeScreenIn;
    public GameObject customer1;
    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(1);
        fadeScreenIn.SetActive(false);
        yield return new WaitForSeconds(2);
        customer1.SetActive(true);
        //trigger customer drink order dialogue

    }
}
