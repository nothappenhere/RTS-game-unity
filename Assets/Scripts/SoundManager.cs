using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    private AudioSource infantryAttackChannel;
    public AudioClip infantryAttackClip;

    private void Awake()
    {
        // Menetapkan singleton instance dan menghancurkan duplikat jika ada
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        infantryAttackChannel = gameObject.AddComponent<AudioSource>();
        infantryAttackChannel.volume = 0.1f;
        infantryAttackChannel.playOnAwake = false;
    }

    public void PlayInfantryAttackSound()
    {
        if (infantryAttackChannel.isPlaying == false)
        {
            infantryAttackChannel.PlayOneShot(infantryAttackClip);
        }
    }
}
