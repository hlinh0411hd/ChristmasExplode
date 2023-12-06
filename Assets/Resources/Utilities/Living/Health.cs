using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public ParticleSystem hurt;
    public ParticleSystem die;

    public AnimationMachine animationMachine;

    public AudioSource audioSource;

    float isImmue;

    void Awake(){
        animationMachine = GetComponent<AnimationMachine>();
    }

    private void Start()
    {
        isImmue = 0;
    }

    void Update(){
        if (isImmue > 0){
            isImmue -= Time.deltaTime;
        }
    }

    public void SetImmue(float i)
    {
        isImmue = i;
    }

    public void ChangeHealth(float h)
    {
        if (isImmue > 0 && h < 0)
        {
            return;
        }
        if (health <= 0)
        {
            return;
        }
        health += h;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            health = 0;
        }
        if (h < 0)
        {
            GetHurt();
        }
        if (health <= 0)
        {
            GetDie();
        }
    }

    public void GetHurt()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(spriteRenderer.DOColor(Color.red, 0.2f));
            sequence.PrependInterval(0.2f);
            sequence.Append(spriteRenderer.DOColor(Color.white, 0.2f));
            sequence.PrependInterval(0.2f);
            sequence.SetLoops(3, LoopType.Yoyo);
        }
        if (hurt != null)
        {
            Debug.Log("play hurt");
            hurt.Play();
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
        animationMachine?.ChangeState(AnimationState.HURT, 0.5f);
    }

    public void GetDie()
    {
        if (die != null)
        {
            die.Play();
        }
        animationMachine?.ChangeState(AnimationState.DIE, 0.5f);
    }
}
