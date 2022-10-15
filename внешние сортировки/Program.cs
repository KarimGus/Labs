using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var file2 = Directory.GetCurrentDirectory() + "\\F2.txt";
        var file1 = Directory.GetCurrentDirectory() + "\\F1.txt";
        var file0 = Directory.GetCurrentDirectory() + "\\F0.txt";
        using (FileStream fs1 = File.Create(file1))
            fs1.Dispose();
        using (FileStream fs2 = File.Create(file2)) 
        fs2.Dispose();
        try
        {
            Console.Write("введите число элементов:");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.Write("введите минимальное число:");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.Write("введите максимальное число:");
            int max = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("генерирую числа");
            MakeRNDFile(count, file0, min, max);
        }
        catch (InvalidCastException e)
        {
            Console.WriteLine("вы ввели неправильный формат" + e);
        }
        MergeSort(file0, file1, file2);
        Console.Read();
    }

    static void MakeRNDFile(int count, string file0, int min, int max)
    {
        Random rnd = new Random();
        using (FileStream fs0 = File.Create(file0))
            fs0.Dispose();
        using (StreamWriter sr = new StreamWriter (file0))
        {
            for (int i = 0; i < count; i++)
                sr.WriteLine(rnd.Next(min, max));
        }
        string[] sre = File.ReadAllLines(file0);
        foreach (string e in sre)
            Console.WriteLine(e);
    }

    public static void DistributeFiles(string file0, string file1, string file2)
    {
        int counter = 0;
        int tmp = int.MinValue;
        int tmp1 = int.MinValue;
        int tmp2 = int.MinValue;
        bool fileSwitch = true;
        string[] fs = File.ReadAllLines(file0);
        StreamWriter sw1 = new StreamWriter(file1);
        StreamWriter sw2 = new StreamWriter(file2);        
        foreach (var currentLine in fs)
        {
            int currentNumber = int.Parse(currentLine);
            if (currentNumber >= tmp & fileSwitch)
            {
                sw1.WriteLine(currentLine, true);
                tmp = currentNumber;
                tmp1 = currentNumber;
                Console.WriteLine('\t' +"1file:" + currentLine );
            }
            else
            if (currentNumber >= tmp & fileSwitch)
            {
                sw2.WriteLine(currentLine, true);
                tmp = currentNumber;
                tmp2 = currentNumber;
                Console.WriteLine('\t' + "2file:" + currentLine);
            }
            else
            {
                switch (fileSwitch)
                {
                    case true:
                        sw2.WriteLine(currentLine, true);
                        tmp2 = currentNumber;
                        Console.WriteLine('\t' + "2file:" + currentLine);
                        break;
                    case false:
                        sw1.WriteLine(currentLine, true);
                        tmp1 = currentNumber;
                        Console.WriteLine('\t' + "1file:" + currentLine);
                        break;
                }
                counter++;
                fileSwitch = !fileSwitch;
                tmp = int.MinValue;
            }
        }
        sw1.Close();
        sw2.Close();
        sw1.Dispose();
        sw2.Dispose();
    }

    public static void MergeFiles(string file0, string file1, string file2)
    {      
        int j = 0; 
        var list1 = File.ReadAllLines(file1);
        var list2 = File.ReadAllLines(file2);    
        using (StreamWriter wr = new StreamWriter(file0))
        {
            for (int i=0; i < list1.Length;i++)
            {
                while(j<list2.Length)
                {
                    if (i < list1.Length && int.Parse(list2[j]) <= int.Parse(list1[i]))
                    {
                       
                        wr.WriteLine(list2[j]);
                        j++;
                    } 
                    else
                    {
                        wr.WriteLine(list1[i]);
                        i++;
                    }                  
                }
                wr.WriteLine(list1[i]);
            }
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


        


