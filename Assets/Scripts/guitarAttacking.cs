using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guitarAttacking : MonoBehaviour
{
    public bool attacking;
    public Animator playerAnimator;
    public int damage;

    BoxCollider colider;

    private void Start()
    {
        colider = GetComponent<BoxCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        attacking = checkAttacking();

        colider.enabled = attacking;
    }

    private bool checkAttacking()
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack Melee");
    }
}
