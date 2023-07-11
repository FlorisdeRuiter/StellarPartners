using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class RobotMovement : Movement
{
    public override void HandleMovement()
    {
        _appliedMoveDirection.x = CurrentInputDirection.x * _moveSpeed;

        if (!_characterController.isGrounded)
            _appliedMoveDirection.y -= _gravityForce;
    }
}
