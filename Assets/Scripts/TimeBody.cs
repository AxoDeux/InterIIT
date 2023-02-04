using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    //isRewinding = false;



    List<Vector3> positions;
    private void Start()
    {
        positions = new List<Vector3>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //GameManager.OnStartRewinding?.Invoke();
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
            //GameManager.OnStopRewinding?.Invoke();
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.isRewinding)
            Rewind();
        else
            Record();
    }
    void Rewind()
    {
        //if (positions[0] != null)
        transform.position = positions[0];
        if (positions.Count > 0)
        {
            positions.RemoveAt(0);
        }
        else StopRewind();
    }
    void Record()
    {
        //inserting positions from the top position of the enemies
        positions.Insert(0, transform.position);
    }

    public void StopRewind()
    {
        GameManager.isRewinding = false;
    }

    public void StartRewind()
    {
        GameManager.isRewinding = true;
    }
}
