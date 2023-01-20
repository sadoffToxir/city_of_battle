using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    private AudioSource audioSource;
    private void start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
