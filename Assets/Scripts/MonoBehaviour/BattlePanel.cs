using Statement;
using TMPro;
using UnityEngine;

public class BattlePanel : MonoBehaviour
{
    BattleState _state;
    [SerializeField] TextMeshProUGUI _currencyTitle;
    [SerializeField] TowerSlot[] _slots;

    public void Init(BattleState state)
    {
        _state = state;

        _state.OnCurrencyChanged += CurrencyChange;

        _slots = GetComponentsInChildren<TowerSlot>();

        foreach (var slot in _slots)
        {
            slot.Init(state);
        }
    }

    private void OnDestroy()
    {
        _state.OnCurrencyChanged -= CurrencyChange;
    }

    void CurrencyChange(int value)
    {
        _currencyTitle.text = value.ToString();
    }
}
