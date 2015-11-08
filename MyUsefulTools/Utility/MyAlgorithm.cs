using System;
using System.Data;
using System.Configuration;

namespace MySpace.Utils
{
    /// <summary>
    /// 提供一些公用的小算法
    /// </summary>
    public class MyAlgorithm
    {
        public MyAlgorithm()
        {

        }
        /// <summary>
        /// 对源数组中的元素进行随机排序，就是将元素顺序打乱
        /// </summary>
        /// <param name="source">源数组</param>
        /// <returns>排序后的数组</returns>
        public static int[] RandomSort(int[] source)
        {
            int count = source.Length;
            int[] mark = new int[count];//下标数组
            for (int i = 0; i < count; i++)
            {
                mark[i] = i;
            }
            //下标顺序重排，使用随机交换的方法（随机选个数，和最后一个数交换）,交换N-1回
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < count - 1; i++)
            {
                int randomint = random.Next(0, count - i);
                int temp = mark[randomint];
                mark[randomint] = mark[count - i - 1];
                mark[count - i - 1] = temp;
            }
            //生成排序好的新的数组
            int[] direct = new int[count];
            for (int i = 0; i < count; i++)
            {
                direct[i] = source[mark[i]];
            }
            return direct;
        }
        /// <summary>
        /// 下标随机排序
        /// </summary>
        /// <param name="count">下标数</param>
        /// <returns>重排后的下标数组</returns>
        public static int[] RandomSortMarks(int count)
        {
            int[] mark = new int[count];//下标数组
            for (int i = 0; i < count; i++)
            {
                mark[i] = i;
            }
            //下标顺序重排，使用随机交换的方法（随机选个数，和最后一个数交换）,交换N-1回
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < count - 1; i++)
            {
                int randomint = random.Next(0, count - i);
                int temp = mark[randomint];
                mark[randomint] = mark[count - i - 1];
                mark[count - i - 1] = temp;
            }
            return mark;
        }
        /// <summary>
        /// 从0到upnum（不包括）中随机选n个数
        /// </summary>
        /// <param name="upnum"></param>
        /// <returns></returns>
        public static int[] RandomNumbers(int upnum, int n)
        {
            Random random = new Random(GetRandomSeed());
            int[] randomNums = new int[n];
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    int rannum = random.Next(upnum);
                    //判断这个新取到的数是否已经取到过
                    bool isUsed = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (randomNums[j] == rannum)
                        {
                            isUsed = true;
                            break;
                        }
                    }
                    if (!isUsed)
                    {
                        randomNums[i] = rannum;
                        break;
                    }
                }
            }
            return randomNums;
        }
        /// <summary>
        /// 网上提高随机数不重复概率的种子生成方法
        /// </summary>
        /// <returns>随机种子</returns>
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];//表示四个字节，就是32位
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}