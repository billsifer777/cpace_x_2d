using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //для перезагрузки сцены 
using UnityEngine.EventSystems;    //работа с тачами
public class control : MonoBehaviour, IBeginDragHandler
{
   // public control control;
    public Vector2 vector; //
    public float acceleration; // ускорение гг
    public Transform planet; //трансформ планеты на которой стоим
    public Transform planet_orb; //трансформ планеты на орбиту которой выходим
    private Rigidbody2D rb;  //физика гг
    public Transform tr; //его трансформ
    public bool fly;
    public bool vlet;
    public float min_dist=0;


    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>(); //кешируем физику гг
        tr=this.GetComponent<Transform>();   //кешируем трансформ ггs
        Debug.Log("start");
    //    control=gg.GetComponent<control>(); 
    }

    void FixedUpdate() //физические расчеты
    {
        if(this.rb.velocity.magnitude!=0)  //проверяем в полете ли гг
            fly=true; 
        else
            fly=false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //при клике
         {
             if(this.rb.velocity.magnitude==0)  // если стоим
                    {
                        rb.simulated=true;               //включаем физику
                        Vector2 vector = (tr.position-planet.position);  //вычисляем направление полета
                        rb.AddForce(vector*acceleration, ForceMode2D.Impulse); //придаем импульс гг

                         tr.SetParent(null);    //отпускаем от планеты гг
                        //  planet=null;
                    }
         }
    }
    private void OnCollisionEnter2D(Collision2D coll) //при соприкосновении с телом
    {
        if (coll.gameObject.CompareTag("planet")&(planet.name!=coll.gameObject.name)) //если тег=планета 
            {
                rb.velocity = new Vector2(0, 0); //задаем скорость по нулям
                fly=false;
                //rb.velocity = Vector2.zero;
                planet=coll.transform;   //переменная планет=планета с которой мы столкнулись
                tr.parent = coll.transform; //планета становится родителем гг
                Vector2 DirectionPlanet1 = (tr.position-planet.position).normalized;  //вектор по направлению к планете
                tr.up=DirectionPlanet1;  //выравниваем гг
                rb.simulated=false; //отключаем гг физику
                
            }       
        if(coll.gameObject.CompareTag("sun")&(planet.name!=coll.gameObject.name)) //если коснулимь солнца(не тригер)
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            
    }

    private void OnTriggerEnter2D(Collider2D coll)  //если косаемся тригера
            {
            //    if (coll.gameObject.CompareTag("die")) //тригер= смерть
           // {
          //      SceneManager.LoadScene(SceneManager.GetActiveScene().name); //перезагрузка сцены
          //  } 
         //    if (coll.gameObject.CompareTag("money")) //тригер= денижки
         //   {
         //     Destroy(coll.gameObject);
         //     Debug.Log("collision");
          //  } 
           
              

            }
    private void OnTriggerStay2D(Collider2D coll)  //находимся ли в поле воздейсвия солнца
        {
  {
  if(coll.gameObject.CompareTag("planet"))
                    planet_orb=coll.transform;   //переменная планет=планета с которой мы столкнулись
                   Vector2 DirectionPlanet3 = (planet_orb.position-tr.position).normalized;  //вектор по направлению к планете
                   Vector2 w=Vector3.Cross(DirectionPlanet3, transform.forward);
                   float dist = Vector2.Distance(planet_orb.position, tr.position); 
                   if(min_dist==0)
                   {
                       min_dist=dist;  //если дистанцмя 0 то присваиваем
                   }
                   if(dist<min_dist)  //ищем минимальню
                   {
                        min_dist=dist;
                   }
                   if(vlet==false)     
                   {
                      min_dist=3f;  
                   }

                    if(fly & dist<min_dist)  // если летим  
                    {
                        vlet=true;
                    }
                   //   rb.AddForce(transform.forward*10,ForceMode2D.Impulse);
                   /*     rb.velocity = Vector2.zero;
                        rb.AddForce(w*2f,ForceMode2D.Impulse);
                        //tr.position=new Vector2(tr.position.x-0.1f, tr.position.y);
                        tr.up=w;
                        Debug.Log(dist);
                        min_dist=5f;
                        vlet=true;
                     //придаем импульс гг  */
                }
        }
    
    
    public void  OnBeginDrag(PointerEventData eventData) //коснулись экрана
    {
        if(this.rb.velocity.magnitude==0)  // если стоим
            {
                fly=true;     //начали полет(оторвались от планеты)
                rb.simulated=true;  //включаем физику
                Vector2 vector = (tr.position-planet.position);  //вычисляем направление полета
                rb.AddForce(vector*acceleration, ForceMode2D.Impulse); //придаем импульс гг
                tr.SetParent(null);  //отпускаем от планеты гг   
            }

    }

}

