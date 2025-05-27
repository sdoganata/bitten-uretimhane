using DG.Tweening;
using UnityEngine;

public class Serum : MonoBehaviour
{

    private void Start()
    {
        transform.DOMoveY(.5f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, .2f).SetEase(Ease.OutBack);

    }

    private void OnTriggerEnter(Collider other)
    {
        print("collided");
        if (other.CompareTag("Player"))
        {
            GameDirector.instance.LevelCompleted();
            GameDirector.instance.fxManager.PlaySerumCollectedFX(transform.position);
            gameObject.SetActive(false);
        }

    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
