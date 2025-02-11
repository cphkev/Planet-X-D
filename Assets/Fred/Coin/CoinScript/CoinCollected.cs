using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            GameManager.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}