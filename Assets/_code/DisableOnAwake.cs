using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDisableOnAwake : MonoBehaviour
{
	public bool disableOnAwake;

	void Awake()
	{
		gameObject.SetActive(!disableOnAwake);
	}
}
