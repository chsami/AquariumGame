using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using AquariumGame._0.Screens;
using AquariumGame._0.User;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


namespace AquariumGame._0.FileHandler
{
    public class FileManager
    {

        public struct SaveGameData
        {
            public string PlayerName;
            public Vector2 AvatarPosition;
            public int Level;
            public int Score;
        }

        public static void saveGame(StorageDevice device)
        {
            SaveGameData data = new SaveGameData();
            data.PlayerName = "Hiro";
            data.AvatarPosition = new Vector2(360, 360);
            data.Level = 11;
            data.Score = 4200;

            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            Stream stream = container.CreateFile(filename);

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

            serializer.Serialize(stream, data);
            stream.Close();
        }

        public static bool loadGame(StorageDevice device, string name)
        {
            try
            {
                // Open a storage container.
                IAsyncResult result =
                    device.BeginOpenContainer(name, null, null);

                // Wait for the WaitHandle to become signaled.
                result.AsyncWaitHandle.WaitOne();

                StorageContainer container = device.EndOpenContainer(result);

                // Close the wait handle.
                result.AsyncWaitHandle.Close();

                string filename = "savegame.sav";

                // Check to see whether the save exists.
                if (!container.FileExists(filename))
                {
                    // If not, dispose of the container and return.
                    container.Dispose();
                    return false;
                }

                // Open the file.
                Stream stream = container.OpenFile(filename, FileMode.Open);

                XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

                SaveGameData data = (SaveGameData)serializer.Deserialize(stream);
                // Close the file.
                stream.Close();

                // Dispose the container.
                container.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

    }
}
