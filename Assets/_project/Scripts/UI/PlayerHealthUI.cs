using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image healthBar;
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
        
        healthBar.DOKill();
        healthBar.DOFillAmount(ratio, .2f);
        healthBar.color = new Color(.64f, .15f, .15f, 1);
        healthBar.DOColor(Color.white, .1f).SetLoops(2, LoopType.Yoyo);
        //healthBar.fillAmount = ratio;
    }
}
