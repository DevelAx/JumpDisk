using UnityEngine;

[DisallowMultipleComponent]
public class HumanSounds : MyMonoBehaviour
{
    [SerializeField]
    private AudioSource _jumpAudio = default;

    [SerializeField]
    private AudioSource _landingAudio = default;

    public void PlayJump(float strength)
    {
        TryPlay(_jumpAudio, strength);
    }

    public void PlayLanding(float strength)
    {
        TryPlay(_landingAudio, strength);
    }

	#region Helpers

	private static void TryPlay(AudioSource audio, float strength)
    {
        if (!audio)
            return;

        audio.volume = strength;
        audio.pitch = 0.75f + strength;
        audio.Play();
    }

	#endregion
}
