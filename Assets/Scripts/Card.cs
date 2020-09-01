using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public void Scale(float percentage)
    {
        Vector3 newScale = Vector3.one * percentage;
        transform.localScale = newScale;
    }

    public void Move(Vector3 moveAmount)
    {
        transform.position += moveAmount;
    }
}
