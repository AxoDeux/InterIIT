using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    bool isRewinding = false;
    public static bool isRewindCalled = false;
    public static bool isStartRewindCalled = false;
    public static bool isStopRewindCalled = false;

    public float recordTime = 3f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.canRewind)
            StartRewind();
        if(Input.GetKeyUp(KeyCode.E)) {
            StopRewind();
            isRewindCalled = false;
            isStopRewindCalled = false;
            isStartRewindCalled = false;
        }

    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
            if(!isRewindCalled) {
                SoundManager.PlaySound(SoundManager.Sound.timeRewind);
                isRewindCalled = true;
                Debug.Log("Rewind called");
            }
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        EnemyShooting.pauseShoot = true;

        //rb.isKinematic = true;
        if(!isStartRewindCalled) {
            PostProcessingManager.Instance.TimeRewinding();
            isStartRewindCalled = true;
        }

    }

    public void StopRewind()
    {
        isRewinding = false;
        GameManager.canRewind = false;
        GameManager.isRewinding = false;

        if(!isStopRewindCalled) {
            EnemyShooting.pauseShoot = false;
            ScoreManager.Instance.OnBatteryDischarged();
            //rb.isKinematic = false;
            PostProcessingManager.Instance.ResetVignette();
            isStopRewindCalled = true;
        }
        

    }
}
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class TimeBody : MonoBehaviour
//{
//    //isRewinding = false;
//    List<TimeStamp> positions;

//    public float rewindTime = GameManager.rewindTime;
//    private void Start()
//    {
//        positions = new List<TimeStamp>();
//    }

//    private void FixedUpdate()
//    {
//        Debug.Log("Number in list " + positions.Count);
//        if (GameManager.isRewinding)
//            Rewind();
//        else
//            Record();
//    }
//    void Rewind()
//    {
//        if (positions.Count > 0)
//        {
//            transform.position = positions[0].position;
//            transform.rotation = positions[0].rotation;
//            positions.RemoveAt(0);

//        }
//        else
//        {
//            GameManager.StopRewind();
//        }
//    }
//    void Record()
//    {
//        if (positions.Count > Mathf.Round(GameManager.rewindTime * (1 / Time.fixedDeltaTime)))
//        {
//            positions.RemoveAt(positions.Count - 1);
//        }
//        //inserting positions from the top position of the enemies
//        positions.Insert(0, new TimeStamp(transform.position, transform.rotation));
//    }


//}
