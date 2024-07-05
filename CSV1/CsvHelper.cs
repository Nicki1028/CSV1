using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CSV1
{
    // 存取範圍: public internal protected private  
    public class CsvHelper
    {
       

        public List<T> Noheader<T>(string filepath, int? startline = null, int? linecount = null) where T : new()
        {
            List<T> listitem = new List<T>();
            StreamReader reader = new StreamReader(filepath);
            int count = 0;
            while (!reader.EndOfStream)
            {
                count++;
                if (count < startline)
                    continue;
                if (count > startline + linecount)
                    break;

                T t = new T();
                string[] datas = reader.ReadLine().Split(',');
                var props = t.GetType().GetProperties();
                for (int i = 0; i < datas.Length; i++)
                {
                    props[i].SetValue(t, datas[i]);
                }
                listitem.Add(t);
            }
            reader.Close();
            return listitem;
        }
        public List<T> Header<T>(string filepath, int? startline = null, int? linecount = null) where T : new()
        {
            // ORM => Object Relaction Mapping
            List<T> listitem = new List<T>();
            StreamReader reader = new StreamReader(filepath);
            T x = new T();
            Dictionary<string, int> mapping = new Dictionary<string, int>();
            string[] header = reader.ReadLine().Split(',');
            var props = x.GetType().GetProperties();

            for (int k = 0; k < header.Length; k++)
            {
                for (int j = 0; j < props.Length; j++)
                {
                    if (header[k].Equals(props[j].Name))
                    {
                        mapping.Add(header[k], j);
                    }
                }
            }
            int count = 0;
            while (!reader.EndOfStream)
            {
                count++;
                if (count < startline)
                    continue;
                if (count > startline + linecount)
                    break;

                T t = new T();
                string[] datas = reader.ReadLine().Split(',');
                for (int i = 0; i < datas.Length; i++)
                {
                    if (mapping.ContainsKey(header[i]))
                    {
                        props[mapping[header[i]]].SetValue(t, datas[i]);
                    }
                }
                listitem.Add(t);
            }
            reader.Close();
            return listitem;
        }
        public List<T> Read<T>(String filepath, bool hasHeaders = false, int? startline = null, int? linecount = null) where T : new()
        {
            string[] file = filepath.Split('\\');
            string filepathfinal = "";
            string[] filename = file.Last().Split('.');

            for (int i = 0; i < file.Length - 1; i++)
            {
                filepathfinal += file[i] + '\\';
            }
            if (!Directory.Exists(filepathfinal))
            {
                throw new Exception("路徑不存在");
            }
            if (filename[1] != "csv")
            {
                throw new Exception("檔案格式錯誤");
            }
            if (!File.Exists(filepath))
            {
                throw new Exception("檔案不存在");
            }

            if (!hasHeaders)
            {
                return Noheader<T>(filepath, startline, linecount);
            }
            else
            {
                return Header<T>(filepath, startline, linecount);
            }
        }


        public void Write<T>(T data, StreamWriter writer)
        {
           
            Type type = data.GetType();
            var props = type.GetProperties();
            string final = "";
            foreach (var prop in props)
            {
                if (prop.GetValue(data) != null)
                {
                    final += prop.GetValue(data).ToString() + ",";
                }
            }
            final = final.TrimEnd(',');
            writer.WriteLine(final);
            writer.Flush();
        }
      
        public void Writelist<T>(List<T> data, string filepath)
        {
            string[] file = filepath.Split('\\');
            string filepathfinal = "";
            string[] filename = file.Last().Split('.');
            if (filename[1] != "csv")
            {
                throw new Exception("檔案格式錯誤");
            }
            for (int i = 0; i < file.Length - 1; i++)
            {
                filepathfinal += file[i] + '\\';
            }

            if (!Directory.Exists(filepathfinal))
            {
                Directory.CreateDirectory(filepathfinal);
            }

            using (StreamWriter writer = new StreamWriter(filepath, true, Encoding.UTF8))
            {
                foreach (var item in data)
                {
                    Write(item, writer);
                }
                writer.Close();
                GC.Collect();
            }

        }
    }
}

