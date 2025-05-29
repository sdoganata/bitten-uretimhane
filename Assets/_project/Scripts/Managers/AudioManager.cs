using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource machinegunShootAS;
    public AudioSource coinCollectedAS;
    public AudioSource getHitAS;
    public AudioSource zombieGrowlAS;

    public void PlayMachineGunShootSFX()
    {
        machinegunShootAS.Play();
    }
    
    public void PlayCoinCollectedSFX()
    {
            coinCollectedAS.Play();
        
    }
    public void PlayGetHitSFX()
    {
        coinCollectedAS.Play();

    }
    public void PlayZombieGrowlSFX()
    {
        coinCollectedAS.Play();

    }
}
