using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosionEffect.transform.position = asteroid.transform.position;
        this.explosionEffect.Play();
        AchievementsManager.Instance.IncreaseKillCount();
    }

}
