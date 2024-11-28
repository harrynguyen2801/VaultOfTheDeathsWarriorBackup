using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBossControl : MonoBehaviour
{
    private Enemy _enemy;
    public List<GameObject> listMeteorStrike;

    private void OnEnable()
    {
        ActionManager.OnBossChangePhase += OnBossChangePhase;
    }
    
    private void OnDisable()
    {
        ActionManager.OnBossChangePhase -= OnBossChangePhase;
    }
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    private void OnBossChangePhase(int phase)
    {
        
    }
    public void DragonFly()
    {
        StartCoroutine(ChangeToDragonFlyAttack());
    }

    IEnumerator ChangeToDragonFlyAttack()
    {
        yield return new WaitForSeconds(1f);
        _enemy.SetAnimatorClip(AnimationManager.Instance.animIDFlyAttack);
        yield return new WaitForSeconds(.75f);
        DragonFlyAttack();
        yield return new WaitForSeconds(7f);
        _enemy.SetAnimatorClip(AnimationManager.Instance.animIDLand);
    }

    private void DragonFlyAttack()
    {
        StartCoroutine(MeteorAttack());
    }
    
    IEnumerator MeteorAttack()
    {
        foreach (var meteor in listMeteorStrike)
        {
            meteor.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    private void DragonFlyToNormal()
    {
        _enemy.DisableInvincibility();
        GetComponent<Character>().SwitchStateTo(Character.CharacterState.Normal);
        MainSceneManager.Instance.ZoomInCamera();
    }
}
