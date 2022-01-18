using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialElevator : MonoBehaviour
{
    public GameObject elevator;
    public GameObject player;
    [Tooltip("Where the elevator should be when the\n end animation starts")]
    public Vector3 elevatorEndAnimationPosition;
    [Tooltip("The level the elevator should be at to\ntrigger the end animation of the ride")]
    public float elevatorTrigger;
    [Tooltip("Set a value one point above \"Elevator End Animation\" y-axis")]
    public float endTheElevatorRideTrigger = 9f;
    public FadeToBlack fadeToBlack;
    public string startElevatorBoolName;
    public string endElevatorBoolName;
    private float playerElevatorConst = 2f;// to avoid player falling from platform, position is forced
    private Animator tutorialElevator;
    private bool tutorialFinished = false;
    private bool endAnimation = true;

    private void OnEnable()
    {
        tutorialElevator = GetComponent<Animator>();
    }
    private void FixedUpdate() 
    {
        if (elevator.transform.localPosition.y > elevatorTrigger)
        {
            RideElevatorFinish();
        }

        if (tutorialFinished) fadeToBlack.FadeOut();
        if (elevator.transform.position.y >= endTheElevatorRideTrigger)
            EndElevator();

    }

    public void RideElevatorStart()
    {
        player.transform.parent = elevator.transform;
        tutorialFinished = true;
        tutorialElevator.SetBool(startElevatorBoolName, tutorialFinished);
    }

    private void RideElevatorFinish()
    {
        if (endAnimation)
        {
            elevator.transform.localPosition = elevatorEndAnimationPosition * playerElevatorConst;
            tutorialElevator.SetBool(endElevatorBoolName, endAnimation);
            endAnimation = false;

        }
    }

    private void EndElevator()
    {
        fadeToBlack.FadeIn();
        player.transform.parent = null;
       if(fadeToBlack.FadeIn()) Destroy(this); //Destroy this script to avoid running an update when it is not needed. 
    }

}
