using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using GameSystem.Views;
using MoveSystem;
using StateSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    public event EventHandler Initialized;

    [SerializeField]
    private PositionHelper _positionHelper;

    public PositionHelper PositionHelper => _positionHelper;

    public Board<Piece, Card> Board { get; } = new Board<Piece, Card>(3);

    public MoveManager<Piece, Card> MoveManager { get; internal set; }

    public StateMachine<GameStateBase> StateMachine;

    private void Awake()
    {
        MoveManager = new MoveManager<Piece, Card>(Board);
        var CardDeck = FindObjectOfType<CardViewFactory>();
        StateMachine = new StateMachine<GameStateBase>();

        ConnectViewsToModel();

        StateMachine.RegisterState(GameStates.Play, new PlayGameState(Board, MoveManager, CardDeck));
        StateMachine.RegisterState(GameStates.FindActive, new FindActivePlayerState());
        StateMachine.MoveTo(GameStates.FindActive);

        MoveManager.Register(PlayerMoveSweepCommand.Name, new PlayerMoveSweepCommand());
        MoveManager.Register(PlayerMoveAxialCommand.Name, new PlayerMoveAxialCommand());
        MoveManager.Register(PlayerMovePushCommand.Name, new PlayerMovePushCommand());
        MoveManager.Register(PlayerMoveTeleportCommand.Name, new PlayerMoveTeleportCommand());
    }

    private void Start()
    {
        StateMachine.CurrentState.LoadCards();

        StartCoroutine(PostStart());
    }

    private IEnumerator PostStart()
    {
        yield return new WaitForEndOfFrame();

        OnInitialized(EventArgs.Empty);
    }

    protected virtual void OnInitialized(EventArgs arg)
    {
        EventHandler handler = Initialized;
        handler?.Invoke(this, arg);
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

    public Tile FindPlayerTile()    //REVISIT THIS
    {
        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
        {
            if (pieceView.IsPlayer == true /*&& pieceView.Model.IsActive*/) // if we find a player, find the tile it's on and if its active
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

                var playerTile = Board.TileAt(boardPosition);

                return playerTile;
            }
        }
        return null;
    }

    public void OnTileEnter(Tile hovereTile)
    {
        StateMachine.CurrentState.OnEnterTile(hovereTile);
    }

    public void OnTileDrop()
    {
        StateMachine.CurrentState.OnDropTile();
    }

    public void OnTileExit()
    {
        StateMachine.CurrentState.OnTileExit();
    }

    public void OnEndCardDrag(List<TileView> tiles, GameObject cardObj)
    {
        StateMachine.CurrentState.OnEndCardDrag(tiles, cardObj);
    }

    public void OnCardBeginDrag(Card modelCard)
    {
        StateMachine.CurrentState.OnCardBeginDrag(modelCard);
    }

}
