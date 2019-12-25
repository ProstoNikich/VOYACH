using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction { clockwise  = 1, counterclockwise = -1 }
public class Pinwheel : MonoBehaviour
{
    [SerializeField] float speed = 250.0f;
    [SerializeField] Direction direction = Direction.clockwise;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime * (int)direction, 0);
    }
}
