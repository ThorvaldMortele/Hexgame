using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommands;
using GameSystem.Views;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    [SerializeField]
    private PositionHelper _positionHelper;

    public PositionHelper PositionHelper => _positionHelper;

    public Board<Piece, Card> Board { get; } = new Board<Piece, Card>(3);

    private Card _selectedCard = null;
    public Card SelectedCard => _selectedCard;

    public MoveManager<Piece, Card> MoveManager { get; internal set; }

    public Tile HoveredTile;


    private void Awake()
    {
        MoveManager = new MoveManager<Piece, Card>(Board);

        MoveManager.Register(PlayerMoveSweepCommand.Name, new PlayerMoveSweepCommand());
        MoveManager.Register(PlayerMoveAxialCommand.Name, new PlayerMoveAxialCommand());
        MoveManager.Register(PlayerMovePushCommand.Name, new PlayerMovePushCommand());
        MoveManager.Register(PlayerMoveTeleportCommand.Name, new PlayerMoveTeleportCommand());
    }

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

            Board.Place(tile, piece);

            pieceView.Model = piece;
        }

        var cardViews = FindObjectsOfType<CardView>();
        foreach (var cardView in cardViews)
        {
            var card = new Card();

            MoveManager.Register(card, cardView.MovementName);

            cardView.ModelCard = card;
        }
    }

    public Tile FindPlayerTile()
    {
        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
        {
            if (pieceView.IsPlayer == true) // if we find a player, find the tile it's on
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

                var playerTile = Board.TileAt(boardPosition);

                return playerTile;
            }
        }
        return null;
    }

    public void Select(Card card)
    {
        Board.UnHighlightAll(MoveManager.Tiles());

        MoveManager.Deactivate();

        _selectedCard = card;

        MoveManager.Activate(card);

        Board.HighlightAll(MoveManager.Tiles());
    }

    public void ShowAllAvailableTiles(Card modelcard)  //toont alle validtiles die mogelijk kunnen zijn voor die move
    {
        Select(modelcard);
    }

    public void SelectActivate(Tile tile) // dropping
    {
        if (_selectedCard != null)
        {
            MoveManager.Execute(_selectedCard, tile);
        }
    }
}
