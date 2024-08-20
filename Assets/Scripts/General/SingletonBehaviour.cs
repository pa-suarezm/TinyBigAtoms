using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour
{
	private static T instance;
	public static T Instance => instance;

	protected void SingletonInit(T pInstance)
	{
		if (instance != null && !instance.Equals(pInstance))
		{
			Destroy(this);
		}
		else
		{
			instance = pInstance;
		}
	}
}
