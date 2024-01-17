using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMain : MonoBehaviour
{
    private Button btn;
    private void Start()
    {
        TryGetComponent(out btn);
        
    }
    public void GoMain()
    {
        SceneLoadManager.instance.BackToMainBtn(gameObject.scene);
    }
}
