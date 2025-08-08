using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveToZeroOnAwake : MonoBehaviour
{
	void Awake()
	{
		GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
	}
}
