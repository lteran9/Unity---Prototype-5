using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
   public int difficulty;

   public Button _button;
   public GameManager _gameManager;

   // Start is called before the first frame update
   void Start()
   {
      // Set on the front-end
      // _button = GetComponent<Button>();
      // _gameManager = GameObject.FindObjectOfType<GameManager>(); 

      _button.onClick.AddListener(SetDifficultyAndStartGame);
   }

   // Update is called once per frame
   void Update()
   {

   }

   void SetDifficultyAndStartGame()
   {
      _gameManager.StartGame(difficulty);
   }
}
