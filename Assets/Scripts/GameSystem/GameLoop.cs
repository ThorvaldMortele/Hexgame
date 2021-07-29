using BoardSystem;
using GameSystem.Models;
using GameSystem.Models.MoveCommands;
using GameSystem.MoveCommandProviders;
using GameSystem.Views;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    private Piece _selectedPiece = null;

    [SerializeField]
    private PositionHelper _positionHelper = null;

    //PlayerMoveSweepCommand _playerMovement = new PlayerMoveSweepCommand();

    public bool IsPlayerTurn { get; internal set; } = true;

    public MoveManager<Piece> MoveManager { get; internal set; }

    public Board<Piece> Board { get; } = new Board<Piece>(3);

    private void Awake()
    {
        MoveManager = new MoveManager<Piece>(Board);
        MoveManager.Register(PlayerSweepMoveCommandProvider.Name, new PlayerSweepMoveCommandProvider());    //we need to register a card associated with a move
    }                                                                                                       //here the first parameter says this name and the second 
                                                                                                            //says is associated with this move
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

            MoveManager.Register(piece, pieceView.MovementName);

            pieceView.Model = piece;
        }
    }

    public void Select(Piece piece)
    {
        if (piece == null || piece == _selectedPiece)
            return;

        if (piece != null && piece.IsPlayer != IsPlayerTurn )  //if theres a piece and its not ours, then we want to select the tile below it
        {
            var tile = Board.TileOf(piece); //THIS MIGHT BREAK THIS METHOD CHECK IT
            Select(tile);
        }
        else
        {
            Board.UnHighlight(MoveManager.Tiles()); //deselect the previous tiles

            MoveManager.Activate(_selectedPiece);

            Board.Highlight(MoveManager.Tiles());   //highlight the new valid tiles
        }
    }

    public void Select(Tile tile) //maybe give the move in a parameter with the player (or the validtiles idk)
    {
        if (_selectedPiece != null)
        {
            MoveManager.Execute(_selectedPiece, tile);

            Board.UnHighlight(MoveManager.Tiles());

            MoveManager.Deactivate();

            _selectedPiece = null;
        }
    }
}
