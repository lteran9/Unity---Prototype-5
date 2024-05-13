using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages the important aspects of the game like starting, pausing, restarting, and dislaying the correct UI when appropriate.
/// </summary>
public class GameManager : MonoBehaviour
{
   /// <summary>
   /// Ends the game when the value reaches 0.
   /// </summary>
   [SerializeField] private int lives;

   /// <summary>
   /// Use this variable to determine if the game is in session or not.
   /// </summary>
   [SerializeField] private bool isGameActive;
   /// <summary>
   /// Use this variable to determine if the game is paused.
   /// </summary>
   [SerializeField] private bool isGamePaused;

   /// <summary>
   /// The time in seconds between enemy spawns.
   /// </summary>
   [SerializeField] private float spawnRate;

   [SerializeField] private Slider _volumeSlider;
   [SerializeField] private AudioSource _audioSource;
   [SerializeField] private AudioClip _targetSpawn;
   [SerializeField] private Button restartButton;
   [SerializeField] private GameObject titleScreen;
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private TextMeshProUGUI livesText;
   [SerializeField] private TextMeshProUGUI gameOverText;

   /// <summary>
   /// A list of the objects on screen that can be clicked on by the user.
   /// </summary>
   [SerializeField] private List<GameObject> targets;

   /// <summary>
   /// Cumulative score of the active game session.
   /// </summary>
   private int score;

   // Start is called before the first frame update
   private void Start()
   {

   }

   // Update is called once per frame
   private void Update()
   {
      // If user clicks the P key
      if (Input.GetKeyDown(KeyCode.P))
      {
         // If the game is active and *not* paused
         if (isGameActive && isGamePaused == false)
         {
            // Pause game
            Time.timeScale = 0;
            isGamePaused = true;
            _audioSource.volume = 0;
         }
         else if (isGameActive && isGamePaused == true)
         {
            // Unpause game
            Time.timeScale = 1;
            isGamePaused = false;
            _audioSource.volume = 1;
         }
      }
   }

   /// <summary>
   /// This coroutine will continuously spawn enemy objects at a random position until the game ends.
   /// </summary>
   private IEnumerator SpawnTarget()
   {
      while (isGameActive)
      {
         yield return new WaitForSeconds(Random.Range(spawnRate, spawnRate + 1));

         int index = Random.Range(0, targets.Count);
         Instantiate(targets[index]);

         // Play sound when object is created
         _audioSource.PlayOneShot(_targetSpawn, 1f);
      }
   }

   public void UpdateScore(int scoreToAdd)
   {
      score += scoreToAdd;
      scoreText.text = "Score: " + score;
   }

   public void UpdateVolume()
   {
      _audioSource.volume = _volumeSlider.value;
   }

   public void UpdateLives()
   {
      if (lives > 0)
      {
         lives -= 1;
      }

      livesText.text = "Lives: " + lives;
   }

   public void StartGame(int difficulty)
   {
      isGameActive = true;
      score = 0;
      spawnRate /= difficulty;

      StartCoroutine(SpawnTarget());
      UpdateScore(0);

      titleScreen.SetActive(false);
   }

   public void GameOver()
   {
      UpdateLives();

      if (lives <= 0)
      {
         restartButton.gameObject.SetActive(true);
         gameOverText.gameObject.SetActive(true);
         isGameActive = false;
      }
   }

   public void RestartGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public bool IsGameActive()
   {
      return isGameActive;
   }

   public bool IsGamePaused()
   {
      return isGamePaused;
   }
}
