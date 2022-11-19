using System.Collections.Generic;
using UnityEngine;
using VisitorSystem;

namespace BuildingSystem.CashSystem
{
    public class CashQueue : MonoBehaviour
    {
        public bool HasArePlacesInLine => _queuePositions.Count > _visitors.Count;
        public bool IsEmptyQueue => _visitors.Count == 0;

        [SerializeField]
        private List<Transform> _queuePositions = new List<Transform>();

        private readonly List<Visitor> _visitors = new List<Visitor>();

        public void AddVisitor(Visitor visitor)
        {
            _visitors.Add(visitor);
        }

        public bool HasFirstVisitorAtTheCheckout()
        {
            if (_visitors.Count == 0)
            {
                return false;
            }

            var visitor = _visitors[0];
            var firstPosition = _queuePositions[0];
            var pointOne = new Vector2(visitor.transform.position.x, visitor.transform.position.z);
            var pointTwo = new Vector2(firstPosition.position.x, firstPosition.position.z);
            return Vector2.Distance(pointOne, pointTwo) < 0.5f;
        }
        

        public Transform GetPoint()
        {
            if (_queuePositions.Count == 0)
            {
                return transform;
            }
            
            return _queuePositions[_visitors.Count - 1];
        }

        public List<Visitor> GetAllVisitor()
        {
            var visitors = new List<Visitor>(_visitors);
            _visitors.Clear();
            return visitors;
        }

        public Visitor GetFirstVisitor()
        {
            var visitor = _visitors[0];
            _visitors.RemoveAt(0);
            return visitor;
        }

        public void RefreshQueue()
        {
            for (var i = 0; i < _visitors.Count; i++)
            {
                _visitors[i].SetDestination(_queuePositions[i], PointType.Queue);
            }
        }
    }
}