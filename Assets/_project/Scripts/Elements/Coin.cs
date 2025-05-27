using System;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private CoinManager _coinManager;
    private FXManager _fxManager;

    private void Start()
    {
        _coinManager = GameDirector.instance.coinManager;
        _fxManager = GameDirector.instance.fxManager;
        transform.DORotate(Vector3.up * 360, 1f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collected();
        }
    }

    private void Collected()
    {
        _coinManager.CoinCollected();
        _fxManager.PlayCoinCollectedFX(transform.position);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

}
