using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

internal class Program
{
    //jjkjkj
    private static void Main(string[] args)
    {
        using (FileStream fs1 = File.Create(Directory.GetCurrentDirectory() + "\\F1.txt"))  
        using (FileStream fs2 = File.Create(Directory.GetCurrentDirectory() + "\\F2.txt"))
        using (FileStream fs0 = File.Create(Directory.GetCurrentDirectory() + "\\F0.txt"))

        Console.Write("введите натуральное число:"); 
        int count = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("генерирую числа");

        MakeRNDFile(count);
        SeparateFiles(count);
        


    }
    public static void MakeRNDFile(int count)
    {
        Random rnd = new Random();
        using StreamWriter sr = new(Directory.GetCurrentDirectory() + "\\F0.txt");
        {
            for (int i = 0; i < count; i++) 
                sr.WriteLine(rnd.Next(0, 1000));                               
        }
    }
    public static void SeparateFiles(int count)
    {
        var pathf2 = Directory.GetCurrentDirectory() + "\\F2.txt";
        var pathf1 = Directory.GetCurrentDirectory() + "\\F1.txt";
        var pathf0 = Directory.GetCurrentDirectory() + "\\F0.txt";
        int b = int.MinValue;
        bool selectedfile = true;     
        StreamReader stream0 = new(pathf0);
        StreamWriter stream1 = new(pathf1);
        StreamWriter stream2 = new(pathf2);

        string line;
        while ((line = stream0.ReadLine()) != null)
        {
            Console.Write ('\t'+line+'\n');           
            if (Convert.ToInt32(line) > b & selectedfile)
            {
                stream1.WriteLine(line, true); ;
                b = Convert.ToInt32(line);
                Console.Write("f1:" +line); 
            }
            else           
            if (Convert.ToInt32(line) > b & selectedfile)
            {
                stream2.WriteLine(line, true);
                b = Convert.ToInt32(line);
                Console.Write(' ' + "f2:" +line);
            }
            else
            {
                switch (selectedfile)
                { 
                    case true:
                        stream2.WriteLine(line, true);
                        Console.Write(' ' + "f2:" + line);
                        break;
                    case false:
                        stream1.WriteLine(line, true);
                        Console.Write("f1:" + line);
                        break;
                }                
                selectedfile=!selectedfile;
                b = int.MinValue;
            }
        }     
        stream0.Close();
        stream1.Close();
        stream2.Close();
        stream0.Dispose();
        stream1.Dispose();      
        stream2.Dispose();  
    }

}
    

        

        

