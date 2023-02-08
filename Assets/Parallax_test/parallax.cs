using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{

    public float parallax_effect;
    public float scrollSpeed;
    public GameObject cam;
    float length, startpos;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<MeshRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallax_effect));
        float dist = (cam.transform.position.x * parallax_effect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) {
            startpos += length;
        }
        else if (temp < startpos - length) {
            startpos -= length;
        }
    }
}
