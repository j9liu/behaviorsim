﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorSim.BehaviorTree {

    public class SelectorNode : Node
    {
        private Node _activeChild;
        private int _activeChildIndex;

        public SelectorNode(string name) : base(name, false)
        {
            _children = new List<Node>();
            _activeChild = null;
            _activeChildIndex = -1;
        }

        public override NodeStatus Tick()
        {
            int childrenCount = _children.Count;
            if (childrenCount > 0)
            {
                if (_activeChildIndex < 0)
                {
                    _activeChildIndex = 0;
                }

                while (_activeChildIndex < childrenCount)
                {
                    _activeChild = _children[_activeChildIndex];
                    NodeStatus result = _activeChild.Tick();
                    switch (result)
                    {
                        case NodeStatus.RUNNING:
                            return result;
                        case NodeStatus.SUCCESS:
                            _activeChild = null;
                            _activeChildIndex = -1;
                            return result;
                        case NodeStatus.FAILURE:
                            _activeChildIndex++;
                            break;
                    }
                }

                return NodeStatus.FAILURE;
            }

            return NodeStatus.SUCCESS;
        }
    }
}