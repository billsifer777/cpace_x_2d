using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject obj;
    public Vector2 vector;
    // Start is called before the first frame update
    void Start()
    {
        obj.transform.position=vector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
