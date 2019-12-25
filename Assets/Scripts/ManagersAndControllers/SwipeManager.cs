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
    public bool Tap { get; private set; }
	
    /// <summary>
    /// return StartSwipe - EndSwipe
    /// </summary>
    public Vector3 DeltaSwipe { get; private set; }
    public SwipeDirection Direction { get; private set; }

    private Vector3 startTouch;
    private Vector3 endTouch;

    const float sensitivity = 32;
    private float swipeResistanceX = Screen.width / sensitivity;
    private float swipeResistanceY = Screen.height / sensitivity;


    private void Start()
    {
        instance = this;

    }

    private void Update()
    {
        Direction = SwipeDirection.None;
        SwipeOn = false;
        Tap = false;
        DeltaSwipe = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTouch = Input.mousePosition;
            DeltaSwipe = startTouch - endTouch;

            if (Mathf.Abs(DeltaSwipe.x) > swipeResistanceX)
            {
                SwipeOn = true;
                Direction |= (DeltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
            }
            if (Mathf.Abs(DeltaSwipe.y) > swipeResistanceY)
            {
                SwipeOn = true;
                Direction |= (DeltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
            }
            Tap = SwipeOn ? false : true;
        }

    }
    public bool IsSwiping(SwipeDirection dir) => (Direction & dir) == dir;

    public Vector3 GetRotationTargetPoint(float vectorY) //поменять название
    {
        return new Vector3(endTouch.x - startTouch.x, vectorY, endTouch.y - startTouch.y);
    }

    public float GetForseSwipe(float minForse, float minDistanceMultiplier,  float maxForse, float maxDistanceMultiplier)
    {
        var minDistance = DiagonalScreen() * minDistanceMultiplier;
        var maxDistance = DiagonalScreen() * maxDistanceMultiplier;

        if (SwipeDistance() >= maxDistance) return maxForse;
        if (SwipeDistance() <= minDistance) return minForse;

        var longSwipe = maxDistance - minDistance; 
        var xDistance = SwipeDistance() - minDistance;
        var percentDistance = xDistance / (longSwipe / 100);

        var longForse = maxForse - minForse;

        return longForse / 100 * percentDistance + minForse;
    }
    
    public float GetAngle()
    {
        if (!SwipeOn) return 0;

        var pointA = startTouch;
        var pointB = endTouch;
        var pointC = startTouch + new Vector3(0, 100);

        var AB = Mathf.Sqrt(Mathf.Pow(pointB.x - pointA.x, 2) + Mathf.Pow(pointB.y - pointA.y, 2));
        var AC = 100.0f;
        var CB = Mathf.Sqrt(Mathf.Pow(pointB.x - pointC.x, 2) + Mathf.Pow(pointB.y - pointC.y, 2));

        var angleA = Mathf.Acos((Mathf.Pow(AC, 2) + Mathf.Pow(AB, 2) - Mathf.Pow(CB, 2))
                                    / (2 * AC * AB));

        if (IsSwiping(SwipeDirection.Left))
            return -(angleA * 180 / Mathf.PI);
        else
            return angleA * 180 / Mathf.PI;
    }
    
    public float GetResistAngle()
    {
        if (!SwipeOn) return 0;

        var pointA = startTouch;
        var pointB = endTouch;
        var pointC = startTouch + new Vector3(0, 100);

        var AB = Mathf.Sqrt(Mathf.Pow(pointB.x - pointA.x, 2) + Mathf.Pow(pointB.y - pointA.y, 2));
        var AC = 100.0f;
        var CB = Mathf.Sqrt(Mathf.Pow(pointB.x - pointC.x, 2) + Mathf.Pow(pointB.y - pointC.y, 2));

        var angleA = Mathf.Acos((Mathf.Pow(AC, 2) + Mathf.Pow(AB, 2) - Mathf.Pow(CB, 2))
                                    / (2 * AC * AB));

        if (IsSwiping(SwipeDirection.Left))
            return -((angleA * 180 / Mathf.PI) - transform.eulerAngles.y + transform.eulerAngles.z);
        else
            return (angleA * 180 / Mathf.PI) + transform.eulerAngles.y - transform.eulerAngles.z;
    }

    float SwipeDistance()
    {
        if (!SwipeOn) return 0f;
        var point = endTouch - startTouch;
        return Mathf.Sqrt(Mathf.Pow(point.x, 2f) + Mathf.Pow(point.y, 2f));
    }

    float DiagonalScreen() => Mathf.Sqrt(Mathf.Pow(Screen.height, 2f) + Mathf.Pow(Screen.width, 2f));
}
