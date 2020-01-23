using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
public class GameSettings : ScriptableObject
{
    public HumanSettings Human;
    public DiskSettings[] Disks;

    #region Inner Types

    [Serializable]
    public class HumanSettings
    {
        [Range(0.5f, 5f)]
        public float RunSpeed = 2f;

        [Range(3f, 10f)]
        public float JumpBackSpeed = 5f;
    }

    [Serializable]
    public class DiskSettings
    {
        public float Radius = 1.5f;
        //public float JumpHeight = 4f;
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