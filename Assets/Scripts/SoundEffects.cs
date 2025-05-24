using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource swordSoundEffect;




    public void SwordSoundEffect()
    {
        swordSoundEffect.Play();
    }
}
