using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
	[SerializeField] private AudioSource _source;

	[Space (10)]

	[Header("Audio clips")]
	[SerializeField] private AudioClip _bgClipMenu;
	[SerializeField] private AudioClip _bgClipGameplay;

	public void PlayMainMenuMusic()
	{
		_source.Stop();

		_source.volume = 0.7f;
		_source.clip = _bgClipMenu;

		_source.Play();
	}

	public void PlayGameplayMusic()
	{
		_source.Stop();

		_source.volume = 1f;
		_source.clip = _bgClipGameplay;

		_source.Play();
	}
}
