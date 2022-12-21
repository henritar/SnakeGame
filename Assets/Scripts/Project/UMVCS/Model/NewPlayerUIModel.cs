using Architectures.UMVCS.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerUIModel : BaseModel
{
    [SerializeField]
    private TextMeshProUGUI _playerTag;
    [SerializeField]
    private TextMeshProUGUI _leftKey;
    [SerializeField]
    private TextMeshProUGUI _rightKey;
    [SerializeField]
    private Image _sprite;
    public TextMeshProUGUI PlayerTag { get => _playerTag; set => _playerTag = value; }
    public TextMeshProUGUI LeftKey { get => _leftKey; set => _leftKey = value; }
    public TextMeshProUGUI RightKey { get => _rightKey; set => _rightKey = value; }
    public Image Sprite { get => _sprite; set => _sprite = value; }
}
