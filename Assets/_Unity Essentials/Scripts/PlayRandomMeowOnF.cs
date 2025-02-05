using System.Collections;
using UnityEngine;

public class PlayRandomMeowOnF : MonoBehaviour
{
    public AudioClip[] soundClips; // Assign your 7 sound clips in the Inspector
    private AudioSource audioSource;
    private bool canMeow = true;
    private int meowCooldown = 1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canMeow)
        {
            int randomIndex = Random.Range(0, soundClips.Length);
            audioSource.PlayOneShot(soundClips[randomIndex]);
            StartCoroutine(MeowCooldown());
        }
        
    }

    private IEnumerator MeowCooldown()
    {
        canMeow = false;
        yield return new WaitForSeconds(meowCooldown);
        canMeow = true;
    }
}
