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

    public List<PieceView> Players { get; set; }

    private void Awake()
    {
        
        MoveManager = new MoveManager<Piece, Card>(Board);
        StateMachine = new StateMachine<GameStateBase>();
        Players = FindObjectsOfType<PieceView>().ToList();

        var CardDeck = FindObjectOfType<CardViewFactory>();

        ConnectViewsToModel();
        StateMachine.RegisterState(GameStates.Play, new PlayGameState(Board, MoveManager, CardDeck));
        StateMachine.RegisterState(GameStates.FindActive, new FindActivePlayerState());
        

        MoveManager.Register(PlayerMoveSweepCommand.Name, new PlayerMoveSweepCommand());
        MoveManager.Register(PlayerMoveAxialCommand.Name, new PlayerMoveAxialCommand());
        MoveManager.Register(PlayerMovePushCommand.Name, new PlayerMovePushCommand());
        MoveManager.Register(PlayerMoveTeleportCommand.Name, new PlayerMoveTeleportCommand());
    }

    private void Start()
    {
        StateMachine.MoveTo(GameStates.FindActive);

        StateMachine.CurrentState.LoadCards();

        StartCoroutine(PostStart());
    }

    public void StartTurnSwitch()
    {
        StartCoroutine(DelayTurnSwitch());
    }

    private IEnumerator DelayTurnSwitch()
    {
        yield return new WaitForSeconds(0.1f);

        StateMachine.MoveTo(GameStates.FindActive);
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

            var piece = new Piece(pieceView);

            Board.Place(tile, piece);

            pieceView.Model = piece;
        }
    }

    public Tile FindPlayerTile()    
    {
        foreach (var pieceView in Players)
        {
            if (pieceView.IsActive && pieceView != null) //find the tile it's on and if its active
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

                var playerTile = Board.TileAt(boardPosition);

                return playerTile;
            }
        }
        return null;
    }

    public List<PieceView> UpdatePlayersList()
    {
        var currentplayers = FindObjectsOfType<PieceView>();
        foreach (var pieceView in Players.ToList())
        {
            if (!currentplayers.Contains(pieceView))
            {
                Players.Remove(pieceView);
            }
        }

        return Players;
    }

    public void OnTileEnter(Tile hovereTile)
    {
        StateMachine.CurrentState.OnEnterTile(hovereTile);
    }

    public void OnTileDrop()
    {
        StateMachine.CurrentState.OnDropTile();
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
