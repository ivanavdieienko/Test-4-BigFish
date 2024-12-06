using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AddMoneyPopup : MonoBehaviour
{
    [SerializeField]
    private Button buttonClose;

    private void OnEnable()
    {
        buttonClose.onClick.AddListener(OnCloseClick);
    }

    private void OnDisable()
    {
        buttonClose.onClick.RemoveListener(OnCloseClick);
    }

    private void OnCloseClick()
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<AddMoneyPopup>
    {
    }
}
