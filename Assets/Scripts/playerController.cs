using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controls the player
public class playerController : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed;

    public bool player1;
    Vector3 leftScale, rightScale;
    CharacterController characterController;
    Animator animator;
    public Slider healthBar;
    AudioSource soundEffect;

    KeyCode leftButton;
    KeyCode rightButton;
    KeyCode emoteButton;
    KeyCode meleeAttackButton;

    private bool soundEffectPlayed;
    public float soundEffectDelay;

    // Start is called before the first frame update
    void Start()
    {
        soundEffectPlayed = false;
        characterController = GetComponent<CharacterController>();
        soundEffect = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        rightScale = transform.localScale;
        leftScale = new Vector3(-rightScale.x, rightScale.y, rightScale.z);

        getControls();
        print("Player 1 Score: " + ScoreManager.get_player_1_score().ToString() + " Player 2 Score: " + ScoreManager.get_player_2_score().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // Check movement Input
        if (Input.GetKey(leftButton) || (Input.GetKey(rightButton)))
        {   
            if (!animator.GetBool("isWalking"))
            {
                animator.SetBool("isWalking", true);
            }

        } else
        {
            if (animator.GetBool("isWalking"))
            {
                animator.SetBool("isWalking", false);
            } 
        }

        // Check Melee Attack Input
        if (Input.GetKeyDown(meleeAttackButton)) 
        {
            if (!animator.GetBool("isAttackMelee"))
            {
                animator.SetBool("isAttackMelee", true);
            }

        } else
        {
            if (animator.GetBool("isAttackMelee"))
            {
                animator.SetBool("isAttackMelee", false);
            }
        }

        // Check Range Attack Input 
        if (Input.GetKeyDown(emoteButton))
        {
            if (!animator.GetBool("isEmoting"))
            {
                animator.SetBool("isEmoting", true);
            }

        } else
        {
            if (animator.GetBool("isEmoting"))
            {
                animator.SetBool("isEmoting", false);
            }
        }

        walk();
        SoundEffects();

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void walk()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (Input.GetKey(leftButton))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                if (player1)
                {
                    transform.localScale = leftScale;
                } else
                {
                    transform.localScale = rightScale;
                }
               
            }
            else if (Input.GetKey(rightButton))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                if (player1)
                {
                    transform.localScale = rightScale;
                }
                else
                {
                    transform.localScale = leftScale;
                }
            }

            characterController.Move(transform.forward * moveSpeed * Time.deltaTime);
        }

        if (!characterController.isGrounded)
        {
            characterController.Move(-transform.up * Time.deltaTime);
        }
    }

    void getControls()
    {
        if (player1)
        {
            leftButton = KeyCode.A;
            rightButton = KeyCode.D;
            emoteButton = KeyCode.G;
            meleeAttackButton = KeyCode.F;

        } else
        {
            leftButton = KeyCode.LeftArrow;
            rightButton = KeyCode.RightArrow;
            emoteButton = KeyCode.L;
            meleeAttackButton = KeyCode.K;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Guitar")
        {
            health -= collider.GetComponent<guitarAttacking>().damage;
            healthBar.value = health;

            if (health <= 0 && animator.GetBool("isAlive"))
            {
                StartCoroutine(die());
            }
        }
    }
    private IEnumerator die()
    {
        animator.SetBool("isAlive", false);

        if (gameObject.name == "Player 1")
        {
            ScoreManager.player_2_win();
        } 
        else if (gameObject.name == "Player 2")
        {
            ScoreManager.player_1_win();
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SoundEffects()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Emote"))
        {
            if (soundEffectPlayed)
            {
                soundEffectPlayed = false;
                StartCoroutine(playSoundEffect());
            }
        } 
        else
        {
            soundEffectPlayed = true;
        }
    }

    private IEnumerator playSoundEffect()
    {
        yield return new WaitForSeconds(soundEffectDelay);
        soundEffect.Play();
        print("play sound effect");
    }
}
