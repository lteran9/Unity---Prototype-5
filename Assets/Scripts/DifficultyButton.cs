using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
   [SerializeField] private int difficulty;

   [SerializeField] private Button _button;
   [SerializeField] private GameManager _gameManager;

   // Start is called before the first frame update
   private void Start()
   {
      _button.onClick.AddListener(SetDifficultyAndStartGame);
   }

   // Update is called once per frame
   private void Update()
   {

   }

   private void SetDifficultyAndStartGame()
   {
      _gameManager.StartGame(difficulty);
   }
}
