using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarForFolderAndFiles
{
    class CustomFolderInfo
    {
        CustomFolderInfo prev;
        int index;
        FileSystemInfo[] objs;

        public CustomFolderInfo(CustomFolderInfo prev, int index, FileSystemInfo[] list)
        {
            this.prev = prev;
            this.index = index;
            this.objs = list;
        }

        public void PrintFolderInfo()
        {
            Console.Clear();

            for (int i = 0; i < objs.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(objs[i]);
            }
        }

        public void DecreaseIndex()
        {
            if (index - 1 >= 0) index--;
        }

        public void IncreaseIndex()
        {
            if (index + 1 < objs.Length) index++;
        }

        public CustomFolderInfo GetNextItem()
        {
            FileSystemInfo active = objs[index];

            if (active.GetType() == typeof(DirectoryInfo))
            {
                List<FileSystemInfo> list = new List<FileSystemInfo>();
                DirectoryInfo d = (DirectoryInfo)active;
                list.AddRange(d.GetDirectories());
                list.AddRange(d.GetFiles());
                CustomFolderInfo x = new CustomFolderInfo(this,0,list.ToArray());
                return x;
            }
            else
            {
                Console.Clear();
                FileStream fs = new FileStream(active.FullName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                Console.WriteLine(sr.ReadToEnd());

                sr.Close();
                fs.Close();
            }

            return null;
        }

        public CustomFolderInfo GetPrevItem()
        {
            return prev;
        }
    }
}
