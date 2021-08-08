using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystem.Views
{
    public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        public bool dragOnSurfaces = true;

        private GameObject m_DraggingIcon;
        private RectTransform m_DraggingPlane;

        [SerializeField]
        private string _movementName = null;
        public string MovementName => _movementName;

        private Card _modelCard;
        public Card ModelCard
        {
            get => _modelCard;

            internal set
            {
                if (_modelCard != null)
                {
                    _modelCard.CardDropped -= CardModelDropped;
                }

                _modelCard = value;

                if (_modelCard != null)
                {
                    _modelCard.CardDropped += CardModelDropped;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            // We have clicked something that can be dragged.
            // What we want to do is create an icon for this (copy of the object that is being dragged).
            m_DraggingIcon = new GameObject("icon"); //copy van de kaart
            m_DraggingIcon.transform.SetParent(canvas.transform, true);
            m_DraggingIcon.transform.SetAsLastSibling(); //onderaan de lijst staan in de hierarchie

            var image = m_DraggingIcon.AddComponent<Image>();
            image.sprite = GetComponentInChildren<Image>().sprite;
            image.SetNativeSize();
            image.rectTransform.sizeDelta = image.rectTransform.sizeDelta / 4; // downscales the image
            image.raycastTarget = false;

            if (dragOnSurfaces)
                m_DraggingPlane = transform as RectTransform;
            else
                m_DraggingPlane = canvas.transform as RectTransform;

            SetDraggedPosition(eventData);

            GameLoop.Instance.ShowAllAvailableTiles(ModelCard);
        }

        

        public void OnDrag(PointerEventData eventData)
        {
            if (m_DraggingIcon != null)
                SetDraggedPosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (m_DraggingIcon != null)
                Destroy(m_DraggingIcon);

            var tileViews = FindObjectsOfType<TileView>();
            var tiles = new List<Tile>();

            foreach (var tileV in tileViews)
            {
                tiles.Add(tileV.Model);
            }

            var board = GameLoop.Instance.Board;

            board.UnHighlightAll(tiles);

            GameLoop.Instance.SelectedCard = null;

            GameLoop.Instance.CardDeck.SwapCard(this.gameObject, GameLoop.Instance.MoveManager);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

            var rt = m_DraggingIcon.GetComponent<RectTransform>();

            Vector3 globalMousePos;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = m_DraggingPlane.rotation;
            }
        }

        static public T FindInParents<T>(GameObject go) where T : Component //makes a child obj from the card u drag
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            Transform t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }

        private void CardModelDropped(object sender, CardDroppedEventArgs e)
        {
            
        }
    }
}
