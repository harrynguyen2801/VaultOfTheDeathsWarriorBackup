using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Character_Input : MonoBehaviour
{
    [Header("CharacterFirstDesign Input Values")]
    public Vector2 move;
    public bool jump;
    public bool sprint;
    public bool roll;
    public bool attack;
    public bool callTele;
    public bool guard;
    public bool magic;
    public bool sword;
    public bool openShop;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    
    public void OnOpenShop(InputValue value)
    {
        OpenShopInput(value.isPressed);
    }
    public void OnGuard(InputValue value)
    {
        GuardSkillInput(value.isPressed);
    }
    
    public void OnMagic(InputValue value)
    {
        MagicSkillInput(value.isPressed);
    }
    
    public void OnSword(InputValue value)
    {
        SwordSkillInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
    
    public void OnRoll(InputValue value)
    {
        RollInput(value.isPressed);
    }

    public void OnAttack(InputValue value)
    {
        AttackInput(value.isPressed);
    }
    
    public void OnCallTelePort(InputValue value)
    {
        CallTeleInput(value.isPressed);
    }
    
    private void MoveInput(Vector2 moveDirections)
    {
        move = moveDirections;
    }

    private void JumpInput(bool jumpState) 
    {
        jump = jumpState;
    }
    
    private void OpenShopInput(bool openShopState) 
    {
        openShop = openShopState;
    }
    
    private void RollInput(bool jumpState)
    {
        roll = jumpState;
    }
    
    private void SprintInput(bool sprintState)
    {
        sprint = sprintState;
    }
    
    private void AttackInput(bool attackState)
    {
        attack = attackState;
    }
    
    private void GuardSkillInput(bool guardState)
    {
        guard = guardState;
    }
    
    private void MagicSkillInput(bool magicState)
    {
        magic = magicState;
    }
    
    private void SwordSkillInput(bool swordState)
    {
        sword = swordState;
    }
    private void CallTeleInput(bool teleCall)
    {
        callTele = teleCall;
    }

    public void ClearCache()
    {
        attack = false;
        jump = false;
        sprint = false;
        roll = false;
        callTele = false;
        guard = false;
        openShop = false;
        sword = false;
        magic = false;
    }
}
