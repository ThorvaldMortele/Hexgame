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
    [SerializeField]
    private PositionHelper _positionHelper;
    public PositionHelper PositionHelper => _positionHelper;

    public Board<Piece, Card> Board { get; } = new Board<Piece, Card>(3);

    private Piece _selectedPiece = null;
    public Piece SelectedPiece => _selectedPiece;

    private Card _selectedCard = null;
    public Card SelectedCard => _selectedCard;

    private IMoveCommand<Piece, Card> _currentMoveCommand;
    public IMoveCommand<Piece, Card> CurrentMoveCommand => _currentMoveCommand;

    public MoveManager<Piece, Card> MoveManager { get; internal set; }

    private void Awake()
    {
        MoveManager = new MoveManager<Piece, Card>(Board);

        //Movemanager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider());
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
            //Movemanager.Register(piece, pieceView.MovementName); // CARD

            pieceView.Model = piece;
        }


        var cardViews = FindObjectsOfType<CardView>();
        foreach (var cardView in cardViews)
        {
            var card = new Card();

            MoveManager.Register(card, cardView.MovementName);

            cardView.ModelCard = card;


            // (check MoveCommandProviderView van chess), daar connecteer je de IMoveCommand aan elke Card
            var cardMoves = MoveManager.Provider(card).MoveCommands();
            foreach (var cardIMove in cardMoves)
            {
                cardView.ModelMove = cardIMove;
            }
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
    //public Tile FindHoverTile() // pretty taxing, should be done differently, but will do for now
    //{
    //    var tileViews = FindObjectsOfType<TileView>();
    //    foreach (var tileView in tileViews)
    //    {
    //        if (tileView.HoveredTile != null)
    //            return tileView.HoveredTile;
    //    }
    //    return null;
    //}


    //public void Select(HexPiece hexPiece)
    //{
    //    if (hexPiece == null || hexPiece == _selectedPiece)
    //        return;

    //    //if (hexPiece != null && hexPiece.IsPlayer != )
    //    //{
    //    //    var tile = Board.TileOf(hexPiece);
    //    //    Select(tile);
    //    //}
    //    else
    //    {
    //        Board.UnHighlight(Movemanager.Tiles());

    //        Movemanager.ActivateFor(_selectedPiece);

    //        Board.Highlight(Movemanager.Tiles());
    //    }
    //}

    //public void Select(Tile tile)
    //{
    //    if (_selectedPiece != null)
    //    {
    //        Movemanager.Execute(_selectedPiece, tile);

    //        Board.UnHighlight(Movemanager.Tiles());

    //        Movemanager.DeActivate();

    //        _selectedPiece = null;
    //    }
    //}


    public void Select(Card card, IMoveCommand<Piece, Card> moveCommand)
    {
        // 1) find player tile
        //    => FindPlayerTile() (MAYBE NOT)
        // 2) find all possible tiles to use the card on
        //    => MoveManager / MovementHelper (fix)

        Board.UnHighlight(MoveManager.Tiles());

        MoveManager.Deactivate();

        _selectedCard = card;
        _currentMoveCommand = moveCommand;

        MoveManager.Activate(card);

        Board.Highlight(MoveManager.Tiles());
    }



    //public void SelectHover(Tile tile) // hovering
    //{
    //    if (_selectedCard != null)
    //    {
    //        var tiles = _currentMoveCommand.Tiles(Board, _selectedCard);
    //        var tiles2 = _currentMoveCommand.TilesExecutable(Board, _selectedCard);

    //        if (tiles.Contains(tile)) // als ik card drop op gehighlighte tile...
    //        {
    //            // verander highlighting van tiles
    //            Board.UnHighlight(tiles);
    //            MoveManager.DeactivateExecutable();
    //            MoveManager.ActivateForExecutable(_selectedCard);
    //            Board.Highlight(tiles2);

    //        }
    //    }
    //}


    public void SelectActivate(Tile tile) // dropping
    {
        if (_selectedCard != null)
        {
            MoveManager.Execute(_selectedCard, tile);
        }
    }

    //protected virtual void OnInitialized(EventArgs arg)  // FOR LATER VIDEO
    //{
    //    EventHandler handler = Initialized;
    //    handler?.Invoke(this, arg);
    //}
}
