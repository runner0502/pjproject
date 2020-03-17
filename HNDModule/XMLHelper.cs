using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Hytera.I18N
{
    /******************************************************************
     * Author:              Time:10/24/2012 1:58:07 PM
     * File Name:XMLHelper
     * Company:Hytera
     * Descprition:xml文件序列化与反序列化
     * Update:
     * ****************************************************************/
    public static class XMLHelper
    {
        /// <summary>
        /// 反序列化指定的文件为一个对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="filePath">文件</param>
        /// <returns></returns>
        public static T DeSerializeFromFile<T>(string filePath)
        {
            T obj;
            if (File.Exists(filePath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                FileStream fs = new FileStream(filePath, FileMode.Open);
                XmlReader xr = XmlReader.Create(fs);
                obj = (T)ser.Deserialize(xr);
                fs.Close();
                return obj;
            }

            return default(T);
        }
        /// <summary>
        /// 序列化对象为指定文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="filePath">文件</param>
        public static void SerializeToFile<T>(this T obj,string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                    xmlns.Add(string.Empty, string.Empty);
                    ser.Serialize(sw, obj, xmlns);
                    sw.Close();
                }
            }
            catch
            {
                throw ;
            }
        }
    }
}
