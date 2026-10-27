using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode moveFowardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterwiseKey;
    public KeyCode shootKey;
    public KeyCode sneakKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Add(this);
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetKey(moveFowardKey)) {pawn.MoveForward();};
        if (Input.GetKey(moveBackwardKey)) {pawn.MoveBackward();};
        if (Input.GetKey(rotateClockwiseKey)) {pawn.RotateClockwise();};
        if (Input.GetKey(rotateCounterwiseKey)) {pawn.RotateCounterClockwise();};
        if (Input.GetKey(shootKey)) {pawn.Shoot();};
        if (Input.GetKeyDown(sneakKey)) {pawn.SneakToggle(true);};
        if (Input.GetKeyUp(sneakKey)) {pawn.SneakToggle(false);};
        base.Update();
    }
}
