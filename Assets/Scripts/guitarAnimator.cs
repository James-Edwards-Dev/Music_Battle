using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Rotatees guitar when the player is performing an emote

public class guitarAnimator : MonoBehaviour
{
    public Animator playerAnimator;

    public Vector3 DefaultRotation;

    public Vector3 emoteRotation;
    public Vector3 emotePosition;

    Vector3 defaultPosition;
    // Start is called before the first frame update
    void Start()
    {
        // Gets defult position
        defaultPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Emote"))
        {
            transform.localRotation = Quaternion.Euler(emoteRotation);
            transform.localPosition = emotePosition;
            
        }
        else
        {
            transform.localPosition = defaultPosition;
            transform.localRotation = Quaternion.Euler(DefaultRotation);
        }
    }
}
