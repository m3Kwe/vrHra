using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = score.ToString();
        EventBus.On<OnCollectableCollected>(OnColectableCollected);
    }

    private void OnColectableCollected(OnCollectableCollected e)
    {
        AddScore(e.scoreValue);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

}
