using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLVoxelFileWriter
{
    public static int[, ,] LoadChunkFromXMLFile(int size, string fileName)
    {
        int[,,] voxelArray = new int[size, size, size];

        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");

        while (xmlReader.Read())
        {
            if (xmlReader.IsStartElement("Voxel"))
            {
                // Retrieves attributes and stores them as ints
                int x = int.Parse(xmlReader["x"]);
                int y = int.Parse(xmlReader["y"]);
                int z = int.Parse(xmlReader["z"]);

                xmlReader.Read();

                int value = int.Parse(xmlReader.Value);

                voxelArray[x, y, z] = value;
            }
        }

        return voxelArray;
    }
}