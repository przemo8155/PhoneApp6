using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp6.Resources;
using Microsoft.Devices.Sensors;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.BackgroundAudio;
using System.Diagnostics;
using Windows.Storage;

namespace PhoneApp6
{
    public partial class Labirynth : PhoneApplicationPage
    {
        Others othersClass = new Others();
        private const double _gravity = 4.05; // przyspieszenie
        private const double _damping = 0.3;  // odbicie
        private const double _mul = 64.0;    // czulosc

        private const double _widthMinus = 115;
        private const double _heightMinus = 145;
        private const double _sMinus = -36;

        private Accelerometer _accel = new Accelerometer();
        private double _sx = -20.0;
        private double _sy = -20.0;
        private double _vx = 0.0;
        private double _vy = 0.0;
        private double _ax = 0.0;
        private double _ay = 0.0;
        private double _time = DateTime.Now.Ticks;

        private DispatcherTimer dispatcherTimer;
        private int timerCounter = timeLeft;
        private const int timeLeft = 20;
       
        
        public int publicLevel = 1;

        private double _width, _height;

        List<double> asd = new List<double>();

        public int randWall1, randWall2, randWall3, randWall4, randWall5, randWall6, randWall7, randWall8,
            randWall9, randWall10, randWall11, randWall12, randWall13, randWall14, randWall15, randWall16,
            randWall17, randWall18, randWall19, randWall20, randWall21, randWall22, randWall23, randWall24,
            randWall25, randWall26, randWall27, randWall28,
            randWallX1, randWallX2, randWallX3, randWallX4, randWallX5, randWallX6, randWallX7, randWallX8,
            randWallX9, randWallX10, randWallX11, randWallX12, randWallX13, randWallX14, randWallX15, randWallX16,
            randWallX17, randWallX18, randWallX19, randWallX20, randWallX21, randWallX22, randWallX23, randWallX24,
            randWallX25, randWallX26, randWallX27, randWallX28,
            randWallM1, randWallM2, randWallM3, randWallM4, randWallM5, randWallM6, randWallM7;

        BitmapImage imageWall = new BitmapImage(new Uri("Images/2.jpg", UriKind.Relative));
        BitmapImage imageClear = new BitmapImage(new Uri("Images/10.jpg", UriKind.Relative));

        public int endGameResult = 0;
        public int collisions = 0;

        public Labirynth()
        {

            InitializeComponent();
            _width = ((Application.Current.Host.Content.ActualWidth)) - _widthMinus;
            _height = ((Application.Current.Host.Content.ActualHeight)) - _heightMinus;
            SetImages();
            _accel.ReadingChanged +=
                new EventHandler<AccelerometerReadingEventArgs>(OnReadingChanged);
            _accel.Start();
            EnableTimer();
            SetLevel();
            SetStartupBallPosition();
            SetResult(false);
            

        }

        public void CalculateResult()
        {
            endGameResult = (timerCounter * 1000) - (collisions * 3);
        }

        public void SetResult(bool visible)
        {
            if(visible)
            {
                yourResultText.Visibility = Visibility.Visible;
                endGameResultText.Text = endGameResult.ToString();
                endGameResultText.Visibility = Visibility.Visible;
            }
            else
            {
                yourResultText.Visibility = Visibility.Collapsed;
                endGameResultText.Text = String.Empty;
                endGameResultText.Visibility = Visibility.Collapsed;
            }
        }


        public void SetImages()
        {
            var obj = App.Current as App;

            const int randFromLevel = 23;
            int maxRand = randFromLevel - obj.selectedLevel;
            wall1.Source = imageClear;
            wall2.Source = imageClear;
            wall3.Source = imageClear;
            wall4.Source = imageClear;
            wall5.Source = imageClear;
            wall6.Source = imageClear;
            wall7.Source = imageClear;
            wall8.Source = imageClear;
            wall9.Source = imageClear;
            wall10.Source = imageClear;
            wall11.Source = imageClear;
            wall12.Source = imageClear;
            wall13.Source = imageClear;
            wall14.Source = imageClear;
            wall15.Source = imageClear;
            wall16.Source = imageClear;
            wall17.Source = imageClear;
            wall18.Source = imageClear;
            wall19.Source = imageClear;
            wall20.Source = imageClear;
            wall21.Source = imageClear;
            wall22.Source = imageClear;
            wall23.Source = imageClear;
            wall24.Source = imageClear;
            wall25.Source = imageClear;
            wall26.Source = imageClear;
            wall27.Source = imageClear;
            wall28.Source = imageClear;

            wallX1.Source = imageClear;
            wallX2.Source = imageClear;
            wallX3.Source = imageClear;
            wallX4.Source = imageClear;
            wallX5.Source = imageClear;
            wallX6.Source = imageClear;
            wallX7.Source = imageClear;
            wallX8.Source = imageClear;
            wallX9.Source = imageClear;
            wallX10.Source = imageClear;
            wallX11.Source = imageClear;
            wallX12.Source = imageClear;
            wallX13.Source = imageClear;
            wallX14.Source = imageClear;
            wallX15.Source = imageClear;
            wallX16.Source = imageClear;
            wallX17.Source = imageClear;
            wallX18.Source = imageClear;
            wallX19.Source = imageClear;
            wallX20.Source = imageClear;
            wallX21.Source = imageClear;
            wallX22.Source = imageClear;
            wallX23.Source = imageClear;
            wallX24.Source = imageClear;
            wallX25.Source = imageClear;
            wallX26.Source = imageClear;
            wallX27.Source = imageClear;
            wallX28.Source = imageClear;

            wallM1.Source = imageClear;
            wallM2.Source = imageClear;
            wallM3.Source = imageClear;
            wallM4.Source = imageClear;
            wallM5.Source = imageClear;
            wallM6.Source = imageClear;
            wallM7.Source = imageClear;

            Random rand = new Random();
            randWall1 = rand.Next(1, maxRand);
            randWall2 = rand.Next(1, maxRand);
            randWall3 = rand.Next(1, maxRand);
            randWall4 = rand.Next(1, maxRand);
            randWall5 = rand.Next(1, maxRand);
            randWall6 = rand.Next(1, maxRand);
            randWall7 = rand.Next(1, maxRand);
            randWall8 = rand.Next(1, maxRand);
            randWall9 = rand.Next(1, maxRand);
            randWall10 = rand.Next(1, maxRand);
            randWall11 = rand.Next(1, maxRand);
            randWall12 = rand.Next(1, maxRand);
            randWall13 = rand.Next(1, maxRand);
            randWall14 = rand.Next(1, maxRand);
            randWall15 = rand.Next(1, maxRand);
            randWall16 = rand.Next(1, maxRand);
            randWall17 = rand.Next(1, maxRand);
            randWall18 = rand.Next(1, maxRand);
            randWall19 = rand.Next(1, maxRand);
            randWall20 = rand.Next(1, maxRand);
            randWall21 = rand.Next(1, maxRand);
            randWall22 = rand.Next(1, maxRand);
            randWall23 = rand.Next(1, maxRand);
            randWall24 = rand.Next(1, maxRand);
            randWall25 = rand.Next(1, maxRand);
            randWall26 = rand.Next(1, maxRand);
            randWall27 = rand.Next(1, maxRand);
            randWall28 = rand.Next(1, maxRand);

            randWallX1 = rand.Next(1, maxRand);
            randWallX2 = rand.Next(1, maxRand);
            randWallX3 = rand.Next(1, maxRand);
            randWallX4 = rand.Next(1, maxRand);
            randWallX5 = rand.Next(1, maxRand);
            randWallX6 = rand.Next(1, maxRand);
            randWallX7 = rand.Next(1, maxRand);
            randWallX8 = rand.Next(1, maxRand);
            randWallX9 = rand.Next(1, maxRand);
            randWallX10 = rand.Next(1, maxRand);
            randWallX11 = rand.Next(1, maxRand);
            randWallX12 = rand.Next(1, maxRand);
            randWallX13 = rand.Next(1, maxRand);
            randWallX14 = rand.Next(1, maxRand);
            randWallX15 = rand.Next(1, maxRand);
            randWallX16 = rand.Next(1, maxRand);
            randWallX17 = rand.Next(1, maxRand);
            randWallX18 = rand.Next(1, maxRand);
            randWallX19 = rand.Next(1, maxRand);
            randWallX20 = rand.Next(1, maxRand);
            randWallX21 = rand.Next(1, maxRand);
            randWallX22 = rand.Next(1, maxRand);
            randWallX23 = rand.Next(1, maxRand);
            randWallX24 = rand.Next(1, maxRand);
            randWallX25 = rand.Next(1, maxRand);
            randWallX26 = rand.Next(1, maxRand);
            randWallX27 = rand.Next(1, maxRand);
            randWallX28 = rand.Next(1, maxRand);

            randWallM1 = rand.Next(1, maxRand);
            randWallM2 = rand.Next(1, maxRand);
            randWallM3 = rand.Next(1, maxRand);
            randWallM4 = rand.Next(1, maxRand);
            randWallM5 = rand.Next(1, maxRand);
            randWallM6 = rand.Next(1, maxRand);
            randWallM7 = rand.Next(1, maxRand);

            if(randWall9 == 2 && randWall10 == 2 && randWallX1 == 2)
            {
                int result = rand.Next(1, 3);
                switch(result)
                {
                    case 1:
                        randWall9 = 1;
                        break;
                    case 2:
                        randWall10 = 1;
                        break;
                    case 3:
                        randWallX1 = 1;
                        break;
                }
            }

            if(randWallX3 == 2 && randWallX4 == 2 && randWallM2 == 2 && randWall5 == 2 & randWall1 == 2)
            {
                int result = rand.Next(1, 5);
                switch(result)
                {
                    case 1:
                        randWallX3 = 1;
                        break;
                    case 2:
                        randWallX4 = 1;
                        break;
                    case 3:
                        randWallM2 = 1;
                        break;
                    case 4:
                        randWall5 = 1;
                        break;
                    case 5:
                        randWall1 = 1;
                        break;
                }
               

            }

            if(randWall9 == 2 && randWall10 == 2 && randWallM1 == 2 && randWall12 == 2 && randWallX18 == 2 && randWall2 == 2)
            {
                int result = rand.Next(1, 6);
                switch(result)
                {
                    case 1:
                        randWall9 = 1;
                        break;
                    case 2:
                        randWall10 = 1;
                        break;
                    case 3:
                        randWallM1 = 1;
                        break;
                    case 4:
                        randWall12 = 1;
                        break;
                    case 5:
                        randWallX18 = 1;
                        break;
                    case 6:
                        randWall2 = 1;
                        break;
                }
                    
            }

            if (randWall9 == 2 && randWall10 == 2 && randWallM1 == 2 && randWall7 == 2 && randWallX4 == 2 && randWall2 == 2)
            {
                int result = rand.Next(1, 6);
                switch (result)
                {
                    case 1:
                        randWall9 = 1;
                        break;
                    case 2:
                        randWall10 = 1;
                        break;
                    case 3:
                        randWallM1 = 1;
                        break;
                    case 4:
                        randWall7 = 1;
                        break;
                    case 5:
                        randWallX4 = 1;
                        break;
                    case 6:
                        randWall2 = 1;
                        break;
                }

            }

            if (randWall9 == 2 && randWall10 == 2 && randWallM2 == 2 && randWall7 == 2 && randWallX2 == 2 && randWall2 == 2)
            {
                int result = rand.Next(1, 6);
                switch (result)
                {
                    case 1:
                        randWall9 = 1;
                        break;
                    case 2:
                        randWall10 = 1;
                        break;
                    case 3:
                        randWallM2 = 1;
                        break;
                    case 4:
                        randWall7 = 1;
                        break;
                    case 5:
                        randWallX2 = 1;
                        break;
                    case 6:
                        randWall2 = 1;
                        break;
                }

            }

            if (randWall9 == 2 && randWall5 == 2 && randWallM2 == 2 && randWall7 == 2 && randWallX17 == 2 && randWall2 == 2)
            {
                int result = rand.Next(1, 6);
                switch (result)
                {
                    case 1:
                        randWall9 = 1;
                        break;
                    case 2:
                        randWall5 = 1;
                        break;
                    case 3:
                        randWallM2 = 1;
                        break;
                    case 4:
                        randWall7 = 1;
                        break;
                    case 5:
                        randWallX17 = 1;
                        break;
                    case 6:
                        randWall2 = 1;
                        break;
                }

            }

            if (randWall9 == 2 && randWall10 == 2 && randWall11 == 2 && randWall12 == 2 && randWallM1 == 2)
            {
                int result = rand.Next(1, 5);
                switch(result)
                {
                    case 1:
                        randWall9 = 1;
                        break;
                    case 2:
                        randWall10 = 1;
                        break;
                    case 3:
                        randWall11 = 1;
                        break;
                    case 4:
                        randWall12 = 1;
                        break;
                    case 5:
                        randWallM1 = 1;
                        break;
                }
            }

            if (randWall1 == 2 && randWall5 == 2 && randWall7 == 2 && randWall2 == 2 && randWallM2 == 2)
            {
                int result = rand.Next(1, 5);
                switch (result)
                {
                    case 1:
                        randWall1 = 1;
                        break;
                    case 2:
                        randWall5 = 1;
                        break;
                    case 3:
                        randWall7 = 1;
                        break;
                    case 4:
                        randWall2 = 1;
                        break;
                    case 5:
                        randWallM2 = 1;
                        break;
                }
            }

            if (randWall14 == 2 && randWall13 == 2 && randWall16 == 2 && randWall15 == 2 && randWallM3 == 2)
            {
                int result = rand.Next(1, 5);
                switch (result)
                {
                    case 1:
                        randWall14 = 1;
                        break;
                    case 2:
                        randWall13 = 1;
                        break;
                    case 3:
                        randWall16 = 1;
                        break;
                    case 4:
                        randWall15 = 1;
                        break;
                    case 5:
                        randWallM3 = 1;
                        break;
                }
            }

            if (randWall20 == 2 && randWall19 == 2 && randWall17 == 2 && randWall18 == 2 && randWallM4 == 2)
            {
                int result = rand.Next(1, 5);
                switch (result)
                {
                    case 1:
                        randWall20 = 1;
                        break;
                    case 2:
                        randWall19 = 1;
                        break;
                    case 3:
                        randWall17 = 1;
                        break;
                    case 4:
                        randWall18 = 1;
                        break;
                    case 5:
                        randWallM4 = 1;
                        break;
                }
            }


            if (randWall4 == 2 && randWall6 == 2 && randWall8 == 2 && randWall3 == 2 && randWallM5 == 2)
            {
                int result = rand.Next(1, 5);
                switch (result)
                {
                    case 1:
                        randWall4 = 1;
                        break;
                    case 2:
                        randWall6 = 1;
                        break;
                    case 3:
                        randWall8 = 1;
                        break;
                    case 4:
                        randWall3 = 1;
                        break;
                    case 5:
                        randWallM5 = 1;
                        break;
                }
            }

            if (randWall24 == 2 && randWall23 == 2 && randWall21 == 2 && randWall22 == 2 && randWallM6 == 2)
            {
                int result = rand.Next(1, 5);
                switch (result)
                {
                    case 1:
                        randWall24 = 1;
                        break;
                    case 2:
                        randWall23 = 1;
                        break;
                    case 3:
                        randWall21 = 1;
                        break;
                    case 4:
                        randWall22 = 1;
                        break;
                    case 5:
                        randWallM6 = 1;
                        break;
                }
            }

            if (randWall27 == 2 && randWall28 == 2 && randWall26 == 2 && randWall25 == 2 && randWallM7 == 2)
            {
                int result = rand.Next(1, 5);
                switch (result)
                {
                    case 1:
                        randWall27 = 1;
                        break;
                    case 2:
                        randWall28 = 1;
                        break;
                    case 3:
                        randWall26 = 1;
                        break;
                    case 4:
                        randWall25 = 1;
                        break;
                    case 5:
                        randWallM7 = 1;
                        break;
                }
            }

            if(randWallX18 == 2 && randWallX20 == 2 && randWallX22 == 2 && randWallX24 == 2 && randWallX26 == 2 && randWallX28 == 2)
            {
                int result = rand.Next(1, 6);
                switch (result)
                {
                    case 1:
                        randWallX18 = 1;
                        break;
                    case 2:
                        randWallX20 = 1;
                        break;
                    case 3:
                        randWallX22 = 1;
                        break;
                    case 4:
                        randWallX24 = 1;
                        break;
                    case 5:
                        randWallX26 = 1;
                        break;
                    case 6:
                        randWallX28 = 1;
                        break;
                }

            }


            if (randWallX3 == 2 && randWallX4 == 2 && randWallX5 == 2 && randWallX8 == 2 && randWallX10 == 2 && randWallX12 == 2 && randWallX16 == 2 && randWallX14 == 2)
            {
                int result = rand.Next(1, 8);
                switch (result)
                {
                    case 1:
                        randWallX3 = 1;
                        break;
                    case 2:
                        randWallX4 = 1;
                        break;
                    case 3:
                        randWallX5 = 1;
                        break;
                    case 4:
                        randWallX8 = 1;
                        break;
                    case 5:
                        randWallX10 = 1;
                        break;
                    case 6:
                        randWallX12 = 1;
                        break;
                    case 7:
                        randWallX16 = 1;
                        break;
                    case 8:
                        randWallX14 = 1;
                        break;
                }

            }


            if (randWallX1 == 2 && randWallX2 == 2 && randWallX6 == 2 && randWallX7 == 2 && randWallX9== 2 && randWallX11 == 2 && randWallX15 == 2 && randWallX13 == 2)
            {
                int result = rand.Next(1, 8);
                switch (result)
                {
                    case 1:
                        randWallX1 = 1;
                        break;
                    case 2:
                        randWallX2 = 1;
                        break;
                    case 3:
                        randWallX6 = 1;
                        break;
                    case 4:
                        randWallX7 = 1;
                        break;
                    case 5:
                        randWallX9 = 1;
                        break;
                    case 6:
                        randWallX11 = 1;
                        break;
                    case 7:
                        randWallX15 = 1;
                        break;
                    case 8:
                        randWallX13 = 1;
                        break;
                }

            }

            if (randWallX17 == 2 && randWallX19 == 2 && randWallX21 == 2 && randWallX23 == 2 && randWallX25 == 2 && randWallX27 == 2)
            {
                int result = rand.Next(1, 6);
                switch (result)
                {
                    case 1:
                        randWallX17 = 1;
                        break;
                    case 2:
                        randWallX19 = 1;
                        break;
                    case 3:
                        randWallX21 = 1;
                        break;
                    case 4:
                        randWallX23 = 1;
                        break;
                    case 5:
                        randWallX25 = 1;
                        break;
                    case 6:
                        randWallX27 = 1;
                        break;
                }

            }


            if (randWall1 == 2) wall1.Source = imageWall;
            if (randWall2 == 2) wall2.Source = imageWall;
            if (randWall3 == 2) wall3.Source = imageWall;
            if (randWall4 == 2) wall4.Source = imageWall;
            if (randWall5 == 2) wall5.Source = imageWall;
            if (randWall6 == 2) wall6.Source = imageWall;
            if (randWall7 == 2) wall7.Source = imageWall;
            if (randWall8 == 2) wall8.Source = imageWall;
            if (randWall9 == 2) wall9.Source = imageWall;
            if (randWall10 == 2) wall10.Source = imageWall;
            if (randWall11 == 2) wall11.Source = imageWall;
            if (randWall12 == 2) wall12.Source = imageWall;
            if (randWall13 == 2) wall13.Source = imageWall;
            if (randWall14 == 2) wall14.Source = imageWall;
            if (randWall15 == 2) wall15.Source = imageWall;
            if (randWall16 == 2) wall16.Source = imageWall;
            if (randWall17 == 2) wall17.Source = imageWall;
            if (randWall18 == 2) wall18.Source = imageWall;
            if (randWall19 == 2) wall19.Source = imageWall;
            if (randWall20 == 2) wall20.Source = imageWall;
            if (randWall21 == 2) wall21.Source = imageWall;
            if (randWall22 == 2) wall22.Source = imageWall;
            if (randWall23 == 2) wall23.Source = imageWall;
            if (randWall24 == 2) wall24.Source = imageWall;
            if (randWall25 == 2) wall25.Source = imageWall;
            if (randWall26 == 2) wall26.Source = imageWall;
            if (randWall27 == 2) wall27.Source = imageWall;
            if (randWall28 == 2) wall28.Source = imageWall;

            if (randWallX1 == 2) wallX1.Source = imageWall;
            if (randWallX2 == 2) wallX2.Source = imageWall;
            if (randWallX3 == 2) wallX3.Source = imageWall;
            if (randWallX4 == 2) wallX4.Source = imageWall;
            if (randWallX5 == 2) wallX5.Source = imageWall;
            if (randWallX6 == 2) wallX6.Source = imageWall;
            if (randWallX7 == 2) wallX7.Source = imageWall;
            if (randWallX8 == 2) wallX8.Source = imageWall;
            if (randWallX9 == 2) wallX9.Source = imageWall;
            if (randWallX10 == 2) wallX10.Source = imageWall;
            if (randWallX11 == 2) wallX11.Source = imageWall;
            if (randWallX12 == 2) wallX12.Source = imageWall;
            if (randWallX13 == 2) wallX13.Source = imageWall;
            if (randWallX14 == 2) wallX14.Source = imageWall;
            if (randWallX15 == 2) wallX15.Source = imageWall;
            if (randWallX16 == 2) wallX16.Source = imageWall;
            if (randWallX17 == 2) wallX17.Source = imageWall;
            if (randWallX18 == 2) wallX18.Source = imageWall;
            if (randWallX19 == 2) wallX19.Source = imageWall;
            if (randWallX20 == 2) wallX20.Source = imageWall;
            if (randWallX21 == 2) wallX21.Source = imageWall;
            if (randWallX22 == 2) wallX22.Source = imageWall;
            if (randWallX23 == 2) wallX23.Source = imageWall;
            if (randWallX24 == 2) wallX24.Source = imageWall;
            if (randWallX25 == 2) wallX25.Source = imageWall;
            if (randWallX26 == 2) wallX26.Source = imageWall;
            if (randWallX27 == 2) wallX27.Source = imageWall;
            if (randWallX28 == 2) wallX28.Source = imageWall;

            if (randWallM1 == 2) wallM1.Source = imageWall;
            if (randWallM2 == 2) wallM2.Source = imageWall;
            if (randWallM3 == 2) wallM3.Source = imageWall;
            if (randWallM4 == 2) wallM4.Source = imageWall;
            if (randWallM5 == 2) wallM5.Source = imageWall;
            if (randWallM6 == 2) wallM6.Source = imageWall;
            if (randWallM7 == 2) wallM7.Source = imageWall;

        }


        public void OnReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {

            double time = (e.Timestamp.Ticks - _time) / 10000000.0;
            double ax = ((e.X * _gravity) + _ax) / 2.0;
            double ay = ((e.Y * _gravity) + _ay) / 2.0;

            double vx = _vx + (ax * time);
            double vy = _vy + (ay * time);

            double sx = _sx + ((((_vx + vx) / 2.0) * time) * _mul);
            double sy = _sy - ((((_vy + vy) / 2.0) * time) * _mul);

            double _remsx = _sx;
            double _remsy = _sy;





            //dolna sciana
            if (sx < _sMinus)
            {
                sx = _sMinus;
                vx = -vx * _damping;
                collisions += 1;
            }

            //gorna sciana
            else if (sx > _width)
            {
                sx = _width;
                vx = -vx * _damping;
                collisions += 1;
            }

            //lewa sciana
            if (sy < _sMinus)
            {
                sy = _sMinus;
                vy = -vy * _damping;
                collisions += 1;
            }

            //prawa sciana
            else if (sy > _height)
            {
                sy = _height;
                vy = -vy * _damping;
                collisions += 1;
            }



            //wall1


            else if (sy > 120 && sx < 50 && sy < 150)
            {
                if (randWall1 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 120;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 150;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }






            //wall2

            else if (sy > 120 && sx > 280 && sy < 150)
            {
                if (randWall2 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 120;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 150;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }



            //wall3


            else if (sy > 403 && sx > 280 && sy < 433)
            {
                if (randWall3 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 403;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 433;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }



            //wall4
            else if (sy > 403 && sx < 50 && sy < 433)
            {
                if (randWall4 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 403;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 433;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall5
            else if (sy > 120 && sx > 50 && sx < 120 && sy < 150)
            {
                if (randWall5 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 120;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 150;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall6
            else if (sy > 403 && sx > 50 && sx < 120 && sy < 433)
            {
                if (randWall6 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 403;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 433;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall7
            else if (sy > 120 && sx < 280 && sx > 210 && sy < 150)
            {
                if (randWall7 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 120;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 150;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall8
            else if (sy > 403 && sx < 280 && sx > 210 && sy < 433)
            {
                if (randWall8 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 403;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 433;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall9
            else if (sy > 32 && sx < 50 && sy < 62)
            {
                if (randWall9 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 32;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 62;
                        vy = -vy * _damping;
                        collisions += 1;
                    }

                }

            }

            //wall10

            else if (sy > 32 && sx > 49 && sx < 120 && sy < 62)
            {
                if (randWall10 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 32;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 62;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall11
            else if (sy > 32 && sx > 280 && sy < 62)
            {
                if (randWall11 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 32;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 62;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }


            //wall12
            else if (sy > 32 && sx < 280 && sx > 210 && sy < 62)
            {
                if (randWall12 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 32;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 62;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall13
            else if (sy > 213 && sx > 50 && sx < 120 && sy < 243)
            {
                if (randWall13 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 213;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 243;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall14
            else if (sy > 213 && sx < 50 && sy < 243)
            {
                if (randWall14 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 213;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 243;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }


            //wall15
            else if (sy > 213 && sx > 210 && sx < 280 && sy < 243)
            {
                if (randWall15 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 213;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 243;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall16
            else if (sy > 213 && sx > 280 && sy < 243)
            {
                if (randWall16 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 213;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 243;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall17
            else if (sy > 308 && sx > 210 && sx < 280 && sy < 338)
            {
                if (randWall17 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 308;
                        vy = -vy * _damping;
                        collisions += 1;

                    }
                    else
                    {
                        sy = 338;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall18
            else if (sy > 308 && sx > 280 && sy < 338)
            {
                if (randWall18 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 308;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 338;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }


            //wall19
            else if (sy > 308 && sx > 50 && sx < 120 && sy < 338)
            {
                if (randWall19 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 308;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 338;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }


            //wall20
            else if (sy > 308 && sx < 50 && sy < 338)
            {
                if (randWall20 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 308;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 338;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }


            }

            //wall21
            else if (sy > 495 && sx < 280 && sx > 210 && sy < 525)
            {
                if (randWall21 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 495;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 525;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall22
            else if (sy > 495 && sx > 280 && sy < 525)
            {
                if (randWall22 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 495;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 525;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall23
            else if (sy > 495 && sx > 50 && sx < 120 && sy < 525)
            {
                if (randWall23 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 495;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 525;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall24
            else if (sy > 495 && sx < 50 && sy < 525)
            {
                if (randWall24 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 495;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 525;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall25
            else if (sy > 560 && sx > 280 && sy < 590)
            {
                if (randWall25 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 560;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 590;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall26
            else if (sy > 560 && sx < 280 && sx > 210 && sy < 590)
            {
                if (randWall26 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 560;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 590;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall27
            else if (sy > 560 && sx > 50 && sx < 120 && sy < 590)
            {
                if (randWall27 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 560;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 590;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }


            //wall28
            else if (sy > 560 && sx < 50 && sy < 590)
            {
                if (randWall28 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 560;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 590;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }

            }













            //wallX1
            else if (sy < 35 && sx > 100 && sx < 130)
            {
                if (randWallX1 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX2
            else if (sy > 35 && sy < 125 && sx > 100 && sx < 130)
            {
                if (randWallX2 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX3
            else if (sy < 35 && sx > 195 && sx < 225)
            {
                if (randWallX3 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX4
            else if (sy > 35 && sy < 125 && sx > 195 && sx < 225)
            {
                if (randWallX4 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX5
            else if (sy > 125 && sy < 220 && sx > 195 && sx < 225)
            {
                if (randWallX5 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }


            //wallX6
            else if (sy > 125 && sy < 220 && sx > 100 && sx < 130)
            {
                if (randWallX6 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX7
            else if (sy > 220 && sy < 310 && sx > 100 && sx < 130)
            {
                if (randWallX7 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX8
            else if (sy > 220 && sy < 310 && sx > 195 && sx < 225)
            {
                if (randWallX8 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX9
            else if (sy > 310 && sy < 420 && sx > 100 && sx < 130)
            {
                if (randWallX9 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }


            //wallX10
            else if (sy > 310 && sy < 420 && sx > 195 && sx < 225)
            {
                if (randWallX10 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }
            }

            //wallX11
            else if (sy > 420 && sy < 510 && sx > 100 && sx < 130)
            {
                if (randWallX11 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }


            //wallX12
            else if (sy > 420 && sy < 510 && sx > 195 && sx < 225)
            {
                if (randWallX12 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX13
            else if (sy > 580 && sx > 100 && sx < 130)
            {
                if (randWallX13 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX14
            else if (sy > 580 && sx > 195 && sx < 225)
            {
                if (randWallX14 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX15
            else if (sy > 510 && sy < 580 && sx > 100 && sx < 130)
            {
                if (randWallX15 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 100;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 130;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX16
            else if (sy > 510 && sy < 580 && sx > 195 && sx < 225)
            {
                if (randWallX16 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 195;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 225;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX17
            else if (sy > 35 && sy < 125 && sx > 20 && sx < 50)
            {
                if (randWallX17 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 20;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 50;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }


            //wallX18
            else if (sy > 35 && sy < 125 && sx > 280 && sx < 310)
            {
                if (randWallX18 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 280;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 310;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX19
            else if (sy > 125 && sy < 220 && sx > 20 && sx < 50)
            {
                if (randWallX19 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 20;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 50;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX20
            else if (sy > 125 && sy < 220 && sx > 280 && sx < 310)
            {
                if (randWallX20 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 280;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 310;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX21
            else if (sy > 220 && sy < 310 && sx > 20 && sx < 50)
            {
                if (randWallX21 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 20;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 50;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX22
            else if (sy > 220 && sy < 310 && sx > 280 && sx < 310)
            {
                if (randWallX22 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 280;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 310;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX23
            else if (sy > 310 && sy < 420 && sx > 20 && sx < 50)
            {
                if (randWallX23 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 20;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 50;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wallX24
            else if (sy > 310 && sy < 420 && sx > 280 && sx < 310)
            {
                if (randWallX24 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 280;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 310;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }
            }

            //wall25
            else if (sy > 420 && sy < 510 && sx > 20 && sx < 50)
            {
                if (randWallX25 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 20;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 50;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall26
            else if (sy > 420 && sy < 510 && sx > 280 && sx < 310)
            {
                if (randWallX26 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 280;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 310;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }
            }


            //wall27
            else if (sy > 510 && sy < 590 && sx > 20 && sx < 50)
            {
                if (randWallX27 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 20;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 50;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }

            }

            //wall28
            else if (sy > 510 && sy < 590 && sx > 280 && sx < 310)
            {
                if (randWallX28 == 2)
                {
                    if (_remsx < sx)
                    {

                        sx = 280;
                        vx = -vx * _damping;
                        collisions += 1;
                    }

                    else
                    {
                        sx = 310;
                        vx = -vx * _damping;
                        collisions += 1;
                    }
                }
            }
















            //wallM1
            else if (sy > 32 && sx < 195 && sx > 130 && sy < 62)
            {
                if (randWallM1 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 32;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 62;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }



            //wallM2
            else if (sy > 120 && sx < 195 && sx > 130 && sy < 150)
            {
                if (randWallM2 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 120;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 150;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }


            //wallM3
            else if (sy > 213 && sx < 195 && sx > 130 && sy < 243)
            {
                if (randWallM3 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 213;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 243;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }



            //wallM4
            else if (sy > 308 && sx < 195 && sx > 130 && sy < 338)
            {
                if (randWallM4 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 308;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 338;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }
            //wallM5
            else if (sy > 403 && sx < 195 && sx > 130 && sy < 433)
            {
                if (randWallM5 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 403;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 433;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }


            //wallM6
            else if (sy > 495 && sx < 195 && sx > 130 && sy < 525)
            {
                if (randWallM6 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 495;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 525;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }


            //wallM7
            else if (sy > 560 && sx < 195 && sx > 130 && sy < 590)
            {
                if (randWallM7 == 2)
                {
                    if (_remsy < sy)
                    {
                        sy = 560;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                    else
                    {
                        sy = 590;
                        vy = -vy * _damping;
                        collisions += 1;
                    }
                }
            }





            _time = e.Timestamp.Ticks;
            _ax = ax;
            _ay = ay;
            _vx = vx;
            _vy = vy;
            _sx = sx;
            _sy = sy;


            Dispatcher.BeginInvoke(() => UpdateDisplay(sx, sy));
        }

        private void UpdateDisplay(double x, double y)
        {

            BallTransform.X = x;
            BallTransform.Y = y;
            if (x > _width - 40 && y > _height - 40)
            {

                var obj = App.Current as App;
                int unl = obj.unlockedLevel;
                int lvl = int.Parse(levelText.Text);
                if(lvl > unl)
                {
                    obj.unlockedLevel = int.Parse(levelText.Text);
                }
                
                

                ClearLevelGameScreen(true);
                

            }

        }

        private void EnableTimer()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            timerText.Text = timerCounter.ToString();

            if (timerCounter <= 5)
            {
                timerText.Foreground = new SolidColorBrush(Colors.Red);
                timerText.FontSize = 24;
            }

            if (timerCounter <= 0)
            {
                EndGameScreen(true);

                timerText.Text = "";
                dispatcherTimer.Stop();
                _accel.Stop();
            }

            timerCounter--;
        }

        private void EndGameScreen(bool visible)
        {
            if (visible)
            {
                gameOver.Text = "YOU LOSE!";
                exitLevel.Visibility = Visibility.Visible;
                image.Source = new BitmapImage(new Uri(@"Images/woodendgame.png", UriKind.RelativeOrAbsolute));
                if (randWall1 == 2) wall1.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall2 == 2) wall2.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall3 == 2) wall3.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall4 == 2) wall4.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall5 == 2) wall5.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall6 == 2) wall6.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall7 == 2) wall7.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall8 == 2) wall8.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall9 == 2) wall9.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall10 == 2) wall10.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall11 == 2) wall11.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall12 == 2) wall12.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall13 == 2) wall13.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall14 == 2) wall14.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall15 == 2) wall15.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall16 == 2) wall16.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall17 == 2) wall17.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall18 == 2) wall18.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall19 == 2) wall19.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall20 == 2) wall20.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall21 == 2) wall21.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall22 == 2) wall22.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall23 == 2) wall23.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall24 == 2) wall24.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall25 == 2) wall25.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall26 == 2) wall26.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall27 == 2) wall27.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWall28 == 2) wall28.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));

                if (randWallX1 == 2) wallX1.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX2 == 2) wallX2.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX3 == 2) wallX3.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX4 == 2) wallX4.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX5 == 2) wallX5.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX6 == 2) wallX6.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX7 == 2) wallX7.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX8 == 2) wallX8.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX9 == 2) wallX9.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX10 == 2) wallX10.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX11 == 2) wallX11.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX12 == 2) wallX12.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX13 == 2) wallX13.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX14 == 2) wallX14.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX15 == 2) wallX15.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX16 == 2) wallX16.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX17 == 2) wallX17.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX18 == 2) wallX18.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX19 == 2) wallX19.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX20 == 2) wallX20.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX21 == 2) wallX21.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX22 == 2) wallX22.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX23 == 2) wallX23.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX24 == 2) wallX24.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX25 == 2) wallX25.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX26 == 2) wallX26.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX27 == 2) wallX27.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallX28 == 2) wallX28.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));

                if (randWallM1 == 2) wallM1.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallM2 == 2) wallM2.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallM3 == 2) wallM3.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallM4 == 2) wallM4.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallM5 == 2) wallM5.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallM6 == 2) wallM6.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));
                if (randWallM7 == 2) wallM7.Source = new BitmapImage(new Uri(@"Images/2endgame.png", UriKind.RelativeOrAbsolute));

            }
            if (!visible)
            {
                gameOver.Text = String.Empty;
                exitLevel.Visibility = Visibility.Collapsed;
                image.Source = new BitmapImage(new Uri(@"Images/wood.png", UriKind.RelativeOrAbsolute));
            }
            dispatcherTimer.Stop();
            _accel.Stop();
            SetStartupBallPosition();


        }

        private void playAgain_Click(object sender, RoutedEventArgs e)
        {

            EndGameScreen(false);
            ClearLevelGameScreen(false);
            PlayAgain();
            SetStartupBallPosition();
        }

        private void exitLevel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SelectLevel.xaml", UriKind.Relative));
        }

        private void ClearLevelGameScreen(bool visible)
        {
            _accel.Stop();
            dispatcherTimer.Stop();
            CalculateResult();
            if (visible)
            {
                gameOver.Text = "YOU WON!";
                exitLevel.Visibility = Visibility.Visible;
                SetResult(true);
            }
            if (!visible)
            {
                gameOver.Text = String.Empty;
                exitLevel.Visibility = Visibility.Collapsed;
                SetResult(false);
            }

            



        }

        private void PlayAgain()
        {
            dispatcherTimer.Start();
            _accel.Start();
            
            timerCounter = timeLeft;
            timerText.Foreground = new SolidColorBrush(Colors.White);
            timerText.FontSize = 20;
            SetStartupBallPosition();

        }

        private void PlayNextLevel()
        {
            dispatcherTimer.Start();
            _accel.Start();
            SetStartupBallPosition();
            timerCounter = timeLeft;
            timerText.Foreground = new SolidColorBrush(Colors.White);
            timerText.FontSize = 20;
        }

        public void SetLevel()
        {
            var obj = App.Current as App;

            levelText.Text = obj.selectedLevel.ToString();

        }

        private void SetStartupBallPosition()
        {
            UpdateDisplay(-25, -20);
            BallTransform.X = -25;
            BallTransform.Y = -20;
        }

        public void RefreshPage()
        {
            _accel.Stop();
            _sx = -20.0;
            _sy = -20.0;
            _vx = 0.0;
            _vy = 0.0;
            _ax = 0.0;
            _ay = 0.0;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Labirynth.xaml", UriKind.Relative));

        }

        public List<double> CreateWall(double _posx, double _posy, double _vx, double _vy, int leftSideHitX, int leftSideHitY, double _remembersx, double _remembersy)
        {
            List<double> list = new List<double>();
            if (_posy > leftSideHitY && _posx < leftSideHitX)
            {
                if (_remembersy < _posy)
                {
                    _posy = 172;
                    _vy = -_vy * _damping;

                }

            }
            list.Add(_posy);
            list.Add(_vy);
            return list;
        }

    }
}