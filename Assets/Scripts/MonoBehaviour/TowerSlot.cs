using Statement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerSlot : MonoBehaviour
{
    BattleState _state;
    [SerializeField] private TowerBase _tower;
    [SerializeField] private TextMeshProUGUI _cost;
    public void Init(BattleState state)
    {
        _state = state;
        _cost.text = _tower.Cost.ToString();
        GetComponent<Button>().onClick.AddListener(TryBuyTower);
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    void TryBuyTower()
    {
        if (_state.TrySpendCurrency(_tower.Cost))
        {
            _state.InvokeEntity(_tower);
        }
    }
} 