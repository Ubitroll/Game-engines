using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLVoxelFileWriter
{

    public static void SaveChunkToXMLFile(int[, ,] voxelArray, string fileName)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;

        // Create a write instance
        XmlWriter xmlWriter = XmlWriter.Create(fileName + ".xml", writerSettings);
        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("VoxelChunk");
        
        // iterate through all array elements
        for (int x = 0; x < voxelArray.GetLength(0); x++)
        {
            for (int y = 0; y < voxelArray.GetLength(1); y++)
            {
                for (int z = 0; z < voxelArray.GetLength(2); z++)
                {
                    if (voxelArray[x, y, z] != 0)
                    {
                        // Create a single voxel element
                        xmlWriter.WriteStartElement("Voxel");
                        // write the attributes to write for the indices x y and z
                        xmlWriter.WriteAttributeString("x", x.ToString());
                        xmlWriter.WriteAttributeString("y", y.ToString());
                        xmlWriter.WriteAttributeString("z", z.ToString());
                        // Store the voxel type
                        xmlWriter.WriteString(voxelArray[x, y, z].ToString());
                        // End voxel element
                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }
        // End the root element
        xmlWriter.WriteEndElement();
        // Write the end of the document
        xmlWriter.WriteEndDocument();
        // Close the document to save
        xmlWriter.Close();
        
    }

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