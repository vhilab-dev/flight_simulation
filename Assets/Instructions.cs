using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This code takes control of the audio instructions in the beginning of the scene.
 * To bypass this, press 'f'.
 *
 */
public class Instructions : MonoBehaviour
{
    public AudioSource fly1;
    public AudioSource fly2;
    public AudioSource fly3;
    public AudioSource fly4;
    public AudioSource fly5;
    public AudioSource fly6;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject lController;
    public GameObject rController;
    public GameObject hmd;

    private bool finished1 = false;
    private bool finished2 = false;
    private bool finished3 = false;
    private bool finished4 = false;
    private bool finished5 = false;
    private bool finished6 = false;
    private bool flipped = false;
    private float timePassed = 0f;
    private Color color;
    private Renderer leftRenderer;
    private Renderer rightRenderer;



    // Start is called before the first frame update
    void Start()
    {
        color = leftArrow.GetComponent<Renderer>().material.color;
        leftRenderer = leftArrow.GetComponent<Renderer>();
        rightRenderer = rightArrow.GetComponent<Renderer>();
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 2f && !finished1)
        {
            if (!fly1.isPlaying)
            {
                fly1.Play();
            }
            if (timePassed >= 15f)
            {
                Debug.Log("Got to showing arrow");
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
            }
            if ((lController.transform.position)[1] - (hmd.transform.position)[1] > 0f)
            {
                Debug.Log("Got to showing arrow color change");
                leftRenderer.material.SetColor("_Color", Color.blue);
                rightRenderer.material.SetColor("_Color", Color.blue);
            }
            else
            {
                Debug.Log("Got to showing arrow color change");
                leftRenderer.material.SetColor("_Color", color);
                rightRenderer.material.SetColor("_Color", color);
            }
        }
        if (timePassed >= 24f && !finished2)
        {
            if (!fly2.isPlaying)
            {
                fly2.Play();
            }
            finished1 = true;
        }
        if (timePassed >= 27f && !finished3)
        {
            if (!fly3.isPlaying)
            {
                fly3.Play();
            }
            finished2 = true;
            if (timePassed >= 28f && !flipped)
            {
                leftArrow.transform.Rotate(-90, 0, 0, Space.Self);
                rightArrow.transform.Rotate(90, 0, 0, Space.Self);
                flipped = !flipped;
            }
            if (Vector3.Distance(lController.transform.position, rController.transform.position) > 1.2f)
            {
                leftRenderer.material.SetColor("_Color", Color.blue);
                rightRenderer.material.SetColor("_Color", Color.blue);
            }
            else
            {
                leftRenderer.material.SetColor("_Color", color);
                rightRenderer.material.SetColor("_Color", color);
            }
        }
        if (timePassed >= 33f && !finished4)
        {
            if (!fly4.isPlaying)
            {
                fly4.Play();
            }
            finished3 = true;
            if (timePassed >= 33f && flipped)
            {
                leftArrow.transform.Rotate(180, 0, 0, Space.Self);
                rightArrow.transform.Rotate(-180, 0, 0, Space.Self);
                flipped = !flipped;
            }
            if (Vector3.Distance(lController.transform.position, rController.transform.position) < 0.25f)
            {
                leftRenderer.material.SetColor("_Color", Color.blue);
                rightRenderer.material.SetColor("_Color", Color.blue);
            }
            else
            {
                leftRenderer.material.SetColor("_Color", color);
                rightRenderer.material.SetColor("_Color", color);
            }
        }
        if (timePassed >= 39f && !finished5)
        {
            if (!fly5.isPlaying)
            {
                fly5.Play();
            }
            finished4 = true;
            if (!flipped)
            {
                leftArrow.transform.Rotate(90, 0, 0, Space.Self);
                rightArrow.transform.Rotate(-90, 0, 0, Space.Self);
                flipped = !flipped;
            }
            if ((lController.transform.position)[1] - (hmd.transform.position)[1] < -0.75f)
            {
                leftRenderer.material.SetColor("_Color", Color.blue);
                rightRenderer.material.SetColor("_Color", Color.blue);
            }
            else
            {
                leftRenderer.material.SetColor("_Color", color);
                rightRenderer.material.SetColor("_Color", color);
            }

        }
        if (timePassed >= 44f && !finished6)
        {
            if (!fly6.isPlaying)
            {
                fly6.Play();
            }
            finished5 = true;
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        if (timePassed >= 50f)
        {
            finished6 = true;
        }

        if (Input.GetKeyDown("f"))
        {
            fly1.Stop();
            fly2.Stop();
            fly3.Stop();
            fly4.Stop();
            fly5.Stop();
            fly6.Stop();
            finished1 = true;
            finished2 = true;
            finished3 = true;
            finished4 = true;
            finished5 = true;
            finished6 = true;
        }
    }
}
