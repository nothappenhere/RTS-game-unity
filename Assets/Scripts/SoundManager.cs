using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    [Header("Infantry")]
    private AudioSource infantryAttackChannel;
    public AudioClip infantryAttackClip;

    [Header("Buildings")]
    private AudioSource destructionBuildingChannel;
    private AudioSource constructionBuildingChannel;
    private AudioSource extraBuildingChannel;

    public AudioClip sellingSound;
    public AudioClip buildingConstructionSound;
    public AudioClip buildingDestructionSound;

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

        destructionBuildingChannel = gameObject.AddComponent<AudioSource>();

        constructionBuildingChannel = gameObject.AddComponent<AudioSource>();

        extraBuildingChannel = gameObject.AddComponent<AudioSource>();
    }

    public void PlayInfantryAttackSound()
    {
        if (infantryAttackChannel.isPlaying == false)
        {
            infantryAttackChannel.PlayOneShot(infantryAttackClip);
        }
    }

    public void PlayBuildingSellingSound()
    {
        if (extraBuildingChannel != null && extraBuildingChannel.isPlaying == false)
        {
            extraBuildingChannel.PlayOneShot(sellingSound);
        }
    }

    public void PlayBuildingConstructionSound()
    {
        if (constructionBuildingChannel.isPlaying == false)
        {
            constructionBuildingChannel.PlayOneShot(buildingConstructionSound);
        }
    }

    public void PlayBuildingDestructionSound()
    {
        if (destructionBuildingChannel.isPlaying == false)
        {
            destructionBuildingChannel.PlayOneShot(buildingDestructionSound);
        }
    }
}
