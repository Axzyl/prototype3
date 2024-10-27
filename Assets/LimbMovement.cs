using UnityEngine;

public class LimbMovement : MonoBehaviour
{
    [SerializeField] Transform targetLeftArm;
    [SerializeField] Transform targetRightArm;
    [SerializeField] Transform targetLeftLeg;
    [SerializeField] Transform targetRightLeg;

    [SerializeField] Transform leftArmAnchor;
    [SerializeField] Transform rightArmAnchor;
    [SerializeField] Transform leftLegAnchor;
    [SerializeField] Transform rightLegAnchor;

    [SerializeField] float maxLegDistance;
    [SerializeField] float maxArmDistance;

    Animator animator;

    public enum Limb
    {
        LeftArm, RightArm, LeftLeg, RightLeg,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 

        
    }

    /*
     * Function to move a specified limb. 
     * Movement is the raw movement in that specific frame, please use Time.fixedDeltaTime
     * 
     */
    public void MoveLimb(Limb limb, Vector3 movement)
    {
        switch (limb) {
            case Limb.LeftArm:
                targetLeftArm.localPosition += movement;
                break;
            case Limb.RightArm: 
                targetRightArm.localPosition += movement; 
                break;
            case Limb.LeftLeg:
                targetLeftLeg.localPosition += movement;
                break;
            case Limb.RightLeg:
                targetRightLeg.localPosition += movement;
                break;
        }

        if (Vector3.SqrMagnitude(targetLeftArm.position - leftArmAnchor.position) > Mathf.Pow(maxArmDistance, 2))
        {
            targetLeftArm.position = leftArmAnchor.position + Vector3.Normalize(targetLeftArm.position - leftArmAnchor.position) * maxArmDistance;
        }

        if (Vector3.SqrMagnitude(targetRightArm.position - rightArmAnchor.position) > Mathf.Pow(maxArmDistance, 2))
        {
            targetRightArm.position = rightArmAnchor.position + Vector3.Normalize(targetRightArm.position - rightArmAnchor.position) * maxArmDistance;
        }

        if (Vector3.SqrMagnitude(targetLeftLeg.position - leftLegAnchor.position) > Mathf.Pow(maxLegDistance, 2))
        {
            targetLeftLeg.position = leftLegAnchor.position + Vector3.Normalize(targetLeftLeg.position - leftLegAnchor.position) * maxLegDistance;
        }

        if (Vector3.SqrMagnitude(targetRightLeg.position - rightLegAnchor.position) > Mathf.Pow(maxLegDistance, 2))
        {
            targetRightLeg.position = rightLegAnchor.position + Vector3.Normalize(targetRightLeg.position - rightLegAnchor.position) * maxLegDistance;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // Set the target position and rotation for the limb

            if (targetLeftArm != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, targetLeftArm.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, targetLeftArm.rotation);
            }

            if (targetRightArm != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, targetRightArm.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, targetRightArm.rotation);
            }

            if (targetLeftLeg != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, targetLeftLeg.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, targetLeftLeg.rotation);
            }

            if (targetRightLeg != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, targetRightLeg.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, targetRightLeg.rotation);
            }
        }

    }
}
