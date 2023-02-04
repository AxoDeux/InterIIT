using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    //isRewinding = false;



    List<TimeStamp> positions;
    private void Start()
    {
        positions = new List<TimeStamp>();
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
        if (positions.Count == 0) return;
        transform.position = positions[0].position;
        transform.rotation = positions[0].rotation;
        positions.RemoveAt(0);
        //else StopRewind();
    }
    void Record()
    {
        //inserting positions from the top position of the enemies
        positions.Insert(0, new TimeStamp(transform.position, transform.rotation));
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
