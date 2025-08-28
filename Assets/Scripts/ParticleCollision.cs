using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public GameObject tesla;
    public Color hitColor = Color.red;
    public float colorDuration = 0.5f;
    public Color originalColor;

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");
            Renderer rend = tesla.GetComponent<Renderer>();
            StartCoroutine(ChangeColor(rend, hitColor, colorDuration));
        }
    }

    System.Collections.IEnumerator ChangeColor(Renderer rend, Color newColor, float duration)
    {
        Material mat = rend.material;
        originalColor = mat.color;

        mat.color = newColor;
        yield return new WaitForSeconds(duration);
        mat.color = originalColor;
    }
}
