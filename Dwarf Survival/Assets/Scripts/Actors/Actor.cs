using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void Move(Vector2 dir)
    {
        rb.velocity = dir * 3;
    }
}
