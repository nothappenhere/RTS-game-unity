using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public float speed = 5.0f; // Kecepatan patroli

    private bool movingForward = true;
    private float timer = 0.0f;
    private float switchDirectionTime = 5.0f; // Durasi sebelum putar arah

    void Update()
    {
        timer += Time.deltaTime;

        // Ganti arah setiap beberapa detik
        if (timer >= switchDirectionTime)
        {
            movingForward = !movingForward;
            timer = 0.0f;
        }

        // Gerak maju atau mundur sesuai arah
        if (movingForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
