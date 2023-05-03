using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{

    virtual protected void OnClicked() { }
    virtual protected void OnSelected() { }
    public void Click()
    {
        OnClicked();
    }
    public void Selected()
    {
        OnSelected();
    }
}
