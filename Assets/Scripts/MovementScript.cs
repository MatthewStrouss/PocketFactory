using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private PlayerScript playerScript;
    public TapGestureRecognizer tapGesture;
    public TapGestureRecognizer doubleTapGesture;
    public PanGestureRecognizer panGesture;
    public PanGestureRecognizer actionGesture;
    public ScaleGestureRecognizer scaleGesture;
    public SwipeGestureRecognizer swipeGesture;
    private static readonly float PanSpeed = 0.02f;
    private static readonly float[] BoundsX = new float[] { -8f, 7f };
    private static readonly float[] BoundsY = new float[] { -8f, 7f };
    private static readonly float[] ZoomBounds = new float[] { 3f, 16f };

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        CreateTapGesture();
        CreatePanGesture();
        CreateScaleGesture();
        CreateSwipeGesture();

        panGesture.AllowSimultaneousExecution(scaleGesture);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableBuildMode()
    {
        panGesture.MinimumNumberOfTouchesToTrack = 2;
    }

    public void DisableBuildMode()
    {
        panGesture.MinimumNumberOfTouchesToTrack = 1;
    }

    private void CreateTapGesture()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapGestureCallback;
        tapGesture.StateUpdated += this.playerScript.HandleClick;
        tapGesture.RequireGestureRecognizerToFail = doubleTapGesture;
        FingersScript.Instance.AddGesture(tapGesture);
    }

    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 1;
        panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);

        actionGesture = new PanGestureRecognizer();
        actionGesture.MinimumNumberOfTouchesToTrack = 1;
        actionGesture.StateUpdated += this.playerScript.HandlePan;
        FingersScript.Instance.AddGesture(actionGesture);
    }

    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private void CreateSwipeGesture()
    {
        //swipeGesture = new SwipeGestureRecognizer();
        //swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;
        //swipeGesture.StateUpdated += SwipeGestureCallback;
        //swipeGesture.StateUpdated += this.playerScript.HandleSwipe;
        //swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        //FingersScript.Instance.AddGesture(swipeGesture);
    }

    private void ScaleGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            //DebugText("Scaled: {0}, Focus: {1}, {2}", scaleGesture.ScaleMultiplier, scaleGesture.FocusX, scaleGesture.FocusY);
            //Earth.transform.localScale *= scaleGesture.ScaleMultiplier;

            //cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize / scaleGesture.ScaleMultiplier, ZoomBounds[0], ZoomBounds[1]);
        }
    }

    private void TapGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            //this.IsBuildMode = !this.IsBuildMode;
        }
    }

    private void PanGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            //DebugText("Panned, Location: {0}, {1}, Delta: {2}, {3}", gesture.FocusX, gesture.FocusY, gesture.DeltaX, gesture.DeltaY);

            float deltaX = panGesture.DeltaX / 25.0f;
            float deltaY = panGesture.DeltaY / 25.0f;
            Vector3 pos = transform.position;
            pos.x += -1 * deltaX * PanSpeed * cam.orthographicSize;
            pos.y += -1 * deltaY * PanSpeed * cam.orthographicSize;
            transform.position = pos;
            pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
            pos.y = Mathf.Clamp(transform.position.y, BoundsY[0], BoundsY[1]);
            transform.position = pos;
        }
    }

    private void SwipeGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            //HandleSwipe(gesture.FocusX, gesture.FocusY);
            //DebugText("Swiped from {0},{1} to {2},{3}; velocity: {4}, {5}", gesture.StartFocusX, gesture.StartFocusY, gesture.FocusX, gesture.FocusY, swipeGesture.VelocityX, swipeGesture.VelocityY);
        }
    }
}