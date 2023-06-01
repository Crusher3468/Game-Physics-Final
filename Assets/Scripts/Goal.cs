using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Goal : MonoBehaviour
{
    [SerializeField] int points;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.AddPoints(points);
        Destroy(collision.gameObject, 1f);
        
    }
}
