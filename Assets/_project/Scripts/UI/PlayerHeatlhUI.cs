using DG.Tweening;
using UnityEngine;

public class PlayerHeatlhUI : MonoBehaviour
{
    //public ImageConversion healthBar;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .2f);

    }
    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }

    public void UpdateHealth(float ratio)
    {

    }
}
