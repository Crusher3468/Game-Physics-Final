using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class pegs : MonoBehaviour
{
    SpriteRenderer color;
    [SerializeField] AudioClip impact;
    AudioSource hit;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>();
        hit = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1);
        color.material.color =   newColor;
        hit.PlayOneShot(impact);
     }
}
