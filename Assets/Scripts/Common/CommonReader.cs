using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace MyDota.Common
{
	/// <summary>
	/// 
	/// </summary>
	public class CommonReader
	{
        /// <summary>
        /// 读取csv文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isFirst">是否读取第一行作为表头</param>
        /// <returns></returns>
        public static DataTable ReadCSV(string filePath, bool isFirst = true)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (isFirst == true)
                {
                    isFirst = false;
                    columnCount = aryLine.Length;
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            sr.Close();
            fs.Close();
            return dt;
        }
    }
}