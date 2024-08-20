using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class BackgroundController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _background1;
	[SerializeField] private SpriteRenderer _background2;
	[SerializeField] private SpriteRenderer _background3;

	public void ShowAllBackgrounds(bool show)
	{
		float alpha = show ? 1f : 0f;
		Color aux;

		aux = _background1.color;
		aux.a = alpha;
		_background1.color = aux;
		_background2.color = aux;
		_background3.color = aux;
	}

	public void UpdateBackground(int highestAtomNumber)
	{
		Color aux;
		if (1 < highestAtomNumber && highestAtomNumber < 40)
		{
			aux = _background1.color;
			aux.a += 0.025f;
			_background1.color = aux;
		}
		else if (40 <= highestAtomNumber && highestAtomNumber < 80)
		{
			aux = _background1.color;
			aux.a = 1;
			_background1.color = aux;

			aux = _background2.color;
			aux.a += 0.025f;
			_background2.color = aux;
		}
		else if (80 <= highestAtomNumber && highestAtomNumber < 118)
		{
			aux = _background2.color;
			aux.a = 1;
			_background2.color = aux;

			aux = _background3.color;
			aux.a += 0.027f;
			_background3.color = aux;
		}

		if (highestAtomNumber == 1)
			ShowAllBackgrounds(false);
		else if (highestAtomNumber == 118)
			ShowAllBackgrounds(true);
	}
}
