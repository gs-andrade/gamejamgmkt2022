using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IResetable
{
    [Header("Character Attributes")]
    public float Speed;
    public float JumpForce;
    public int lifeMax;

    [Header("Attack")]
    public GameObject Chicote;
    public float AttackCd;

    public static CharacterController Instance;

    private int lifeCurrent;

    private CharacterInstance character;

    private PlayerInput input;

    private bool wasOnAir = true;
    private float attackCd;

    private CharacterState state;

    private float inputDelay;
    private float coyoteJump;

    private Vector2 startPosition;

    private bool isAttacking = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        Setup();
    }

    private bool IsAlive()
    {
        return lifeCurrent > 0;
    }
    public void SetupOnStartLevel()
    {
        
    }

    public void ForceState(CharacterState newState)
    {
        state = newState;
    }

    public void ResetObject()
    {
        if (!IsAlive())
            character.SetAnimationTrigger("Revive");

        lifeCurrent = lifeMax;
        character.SetMovement(Vector2.zero);
        character.transform.position = startPosition;
        AttackFinish();
    }
    public void Setup()
    {
        if (character == null)
        {
            character = GetComponentInChildren<CharacterInstance>(true);
            character.Setup();
        }

        lifeCurrent = lifeMax;

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
        //SoundController.instance.PlayAudioEffect(name);
    }

    public void AttackStart()
    {
        Chicote.SetActive(true);
        FindObjectOfType<AudioManeger>().Play("Ataque", 1);
        attackCd = AttackCd;
    }

    public void AttackFinish()
    {
        isAttacking = false;
        Chicote.SetActive(false);
    }

    public void TakeDamage()
    {
        AttackFinish();

        lifeCurrent--;

        if (!IsAlive())
        {
            character.SetAnimationTrigger("Death");
        }
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

        if (character.IsDisabled())
            return;

        if (!IsAlive())
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
                        if (input.JumpPressed && inputDelay <= 0 && !Chicote.activeInHierarchy)
                        {
                            // isHoldingJumpButton = true;

                            if (grounded || coyoteJump > 0)
                            {
                                character.Jump(JumpForce);
                                inputDelay = 0.2f;
                                coyoteJump = 0;

                                if (input.Horizontal != 0)
                                    horizontalMOvement = input.Horizontal * Speed;

                                //PlayCharacterSound("propulsion-jet-engine");
                            }
                        }
                        else if (input.Horizontal != 0)
                        {
                            if (grounded)
                            {
                                isWalking = true;
                                PlayCharacterSound("pasos");
                            }

                            horizontalMOvement = input.Horizontal * Speed;
                        }
                        else
                        {
                            //SoundController.instance.PlayAudioEffect("pasos", SoundAction.Stop);
                        }


                        if (input.Attack && attackCd <= 0)
                        {
                            character.SetAnimationTrigger("Attack");

                            if (grounded)
                                isAttacking = true;
                        }

                        // character.SetAnimationBool("Attack", false);
                        if (grounded)
                        {
                            //if (isAttacking)
                              //  horizontalMOvement = 0;

                        }
                        character.SetXVelocity(horizontalMOvement, !Chicote.activeInHierarchy);
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