using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform audioListener;
    public float minDistance = 0.1f;
    public float maxDistance = 1f;

    void Start()
    {
        if (audioSource != null)
        {
            // Set the audio source settings
            audioSource.spatialBlend = 1.0f; // 3D sound
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.loop = true; // Optional: if you want the sound to loop
            audioSource.Stop(); // Ensure the audio source is initially stopped
        }
    }

    void Update()
    {
        if (audioSource != null && audioListener != null)
        {
            float distance = Vector3.Distance(audioSource.transform.position, audioListener.position);

            // Check if the listener is within the rolloff distance
            if (distance >= minDistance && distance <= maxDistance)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                // Calculate the volume based on the distance
                float t = Mathf.InverseLerp(minDistance, maxDistance, distance);
                audioSource.volume = Mathf.Lerp(1f, 0f, t); // Gradually increase volume from 0 to 1
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}
