using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataBaseOperate.DataStructure;
using System.Reflection;
using StoneUtils.Interface;

namespace StoneBow.DataStructure.Tree
{
    /// <summary>
    /// 树的操作类，用来同步本地模型和数据库数据
    /// </summary>
    public class TreeHandler
    {
        /// <summary>
        /// 根结点
        /// </summary>
        private TreeNode rootNode;
        /// <summary>
        /// 当前访问节点
        /// </summary>
        private TreeNode nowNode;
        /// <summary>
        /// 数据库记录表对应的树形表名称
        /// </summary>
        private string treeTableName;
        /// <summary>
        /// 数据库树形表操作对象
        /// </summary>
        private DataBaseOperate.DataStructure.Tree.Mssql2008Operator databaseTreeHandler;
        /// <summary>
        /// 节点ID与节点对象的映射关系
        /// </summary>
        private Dictionary<int, TreeNode> dictionary_Int_TreeNode;

        public StoneBow.DataStructure.Tree.TreeNode NowNode
        {
            get { return this.nowNode; }
            set { this.nowNode = value; }
        }

        public StoneBow.DataStructure.Tree.TreeNode RootNode
        {
            get { return this.rootNode; }
            set { this.rootNode = value; }
        }
        /// <summary>
        /// 默认构造方法，建立一颗空树（包含根结点）
        /// </summary>
        public TreeHandler()
        {
            rootNode = createRootNode();
            nowNode = rootNode;
            dictionary_Int_TreeNode = new Dictionary<int, TreeNode>();
            dictionary_Int_TreeNode.Add(rootNode.NodeId, rootNode);
        }
        /// <summary>
        /// 关联的数据库表名称，根据此表对应的树形表记录在内存中构建完整的树形结构
        /// </summary>
        /// <param name="_tableName"></param>
        public TreeHandler(string _connectionString, string _tableName)
            : this()
        {
            treeTableName = "tree_" + _tableName;
            databaseTreeHandler = new DataBaseOperate.DataStructure.Tree.Mssql2008Operator(_connectionString, treeTableName);
            //初始化树形模型
            buildTreeByDatabase(treeTableName);
        }
        /// <summary>
        /// 通过数据库中树形表生成树形模型
        /// </summary>
        /// <param name="_treeTableName"></param>
        private void buildTreeByDatabase(string _treeTableName)
        {
            DataTable treedt = databaseTreeHandler.GetAllRecord();//树形表
            //在这里不知道NodeData的数据类型，用反射的方法获取类型
            Assembly assembly = Assembly.GetExecutingAssembly();
            string typeName = "MyUsefulTools.DAO.JingDongGoodsKind";
            Type classType = assembly.GetType(typeName);
            //第一遍扫描，创建对象
            for (int i = 0; i < treedt.Rows.Count; i++)
            {
                DataRow dr = treedt.Rows[i];
                int nodeId = (int)dr["NodeID"];
                int dataId = (int)dr["DataID"];
                //使用反射生成对应类型的对象，并且使用制定的构造方法（ID作为参数）
                object typeObject = classType.Assembly.CreateInstance(classType.FullName, true, BindingFlags.CreateInstance, null,
                    new object[]{dataId}, null, null);
                TreeNode newnode = new TreeNode(nodeId, null, (ITreeNodeData)typeObject);
                dictionary_Int_TreeNode.Add(nodeId, newnode);
            }
            //第二遍扫描，建立树形结构
            for (int i = 0; i < treedt.Rows.Count; i++)
            {
                DataRow dr = treedt.Rows[i];
                int nodeId = (int)dr["NodeID"];
                int fatherNodeId = (int)dr["FatherNodeID"];
                TreeNode nownode = dictionary_Int_TreeNode[nodeId];
                TreeNode fatherNode = dictionary_Int_TreeNode[fatherNodeId];
                fatherNode.AddChild(nownode);
            }
        }
        /// <summary>
        /// 生成默认的根结点
        /// </summary>
        /// <returns></returns>
        private TreeNode createRootNode()
        {
            TreeNode rootNode = new TreeNode(0);
            return rootNode;
        }
        /// <summary>
        /// 在当前节点上添加新的节点
        /// </summary>
        /// <param name="_nodeData">要加的节点数据对象</param>
        public void AddNewNode(ITreeNodeData _nodeData)
        {
            if (nowNode == null) throw new NullReferenceException("当前节点为空");
            if (_nodeData.IsRecord == false) throw new Exception("关联的数据不是表记录");
            //添加数据库相关记录
            int newNodeId = databaseTreeHandler.AddNode(nowNode.NodeId, _nodeData);
            TreeNode newNode = new TreeNode(newNodeId, nowNode, _nodeData);
        }
        /// <summary>
        /// 使用当前Model对象初始化TreeView控件
        /// </summary>
        public void InitTreeView(System.Windows.Forms.TreeView _treeView)
        {
            _treeView.Nodes.Add("根结点");
            System.Windows.Forms.TreeNode treeNodeView = _treeView.Nodes[0];
            Recursion(treeNodeView, rootNode);
        }
        private void Recursion(System.Windows.Forms.TreeNode _treeNodeView, TreeNode _treeNodeModel)
        {
            for (int i = 0; i < _treeNodeModel.ChildNodes.Count; i++)
            { 
                TreeNode childNodeModel = _treeNodeModel.ChildNodes[i];
                _treeNodeView.Nodes.Add(childNodeModel.NodeId.ToString(), childNodeModel.NodeData.ToString());
                Recursion(_treeNodeView.Nodes[childNodeModel.NodeId.ToString()], childNodeModel);
            }
        }
    }
}
