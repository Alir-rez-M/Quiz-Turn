
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource swordSoundEffect;




    public void SwordSoundEffect()
    {
        swordSoundEffect.Play();
    }
}
