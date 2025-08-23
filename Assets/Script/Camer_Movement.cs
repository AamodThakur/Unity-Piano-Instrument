using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer_Movement : MonoBehaviour
{
    public float cam_speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += Vector3.up * Time.deltaTime * cam_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (this.transform.position.y <= 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            }
            else
            {
                this.transform.position += Vector3.down * Time.deltaTime * cam_speed;
            }
        }
    }
}
