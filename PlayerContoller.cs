using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerContoller : MonoBehaviour {
    
    public float speed;
    public Vector3 jump;
    public float jumpForce;
    public bool opGrond;

    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        count = 0;
        SetCountText ();
        winText.text = "";
    }
    void OnCollisionStay()
         {
             opGrond = true;
         }

    void Update() {
        if (Input.GetKey(KeyCode.Space) && opGrond == true){
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            opGrond = false;
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
        
        if(gameObject.transform.position.y < -10){
            winText.text = "Game over!";
             SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
        if (other.gameObject.CompareTag ("JumpBox"))
        {
            rb.AddForce(jump * jumpForce * 12, ForceMode.Impulse);
            // other.gameObject.SetActive (false);
        }
        if (count >= 36) {
            if (other.gameObject.CompareTag ("Finish")) {
                winText.text = "You Win!";
                other.gameObject.SetActive (false);
            }
        }
    }

    void SetCountText () {
        countText.text = "Count: " + count.ToString ();
    }
}