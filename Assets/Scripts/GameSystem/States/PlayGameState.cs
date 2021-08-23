using BoardSystem;
using GameSystem.Models;
using GameSystem.Utils;
using GameSystem.Views;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        private MoveManager<Piece, Card> _moveManager;
        private Board<Piece, Card> _board;
        private Tile _hoveredTile = null;
        //private EnemyMovePathfinding _path;

        private Card _selectedCard = null;
        private CardViewFactory _cardDeck;

        private int _usedCardsPerTurn = 0;

        public PlayGameState(Board<Piece, Card> board, MoveManager<Piece, Card> moveManager, CardViewFactory cardDeck)
        {
            _moveManager = moveManager;
            _board = board;
            _cardDeck = cardDeck;
        }

        public override void OnEnter()
        {
            _usedCardsPerTurn = 0;
        }

        public void SelectActivate(Tile tile) // dropping
        {
            if (_selectedCard != null)
            {
                _moveManager.Execute(_selectedCard, tile);
            }
        }

        public void Select(Card card)
        {
            _board.UnHighlightAll(_moveManager.Tiles());

            _moveManager.Deactivate();

            _selectedCard = card;

            _moveManager.Activate(card, _hoveredTile);

            _board.HighlightAll(_moveManager.Tiles());
        }

        public override void OnEnterTile(Tile hoveredTile)
        {
            _hoveredTile = hoveredTile;

            var oldValidTiles = _moveManager.Tiles(); //assign the validtiles in this variable

            _board.UnHighlightAll(oldValidTiles);   //unhighlight them all

            if (_selectedCard != null)
            {
                _moveManager.Activate(_selectedCard, hoveredTile);

                var newValidTiles = GameLoop.Instance.MoveManager.Tiles(); //assign the validtiles in this variable

                if (_selectedCard != null && newValidTiles.Contains(_hoveredTile)) //if the hoveredtile is part of the validtiles list
                {
                    if (!_selectedCard.MoveName.Equals(MoveNames.Teleport))
                    {
                        _board.HighlightAll(newValidTiles);   //highlight the new one
                    }
                    else
                    {
                        _board.HighlightOne(_hoveredTile);
                    }
                }

            }
        }

        public override void OnDropTile()
        {
            SelectActivate(_hoveredTile);
        }

        public override void OnTileExit()
        {
            if (_hoveredTile != null)
            {
                _board.UnHighlightOne(_hoveredTile);     //unhighlight the previous tile
            }
        }

        public override void OnCardBeginDrag(Card modelCard)
        {
            Select(modelCard);
        }

        public override void OnEndCardDrag(List<TileView> tileViews, GameObject cardObj)
        {
            if (_usedCardsPerTurn < 2)
            {
                if (_hoveredTile != null)
                {
                    List<Tile> tiles = GetTiles(tileViews);

                    _board.UnHighlightAll(tiles);

                    _selectedCard = null;

                    _cardDeck.SwapCard(cardObj, _moveManager);

                    _usedCardsPerTurn += 1;
                }
                
            }

            if (_usedCardsPerTurn > 1)
            {
                StateMachine.MoveTo(GameStates.FindActive);
            }
        }
        private List<Tile> GetTiles(List<TileView> tileViews)
        {
            var tiles = new List<Tile>();

            foreach (var tileV in tileViews)
            {
                tiles.Add(tileV.Model);
            }

            return tiles;
        }

        public override void LoadCards()
        {
            _cardDeck.GenerateCardPile(_moveManager);
        }
    }
}
