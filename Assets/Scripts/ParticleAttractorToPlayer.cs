using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttractorToPlayer : MonoBehaviour
{
    public Transform player;
    [Header("Movimiento")]
    public float targetSpeed = 6f;          // Velocidad objetivo con la que viajarán hacia el jugador
    public float steerStrength = 8f;        // Qué tan rápido giran su dirección hacia el objetivo
    public float attractRange = 50f;        // Si la partícula está más lejos que esto, no la procesamos (ahorro)
    public float stopDistance = 0.3f;       // Distancia a la que “frenan” para no atravesar al jugador
    public bool killOnReach = false;        // Destruir partícula al llegar

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        if (player == null) return;

        int count = ps.GetParticles(particles);
        Vector3 targetPos = player.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 ppos = particles[i].position;
            Vector3 toTarget = targetPos - ppos;
            float dist = toTarget.magnitude;

            if (dist > attractRange) continue;          // fuera de rango: ignora
            if (dist < stopDistance)
            {
                if (killOnReach) particles[i].remainingLifetime = 0f;
                // opcional: desacelera al acercarse
                particles[i].velocity = Vector3.Lerp(particles[i].velocity, Vector3.zero, 0.5f * Time.deltaTime);
                continue;
            }

            Vector3 desiredVel = toTarget.normalized * targetSpeed;
            // Interpola la velocidad actual hacia la deseada para que “curven” y no teletransporten su dirección
            particles[i].velocity = Vector3.Lerp(particles[i].velocity, desiredVel, steerStrength * Time.deltaTime);
        }

        ps.SetParticles(particles, count);
    }
}
