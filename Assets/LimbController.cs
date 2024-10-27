using TMPro;
using UnityEngine;

public class LimbController : MonoBehaviour
{
    [SerializeField] LimbMovement limbMovement;
    [SerializeField] TMP_Text limbText;
    [SerializeField] float mouseSpeed = 0.1f;      // Speed factor for X and Z movement
    [SerializeField] float scrollSpeed = 1.0f;     // Speed factor for Y movement
    [SerializeField] Transform torso;

    LimbMovement.Limb currentLimb = LimbMovement.Limb.RightLeg;
    int currentLimbIndex = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ChangeLimb();
    }

    void ChangeLimb()
    {
        switch (currentLimbIndex)
        {
            case 0:
                currentLimb = LimbMovement.Limb.LeftArm;
                limbText.text = "Current Limb: Left Arm";
                break;
            case 1:
                currentLimb = LimbMovement.Limb.RightArm;
                limbText.text = "Current Limb: Right Arm";
                break;
            case 2:
                currentLimb = LimbMovement.Limb.LeftLeg;
                limbText.text = "Current Limb: Left Leg";
                break;
            case 3:
                currentLimb = LimbMovement.Limb.RightLeg;
                limbText.text = "Current Limb: Right Leg";
                break;

        }
    }

    private void Update()
    { 

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLimbIndex == 0) currentLimbIndex = 3;
            else currentLimbIndex -= 1;
            ChangeLimb();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLimbIndex == 3) currentLimbIndex = 0;
            else currentLimbIndex += 1;
            ChangeLimb();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1)) return;

        // Capture mouse movement for X and Z axes
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
        float mouseZ = Input.GetAxis("Mouse Y") * mouseSpeed;

        // Capture scroll wheel input for Y-axis movement
        float scrollY = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        // Update the target position based on input
        Vector3 movement = new Vector3(mouseX, scrollY, mouseZ);

        // Apply the updated position to the target GameObject
        limbMovement.MoveLimb(currentLimb, movement*Time.fixedDeltaTime);
    }
}
