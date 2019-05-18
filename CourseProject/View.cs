using System.IO;

namespace CourseProject
{
    class View
    {
        const int pointsX = 5;
        const int pointsT = 700;

        public void Writer(string path, double[,] data)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write("{");
                for (int i = 0; i < pointsT; i++)
                {
                    writer.Write("{");
                    for (int j = 0; j < pointsX; j++)
                    {
                        writer.Write(data[i, j]);
                        if (j != pointsX - 1)
                            writer.Write(",");
                    }
                    writer.Write("}");
                    if (i != pointsT - 1)
                        writer.Write(",");
                }
                writer.Write("}");
            }
        }
    }
}
