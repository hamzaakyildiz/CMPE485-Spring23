using UnityEngine;

namespace Car
{
    public class AntiRollBar : MonoBehaviour
    {
        public WheelCollider WheelFL;
        public WheelCollider WheelFR;
        public WheelCollider WheelRL;
        public WheelCollider WheelRR;
        public float AntiRoll = 5000.0f;

        void FixedUpdate()
        {
            ApplyAntiRollBar(WheelFL, WheelFR);
            ApplyAntiRollBar(WheelRL, WheelRR);
        }

        void ApplyAntiRollBar(WheelCollider WheelL, WheelCollider WheelR)
        {
            WheelHit hit;
            float travelL = 1.0f;
            float travelR = 1.0f;

            bool groundedL = WheelL.GetGroundHit(out hit);
            if (groundedL)
                travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

            bool groundedR = WheelR.GetGroundHit(out hit);
            if (groundedR)
                travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

            float antiRollForce = (travelL - travelR) * AntiRoll;

            if (groundedR)
                GetComponent<Rigidbody>().AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);
            if (groundedL)
                GetComponent<Rigidbody>().AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);
        

        }
    }
}
