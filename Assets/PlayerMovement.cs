using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.up * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-1 * transform.up * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * transform.right * Time.deltaTime * speed);
        }



    }
}
