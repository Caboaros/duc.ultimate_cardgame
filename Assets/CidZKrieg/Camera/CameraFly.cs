using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraFly : MonoBehaviour
{
    public Transform  currentMount;
    public int currentSlot = 0;
    public GameObject currentGameObject = new GameObject();
    public GameObject lastGameObject = new GameObject();
    public bool tourModeActive = false;
    public Transform[] mountsMenu = new Transform[1];
    public Transform[] mountsTour = new Transform[1];
    public GameObject menusMountsSwitch = new GameObject();
    public GameObject tourMountsSwitch = new GameObject();
    public float speedFactor = 0.1f;
    public float zoomFactor = 1;
    public Camera cameraComp;


    private Vector3 lastPosition;

    private void Start()
    {
        //menusMountsSwitch = GetComponent<GameObject>();
        //tourMountsSwitch = GetComponent<GameObject>();
        //menusMountsSwitch.SetActive(true);
        //tourMountsSwitch.SetActive(false);

        currentSlot = 0;
        lastPosition = transform.position;
        lastGameObject = currentMount.GetComponent<GameObject>();
    }
    void Update()
    {
        lastGameObject = currentMount.GetComponent<GameObject>();

        if (tourModeActive)
        {
            TourMode();

        }
        else
        {
            currentMount = mountsMenu[currentSlot];
        }

        currentGameObject = currentMount.GetComponent<GameObject>();

        transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);

        float velocity = Vector3.Magnitude(transform.position - lastPosition);
        cameraComp.fieldOfView = 60 + velocity * zoomFactor;

        lastPosition = transform.position;
    }

    void setMount(Transform newMount)
    {
        currentMount = newMount;
    }

    public void BackToMainMenu()
    {
        currentSlot = 0;
        currentMount = mountsMenu[currentSlot];
    }
    public void GoToOptionsMenu()
    {
        currentSlot = 1;
        currentMount = mountsMenu[currentSlot];
    }

    public void SetTourModeActive()
    {
        currentSlot = 0;
        tourModeActive = true;
        //tourMountsSwitch.SetActive(true);
        //menusMountsSwitch.SetActive(false);
        //lastPosition = transform.position;
        Debug.Log("TOUR MODE ON");
    }
    public void SetTourModeInactive()
    {
        currentSlot = 0;
        tourModeActive = false;
        menusMountsSwitch.SetActive(true);
        tourMountsSwitch.SetActive(false);
        //lastPosition = transform.position;

        Debug.Log("TOUR MODE OFF");
    }

    void TourMode()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && currentSlot > 0)
        {
            currentSlot--;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentSlot < mountsTour.Length)
        {
            currentSlot++;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Escape))
        {
            SetTourModeInactive();
        }

        currentMount = mountsTour[currentSlot];
    }
    public void NextSlide()
    {
        
        if (currentSlot < mountsTour.Length)
        {
            currentSlot++;
        }
    }

    public void PreviousSlide()
    {
        if (currentSlot > 0)
        {
            currentSlot--;
        }
    }
}
