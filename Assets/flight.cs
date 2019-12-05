using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/*
 * This code allows for a participant to fly throughout a city either as ironman or superman.
 * To allow the user to begin flying, press the 'f' key.
 * Easter Egg. To fly as as ironman, press 's'.
 */
public class flight : MonoBehaviour
{
    public GameObject lController;
    public GameObject rController;
    public GameObject hmd;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources rightHand;
    public SteamVR_Input_Sources hands;
    public SteamVR_Action_Boolean trigger;
    public AudioSource flightAudio;
    public float thrust = 7.0f;
    public float cap = 15.0f;
    public float maxSpeed = 5.0f;
    public float speedScalar = 6.0f;



    private Rigidbody rb;
    private bool isSuperman = true; //Determines whether user flies as superman or ironman. Press 's' to switch.
    private bool canStart = false;
    private float timeLeft = 50.0f;
    private bool flipped = true;
    private float multiplier = -1.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft < -2 || Input.GetKeyDown("f"))
        {
            // User cannot begin flying until experimenter presses the f key.
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            canStart = true;
        }

        if (Input.GetKeyDown("s"))
        {
            // User will onyl fly as in the traditional flight simulation until experimenter presses the 's' key.
            isSuperman = !isSuperman;
            rb.velocity = Vector3.zero;
        }

        timeLeft -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        if (canStart == false)
        {
            //pass do nothing because we have not started
        }
        // ironman flight simulation
        else if (isSuperman == false)
        {

            rb.useGravity = true;
            if (rb.velocity.magnitude > cap)
            {
                rb.velocity = rb.velocity.normalized * cap;
            }

            // hold triggers to activate thrusters. Each hand is independently controlled.  
            addThruster(leftHand, lController.transform.rotation);
            addThruster(rightHand, rController.transform.rotation);
            playSoundsRegular(cap, rb.velocity.magnitude, isSuperman, flightAudio);
        }
        // Superman flight simulation.
        else
        {
            rb.useGravity = false;

            float distance = Vector3.Distance(lController.transform.position, rController.transform.position);
            float speed = Mathf.Max(maxSpeed - distance * speedScalar, 0.5f);

            Vector3 heading = (lController.transform.position + rController.transform.position) / 2.0f - hmd.transform.position;
            Vector3 direction = heading.normalized;

            Vector3 speedVector = speed * direction;
            rb.velocity = speedVector;
            playSoundsRegular(maxSpeed, speed, isSuperman, flightAudio);
        }
    }

    private void playSoundsRegular(float maxSpeed, float speed, bool isSuperman, AudioSource flightAudio)
    {

        //Allows user to control volume of flight. Volume of flight is scaled by how fast user is flying.

        if (isSuperman || (!isSuperman && trigger.GetState(hands)))
        {
            float volume = speed / (1.5f * maxSpeed);
            if (!isSuperman)
            {
                volume = Mathf.Max(0.2f, volume);
            }
            flightAudio.volume = volume;
            if (!flightAudio.isPlaying)
            {
                flightAudio.Play();
            }
        }
    }


    private void addThruster(SteamVR_Input_Sources hand, Quaternion rot)
    {
        if (trigger.GetState(hand))
        {
            Vector3 forward = rot * Vector3.forward;
            rb.AddForce(-1.0f * forward * thrust);
        }
    }
}