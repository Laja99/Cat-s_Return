using UnityEngine;
using System.Collections;

public class VortexSink : MonoBehaviour
{
    public float pullSpeed = 5f;        // ÓÑÚÉ ÓÍÈ ÇáßÑÉ ááãÑßÒ
    public float rotationSpeed = 500f;  // ÓÑÚÉ ÏæÑÇä ÇáßÑÉ ÏÇÎá ÇáÏæÇãÉ
    public float sinkSpeed = 2f;        // ÓÑÚÉ ÇÎÊİÇÁ ÇáßÑÉ (ÊÕÛíÑ ÇáÍÌã)

    private void OnTriggerStay2D(Collider2D other)
    {
        // ÊÃßÏ Ãä ÇáßÇÆä ÇáĞí ÏÎá åæ ÇáßÑÉ (Úä ØÑíŞ ÇáÜ Tag)
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SinkEffect(other.gameObject));
        }
    }

    IEnumerator SinkEffect(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // 1. ÚØá ŞæÇäíä ÇáİíÒíÇÁ ÇáÚÇÏíÉ ááßÑÉ ÍÊì áÇ ÊŞÇæã ÇáÏæÇãÉ
        if (rb != null) rb.simulated = false;

        while (ball.transform.localScale.x > 0.01f)
        {
            // 2. ÓÍÈ ÇáßÑÉ äÍæ ãÑßÒ ÇáÍİÑÉ ÈÏŞÉ
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, transform.position, pullSpeed * Time.deltaTime);

            // 3. ÊÏæíÑ ÇáßÑÉ Íæá ãÑßÒ ÇáÍİÑÉ (ÊÃËíÑ ÇáÏæÇãÉ)
            ball.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

            // 4. ÊÕÛíÑ ÍÌã ÇáßÑÉ ÊÏÑíÌíÇğ (ÅíÍÇÁ ÈÃäåÇ ÊÛæÕ ááÏÇÎá)
            ball.transform.localScale -= Vector3.one * sinkSpeed * Time.deltaTime;

            yield return null;
        }

        // 5. ÇÎÊİÇÁ ÇáßÑÉ ÊãÇãÇğ
        ball.SetActive(false);

        Debug.Log("ÇáßÑÉ ÛÇÕÊ İí ÇáÏæÇãÉ!");
        // åäÇ íãßäß ÅÖÇİÉ ßæÏ ááÇäÊŞÇá áãÔåÏ ÂÎÑ Ãæ ÅÚÇÏÉ ÇáãÑÍáÉ
    }
}