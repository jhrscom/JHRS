using JHRS.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JHRS.Data
{
    /// <summary>
    /// 序列化辅助操作类
    /// </summary>
    public static class SerializeHelper
    {
        #region 二进制序列化

        /// <summary>
        /// 将对象序列化为byte[]，此方法不需要源类型标记可<see cref="SerializableAttribute"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this object value)
        {
            byte[] bytes = new byte[Marshal.SizeOf(value)];
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            Marshal.StructureToPtr(value, ptr, true);
            return bytes;
        }

        /// <summary>
        /// 将byte[]反序列化为对象，此方法不需要源类型标记可<see cref="SerializableAttribute"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T FromBytes<T>(this byte[] bytes)
        {
            Type type = typeof(T);
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            object obj = Marshal.PtrToStructure(ptr, type);
            return (T)obj;
        }

        /// <summary>
        /// 将数据序列化为二进制数组
        /// </summary>
        public static byte[] ToBinary(this object data)
        {
            data.CheckNotNull("data");
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, data);
                ms.Seek(0, 0);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将二进制数组反序列化为强类型数据
        /// </summary>
        public static T FromBinary<T>(this byte[] bytes)
        {
            bytes.CheckNotNullOrEmpty("bytes");
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 将数据序列化为二进制数组并写入文件中
        /// </summary>
        public static void ToBinaryFile(this object data, string fileName)
        {
            fileName.CheckNotNull("fileName");
            data.CheckNotNull("data");
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }
        }

        /// <summary>
        /// 将指定二进制数据文件还原为强类型数据
        /// </summary>
        public static T FromBinaryFile<T>(this string fileName)
        {
            fileName.CheckFileExists("fileName");
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
        }

        #endregion

        #region XML序列化

        /// <summary>
        /// 将数据序列化为XML形式
        /// </summary>
        public static string ToXml(this object data)
        {
            data.CheckNotNull("data");
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(ms, data);
                ms.Seek(0, 0);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 将XML数据反序列化为强类型
        /// </summary>
        public static T FromXml<T>(this string xml)
        {
            xml.CheckNotNull("xml");
            byte[] bytes = Encoding.Default.GetBytes(xml);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(ms);
            }
        }

        /// <summary>
        /// 将数据序列化为XML并写入文件
        /// </summary>
        public static void ToXmlFile(this object data, string fileName)
        {
            fileName.CheckNotNull("fileName");
            data.CheckNotNull("data");
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(fs, data);
            }
        }

        /// <summary>
        /// 将指定XML数据文件还原为强类型数据
        /// </summary>
        public static T FromXmlFile<T>(this string fileName)
        {
            fileName.CheckFileExists("fileName");
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(fs);
            }
        }

        #endregion
    }
}
