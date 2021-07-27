using BoardSystem;
using GameSystem.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    [SerializeField]
    private PositionHelper _positionHelper;
    public Board Board { get; } = new Board(3);

}
