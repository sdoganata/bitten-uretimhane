using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource machinegunShootAS;
    public AudioSource shotgunShootAS;
    public AudioSource coinCollectedAS;
    public AudioSource getHitAS;
    public AudioSource zombieGrowlAS;

    public void PlayMachineGunShootSFX()
    {
        shotgunShootAS.Play();
    }
    public void PlayShotgunShootSFX()
    {
        machinegunShootAS.Play();
    }
    
    public void PlayCoinCollectedSFX()
    {
            coinCollectedAS.Play();
        
    }
    public void PlayGetHitSFX()
    {
        getHitAS.Play();

    }
    public void PlayZombieGrowlSFX()
    {
        zombieGrowlAS.Play();

    }
}
