using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad_planets : MonoBehaviour
{
    public control control;
    public GameObject gg;
    public float Rotate;
    private Rigidbody2D rb;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        rb=gg.GetComponent<Rigidbody2D>(); //кешируем физику гг
        tr=gg.GetComponent<Transform>();   //кешируем трансформ гг
        control=gg.GetComponent<control>(); 
    }

    // Update is called once per frame
    void Update()
{
    
        Vector2 DirectionPlanet1 = (transform.position-tr.position).normalized;  //вектор по направлению к планете
      
        if(control.fly)  //если гг в полете
            rb.AddForce(DirectionPlanet1*10); //придаем импульс гг
            control.acceleration=0;
        
        transform.Rotate(0,0,Rotate);
    }
}
