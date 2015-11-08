using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoneUtils.Interface;

namespace StoneBow.DataStructure.Tree
{
    /// <summary>
    /// 表示树的节点
    /// </summary>
    public class TreeNode
    {
        private int nodeId;
        private TreeNode fatherNode;
        private List<TreeNode> childNodes;
        private ITreeNodeData nodeData;

        /// <summary>
        /// 节点标识
        /// </summary>
        public int NodeId
        {
            get { return this.nodeId; }
            set { this.nodeId = value; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public StoneBow.DataStructure.Tree.TreeNode FatherNode
        {
            get { return this.fatherNode; }
            set { this.fatherNode = value; }
        }
        /// <summary>
        /// 子节点集
        /// </summary>
        public System.Collections.Generic.List<StoneBow.DataStructure.Tree.TreeNode> ChildNodes
        {
            get { return this.childNodes; }
            set { this.childNodes = value; }
        }
        /// <summary>
        /// 节点数据
        /// </summary>
        public ITreeNodeData NodeData
        {
            get { return this.nodeData; }
            set { this.nodeData = value; }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public TreeNode(int _nodeId)
        {
            nodeId = _nodeId;
            fatherNode = null;
            childNodes = new List<TreeNode>();
            nodeData = null;
        }
        public TreeNode(int _nodeId, TreeNode _fatherNode) : this(_nodeId)
        {
            if(_fatherNode != null) _fatherNode.AddChild(this);
        }
        public TreeNode(int _nodeId, TreeNode _fatherNode, ITreeNodeData _nodeData) : this(_nodeId, _fatherNode)
        {
            nodeData = _nodeData;
        }
        /// <summary>
        /// 当前节点添加子节点，并将当前节点设为对应的父节点
        /// </summary>
        public void AddChild(TreeNode _childNode)
        {
            childNodes.Add(_childNode);
            _childNode.FatherNode = this;
        }
    }
}
