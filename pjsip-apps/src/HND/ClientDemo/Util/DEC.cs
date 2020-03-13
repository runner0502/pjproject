using Hytera.Commom.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo.Util
{
    public class DECEncryptHelper
    {
        public enum Module
        {
            ENCRYPT, // ENCRYPT:加密
            DECRYPT //DECRYPT:解密
        };

        /// <summary>
        /// 写入秘钥，需加密前调用，密钥为HytBSoft可以不调用
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="iLen"></param>
        /// <returns></returns>
        [DllImport("Encrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SetKey(IntPtr pData, int iLen);

        /// <summary>
        /// DEC加密接口
        /// </summary>
        /// <param name="pData">需要加密的文本指針</param>
        /// <param name="iLen">需要加密的文本長度</param>
        /// <param name="pIV">向量</param>
        /// <param name="bModule">加密或解密</param>
        /// <param name="pDst">密文輸出指針</param>
        /// <param name="iDstlen">密文長度</param>
        /// <returns>返回實際長度</returns>
        [DllImport("Encrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static int Des_CBC_crypt(IntPtr pData, int iLen, IntPtr pIV, Module bModule, IntPtr pDst, int iDstlen);

        //默认密钥向量
        private static byte[] _keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string _encryptKey = "HytBSoft";

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string EncryptDES(string encryptString)
        {
            try
            {
                return EncryptCipher(encryptString);

                byte[] rgbKey = Encoding.UTF8.GetBytes(_encryptKey.Substring(0, 8));
                byte[] rgbIV = _keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                GCHandle hObj1 = GCHandle.Alloc(inputByteArray, GCHandleType.Pinned);
                IntPtr ptrBuffer = hObj1.AddrOfPinnedObject();

                GCHandle hObj2 = GCHandle.Alloc(rgbIV, GCHandleType.Pinned);
                IntPtr ptrKeys = hObj2.AddrOfPinnedObject();

                try
                {
                    IntPtr ptrDst = Marshal.AllocHGlobal(32);
                    int nDstLen = 64;
                    int nRet = DECEncryptHelper.Des_CBC_crypt(ptrBuffer, encryptString.Length, ptrKeys, Module.ENCRYPT, ptrDst, nDstLen);
                    string encrypt = "";
                    if (nRet > 0)
                    {
                        Byte[] bDst = new Byte[nRet];
                        Marshal.Copy(ptrDst, bDst, 0, bDst.Length);
                        encrypt = Convert.ToBase64String(bDst);
                        return encrypt;
                    }
                    else
                    {
                        return encryptString;
                    }
                }
                catch (Exception)
                {
                    return encryptString;
                }
                finally
                {
                    hObj1.Free();
                    hObj2.Free();
                }
            }
            catch
            {
                return encryptString;
            }
        }

        [DllImport("SegBase.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static void StringCipherCode(IntPtr pIn, IntPtr pOut);

        [DllImport("SegBase.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static void StringCipherDecode(IntPtr pIn, IntPtr pOut);

        public static string EncryptCipher(string encryptString)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

            GCHandle hObj1 = GCHandle.Alloc(inputByteArray, GCHandleType.Pinned);
            IntPtr ptrBuffer = hObj1.AddrOfPinnedObject();

            try
            {
                IntPtr ptrDst = Marshal.AllocHGlobal(32);
                StringCipherCode(ptrBuffer, ptrDst);
                string encryptCode = Marshal.PtrToStringAnsi(ptrDst);

                Logger.Info("password:" + encryptCode);
                return encryptCode;
            }
            catch (Exception)
            {
                return encryptString;
            }
            finally
            {
                hObj1.Free();
            }
        }

        public static string DecryptCipher(string encryptString)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

            GCHandle hObj1 = GCHandle.Alloc(inputByteArray, GCHandleType.Pinned);
            IntPtr ptrBuffer = hObj1.AddrOfPinnedObject();

            try
            {
                IntPtr ptrDst = Marshal.AllocHGlobal(32);
                StringCipherDecode(ptrBuffer, ptrDst);
                string encryptCode = Marshal.PtrToStringAnsi(ptrDst);

                return encryptCode;
            }
            catch (Exception)
            {
                return encryptString;
            }
            finally
            {
                hObj1.Free();
            }
        }
    }
}
