using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    private Text text;
    public bool isCoroutine = false;

    public void Startco()
    {
        StartCoroutine(ChangeText());
    }
    public IEnumerator ChangeText()
    {
        TryGetComponent(out text);
        isCoroutine = true;
        int i = 1;
        while (isCoroutine)
        {
            if(i > 3)
            {
                i = 1;
            }
            text.text = "Loading" + new string('.', i);
            yield return new WaitForSeconds(0.5f);
            yield return null;
            i++;
        }
        isCoroutine = false;
    }
}
