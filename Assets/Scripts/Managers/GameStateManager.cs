using UnityEngine;

public class GameStateManager : SingletonBehaviour<GameStateManager>
{
	[Header("UI GameObjects")]
	[SerializeField] private GameObject _mainMenuGameObject;
	[SerializeField] private GameObject _gameUIGameObject;
	[SerializeField] private GameObject _gameOverGameObject;

	[Header("UI Controllers")]
	[SerializeField] private TimeBarController _timeBar;
	[SerializeField] private ScoreController _score;
	[SerializeField] private ScoreController _gameOverScore;
	[SerializeField] private BackgroundController _background;

	private bool playing = false;
	public bool Playing => playing;

	private int highestCreatedAtom = 1;

	private void Awake()
	{
		SingletonInit(this);

		Application.targetFrameRate = 60;
	}

	public void Play()
	{
		playing = true;
		_mainMenuGameObject.SetActive(false);
		_gameUIGameObject.SetActive(true);
		_background.ShowAllBackgrounds(false);

		AudioManager.Instance.BackgroundMusic.PlayGameplayMusic();
	}

	public void GameOver()
	{
		playing = false;

		_gameUIGameObject.SetActive(false);
		_gameOverGameObject.SetActive(true);

		_gameOverScore.UpdateScore(highestCreatedAtom);

		AudioManager.Instance.BackgroundMusic.PlayMainMenuMusic();
	}

	public void ResetGame()
	{
		AtomSpawnerManager.Instance.ResetGame();
		
		_timeBar.ChangeTimeToShrink(60f);
		_timeBar.ResetWidth();
		_timeBar.BarShrinks = false;

		_score.UpdateScore(1);

		highestCreatedAtom = 1;

		_background.ShowAllBackgrounds(true);
	}

	public void OnAtomDataSet(int atomicNumber)
	{
		if (playing)
		{
			if (atomicNumber > highestCreatedAtom || atomicNumber == 118)
			{
				highestCreatedAtom = atomicNumber;

				_timeBar.BarShrinks = true;
				_timeBar.ResetWidth();
				_timeBar.ChangeTimeToShrink(highestCreatedAtom);

				_score.UpdateScore(highestCreatedAtom);
				_gameOverScore.UpdateScore(highestCreatedAtom);

				_background.UpdateBackground(highestCreatedAtom);
			}
		}
	}
}
