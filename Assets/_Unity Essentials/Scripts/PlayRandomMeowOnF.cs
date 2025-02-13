using System.Collections;
using UnityEngine;

public class PlayRandomMeowOnF : MonoBehaviour
{
    public AudioClip[] soundClips; // Assign your 7 sound clips in the Inspector
    private AudioSource audioSource;
    private bool canMeow = true;
    private int meowCooldown = 1;

    private KeyCode meowKey;
    private string meowString;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        meowString = PlayerPrefs.GetString("meow");

        if (meowString.EndsWith("-Key"))
        {
            meowString = meowString.Substring(0, meowString.Length - 4); // Remove the last 4 characters ("-Key")
        }
        meowKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), meowString);

        if (Input.GetKey(meowKey) && canMeow || Input.GetButtonDown("Fire2") && canMeow)
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
