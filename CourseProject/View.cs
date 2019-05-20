using System.IO;

namespace CourseProject
{
    class View
    {
        Resolution resolution;
        public View (Resolution resolution)
        {
            this.resolution = resolution;
        }

        public void Writer(string path, double[,] data)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write("{");
                for (int i = 0; i < resolution.GetTPoints; i++)
                {
                    writer.Write("{");
                    for (int j = 0; j < resolution.GetXPoints; j++)
                    {
                        writer.Write(data[i, j]);
                        if (j != resolution.GetXPoints - 1)
                            writer.Write(",");
                    }
                    writer.Write("}");
                    if (i != resolution.GetTPoints - 1)
                        writer.Write(",");
                }
                writer.Write("}");
            }
        }
    }
}
