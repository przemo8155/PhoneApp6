using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PhoneApp6
{
    public class Others
    {

        public void CheckIdle()
        {
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
        }

        public async void SaveFile(string text)
        {
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await folder.CreateFileAsync("data.txt", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, text);
        }


        /*

        public void SaveFile(string o)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if(!settings.Contains("level"))
            {
                settings.Add("level", o);
                settings.Save();
            }
            else
            {

            }
                       
            //else
            //{
            //    string i = settings["level"].ToString();
            //    int iInt = int.Parse(i);

            //    int oInt = int.Parse(o);

            //    if(iInt < oInt)
            //    {
            //        settings.Add("level", o);
                    
            //    }
            //}
            
        }

        public void ReadFile(string o)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("level"))
            {
                o = settings["level"].ToString();
            }
        }

        public string ReadFileString(string o)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("level"))
            {
                o = settings["level"].ToString();
                return o;
            }
            else
            {
                return null;
            }
        }




        
      public async void WriteFile(string con)
      {
          StorageFile sampleFile = await localFolder.CreateFileAsync("dataFile.txt", CreationCollisionOption.ReplaceExisting);
          await FileIO.WriteTextAsync(sampleFile, con);
      }

      public async Task<string> ReadFile(string asd)
      {
          try
          {
              StorageFile sampleFile = await localFolder.GetFileAsync("dataFile.txt");
              asd = await FileIO.ReadTextAsync(sampleFile);
              return asd;
          }
          catch (Exception)
          {
              return null;
          }
      }




      public void SaveFile(string path, string data)
      {
          using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
          {
              using (var fileStream = new IsolatedStorageFileStream(path + fileName, FileMode.Create, FileAccess.Write, storage))
              {
                  using (StreamWriter writer = new StreamWriter(fileStream))
                  {
                      writer.WriteLine(data);
                      writer.Close();
                  }
                  fileStream.Close();
              }
          }
      }



public static void SetTile(RecipeDataItem item, string NavSource)
       {
           FlipTileData tiledata = new FlipTileData
           {
               Title = item.Title,
               BackgroundImage = new Uri("ms-appx:///background1.png", UriKind.Relative),
               SmallBackgroundImage = new Uri("ms - appx:///background1.png", UriKind.Relative),
               BackTitle = item.Title,
               BackContent = item.Ingredients,
               BackBackgroundImage = 
           };
       }
       plik manifest
           <Extensions>
     <Extension ExtensionName = "LockScreen_Notification_IconCount" ConsumerID="{111DFF24-AA15-4A96-8006-2BFF8122084F}" TaskID="_default"/>
     <Extension ExtensionName = "LockScreen_Notification_TextField" ConsumerID="{111DFF24-AA15-4A96-8006-2BFF8122084F}" TaskID="_default"/>
     </Extensions>
        */
    }
}
