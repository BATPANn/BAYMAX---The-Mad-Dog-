using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("AudioSources")]
    public AudioSource sfxSource; // play our sfx sounds 
    public AudioSource DialogueSource; // play our dialogue sounds

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if(clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void StartDialogueLoop(AudioClip clip, float volume = 1f)
    {

        if(clip != null && DialogueSource != null)
        {
            DialogueSource.clip = clip;
            DialogueSource.volume = volume;
            DialogueSource.loop = true;
            DialogueSource.Play();
        }

    }

    public void StopDialogueLoop()
    {
        if(DialogueSource != null)
        {
            DialogueSource.Stop();
            DialogueSource.loop = false;
        }
    }


}
