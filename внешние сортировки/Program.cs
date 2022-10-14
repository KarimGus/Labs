using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

internal class Program
{
    private static void Main(string[] args)
    {
        var file2 = Directory.GetCurrentDirectory() + "\\F2.txt";
        var file1 = Directory.GetCurrentDirectory() + "\\F1.txt";
        var file0 = Directory.GetCurrentDirectory() + "\\F0.txt";
        using FileStream fs1 = File.Create(file1);
        fs1.Dispose();
        using FileStream fs2 = File.Create(file2);
        fs2.Dispose(); 
        //try
        //{
        //    Console.Write("введите число элементов:");
        //    int count = Convert.ToInt32(Console.ReadLine());
        //    Console.Write("введите минимальное число:");
        //    int min = Convert.ToInt32(Console.ReadLine());
        //    Console.Write("введите максимальное число:");
        //    int max = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("генерирую числа");
        //    MakeRNDFile(count, file0, min, max);
        //}
        //catch (InvalidCastException e)
        //{
        //    Console.WriteLine("вы ввели неправильный формат" + e);
        //}    
        MergeSort(file0, file1, file2);
    }

     static void MakeRNDFile(int count, string file0,int min,int max)
    {
        Random rnd = new();
        using (FileStream fs0 = File.Create(file0))
            fs0.Dispose();
        using (StreamWriter sr = new(file0))
        {
            for (int i = 0; i < count; i++)
                sr.WriteLine(rnd.Next(min, max));
        }
        
    }

    public static void DistributeFiles(string file0, string file1, string file2)
    {
        int tmp = int.MinValue;
        int counter = 0;
        int tmp1 = 0;
        int tmp2 = 0;
        string[] fs = File.ReadAllLines(file0);
        bool fileSwitch = true;
        using StreamWriter sw1 = new(file1);
        using StreamWriter sw2 = new(file2);
        while (counter < fs.Length)
        {
            if (fileSwitch)//СВИТЧ НУЖЕН ДЛЯ СМЕНЫ ФАЙЛА КУДА ВЕДЕТСЯ ЗАПИСЬ,то есть для смены СЕРИИ 
            {
                do
                {
                    sw1.WriteLine(fs[counter], true);
                    tmp = Convert.ToInt32(fs[counter]);
                    Console.WriteLine("1:" + fs[counter] + '\t' + '\t');
                    counter++;
                } while (counter != fs.Length && Convert.ToInt32(fs[counter]) >= tmp);
                tmp1 = Convert.ToInt32(fs[counter]);//запсиь последнего числа в серии
                fileSwitch = !fileSwitch;
            }
            else if (!fileSwitch)
            {
                do
                {
                    sw2.WriteLine(fs[counter], true);
                    tmp = Convert.ToInt32(fs[counter]);
                    Console.WriteLine("2:" + fs[counter] + '\t' + '\t');
                    counter++;
                } while (counter != fs.Length && Convert.ToInt32(fs[counter]) >= tmp);
                tmp2 = Convert.ToInt32(fs[counter]);//запсиь последнего числа в серии
                fileSwitch = !fileSwitch;
            }
            tmp = int.MinValue;          
        }
    }

    public static void MergeFiles(string file0, string file1, string file2)
    {
        int count = 0;
        var sr1 = File.ReadLines(file1);
        var sr2 = File.ReadLines(file2);
        using StreamWriter wr = new(file0);      
            while (Math.Min(sr1.LongCount(), sr2.LongCount()) > count)
            {
                int fnumber1 = Convert.ToInt32(sr1.ElementAt(count));
                int fnumber2 = Convert.ToInt32(sr2.ElementAt(count));
                if (fnumber1 > fnumber2)
                {
                    wr.WriteLine(fnumber2);
                    wr.WriteLine(fnumber1);
                }
                else
                {
                    wr.WriteLine(fnumber1);
                    wr.WriteLine(fnumber2);
                }
                count++;
            }
            if (sr1.LongCount() > sr2.LongCount())
                while (count < sr1.LongCount())
                {
                    wr.WriteLine(Convert.ToInt32(sr1.ElementAt(count)));
                    count++;
                }
            if (sr2.LongCount() > sr1.LongCount())
                while (count < sr2.LongCount())
                {
                    wr.WriteLine(Convert.ToInt32(sr2.ElementAt(count)));
                    count++;
                }       
    }

    public static void MergeSort(string file0, string file1, string file2)
    {
        DistributeFiles(file0, file1, file2);
        MergeFiles(file0, file1, file2);
        while (!IsTextFileEmpty(file1) & !IsTextFileEmpty(file2))
        {
            DistributeFiles(file0, file1, file2);
            MergeFiles(file0, file1, file2);
        }
    }

    public static bool IsTextFileEmpty(string filePath)
    {
        var info = new FileInfo(filePath);
        if (info.Length == 0)
            return true;
        else return false;   
    }
}

  
    

        

        


