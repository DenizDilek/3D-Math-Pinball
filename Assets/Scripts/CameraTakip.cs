using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTakip : MonoBehaviour
{
    public GameObject top;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - top.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = top.transform.position + offset;
    }
}
