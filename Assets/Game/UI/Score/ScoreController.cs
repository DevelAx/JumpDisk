using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MyMonoBehaviour
{
	[SerializeField]
	private Text _text;

	private int _score = -1;
	private int Score 
	{ 
		get => _score; 
		set
		{
			if (value == _score)
				return;

			_score = value;
			UpdateScoreText();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		Debug.Assert(_text, nameof(_text));
	}

	private void Start()
	{
		Score = LocalStorage.Score;
	}

	private void OnJumpCompleted(Event_JumpCompleted @event)
	{
		LocalStorage.Score = ++Score;
	}

	private void UpdateScoreText()
	{
		_text.text = $"Jumps: {Score}";
	}

	protected override void SubscribeToEvents()
	{
		base.SubscribeToEvents();
		this.SubscribeTo<Event_JumpCompleted>(OnJumpCompleted);
	}

	protected override void UnsubscribeFromEvents()
	{
		base.UnsubscribeFromEvents();
		this.UnsubscribeFrom<Event_JumpCompleted>(OnJumpCompleted);
	}

	protected override void OnEditorValidate()
	{
		base.OnEditorValidate();
		_text = _text ?? this.RequireComponent<Text>();
	}
}
