using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public Direction[] directions;

}
