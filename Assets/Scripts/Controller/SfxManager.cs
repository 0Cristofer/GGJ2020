using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosion, drop, clickButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayExplosionSfx()
    {
        audioSource.PlayOneShot(explosion);
    }

    public void PlayDropSfx()
    {
        audioSource.PlayOneShot(drop);
    }

    public void PlayClickButtonSfx()
    {
        audioSource.PlayOneShot(clickButton);
    }

}
