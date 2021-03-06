using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateFood: MonoBehaviour
{
    public GameObject food;
    private GameObject generate;
    private float cont;
    private bool full;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = (GameObject) Instantiate(food, transform.position + new Vector3(-0.05f, 0.4f, 0.0f), food.transform.rotation);
        obj.transform.parent = transform;
        obj.GetComponent<pickUpFood>().enabled = false;
        generate = obj;
        full = true;
    }

    // Update is called once per frame
    void Update()
    {
        cont += Time.deltaTime; 
        if (!full && cont >= 5)
        {
            GameObject obj = (GameObject)Instantiate(food, transform.position + new Vector3(-0.05f, 0.4f, 0.0f), food.transform.rotation);
            obj.transform.parent = transform;
            obj.GetComponent<pickUpFood>().enabled = false;
            generate = obj;
            cont = 0;
            full = true;
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space) && full && cont >= 0.5 && !obj.gameObject.GetComponent<MoveCharacter>().checkHold())
            {
                generate.transform.position = obj.transform.Find("ToPickUp").position;
                generate.transform.eulerAngles = obj.transform.Find("ToPickUp").eulerAngles;
                generate.transform.parent = obj.transform.Find("ToPickUp").transform;
                generate.GetComponent<pickUpFood>().enabled = true;
                generate.GetComponent<Rigidbody>().useGravity = false;
                generate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                obj.gameObject.GetComponent<MoveCharacter>().changeHold(true);
                obj.gameObject.GetComponent<MoveCharacter>().holdFood(generate);
                generate.gameObject.GetComponent<pickUpFood>().setPlayer(obj.gameObject);
                full = false;
                cont = 0;
                generate = null;
            }
        }
    }
}