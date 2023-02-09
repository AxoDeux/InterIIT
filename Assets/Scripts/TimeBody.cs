using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeBody : MonoBehaviour
{
    //isRewinding = false;
    List<TimeStamp> positions;

    public float rewindTime = GameManager.rewindTime;
    private void Start()
    {
        positions = new List<TimeStamp>();
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
        if (positions.Count > 0)
        {
            transform.position = positions[0].position;
            transform.rotation = positions[0].rotation;
            positions.RemoveAt(0);

        }
        else
        {
            GameManager.StopRewind();
        }
    }
    void Record()
    {
        if (positions.Count > Mathf.Round(GameManager.rewindTime * (1 / Time.fixedDeltaTime)))
        {
            positions.RemoveAt(positions.Count - 1);
        }
        //inserting positions from the top position of the enemies
        positions.Insert(0, new TimeStamp(transform.position, transform.rotation));
    }


}
