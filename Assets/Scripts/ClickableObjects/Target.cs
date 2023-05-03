using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : ClickableObject
{
    [SerializeField] private MeshRenderer mesh;

    private void Start()
    {
        mesh.material.color = Color.blue;
    }
    protected override void OnClicked()
    {
        mesh.material.color = Color.red;
        Destroy(this.gameObject, 1f);
    }
}
