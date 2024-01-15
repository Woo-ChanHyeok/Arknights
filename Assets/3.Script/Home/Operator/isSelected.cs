using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isSelected : MonoBehaviour
{
    public Button Btn;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out Btn);
        Btn.onClick.AddListener(SelectedBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectedBtn()
    {
        if (SquadManager.Selected != gameObject)
            SquadManager.Selected = gameObject;
        else
            SquadManager.Selected = null;
    }
}
