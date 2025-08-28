using UnityEngine;
using System.Collections;

public class FishManager : MonoBehaviour
{
    public AudioClip clipVoice;
    public AudioClip clipPiano;
    private AudioSource audioSource;
    public GameObject parent;
    public GameObject tesla;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayAudioSequence());
        }
    }

    IEnumerator PlayAudioSequence()
    {
        audioSource.clip = clipPiano;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = clipVoice;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(parent);
    }
}
