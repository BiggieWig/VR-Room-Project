using System;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [Header("Clock Hand Transforms")]
    [SerializeField] private Transform hoursHand;
    [SerializeField] private Transform minutesHand;
    [SerializeField] private Transform secondsHand;

    [Header("Axis Settings")]
    [Tooltip("The axis pointing outward from the clock face (usually forward (Z) or back (-Z)).")]
    [SerializeField] private Vector3 rotationAxis = Vector3.forward;

    [Tooltip("Toggle if the hands rotate counter-clockwise instead of clockwise.")]
    [SerializeField] private bool reverseDirection = false;

    // Standard degrees of rotation
    private const float DegreesPerHour = 360f / 12f;      // 30 degrees
    private const float DegreesPerMinute = 360f / 60f;    // 6 degrees
    private const float DegreesPerSecond = 360f / 60f;    // 6 degrees

    void Update()
    {
        DateTime now = DateTime.Now;

        // Calculate smooth continuous angles based on current time
        float hours = (now.Hour % 12) + (now.Minute / 60f) + (now.Second / 3600f);
        float minutes = now.Minute + (now.Second / 60f);
        float seconds = now.Second + (now.Millisecond / 1000f);

        float direction = reverseDirection ? -1f : 1f;

        // Apply rotation relative to starting/local rotation
        if (hoursHand != null)
        {
            hoursHand.localRotation = Quaternion.AngleAxis(direction * hours * DegreesPerHour, rotationAxis);
        }

        if (minutesHand != null)
        {
            minutesHand.localRotation = Quaternion.AngleAxis(direction * minutes * DegreesPerMinute, rotationAxis);
        }

        if (secondsHand != null)
        {
            secondsHand.localRotation = Quaternion.AngleAxis(direction * seconds * DegreesPerSecond, rotationAxis);
        }
    }
}