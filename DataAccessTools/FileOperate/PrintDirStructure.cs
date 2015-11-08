using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace ZLSpace.FileOperate
{
    /// <summary>
    /// 打印给定磁盘目录的目录结构，包含子文件夹及其中的文件
    /// </summary>
    class PrintDirStructure
    {
        struct Data1
        {
            /// <summary>
            /// 指明当前元素存储的是File(true)还是Directory(false)
            /// </summary>
            public bool IsFile;
            public FileInfo TheFile;
            public DirectoryInfo TheDirectory;
            /// <summary>
            /// bool
            /// 标记对应位置是否为竖线
            /// </summary>
            public ArrayList HasShuxian;
            /// <summary>
            /// 表明是否为当前层最后一个元素
            /// </summary>
            public bool IsLast;
            /// <summary>
            /// 表明当前元素的深度
            /// </summary>
            public int Level;

            public Data1(FileSystemInfo theFile, int level, ArrayList shuxian, bool islast)
            {
                HasShuxian = new ArrayList(shuxian);
                IsLast = islast;
                Level = level;
                if (theFile.GetType().Name.Equals("DirectoryInfo"))
                {
                    IsFile = false;
                    TheFile = null;
                    TheDirectory = (DirectoryInfo)theFile;
                }
                else
                {
                    IsFile = true;
                    TheFile = (FileInfo)theFile;
                    TheDirectory = null;
                }
            }
            public string GetName()
            {
                if (IsFile) return TheFile.Name;
                else return TheDirectory.Name;
            }
        }
        #region PrintOnScreen
        /// <summary>
        /// 使用栈来代替递归操作
        /// </summary>
        /// <param name="dir"></param>
        public static void PrintOnScreen(DirectoryInfo rootdir)
        {
            Stack<Data1> stack = new Stack<Data1>();
            stack.Push(new Data1(rootdir,0,new ArrayList(), true));
            while (stack.Count > 0)
            {
                Data1 data = stack.Pop();
                if (data.IsFile)
                {
                    PrintElem(data);
                }
                else
                {
                    PrintElem(data);
                    //将此目录下所有文件和目录入栈，注意顺序问题，这里没考虑
                    DirectoryInfo dir = data.TheDirectory;
                    FileSystemInfo[] files = dir.GetFileSystemInfos();
                    for (int i = 0; i < files.Length; i++)
                    {
                        FileSystemInfo fsi = files[i];
                        ArrayList shuxian = new ArrayList(data.HasShuxian);
                        if (data.IsLast) shuxian.Add(false);
                        else shuxian.Add(true);

                        if (i == 0)
                        {
                            stack.Push(new Data1(fsi, data.Level + 1, shuxian, true));
                        }
                        else stack.Push(new Data1(fsi, data.Level + 1, shuxian, false));
                    }
                }
            }
        }
        private static void PrintElem(Data1 data)
        {
            for(int i=0;i<data.HasShuxian.Count;i++)
            {
                if ((bool)data.HasShuxian[i] == true)
                {
                    Console.Write("│");
                }
                else Console.Write("  ");
            }
            if(data.IsLast) Console.WriteLine("└{0}",data.GetName());
            else Console.WriteLine("├{0}", data.GetName());
        }
        #endregion
    }
}
/*使用方法
DirectoryInfo di = new DirectoryInfo(@"G:\Desktop\天津项目相关\毕设\");
PrintDirStructure.PrintOnScreen(di);
*/