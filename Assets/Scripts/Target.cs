using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
   /// <summary>
   /// Minimum possible force applied to target rigidbody.
   /// </summary>
   private float minSpeed = 12;
   /// <summary>
   /// Maximum possible force applied to target rigidbody.
   /// </summary>
   private float maxSpeed = 16;
   /// <summary>
   /// Minimum and maximum torque that can be applied to the rotation of the object.
   /// </summary>
   private float torque = 10;
   /// <summary>
   /// Range of the X axis that the object can spawn in.
   /// </summary>
   private float xRange = 4;
   /// <summary>
   /// The Y axis point that the object can spawn on.
   /// </summary>
   private float ySpawnPos = -2;

   private Rigidbody targetRb;
   private GameManager gameManager;

   public int pointValue;
   public ParticleSystem explosionParticle;

   // Start is called before the first frame update
   private void Start()
   {
      targetRb = GetComponent<Rigidbody>();
      gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

      targetRb.AddForce(RandomForce(), ForceMode.Impulse);
      targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());

      transform.position = RandomSpawnPos();
   }

   // Update is called once per frame
   private void Update()
   {

   }

   private void OnMouseDown()
   {
      if (gameManager.IsGameActive() && gameManager.IsGamePaused() == false)
      {
         Destroy(gameObject);
         Instantiate(explosionParticle, transform.position, transform.rotation);

         gameManager.UpdateScore(pointValue);
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      Destroy(gameObject);

      if (!gameObject.CompareTag("Bad"))
      {
         gameManager.GameOver();
      }
   }

   private float RandomTorque()
   {
      return Random.Range(-torque, torque);
   }

   private Vector3 RandomForce()
   {
      return Vector3.up * Random.Range(minSpeed, maxSpeed);
   }

   private Vector3 RandomSpawnPos()
   {
      return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
   }
}
