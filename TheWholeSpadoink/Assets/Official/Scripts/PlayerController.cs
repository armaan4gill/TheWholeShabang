using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;
/*
 * Armaan Gill, Sophia, Owen
 * 05/09/2024
 * Script that will hold basic functions of controlling the drift car and also when hit by object
 */

public class PlayerController : MonoBehaviour
{
    //Raycast
    private bool is_grounded;
    private Rigidbody body;
    private float distance_to_wall_left = 2f;
    private float distance_to_wall_right = 2f;
    private float distance_to_wall_forward = 2f;
    private float distance_to_wall_back = 2f;
    private Vector3 tempDrag;

    // Settings
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;
    public bool stunned;
    public float lerpDrag = 1;
    public GameObject stunIcon;
    public LayerMask groundLayer;
    public float deathHeight; //Y pos that player will die if went below said y pos

    // Variables
    private Rigidbody rb;
    private Vector3 Force;
    private Vector3 respawnPoint;
    private Vector3 startPos; // Set to be position that the player starts the game at

    /// <summary>
    /// Executes before void Start function
    /// </summary>
    private void Awake()
    {

      

    }
    void Update()
    {
        Move();

        


    }
    /// <summary>
    /// removes one life and sets player position back to starting position
    /// </summary>
    public void Respawn()
    {

        //set player position to the position that was started
        transform.position = startPos;

    }

    public void Move()
    {

        // Moving
        Force += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += Force * Time.deltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * Force.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit

        Force *= Drag;
        Force = Vector3.ClampMagnitude(Force, MaxSpeed);

        //Drift using LERP
        float targetDrag = Input.GetKey(KeyCode.Space) ? Drag + 2 : 0.992f; // Set target drag based on Space key
        tempDrag = Vector3.Lerp(tempDrag, new Vector3(targetDrag, 0, 0), Time.deltaTime * lerpDrag); // Lerp temporary Vector3 with target drag value
        Drag = tempDrag.x; // Extract the x component (assuming targetDrag only affects x)

        // Traction
        Debug.DrawRay(transform.position, Force.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        Force = Vector3.Lerp(Force.normalized, transform.forward, Traction * Time.deltaTime) * Force.magnitude;

    }


   /// <summary>
   /// activates stun
   /// </summary>
    public void StunOn()
    {
        stunIcon.gameObject.SetActive(true);
        Invoke(nameof(StunOff), 0.5f);
    }
    /// <summary>
    /// Deactivates stun
    /// </summary>
    public void StunOff()
    {
        stunIcon.gameObject.SetActive(false);
        if (stunned) Invoke(nameof(StunOn), 0.5f);
    }

    IEnumerator Stun()//handles stunned status
    {
        stunned = true;
        StunOn();
        float currentPlayerSpeed = 100;
        float currentDrag = Drag;
        MoveSpeed = 0;
        Drag = 0;
        yield return new WaitForSeconds(3);
        MoveSpeed = currentPlayerSpeed;
        Drag = currentDrag;
        stunned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")//detects the laser colliding w/ player
        {
            StartCoroutine(Stun());
        }
        if (other.gameObject.tag == "Finish")
        {
            // Load the next scene by name 
            SceneManager.LoadScene(6);
        }
    }
    
}
