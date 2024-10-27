using UnityEngine;

public class TorsoBalancer : MonoBehaviour
{
    public Transform hipsTarget;
    public Transform torsoTarget; //spine
    public Transform chestTarget; //upper chest

    public Transform leftLeg;
    public Transform rightLeg;
    public Transform leftArm;
    public Transform rightArm;

    public float followSpeed = 0.1f;

    void Update()
    {
        // Calculate an average balance point based on leg and arm positions
        Vector3 balancePoint = CalculateBalancePoint();

        Debug.Log(balancePoint);

        // Move the hips target towards the balance point
        hipsTarget.position = Vector3.Lerp(hipsTarget.position, balancePoint, followSpeed * Time.deltaTime);

        //// Offset the torso and chest to maintain a slight upward position from the hips
        //Vector3 torsoPosition = balancePoint + Vector3.up * 0.5f;
        //torsoTarget.position = Vector3.Lerp(torsoTarget.position, torsoPosition, followSpeed * Time.deltaTime);

        //Vector3 chestPosition = balancePoint + Vector3.up * 1.0f;
        //chestTarget.position = Vector3.Lerp(chestTarget.position, chestPosition, followSpeed * Time.deltaTime);
    }

    Vector3 CalculateBalancePoint()
    {
        // Calculate an average position of the limbs to use as a dynamic center of mass
        return (leftLeg.position + rightLeg.position) / 2;
    }
}

