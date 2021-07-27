using BoardSystem;
using GameSystem.Models;
using GameSystem.Views;
using System;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    private Piece _currentSelection = null;

    [SerializeField]
    private PositionHelper _positionHelper;
    public Board<Piece> Board { get; } = new Board<Piece>(3);

    private void Start()
    {
        ConnectViewsToModel();
    }

    private void ConnectViewsToModel()
    {
        var pieces = FindObjectsOfType<Piece>();
        foreach (var piece in pieces)
        {
            var worldPosition = piece.transform.position;
            var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

            var tile = Board.TileAt(boardPosition);
            Board.Place(tile, piece);   //this fills up the keys and values list in Board, doesnt do anything visually yet
        }
    }

    public void Select(Piece piece)
    {
        if (piece == null || piece == _currentSelection)
            return;

        if (piece != null)
        {
            var tile = Board.TileOf(piece);
            Select(tile);
        }

        _currentSelection?.Deselect();
        _currentSelection = piece;
    }

    public void Select(Tile tile)
    {
        if (_currentSelection != null)
        {
            if (_currentSelection.Move(tile))
            {
                _currentSelection.Deselect();
                _currentSelection = null;
            }
        }
    }
}
