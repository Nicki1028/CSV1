using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CSV1
{
    // 存取範圍: public internal protected private  
    public class CsvHelper
    {        
        public List<T> Noheader<T>(string filepath) where T : new()
        {
            List<T> listitem = new List<T> ();
            StreamReader reader = new StreamReader(filepath);
                     
            while (!reader.EndOfStream)
            {
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
        public List<T> Header<T>(string filepath) where T : new()
        {
            // ORM => Object Relaction Mapping
            List<T> listitem = new List<T> ();
            StreamReader reader = new StreamReader(filepath);
            T x = new T();
            Dictionary<string, int> mapping = new Dictionary<string, int>();
            string[] header = reader.ReadLine().Split(',');
            var props = x.GetType().GetProperties();
            for (int i = 0; i < header.Length; i++)
            {
                for (int j = 0; j < props.Length; j++)
                {
                    if (header[i].Equals(props[j].Name))
                    {
                        mapping.Add(header[i], j);
                    }
                }
            }
            while (!reader.EndOfStream)
            {
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
        public List<T> Read<T>(String filepath, bool hasHeaders = false) where T : new() 
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

            if(!hasHeaders)
            {
                return Noheader<T>(filepath);
            }
            else
            {
                return Header<T>(filepath);
            }
            
        }
        public void Write<T>(T data, string filepath)
        {
            string[] file = filepath.Split('\\');
            string filepathfinal = "";
            string[] filename = file.Last().Split('.');
            if (filename[1] != "csv")
            {
                throw new Exception("檔案格式錯誤");
            }
            for (int i = 0; i < file.Length-1; i++) 
            {
                filepathfinal += file[i] + '\\';
            }
            if (!Directory.Exists(filepathfinal))
            {
                Directory.CreateDirectory(filepathfinal);        
            }
            
            StreamWriter writer = new StreamWriter(filepath,true, Encoding.UTF8);
            Type type = data.GetType();
            var props = type.GetProperties();
            string final = "";
            foreach (var prop in props)
            {
                final += prop.GetValue(data).ToString() + ",";
            }
            final = final.TrimEnd(',');
            writer.WriteLine(final);
            writer.Flush();
            writer.Close();
        }
        public void Writelist<T>(List<T> data, string filepath)
        {
            foreach (var item in data)
            {
                Write(item, filepath);
            }
        }

    }
}
