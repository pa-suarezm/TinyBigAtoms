using System.Collections;
using UnityEngine;

public class AnimBlink : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _eyesRenderer;
	[SerializeField] private Sprite _eyesOpen;
	[SerializeField] private Sprite _eyesClosed;

	private WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(0.1f);

	private Coroutine blinkCoroutine;

	// Blink rate range in blinks per minute
	private float minBlinksPerMinute = 15f;
	private float maxBlinksPerMinute = 20f;

	// Calculate the time interval between blinks in seconds
	private float nextBlinkTime;

	private void Start()
	{
		CalculateBlinkInterval();	
	}

	private void Update()
	{
		if (blinkCoroutine == null)
		{
			// If the current time is past the next blink time, call the method
			if (Time.time >= nextBlinkTime)
			{
				blinkCoroutine = StartCoroutine(Blink());

				// Set the time for the next blink
				CalculateBlinkInterval();
			}
		}
	}

	void CalculateBlinkInterval()
	{
		float blinkRate = Random.Range(minBlinksPerMinute, maxBlinksPerMinute);
		float blinkIntervalSeconds = 60 / blinkRate;

		// Calculate the next blink time
		nextBlinkTime = Time.time + blinkIntervalSeconds;
	}

	private IEnumerator Blink()
	{
		OpenEyes();

		CloseEyes();
		yield return waitForSeconds;

		OpenEyes();

		blinkCoroutine = null;
	}

	public void OpenEyes()
	{
		_eyesRenderer.sprite = _eyesOpen;
	}

	public void CloseEyes()
	{
		_eyesRenderer.sprite = _eyesClosed;
	}
}
