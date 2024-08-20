using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
	public BackgroundMusicController BackgroundMusic;
	public AudioSource AtomFusion;

	private void Awake()
	{
		SingletonInit(this);
	}
}
