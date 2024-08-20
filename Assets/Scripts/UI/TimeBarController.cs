using UnityEngine;

public class TimeBarController : MonoBehaviour
{
	[SerializeField] private RectTransform _barTransform;

	private float maxWidth = Screen.width;

	public bool BarShrinks = false;
	private float timeToFullyShrink;
	private float widthPerTickToShrink;

	private void Start()
	{
		ChangeTimeToShrink(60f);
	}

	private void Update()
	{
		if (BarShrinks)
		{
			_barTransform.sizeDelta = new Vector2(_barTransform.sizeDelta.x - widthPerTickToShrink, _barTransform.sizeDelta.y);

			if (_barTransform.sizeDelta.x <= 0)
			{
				BarShrinks = false;
				GameStateManager.Instance.GameOver();
			}
		}
	}

	public void ChangeTimeToShrink(float highestCreatedAtom)
	{

		float newTimeToShrink = 40f;
		if (1 <= highestCreatedAtom && highestCreatedAtom < 30)
		{
			newTimeToShrink = 40f;
		}
		else if (30 <= highestCreatedAtom && highestCreatedAtom < 60)
		{
			newTimeToShrink = 30f;
		}
		else if (60 <= highestCreatedAtom && highestCreatedAtom < 90)
		{
			newTimeToShrink = 20f;
		}
		else if (90 <= highestCreatedAtom && highestCreatedAtom <= 120)
		{
			newTimeToShrink = 10f;
		}

		timeToFullyShrink = newTimeToShrink * Application.targetFrameRate;
		widthPerTickToShrink = maxWidth / timeToFullyShrink;
	}

	public void ResetWidth()
	{
		_barTransform.sizeDelta = new Vector2(maxWidth, _barTransform.sizeDelta.y);
	}
}
