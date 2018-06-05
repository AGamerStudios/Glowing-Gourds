using UnityEngine;
using TMPro;
public class ScoreKeeper : MonoBehaviour {
	public int score;
	public TMP_Text scoreText;

	void Update()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        if (score <= 9){
            scoreText.text = "0000" + score;
        }
        else if (score <= 99){
            scoreText.text = "000" + score;
        }
        else if (score <= 999){
            scoreText.text = "00" + score;
        }
        else if (score <= 9999){
            scoreText.text = "0" + score;
        }
        else{
            scoreText.text = "" + score;
        }
    }

    public void IncrementScore(int value){
		score += value;
	}
}
