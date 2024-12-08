using UnityEngine;

public class NightAmbienceController : MonoBehaviour
{
    public AudioSource nightAmbience; // Arrastra el Audio Source aquí

    void Start()
    {
        PlayNightAmbience();
    }

    public void PlayNightAmbience()
    {
        if (!nightAmbience.isPlaying)
        {
            nightAmbience.Play();
        }
    }

    public void StopNightAmbience()
    {
        if (nightAmbience.isPlaying)
        {
            nightAmbience.Stop();
        }
    }
}
