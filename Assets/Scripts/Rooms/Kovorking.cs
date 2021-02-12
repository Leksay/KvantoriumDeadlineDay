using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kovorking : MonoBehaviour
{
    [SerializeField] private Transform rightUpCorner, leftDownCorner;

    public Vector3 GetRightUp()
    {
        return rightUpCorner.position;
    }

    public Vector3 GetLeftDown()
    {
        return leftDownCorner.position;
    }
}
