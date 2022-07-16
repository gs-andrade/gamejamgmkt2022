using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IResetable
{
    [Header("Character Attributes")]
    public float Speed;
    public float JumpForce;

    [Header("Attack")]
    public GameObject Chicote;
    public float AttackCd;


    private float tempAttackDuration;

    public static CharacterController Instance;

    private CharacterInstance character;

    private PlayerInput input;

    private bool wasOnAir = true;
    private float attackCd;

    private CharacterState state;

    private float inputDelay;
    private float coyoteJump;

    private Vector2 startPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        Setup();
    }

    public void SetupOnStartLevel()
    {
        //Setup();
    }

    public void ForceState(CharacterState newState)
    {
        state = newState;
    }

    public void ResetObject()
    {
        character.SetMovement(Vector2.zero);
        character.transform.position = startPosition;
    }
    public void Setup()
    {
        if (character == null)
        {
            character = GetComponentInChildren<CharacterInstance>(true);
            character.Setup();
        }

        if (input == null)
            input = new PlayerInput();

        state = CharacterState.Normal;
    }

    public void SetStartPosition(Vector2 startPosition)
    {
        this.startPosition = startPosition;
        ResetObject();
    }

    public CharacterInstance GetPlayer()
    {
        return character;
    }

    public bool IsGrounded()
    {
        return character.CheckIfIsOnGround();
    }

    private void PlayCharacterSound(string name)
    {
        SoundController.instance.PlayAudioEffect(name);
    }

    public void UpdateCharacter()
    {
        //PlayCharacterSound("pasos");

        if (inputDelay > 0)
            inputDelay -= Time.deltaTime;

        if (attackCd > 0)
            attackCd -= Time.deltaTime;

        if (coyoteJump > 0)
            coyoteJump -= Time.deltaTime;

        if (tempAttackDuration > 0)
            tempAttackDuration -= Time.deltaTime;
        else
            Chicote.SetActive(false);

        if (character.IsDisabled())
            return;

        input.GetInputs();

        bool isWalking = false;

        if (character.moveChar == true)
        {
            switch (state)
            {
                case CharacterState.Normal:
                    {
                        character.SetAnimationBool("IsDeath", false);

                        var horizontalMOvement = 0f;
                        var grounded = character.CheckIfIsOnGround();

                        if (grounded)
                        {
                            coyoteJump = 0.25f;

                            character.SetAnimationBool("DoubleJ", false);
                            character.SetAnimationBool("IsJumping", false);
                        }
                        else
                        {
                           // SoundController.instance.PlayAudioEffect("pasos", SoundAction.Stop);
                            character.SetAnimationBool("IsJumping", true);
                        }



                        wasOnAir = !grounded;
                        // JUMP
                        if (input.JumpPressed && inputDelay <= 0)
                        {
                            // isHoldingJumpButton = true;

                            if (grounded || coyoteJump > 0)
                            {
                                character.Jump(JumpForce);
                                inputDelay = 0.2f;
                                coyoteJump = 0;

                                //PlayCharacterSound("propulsion-jet-engine");
                            }
                        }
                        else if (input.Horizontal != 0)
                        {
                            if (grounded)
                                isWalking = true;

                            horizontalMOvement = input.Horizontal * Speed;

                            if (grounded)
                                PlayCharacterSound("pasos");
                        }
                        else
                        {
                            SoundController.instance.PlayAudioEffect("pasos", SoundAction.Stop);
                        }

                        character.SetXVelocity(horizontalMOvement);

                        if (input.Attack && attackCd <= 0)
                        {
                            Chicote.SetActive(true);
                            attackCd = AttackCd;
                            tempAttackDuration = 0.25f;
                        }

                        character.SetAnimationBool("IsWalking", isWalking);

                        break;
                    }

                case CharacterState.SnapToGround:
                    {
                        character.SnapToGround();
                        break;
                    }

                case CharacterState.Death:
                    {
                        character.SetAnimationBool("IsDeath", true);
                        break;
                    }


            }
        }

    }


}


public enum CharacterState
{
    Normal,
    SnapToGround,
    Death
}