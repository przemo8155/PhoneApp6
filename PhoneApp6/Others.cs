using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp6
{
    public class Others
    {
        public void CheckIdle()
        {
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
        }



        /* public static void SetTile(RecipeDataItem item, string NavSource)
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
