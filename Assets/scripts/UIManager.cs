using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    
    private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = "Score:"+ 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score:" + playerScore.ToString();

    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
           GameOverSequence();
        }

    }

    void GameOverSequence()
    { 

       _gameManager.GameOver();
      _gameOverText.gameObject.SetActive(true);
      _restartText.gameObject.SetActive(true);
     StartCoroutine(GameOverFlickerRoutine());



    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }
}
