using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	[SerializeField] private Image _frameColor;
	[SerializeField] private TextMeshProUGUI _nameTxt;
	[SerializeField] private TextMeshProUGUI _numberTxt;
	[SerializeField] private Image _overlay;

	public void UpdateScore(int newNumber)
	{
		if (gameObject.activeInHierarchy)
			StartCoroutine(FlashOverlay());

		_frameColor.color = AtomSpawnerManager.Instance.allAtomColors[newNumber];
		_nameTxt.text = AtomSpawnerManager.Instance.allAtomNames[newNumber];
		_numberTxt.text = newNumber.ToString();
	}

	private IEnumerator FlashOverlay()
	{
		_overlay.gameObject.SetActive(true);

		yield return new WaitForSecondsRealtime(0.1f);

		_overlay.gameObject.SetActive(false);
	}
}
