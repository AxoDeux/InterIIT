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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.canRewind)
        {
            //GameManager.OnStartRewinding?.Invoke();

            StartRewind();


        }
        if (Input.GetKeyUp(KeyCode.E) && GameManager.isRewinding)
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
        StartCoroutine("SubtractTimeFromRewindTime");
        if (positions.Count > 0)
        {
            transform.position = positions[0].position;
            transform.rotation = positions[0].rotation;
            positions.RemoveAt(0);

        }
        else
        {
            StopRewind();
        }
        //if (positions.Count == 0) return;
    }
    IEnumerator SubtractTimeFromRewindTime()
    {
        yield return new WaitForSeconds(0.1f);
        rewindTime -= Time.deltaTime;
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


    public void StopRewind()
    {
        GameManager.isRewinding = false;
        GameManager.canRewind = false;
        rewindTime = GameManager.rewindTime;
        StopCoroutine("SubtractTimeFromRewindTime");
        ScoreManager.Instance.OnBatteryDischarged();
        //Stop
    }

    public void StartRewind()
    {
        GameManager.isRewinding = true;
    }

}
