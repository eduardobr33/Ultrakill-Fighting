using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip punchSound;
    public AudioClip shootSound;
    public AudioClip kickSound;
    public AudioClip dodgeSound;
    public AudioClip reloadSound;
    public AudioClip hitSound;

    public void PlayPunch()
    {
        SoundManager.Instance.PlayEffect(punchSound);
    }

    public void PlayShoot()
    {
        SoundManager.Instance.PlayEffect(shootSound);
    }

    public void PlayKick()
    {
        SoundManager.Instance.PlayEffect(kickSound);
    }

    public void PlayDodge()
    {
        SoundManager.Instance.PlayEffect(dodgeSound);
    }

    public void PlayReload()
    {
        SoundManager.Instance.PlayEffect(reloadSound);
    }

    public void PlayHit()
    {
        SoundManager.Instance.PlayEffect(hitSound);
    }
}
