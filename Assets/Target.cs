using System.Collections;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText.gameObject.SetActive(false);
    }
    IEnumerator PopupScore(Transform arrow)
    {
        int score = 10;
        _scoreText.gameObject.SetActive(true);
        _scoreText.text = score.ToString();

        yield return new WaitForSeconds(3f);
        _scoreText.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Arrow"))
        {
            collision.rigidbody.isKinematic = true;
            StartCoroutine(PopupScore(collision.transform));
        }
    }
}
