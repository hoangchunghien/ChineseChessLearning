using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IntelliUI.Domain
{
    public class Board
    {
        private Char[,] bArr;
        
        public Board()
        {
            initializeBoard();
        }

        private void initializeBoard()
        {
            this.bArr = new char[,]
            {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','c',' ',' ',' ',' ',' ','c',' '},
                {'p',' ','p',' ','p',' ','p',' ','p'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'P',' ','P',' ','P',' ','P',' ','P'},
                {' ','C',' ',' ',' ',' ',' ','C',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'R','K','M','A','G','A','M','K','R'}
            };            
        }

        public Char[,] getBArr()
        {
            return this.bArr;
        }

        public void setBArr(char[,] bArr)
        {
            this.bArr = bArr;
        }
    }
}
