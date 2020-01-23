using UnityEngine;

public static class LocalStorage
{
	#region Score

	private const string KEY_SCORE = nameof(KEY_SCORE);
	private static int _score = -1;
	
	public static int Score
	{
		get
		{
			if (_score == -1)
				_score = PlayerPrefs.GetInt(KEY_SCORE, 0);

			return _score;
		}
		set
		{
			if (_score == value)
				return;

			_score = value;
			PlayerPrefs.SetInt(KEY_SCORE, value);
		}
	}

	#endregion
}