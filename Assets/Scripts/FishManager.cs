using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject Fish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fish.SetActive(true);
        }
    }
}
