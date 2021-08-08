using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommands;
using GameSystem.Utils;
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

    public Card SelectedCard = null;

    public MoveManager<Piece, Card> MoveManager { get; internal set; }

    public GenerateCards CardDeck { get; internal set; }

    public Tile HoveredTile;

    private void Awake()
    {
        MoveManager = new MoveManager<Piece, Card>(Board);
        CardDeck = FindObjectOfType<GenerateCards>();

        MoveManager.Register(PlayerMoveSweepCommand.Name, new PlayerMoveSweepCommand());
        MoveManager.Register(PlayerMoveAxialCommand.Name, new PlayerMoveAxialCommand());
        MoveManager.Register(PlayerMovePushCommand.Name, new PlayerMovePushCommand());
        MoveManager.Register(PlayerMoveTeleportCommand.Name, new PlayerMoveTeleportCommand());
    }

    private void Start()
    {
        CardDeck.GenerateCardPile(MoveManager);
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

        SelectedCard = card;

        MoveManager.Activate(card);

        Board.HighlightAll(MoveManager.Tiles());
    }

    public void ShowAllAvailableTiles(Card modelcard)  //toont alle validtiles die mogelijk kunnen zijn voor die move
    {
        Select(modelcard);
    }

    public void SelectActivate(Tile tile) // dropping
    {
        if (SelectedCard != null)
        {
            MoveManager.Execute(SelectedCard, tile);
        }
    }
}
