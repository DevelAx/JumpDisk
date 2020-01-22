﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
public class GameSettings : ScriptableObject
{
    public HumanSettings Human;


	#region Inner Types

	[Serializable]
    public class HumanSettings
    {
        [Range(0.1f, 5f)]
        public float RunSpeed = 1.5f;
    }

	#endregion
}

public static class Game
{
    public static GameSettings Settings { get; }

    static Game()
    {
        Settings = Resources.Load<GameSettings>(nameof(GameSettings));
    }
}