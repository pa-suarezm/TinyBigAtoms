using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLinkButton : MonoBehaviour
{
	[SerializeField] private string _url;

	public void OnClick()
	{
		Application.OpenURL(_url);
	}
}
