﻿using System;

namespace WUG.BehaviorTreeDebugger
{
    [Serializable]
    public abstract class Node : NodeBase
    {
        public int EvaluationCount;
        public bool DebugNodeStatus = false;
        public bool IsFirstEvaluation => EvaluationCount == 0;
        private NodeStatus LastNodeStatus = NodeStatus.NotRun;

        /// <summary>
        /// Call on the base part of the Behavior tree to trigger the evaluation of all nodes
        /// </summary>
        public virtual NodeStatus Run()
        {
            NodeStatus nodeStatus = OnRun();

            if (LastNodeStatus != nodeStatus || !m_LastStatusReason.Equals(StatusReason))
            {
                OnNodeStatusChanged(nodeStatus, StatusReason);
                LastNodeStatus = nodeStatus;
                m_LastStatusReason = StatusReason;
            }

            EvaluationCount++;

            if (nodeStatus != NodeStatus.Running)
            {
                Reset();
            }

            return nodeStatus;
        }

        /// <summary>
        /// Execute the reset logic for the node
        /// </summary>
        public void Reset()
        {
            EvaluationCount = 0;
            OnReset();
        }

        /// <summary>
        /// Contains the custom logic for the node
        /// </summary>
        /// <returns></returns>
        protected abstract NodeStatus OnRun();
        protected abstract void OnReset();


    }
}
