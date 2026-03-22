using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{
    private TextMeshProUGUI txtScore;
    public static float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        score=0.0f;
    }
    void Start()
    {
        
        txtScore = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txtScore.text=$"Score: {score}"; // same as python f strings. string interpolation
    }

    public static void HitEnemey()
    {
        score += 1_000; // the _ adds a comma.
    }
}
