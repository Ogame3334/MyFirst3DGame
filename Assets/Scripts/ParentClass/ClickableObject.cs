using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{

    virtual protected void OnClicked()
    {

    }
    public void Click()
    {
        OnClicked();
    }
}
