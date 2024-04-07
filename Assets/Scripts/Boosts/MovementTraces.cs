using UnityEngine;

public class MovementTraces : MonoBehaviour
{
    public SpriteRenderer player;
    public ParticleSystem tracesParticles;

    void Update()
    {
        ParticleSystem.VelocityOverLifetimeModule velocityModule = tracesParticles.velocityOverLifetime;
        ParticleSystem.MinMaxCurve xVelocity = velocityModule.x;
        if (player.flipX)
        {
            xVelocity.constantMin = -75f;
            xVelocity.constantMax = -75f;
        }
        else
        {
            xVelocity.constantMin = 75f;
            xVelocity.constantMax = 75f;
        }
        velocityModule.x = xVelocity;
    }
}
