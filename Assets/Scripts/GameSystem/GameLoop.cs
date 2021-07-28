using BoardSystem;
using GameSystem.Models;
using GameSystem.Models.Moves;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    private Piece _currentSelection = null;

    [SerializeField]
    private PositionHelper _positionHelper = null;

    private List<Tile> _validTiles = new List<Tile>();

    PlayerMovement _playerMovement = new PlayerMovement();

    public bool IsPlayerTurn { get; internal set; } = true;

    public Board<Piece> Board { get; } = new Board<Piece>(3);

    private void Start()
    {
        ConnectViewsToModel();
    }

    private void ConnectViewsToModel()
    {
        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
        {
            var worldPosition = pieceView.transform.position;
            var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

            var tile = Board.TileAt(boardPosition);

            var piece = new Piece(pieceView.IsPlayer);

            Board.Place(tile, piece);   //this fills up the keys and values list in Board, doesnt do anything visually yet

            pieceView.Model = piece;
        }
    }

    public void Select(Piece piece)
    {
        if (piece == null || piece == _currentSelection)
            return;

        if (piece != null && piece.IsPlayer != IsPlayerTurn )  //if theres a piece and its not ours, then we want to select the tile below it
        {
            var tile = Board.TileOf(piece); //THIS MIGHT BREAK THIS METHOD CHECK IT
            Select(tile);
        }
        else
        {
            Board.UnHighlight(_validTiles); //deselect the previous tiles

            _currentSelection = piece;      //set the piece as the new one (the one passes in the parameter)

            _validTiles = _playerMovement.Tiles(Board, _currentSelection); //find the valid tiles for it

           Board.Highlight(_validTiles);   //highlight the new valid tiles
        }
    }

    public void Select(Tile tile) //maybe give the move in a parameter with the player (or the validtiles idk)
    {
        if (_currentSelection != null)
        {
            if (_validTiles.Contains(tile))
            {
                _playerMovement.Move(Board, _currentSelection, tile);

                Board.UnHighlight(_validTiles);
                _validTiles.Clear();

                _currentSelection = null;
            }
        }
    }
}
