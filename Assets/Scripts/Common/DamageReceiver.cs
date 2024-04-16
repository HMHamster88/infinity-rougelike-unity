using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : MonoBehaviour
{
    public List<AudioClip> hitAudoClips = new List<AudioClip>();

    [SerializeField]
    private bool immortal = false;

    private AudioSource audioSource;
    private Health health;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
    }

    public void ReceiveDamage(ComplexDamage damage)
    {
        if (audioSource != null && hitAudoClips.Count > 0)
        {
            audioSource.clip = hitAudoClips.GetRandomElement();
            audioSource.Play();
        }

        if (health != null)
        {
            health.ApplyDamage(CovertDamage(damage));
            if (!immortal && health.Value <= 0)
            {
                Die();
            }
        }
    }

    protected abstract void Die();

    protected abstract float CovertDamage(ComplexDamage damage);
}
