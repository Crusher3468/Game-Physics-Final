using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
	[SerializeField] private string hitTagName = string.Empty;

	//public delegate void CollisionDelegate(GameObject other);
	public Action<GameObject> onEnter;
	public Action<GameObject> onExit;
	public Action<GameObject> onStay;

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
		{
			onEnter?.Invoke(collision.gameObject);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
		{
			onExit?.Invoke(collision.gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
		{
			onStay?.Invoke(collision.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
		{
			onEnter?.Invoke(other.gameObject);
		}		
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
		{
			onExit?.Invoke(other.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
		{
			onStay?.Invoke(other.gameObject);
		}
	}

}
