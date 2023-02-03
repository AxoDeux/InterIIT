using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    List<Vector3> positions;
    private void Start()
    {
        positions = new List<Vector3>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();
    }
    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }
    void Rewind()
    {
        transform.position = positions[0];
        if (positions.Count > 0) positions.RemoveAt(0);
        else StopRewind();
    }
    void Record()
    {
        positions.Insert(0, transform.position);
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    public void StartRewind()
    {
        isRewinding = true;
    }
}
