using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] jz_say ={
                "不积跬步，无以至千里，蝼蚁虽小，亦为万物生。",
                "曾经沧海难为水，除却巫山不是云。",
                "在这里：既可浏览公共分类，也可建立专属分类。",
                "天下归一，终成大千世界。",
                "大千世界，非你不可。" ,"精准品质，诚行天下。","精准品质，诚行天下。  --by 11004821@qq.com"};
        private int OneSecond = 0,jzpos=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            OneSecond++;
            var n=DateTime.Now;
            toolStripStatusLabel1.Text = n.ToString();
            toolStripStatusLabel2.Text = jz_say[jzpos%jz_say.Length].PadLeft(this.Width -700 - OneSecond+1,' ');
            if (this.Width - 700 - OneSecond < 0) { OneSecond = 0; jzpos++; }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                    if (CurrentNode.Text == "专属分类")
                    {
                        CurrentNode.ContextMenuStrip = contextMenuStrip1;
                    }
                }
            }
        }
        private void LoadTreeNode() 
        { 
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument(); 
            string filename = AppDomain.CurrentDomain.BaseDirectory + "111.xml"; 
            xmlDoc.Load(filename); 
            if (xmlDoc.ChildNodes.Count > 0) 
            { 
                System.Xml.XmlNodeList nodes = xmlDoc.ChildNodes[0].ChildNodes; this.LoadNode(nodes, this.treeView1.Nodes); 
            }
        }
        //---保存节点 
        private void SaveTreeNode()
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument(); 
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"); 
            System.Xml.XmlElement rootNode = xmlDoc.CreateElement("root"); 
            xmlDoc.AppendChild(rootNode); 
            this.SaveTreeNode(xmlDoc, rootNode, this.treeView1.Nodes); 
            string filename = AppDomain.CurrentDomain.BaseDirectory + "111.xml";
            xmlDoc.Save(filename);
        }
        private void SaveTreeNode(System.Xml.XmlDocument xmlDoc, System.Xml.XmlElement rootNode, TreeNodeCollection nodes) 
        { 
            foreach (TreeNode node in nodes) 
            { System.Xml.XmlElement pNode = xmlDoc.CreateElement("ChildNode");
                pNode.SetAttribute("Value", node.Text); rootNode.AppendChild(pNode); SaveTreeNode(xmlDoc, pNode, node.Nodes); 
            }
        }
        private void LoadNode(System.Xml.XmlNodeList nodes, TreeNodeCollection pNodes) 
        { 
            foreach (System.Xml.XmlElement element in nodes) 
            { TreeNode node = new TreeNode(); node.Text = element.GetAttribute("Value"); pNodes.Add(node); 
                LoadNode(element.ChildNodes, node.Nodes); 
            }
        }
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //在Tree选择节点的同一级添加节点
            treeView1.LabelEdit = true;
            TreeNode CurrentNode = treeView1.SelectedNode.Nodes.Add("Node1");
            //更新选择节点
            treeView1.SelectedNode.Checked = false;
            CurrentNode.Checked = true;
            //使添加的树节点处于可编辑的状态
            CurrentNode.BeginEdit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveTreeNode();
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            SaveTreeNode();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.LoadFile("d:\\专属世界.rtf", RichTextBoxStreamType.RichText);
            LoadTreeNode();
        }
    }
}
