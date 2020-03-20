using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class movement : MonoBehaviour
{
    Vector3 direction = Vector3.zero;

    private CharacterController pj;

    [SerializeField]
    float normalSpeed = 6.0f;
    float speed;

    bool canMove = true;

    static float maxSprintBar = 200.0f;
    float sprintBar = maxSprintBar;

    float moveHorizontal;
    float moveVertical;

    Vector3 movementDir;

    public Slider slider;


    void Start()
    {
        speed = normalSpeed;
        pj = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {     
        moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;
        moveVertical = Input.GetAxisRaw("Vertical") * speed;

        Vector3 moveH = transform.right * moveHorizontal;
        Vector3 moveV = transform.up * moveVertical;

        if (canMove)
        {
            pj.SimpleMove(-moveV + moveH);
            Inputs();
        }

        float progress = sprintBar / maxSprintBar;
        slider.value = progress;
    }

   private void Inputs()
    {       
        if (Input.GetKey(KeyCode.LeftShift) && sprintBar >= 0)
        {
            speed = 10.0f;
            sprintBar--;
        }
        else
        {
            speed = normalSpeed;
            if (sprintBar < 200)
                sprintBar++;
        }     
    }
  
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "finish")
        {
            SceneManager.LoadScene("menu");
        }
    }
}
