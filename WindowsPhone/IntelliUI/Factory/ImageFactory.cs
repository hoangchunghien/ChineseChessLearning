using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IntelliUI.Factory
{
    public class ImageFactory
    {
        public static BitmapImage getImage(char ch)
        {
            BitmapImage bitmap = new BitmapImage();

            switch (ch)
            {
                case 'G':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/rk.png", UriKind.Relative));
                    break;
                case 'A':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/ra.png", UriKind.Relative));
                    break;
                case 'M':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/rb.png", UriKind.Relative));
                    break;
                case 'R':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/rr.png", UriKind.Relative));
                    break;
                case 'C':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/rc.png", UriKind.Relative));
                    break;
                case 'K':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/rn.png", UriKind.Relative));
                    break;
                case 'P':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/rp.png", UriKind.Relative));
                    break;
                case 'g':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/bk.png", UriKind.Relative));
                    break;
                case 'a':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/ba.png", UriKind.Relative));
                    break;
                case 'm':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/bb.png", UriKind.Relative));
                    break;
                case 'r':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/br.png", UriKind.Relative));
                    break;
                case 'c':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/bc.png", UriKind.Relative));
                    break;
                case 'k':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/bn.png", UriKind.Relative));
                    break;
                case 'p':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/bp.png", UriKind.Relative));
                    break;
                case ' ':
                    bitmap = new BitmapImage();
                    break;
                case '-':
                    bitmap = new BitmapImage(new Uri("/IntelliUI;component/PNG/mask.png", UriKind.Relative));
                    break;
                default:
                    bitmap = new BitmapImage();
                    break;
            }

            return bitmap;
        }

    }
}
