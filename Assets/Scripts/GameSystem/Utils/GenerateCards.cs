using GameSystem.Models;
using GameSystem.Views;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Utils
{
    public class GenerateCards : MonoBehaviour
    {
        private List<GameObject> _cards = new List<GameObject>();
        private int _cardCount = 15;
        private int _deckCount = 5;

        public GameObject AxialCard;
        public GameObject SweepCard;
        public GameObject PushCard;
        public GameObject TeleportCard;

        public Transform Parent;

        public void GenerateCardPile(MoveManager<Piece, Card> MoveManager)
        {
            for (int i = 0; i < _cardCount; i++)
            {
                var randomNr = UnityEngine.Random.Range(0, 4);

                if (randomNr == 0) _cards.Add(AxialCard);

                else if (randomNr == 1) _cards.Add(SweepCard);

                else if (randomNr == 2) _cards.Add(PushCard);

                else _cards.Add(TeleportCard);
            }

            SpawnDeck(MoveManager);
        }

        public void SpawnDeck(MoveManager<Piece, Card> MoveManager)
        {
            for (int i = 0; i < _deckCount; i++)
            {
                var randomNr = UnityEngine.Random.Range(0, 4);

                var cardobj = Instantiate(_cards[randomNr], Parent.position, Quaternion.identity, Parent);
                var cardView = cardobj.GetComponent<CardView>();

                var card = new Card();

                MoveManager.Register(card, cardView.MovementName);

                card.MoveName = cardView.MovementName;
                cardView.ModelCard = card;

                _cards.RemoveAt(randomNr);
            }
        }

        public void SwapCard(GameObject cardObj, MoveManager<Piece, Card> MoveManager)
        {
            Debug.Log(_cards.Count);
            if (_cards.Count != 0)
            {
                var cardobj = Instantiate(_cards[0], Parent.position, Quaternion.identity, Parent);
                var cardView = cardobj.GetComponent<CardView>();

                var card = new Card();

                MoveManager.Register(card, cardView.MovementName);

                card.MoveName = cardView.MovementName;
                cardView.ModelCard = card;

                _cards.RemoveAt(0);
            }

            Destroy(cardObj);
        }
    }
}
