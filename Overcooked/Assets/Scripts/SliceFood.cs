using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceFood : MonoBehaviour
{
    public Transform Player;
    private GameObject food;
    private float cont;
    private bool full;

    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        full = false;
    }

    // Update is called once per frame
    void Update()
    {
        cont += Time.deltaTime;
        if (food != null && cont >= food.gameObject.GetComponent<convertInTo>().getCountNext() && full && food.tag == "Food")
        {
            GameObject a = food.gameObject.GetComponent<convertInTo>().convertFood();
            Destroy(food);
            food = a;
            food.transform.position = this.transform.GetChild(0).position;
            food.transform.parent = this.transform.GetChild(0).transform;
            food.gameObject.GetComponent<pickUpFood>().changeState();
            food.gameObject.GetComponent<pickUpFood>().resetCont();
            food.gameObject.GetComponent<pickUpFood>().enabled = false;
            food.GetComponent<Rigidbody>().useGravity = false;
            GameObject.FindWithTag("Player").gameObject.GetComponent<MoveCharacter>().enabled = true;
        }
        //Debug.Log(food);
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.tag == "Food")
        {
            if (Input.GetKey(KeyCode.Space) && !full && cont >= 0.5)
             {
                //obj.GetComponent<BoxCollider>().enabled = true;
                //obj.GetComponent<Rigidbody>().useGravity = true;
                obj.gameObject.GetComponent<pickUpFood>().emptyPlayer();
                GameObject.FindWithTag("Player").gameObject.GetComponent<MoveCharacter>().enabled = false;
                obj.transform.position = this.transform.GetChild(0).position;
                obj.transform.eulerAngles = this.transform.GetChild(0).eulerAngles;
                obj.transform.parent = this.transform.GetChild(0).transform;
                obj.gameObject.GetComponent<pickUpFood>().changeState();
                obj.gameObject.GetComponent<pickUpFood>().resetCont();
                obj.gameObject.GetComponent<pickUpFood>().enabled = false;
                food = obj.gameObject;
                cont = 0;
                full = true;
            }
        }
        else if (obj.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space) && full && cont >= 1 && !obj.gameObject.GetComponent<MoveCharacter>().checkHold())
            {
                food.transform.position = obj.transform.Find("ToPickUp").position;
                food.transform.eulerAngles = obj.transform.Find("ToPickUp").eulerAngles;
                food.transform.parent = obj.transform.Find("ToPickUp").transform;
                food.GetComponent<pickUpFood>().enabled = true;
                obj.gameObject.GetComponent<MoveCharacter>().changeHold(true);
                obj.gameObject.GetComponent<MoveCharacter>().holdFood(food);
                food.gameObject.GetComponent<pickUpFood>().setPlayer(obj.gameObject);
                food = null;
                full = false;
                cont = 0;
            }
        }
    }

    public void noFull()
    {
        full = false;
    }
}
