using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo.Util
{
    public static class MemoryControl
    {
        /// <summary>
        /// 结构体转成指针
        /// </summary>
        /// <param name="pStruct">结构体</param>
        /// <returns></returns>
        public static IntPtr StructToIntPtr(object pStruct)
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(pStruct));
            Marshal.StructureToPtr(pStruct, ptr, true);
            return ptr;
        }

        public static IntPtr NewIntPtr(Type type, int count)
        {
            return Marshal.AllocHGlobal(Marshal.SizeOf(type) * count);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ptr"></param>
        public static void FreeHGlobalPtr(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        public static object IntPtrToType<T>(IntPtr prt)
        {
            return Marshal.PtrToStructure(prt, typeof(T));
        }

        public static T ToType<T>(IntPtr ptr)
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }
    }
}
