using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
    None = 0,

    Left = 1,
    //LeftAndUp = 5,
    //LeftAndDown = 9,

    Right = 2,
    //RightAndUp = 6,
    //RightAndDown = 10,

    Up = 4,
    Down = 8,

}

public class SwipeManager : MonoBehaviour
{
    private static SwipeManager instance;
    public static SwipeManager Instance { get { return instance; } }
    public bool SwipeOn { get; private set; }

    /// <summary>
    /// return StartSwipe - EndSwipe
    /// </summary>
    public Vector3 DeltaSwipe { get; private set; }
    public SwipeDirection Direction { set; get; }

    private Vector3 startTouch;
    private Vector3 endTouch;
    private float swipeResistanceX = 50.0f;
    private float swipeResistanceY = 100.0f;

    public bool IsSwiping(SwipeDirection dir)
    {
        return (Direction & dir) == dir;
    }

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        Direction = SwipeDirection.None;
        SwipeOn = false;
        DeltaSwipe = Vector3.zero;
        //startTouch = Vector3.zero;
        //endTouch = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTouch = Input.mousePosition;
            DeltaSwipe = startTouch - endTouch;
            //Debug.Log("Start x = " + startTouch.x + "\tStart y = " + startTouch.y);
            //Debug.Log("End x = " + endTouch.x + "\tEnd y = " + endTouch.y);

            if (Mathf.Abs(DeltaSwipe.x) > swipeResistanceX)
            {
                //Свайп по X
                Direction |= (DeltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
                SwipeOn = true;
            }
            if (Mathf.Abs(DeltaSwipe.y) > swipeResistanceY)
            {
                //Свайп по Y
                Direction |= (DeltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
                SwipeOn = true;
            }


        }

    }
    public Vector3 GetVector3Position(float vectorY)
    {
        //Debug.Log("Start x = " + startTouch.x + "\tStart y = " + startTouch.y);
        //Debug.Log("End x = " + endTouch.x + "\tEnd y = " + endTouch.y);
        var res = new Vector3(endTouch.x - startTouch.x , vectorY, endTouch.y - startTouch.y);
        //Debug.Log("Target vector = " + res.ToString());
        return res;
    }

}
