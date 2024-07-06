var source = "D:\\Downloads\\Wafer_Map_Datasets.npz\\tcp";

//var sourceStream = new BinaryReader(new FileStream($"{source}\\1.bin",FileMode.Open, FileAccess.Read));
//var tcpStream = new BinaryReader(new FileStream($"{source}\\1-tcp.bin", FileMode.Open, FileAccess.Read));
//var udpStream = new BinaryReader(new FileStream($"{source}\\1-udp.bin", FileMode.Open, FileAccess.Read));


var diff = CountBitDifferences($"{source}\\10.bin", $"{source}\\10-udp.bin");
Console.WriteLine(diff);

static int CountBitDifferences(string filePath1, string filePath2)
{
    using (FileStream fs1 = new FileStream(filePath1, FileMode.Open))
    using (FileStream fs2 = new FileStream(filePath2, FileMode.Open))
    {
        int differences = 0;
        int bufferSize = 4096; // 可以根据需要调整缓冲区大小
        byte[] buffer1 = new byte[bufferSize];
        byte[] buffer2 = new byte[bufferSize];

        while (true)
        {
            int read1 = fs1.Read(buffer1, 0, bufferSize);
            int read2 = fs2.Read(buffer2, 0, bufferSize);

            var min = Math.Min(read1, read2);

            for (int i = 0; i < min; i++)
            {
                if (buffer1[i] != buffer2[i])
                {
                    differences++;
                }
            }

            if(read1 != read2)
            {
                differences += (read1 - read2);
            }

            // 如果没有更多数据可读，退出循环
            if (read1 < bufferSize)
            {
                break;
            }
        }

        return differences;
    }
}