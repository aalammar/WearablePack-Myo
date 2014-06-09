using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;
using VibrationType = Thalmic.Myo.VibrationType;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class ColorBoxByPose2 : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    // Materials to change to when poses are made.
    public Material waveInMaterial;
    public Material waveOutMaterial;
    public Material twistInMaterial;
	public Material fistMaterial;
	public Material restMaterial;
	public Material fingersspreadMaterial;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    // Update is called once per frame.
    void Update ()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;

			// Vibrate the Myo armband when a fist is made.
            if (thalmicMyo.pose == Pose.Fist) {
                thalmicMyo.Vibrate (VibrationType.Medium);
				renderer.material = fistMaterial;

            // Change material when wave in, wave out or twist in poses are made.
			} else if (thalmicMyo.pose == Pose.Rest) {
				renderer.material = restMaterial;
			} else if (thalmicMyo.pose == Pose.WaveIn) {
                renderer.material = waveInMaterial;
            } else if (thalmicMyo.pose == Pose.WaveOut) {
                renderer.material = waveOutMaterial;
            } else if (thalmicMyo.pose == Pose.TwistIn) {
                renderer.material = twistInMaterial;
			} else if (thalmicMyo.pose == Pose.FingersSpread) {
				renderer.material = fingersspreadMaterial;
            }
        }
    }
}
