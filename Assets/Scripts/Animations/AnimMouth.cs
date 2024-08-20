using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMouth : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _mouthRenderer;
	[SerializeField] private Sprite _mouthSmile;
	[SerializeField] private Sprite _mouthSurprise;
	[SerializeField] private Sprite _mouthFear;

	public void MouthSmile()
	{
		_mouthRenderer.sprite = _mouthSmile;
	}

	public void MouthSurprise()
	{
		_mouthRenderer.sprite = _mouthSurprise;
	}

	public void MouthFear()
	{
		_mouthRenderer.sprite = _mouthFear;
	}
}
