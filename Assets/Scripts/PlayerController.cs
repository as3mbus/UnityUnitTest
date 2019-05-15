using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Mover activeMover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Input.GetButton("Queue"))
                activeMover.addMove(mouseWorldPos);
            else
                activeMover.OverrideTranslateObject(mouseWorldPos);
        }
    }
}
