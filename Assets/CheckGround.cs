using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private bool _onGround;

    public bool OnGround { get => _onGround;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _onGround = false;
    }
}
