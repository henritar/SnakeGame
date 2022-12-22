using Project.Snake.UMVCS.Model;
using UnityEngine;

public class SnakePlayerModel : SnakeModel
{
    [SerializeField]
    private MainModel _mainModelRef;
    public MainModel MainModelRef { get => _mainModelRef; set => _mainModelRef = value; }
}
