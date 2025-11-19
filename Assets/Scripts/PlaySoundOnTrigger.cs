using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private bool playOnce = true;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //  Check tag for PLAYER
        if (other.CompareTag("Player"))
        {
            if (playOnce && hasPlayed) return;
            //Use PlayOneShot to play the audio clip
            audioSource.PlayOneShot(audioSource.clip);
            hasPlayed = true;
        }
    }
}
