using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    bool isRewinding = false;
    bool rewindKeyPressed = false;
    bool hasJustStoppedRewinding = false;
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

    private void OnEnable() {
        RewindButton.RewindEvent += HandleRewindTime;
    }

    private void OnDisable() {
        RewindButton.RewindEvent -= HandleRewindTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (rewindKeyPressed && GameManager.canRewind)
            StartRewind();
        if(hasJustStoppedRewinding && isRewinding) {
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
        GameManager.SetRewindButtonStatus(false);
        GameManager.isRewinding = false;

        if(!isStopRewindCalled) {
            EnemyShooting.pauseShoot = false;
            ScoreManager.Instance.OnBatteryDischarged();
            //rb.isKinematic = false;
            PostProcessingManager.Instance.ResetVignette();
            isStopRewindCalled = true;
        }
        

    }

    private void HandleRewindTime() {
        rewindKeyPressed = !rewindKeyPressed;
        if(!rewindKeyPressed) {
            hasJustStoppedRewinding = true;
            StartCoroutine(ResetRewindingBool());
        }
    }

    private IEnumerator ResetRewindingBool() {
        yield return new WaitForSeconds(0.1f);
        hasJustStoppedRewinding = false;
    }
}
