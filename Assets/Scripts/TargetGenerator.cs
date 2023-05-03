using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startTarget;
    [SerializeField] private GameObject target;
    private float timeOut = 1f;
    private float timeElapsed = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (startTarget == null)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > timeOut)
            {
                Instantiate(target, new Vector3(Random.Range(-45f, 45f), Random.Range(1.5f, 6f), Random.Range(-49f, 49f)), Quaternion.identity);
                timeElapsed = 0;
            }
        }
    }
}
