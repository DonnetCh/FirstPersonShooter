using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perdigon : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
