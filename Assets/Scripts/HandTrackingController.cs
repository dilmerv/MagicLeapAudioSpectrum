using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;
using UnityLWRPEssentials.Assets.Scripts.Core;

namespace MagicLeap
{
    [RequireComponent(typeof(HandTracking))]
    public class HandTrackingController : MonoBehaviourSingleton<HandTrackingController>
    {
    
        [SerializeField, Tooltip("Text to display gesture status to.")]
        private Text _statusText = null;

        [SerializeField]
        private float KeyPoseConfidenceValue = 0.6f;

        [SerializeField]
        private UnityEvent onFistKeyPoseDetected;

        [SerializeField]
        private UnityEvent onOpenHandBackKeyPoseDetected;

        void Awake()
        {
            if(_statusText == null)
            {
                Debug.LogError("Status and Mute State Text needs to be set");
                enabled = false;
                return;
            }
        }

        void Update()
        {
            if (MLHands.IsStarted)
            {
                _statusText.text = string.Format(
                    "Hand Gestures\nLeft: {0}, {2}% confidence\nRight: {1}, {3}% confidence, LHC {4}, RHC {5}",
                    MLHands.Left.KeyPose.ToString(),
                    MLHands.Right.KeyPose.ToString(),
                    (MLHands.Left.KeyPoseConfidence * 100.0f).ToString("n0"),
                    (MLHands.Right.KeyPoseConfidence * 100.0f).ToString("n0"),
                    MLHands.Left.KeyPoseConfidence.ToString(),
                    MLHands.Right.KeyPoseConfidence.ToString());

                if((MLHands.Left.KeyPose.IsFist() && MLHands.Left.KeyPoseConfidence >= KeyPoseConfidenceValue) ||
                MLHands.Right.KeyPose.IsFist() && MLHands.Right.KeyPoseConfidence >= KeyPoseConfidenceValue)
                {
                    onFistKeyPoseDetected?.Invoke();
                }

                if((MLHands.Left.KeyPose.IsOpenHandBack() && MLHands.Left.KeyPoseConfidence >= KeyPoseConfidenceValue) ||
                MLHands.Right.KeyPose.IsOpenHandBack()  && MLHands.Right.KeyPoseConfidence >= KeyPoseConfidenceValue)
                {
                    onOpenHandBackKeyPoseDetected?.Invoke();
                }
            }
        }
    }
}
