// Properties and Windows Form Designer generated code
//
// Copyright (C) 2009 Harvey Pham (harveypham@playxiangqi.com)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// NOTE: This AI is ported from C/C++. The original source code was obtained
// as part of HOXChess source code. HOXChess, in-turn, obtained the source
// from http://www.elephantbase.net/computer/stepbystep1.htm. The following
// are excepts from HOXChess source code:

// Start quote =============================================================
/***************************************************************************
 *  Copyright 2007-2009 Huy Phan  <huyphan@playxiangqi.com>                *
 *                      Bharatendra Boddu (bharathendra at yahoo dot com)  *
 *                                                                         * 
 *  This file is part of HOXChess.                                         *
 *                                                                         *
 *  HOXChess is free software: you can redistribute it and/or modify       *
 *  it under the terms of the GNU General Public License as published by   *
 *  the Free Software Foundation, either version 3 of the License, or      *
 *  (at your option) any later version.                                    *
 *                                                                         *
 *  HOXChess is distributed in the hope that it will be useful,            *
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of         *
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the          *
 *  GNU General Public License for more details.                           *
 *                                                                         *
 *  You should have received a copy of the GNU General Public License      *
 *  along with HOXChess.  If not, see <http://www.gnu.org/licenses/>.      *
 ***************************************************************************/

/////////////////////////////////////////////////////////////////////////////
// Name:            XQWLight.h
// Created:         10/11/2008
//
// Description:     This is 'XQWLight' Engine to interface with HOXChess.
//                  XQWLight is an open-source Xiangqi AI Engine
//                  written by Huang Chen at www.elephantbase.net
//
//  (Original Chinese URL)
//        http://www.elephantbase.net/computer/stepbystep1.htm
//
//  (Translated English URL using Goold Translate)
//       http://74.125.93.104/translate_c?hl=en&langpair=
//         zh-CN|en&u=http://www.elephantbase.net/computer/stepbystep1.htm&
//         usg=ALkJrhj7W0v3J1P-xmbufsWzYq7uKciL1w
/////////////////////////////////////////////////////////////////////////////
// End quote ===============================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.XQWLight_AI
{
    public class XQWLight_AI
    {
        public XQWLight_AI()
        {
        }

        static Random rand;

        static int m_searchDepth = 5;
        public void init_engine(int searchDepth)
        {
            m_searchDepth = searchDepth;
        }

        TimeSpan m_searchTime = new TimeSpan(0, 0, 1);
        public void set_search_time(int seconds)
        {
            m_searchTime = new TimeSpan(0, 0, seconds);
        }
        /* Only approximately... */

        public void init_game(sbyte[,] board, char side /* = 'w' */ )
        {
            rand = new Random();
            InitZobrist();
            LoadBook();
            Startup(board);

            if (side == 'b')
            {
                pos.ChangeSide();
            }
        }

        private void InitZobrist()
        {
            int i, j;
            RC4Struct rc4 = new RC4Struct();

            rc4.InitZero();
            m_zobrist.Player.InitRC4(rc4);
            for (i = 0; i < 14; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    m_zobrist.Table[i, j].InitRC4(rc4);
                }
            }
        }

        static void InitializeIntArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new int();
            }
        }

        static void ClearIntArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

        private void LoadBook()
        {
            try
            {
                using (FileStream fileStream = File.Open("book.dat", FileMode.Open))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    m_search.nBookSize = Convert.ToInt32(reader.BaseStream.Length) / 8;
                    if (m_search.nBookSize > BOOK_SIZE)
                        m_search.nBookSize = BOOK_SIZE;
                    m_search.BookTable = new BookItem[m_search.nBookSize];
                    for (uint book = 0; book < m_search.nBookSize; book++)
                    {
                        m_search.BookTable[book] = new BookItem();
                        m_search.BookTable[book].dwLock = reader.ReadUInt32();
                        m_search.BookTable[book].wmv = reader.ReadUInt16();
                        m_search.BookTable[book].wvl = reader.ReadUInt16();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Startup(sbyte[,] board)
        {
            pos.Startup(board);
        }

        public string generate_move()
        {
            SearchMain();
            string stdMove = _xqwlight2hox(m_search.mvResult);
            pos.MakeMove(m_search.mvResult);
            return stdMove;
        }

        public void on_human_move(string sMove)
        {
            string stdMove = sMove;
            int nMove = _hox2xqwlight(stdMove);
            m_search.mvResult = nMove;
            pos.MakeMove(m_search.mvResult);
        }

        void SearchMain()
        {
            int i, vl, nGenMoves;
            int[] mvs = new int[MAX_GEN_MOVES];
            InitializeIntArray(mvs);
            pos.nDistance = 0; // ³õÊ¼²½Êý

            DateTime startSearch = DateTime.Now;

            // ËÑË÷¿ª¾Ö¿â
            m_search.mvResult = SearchBook();
            if (m_search.mvResult != 0)
            {
                pos.MakeMove(m_search.mvResult);
                if (pos.RepStatus(3) == 0)
                {
                    pos.UndoMakeMove();
                    return;
                }
                pos.UndoMakeMove();
            }

            // ¼ì²éÊÇ·ñÖ»ÓÐÎ¨Ò»×ß·¨
            vl = 0;
            nGenMoves = pos.GenerateMoves(mvs, false);
            for (i = 0; i < nGenMoves; i++)
            {
                if (pos.MakeMove(mvs[i]))
                {
                    pos.UndoMakeMove();
                    m_search.mvResult = mvs[i];
                    vl++;
                }
            }
            if (vl == 1)
            {
                return;
            }

            // µü´ú¼ÓÉî¹ý³Ì
            for (i = 1; i <= m_searchDepth; i++)
            {
                vl = SearchRoot(i);
                // ËÑË÷µ½É±Æå£¬¾ÍÖÕÖ¹ËÑË÷
                if (vl > WIN_VALUE || vl < -WIN_VALUE)
                {
                    break;
                }

                if (DateTime.Now.CompareTo(startSearch.Add(m_searchTime)) > 0)
                {
                    break;
                }
                //Console.WriteLine( "SearchMain: Search depth START = {0}.\n",  i+1);
            }
            //Console.WriteLine( "SearchMain: Search depth = *** {0}.\n", i);
        }

        int SearchBook()
        {
            // 1. Èç¹ûÃ»ÓÐ¿ª¾Ö¿â£¬ÔòÁ¢¼´·µ»Ø
            if ( m_search.BookTable == null || m_search.nBookSize == 0)
            {
                return 0;
            }

            int i, vl, nBookMoves, mv;
            int[] mvs = new int[MAX_GEN_MOVES];
            InitializeIntArray(mvs);
            int[] vls = new int[MAX_GEN_MOVES];
            InitializeIntArray(vls);
            bool bMirror;
            BookItem bkToSearch = new BookItem();
            int lpbk = -1;
            PositionStruct posMirror = new PositionStruct();
            // ËÑË÷¿ª¾Ö¿âµÄ¹ý³ÌÓÐÒÔÏÂ¼¸¸ö²½Öè


            // 2. ËÑË÷µ±Ç°¾ÖÃæ
            bMirror = false;
            bkToSearch.dwLock = pos.zobr.dwLock1;
            lpbk = m_search.BinarySearch(bkToSearch);
            //lpbk = (BookItem *) bsearch(&bkToSearch, Search.BookTable, Search.nBookSize, sizeof(BookItem), CompareBook);
            // 3. Èç¹ûÃ»ÓÐÕÒµ½£¬ÄÇÃ´ËÑË÷µ±Ç°¾ÖÃæµÄ¾µÏñ¾ÖÃæ
            if (lpbk == -1)
            {
                bMirror = true;
                pos.Mirror(posMirror);
                bkToSearch.dwLock = posMirror.zobr.dwLock1;
                lpbk = m_search.BinarySearch(bkToSearch);
                //lpbk = (BookItem *) bsearch(&bkToSearch, Search.BookTable, Search.nBookSize, sizeof(BookItem), CompareBook);
            }
            // 4. Èç¹û¾µÏñ¾ÖÃæÒ²Ã»ÕÒµ½£¬ÔòÁ¢¼´·µ»Ø
            if (lpbk == -1)
            {
                return 0;
            }

            // 5. Èç¹ûÕÒµ½£¬ÔòÏòÇ°²éµÚÒ»¸ö¿ª¾Ö¿âÏî
            while (lpbk >= 0 && m_search.BookTable[lpbk].dwLock == bkToSearch.dwLock)
            {
                lpbk--;
            }
            lpbk++;
            // 6. °Ñ×ß·¨ºÍ·ÖÖµÐ´Èëµ½"mvs"ºÍ"vls"Êý×éÖÐ
            vl = nBookMoves = 0;
            while (lpbk < m_search.nBookSize && m_search.BookTable[lpbk].dwLock == bkToSearch.dwLock)
            {
                mv = (bMirror ? MIRROR_MOVE(m_search.BookTable[lpbk].wmv) : m_search.BookTable[lpbk].wmv);
                if (pos.LegalMove(mv))
                {
                    mvs[nBookMoves] = mv;
                    vls[nBookMoves] = m_search.BookTable[lpbk].wvl;
                    vl += vls[nBookMoves];
                    nBookMoves++;
                    if (nBookMoves == MAX_GEN_MOVES)
                    {
                        break; // ·ÀÖ¹"BOOK.DAT"ÖÐº¬ÓÐÒì³£Êý¾Ý
                    }
                }
                lpbk++;
            }
            if (vl == 0)
            {
                return 0; // ·ÀÖ¹"BOOK.DAT"ÖÐº¬ÓÐÒì³£Êý¾Ý
            }
            // 7. ¸ù¾ÝÈ¨ÖØËæ»úÑ¡ÔñÒ»¸ö×ß·¨
            vl = rand.Next() % vl;
            for (i = 0; i < nBookMoves; i++)
            {
                vl -= vls[i];
                if (vl < 0)
                {
                    break;
                }
            }
            return mvs[i];
        }

        int SearchRoot(int nDepth)
        {
            int vl, vlBest, mv, nNewDepth;
            SortStruct Sort = new SortStruct();

            vlBest = -MATE_VALUE;
            Sort.Init(m_search.mvResult);
            while ((mv = Sort.Next()) != 0)
            {
                if (pos.MakeMove(mv))
                {
                    nNewDepth = pos.InCheck() ? nDepth : nDepth - 1;
                    if (vlBest == -MATE_VALUE)
                    {
                        vl = -SearchFull(-MATE_VALUE, MATE_VALUE, nNewDepth, true);
                    }
                    else
                    {
                        vl = -SearchFull(-vlBest - 1, -vlBest, nNewDepth, false);
                        if (vl > vlBest)
                        {
                            vl = -SearchFull(-MATE_VALUE, -vlBest, nNewDepth, true);
                        }
                    }
                    pos.UndoMakeMove();
                    if (vl > vlBest)
                    {
                        vlBest = vl;
                        m_search.mvResult = mv;
                        if (vlBest > -WIN_VALUE && vlBest < WIN_VALUE)
                        {
                            vlBest += (rand.Next() & RANDOM_MASK) - (rand.Next() & RANDOM_MASK);
                        }
                    }
                }
            }
            RecordHash(HASH_PV, vlBest, nDepth, m_search.mvResult);
            SetBestMove(m_search.mvResult, nDepth);
            return vlBest;
        }

        int SearchFull(int vlAlpha, int vlBeta, int nDepth, bool bNoNull /* false */ )
        {
            int nHashFlag, vl, vlBest;
            int mv;
            int mvBest;
            int mvHash = 0;
            int nNewDepth;
            SortStruct Sort = new SortStruct();
            // Ò»¸öAlpha-BetaÍêÈ«ËÑË÷·ÖÎªÒÔÏÂ¼¸¸ö½×¶Î

            // 1. µ½´ïË®Æ½Ïß£¬Ôòµ÷ÓÃ¾²Ì¬ËÑË÷(×¢Òâ£ºÓÉÓÚ¿Õ²½²Ã¼ô£¬Éî¶È¿ÉÄÜÐ¡ÓÚÁã)
            if (nDepth <= 0)
            {
                return SearchQuiesc(vlAlpha, vlBeta);
            }

            // 1-1. ¼ì²éÖØ¸´¾ÖÃæ(×¢Òâ£º²»ÒªÔÚ¸ù½Úµã¼ì²é£¬·ñÔò¾ÍÃ»ÓÐ×ß·¨ÁË)
            vl = pos.RepStatus(1);
            if (vl != 0)
            {
                return pos.RepValue(vl);
            }

            // 1-2. µ½´ï¼«ÏÞÉî¶È¾Í·µ»Ø¾ÖÃæÆÀ¼Û
            if (pos.nDistance == LIMIT_DEPTH)
            {
                return pos.Evaluate();
            }

            // 1-3. ³¢ÊÔÖÃ»»±í²Ã¼ô£¬²¢µÃµ½ÖÃ»»±í×ß·¨
            vl = ProbeHash(vlAlpha, vlBeta, nDepth, ref mvHash);
            if (vl > -MATE_VALUE)
            {
                return vl;
            }

            // 1-4. ³¢ÊÔ¿Õ²½²Ã¼ô(¸ù½ÚµãµÄBetaÖµÊÇ"MATE_VALUE"£¬ËùÒÔ²»¿ÉÄÜ·¢Éú¿Õ²½²Ã¼ô)
            if (!bNoNull && !pos.InCheck() && pos.NullOkay())
            {
                pos.NullMove();
                vl = -SearchFull(-vlBeta, 1 - vlBeta, nDepth - NULL_DEPTH - 1, true);
                pos.UndoNullMove();
                if (vl >= vlBeta)
                {
                    return vl;
                }
            }

            // 2. ³õÊ¼»¯×î¼ÑÖµºÍ×î¼Ñ×ß·¨
            nHashFlag = HASH_ALPHA;
            vlBest = -MATE_VALUE; // ÕâÑù¿ÉÒÔÖªµÀ£¬ÊÇ·ñÒ»¸ö×ß·¨¶¼Ã»×ß¹ý(É±Æå)
            mvBest = 0;           // ÕâÑù¿ÉÒÔÖªµÀ£¬ÊÇ·ñËÑË÷µ½ÁËBeta×ß·¨»òPV×ß·¨£¬ÒÔ±ã±£´æµ½ÀúÊ·±í

            // 3. ³õÊ¼»¯×ß·¨ÅÅÐò½á¹¹
            Sort.Init(mvHash);

            // 4. ÖðÒ»×ßÕâÐ©×ß·¨£¬²¢½øÐÐµÝ¹é
            while ((mv = Sort.Next()) != 0)
            {
                if (pos.MakeMove(mv))
                {
                    // ½«¾üÑÓÉì
                    nNewDepth = pos.InCheck() ? nDepth : nDepth - 1;
                    // PVS
                    if (vlBest == -MATE_VALUE)
                    {
                        vl = -SearchFull(-vlBeta, -vlAlpha, nNewDepth, false);
                    }
                    else
                    {
                        vl = -SearchFull(-vlAlpha - 1, -vlAlpha, nNewDepth, false);
                        if (vl > vlAlpha && vl < vlBeta)
                        {
                            vl = -SearchFull(-vlBeta, -vlAlpha, nNewDepth, false);
                        }
                    }
                    pos.UndoMakeMove();

                    // 5. ½øÐÐAlpha-Beta´óÐ¡ÅÐ¶ÏºÍ½Ø¶Ï
                    if (vl > vlBest)
                    {    // ÕÒµ½×î¼ÑÖµ(µ«²»ÄÜÈ·¶¨ÊÇAlpha¡¢PV»¹ÊÇBeta×ß·¨)
                        vlBest = vl;        // "vlBest"¾ÍÊÇÄ¿Ç°Òª·µ»ØµÄ×î¼ÑÖµ£¬¿ÉÄÜ³¬³öAlpha-Beta±ß½ç
                        if (vl >= vlBeta)
                        { // ÕÒµ½Ò»¸öBeta×ß·¨
                            nHashFlag = HASH_BETA;
                            mvBest = mv;      // Beta×ß·¨Òª±£´æµ½ÀúÊ·±í
                            break;            // Beta½Ø¶Ï
                        }
                        if (vl > vlAlpha)
                        { // ÕÒµ½Ò»¸öPV×ß·¨
                            nHashFlag = HASH_PV;
                            mvBest = mv;      // PV×ß·¨Òª±£´æµ½ÀúÊ·±í
                            vlAlpha = vl;     // ËõÐ¡Alpha-Beta±ß½ç
                        }
                    }
                }
            }

            // 5. ËùÓÐ×ß·¨¶¼ËÑË÷ÍêÁË£¬°Ñ×î¼Ñ×ß·¨(²»ÄÜÊÇAlpha×ß·¨)±£´æµ½ÀúÊ·±í£¬·µ»Ø×î¼ÑÖµ
            if (vlBest == -MATE_VALUE)
            {
                // Èç¹ûÊÇÉ±Æå£¬¾Í¸ù¾ÝÉ±Æå²½Êý¸ø³öÆÀ¼Û
                return pos.nDistance - MATE_VALUE;
            }
            // ¼ÇÂ¼µ½ÖÃ»»±í
            RecordHash(nHashFlag, vlBest, nDepth, mvBest);
            if (mvBest != 0)
            {
                // Èç¹û²»ÊÇAlpha×ß·¨£¬¾Í½«×î¼Ñ×ß·¨±£´æµ½ÀúÊ·±í
                SetBestMove(mvBest, nDepth);
            }
            return vlBest;
        }

        int ProbeHash(int vlAlpha, int vlBeta, int nDepth, ref int mv)
        {
            bool bMate; // É±Æå±êÖ¾£ºÈç¹ûÊÇÉ±Æå£¬ÄÇÃ´²»ÐèÒªÂú×ãÉî¶ÈÌõ¼þ
            HashItem hsh = m_search.HashTable[pos.zobr.dwKey & (HASH_SIZE - 1)];
            if (hsh.dwLock0 != pos.zobr.dwLock0 || hsh.dwLock1 != pos.zobr.dwLock1)
            {
                mv = 0;
                return -MATE_VALUE;
            }
            mv = hsh.wmv;
            bMate = false;
            if (hsh.svl > WIN_VALUE)
            {
                if (hsh.svl < BAN_VALUE)
                {
                    return -MATE_VALUE; // ¿ÉÄÜµ¼ÖÂËÑË÷µÄ²»ÎÈ¶¨ÐÔ£¬Á¢¿ÌÍË³ö£¬µ«×î¼Ñ×Å·¨¿ÉÄÜÄÃµ½
                }
                hsh.svl -= Convert.ToInt16(pos.nDistance);
                bMate = true;
            }
            else if (hsh.svl < -WIN_VALUE)
            {
                if (hsh.svl > -BAN_VALUE)
                {
                    return -MATE_VALUE; // Í¬ÉÏ
                }
                hsh.svl += Convert.ToInt16(pos.nDistance);
                bMate = true;
            }
            if (hsh.ucDepth >= nDepth || bMate)
            {
                if (hsh.ucFlag == HASH_BETA)
                {
                    return (hsh.svl >= vlBeta ? hsh.svl : Convert.ToInt16(-MATE_VALUE));
                }
                else if (hsh.ucFlag == HASH_ALPHA)
                {
                    return (hsh.svl <= vlAlpha ? hsh.svl : Convert.ToInt16(-MATE_VALUE));
                }
                return hsh.svl;
            }
            return -MATE_VALUE;
        }

        int SearchQuiesc(int vlAlpha, int vlBeta)
        {
            int i, nGenMoves;
            int vl, vlBest;
            int[] mvs = new int[MAX_GEN_MOVES];
            InitializeIntArray(mvs);
            // Ò»¸ö¾²Ì¬ËÑË÷·ÖÎªÒÔÏÂ¼¸¸ö½×¶Î

            // 1. ¼ì²éÖØ¸´¾ÖÃæ
            vl = pos.RepStatus(1);
            if (vl != 0)
            {
                return pos.RepValue(vl);
            }

            // 2. µ½´ï¼«ÏÞÉî¶È¾Í·µ»Ø¾ÖÃæÆÀ¼Û
            if (pos.nDistance == LIMIT_DEPTH)
            {
                return pos.Evaluate();
            }

            // 3. ³õÊ¼»¯×î¼ÑÖµ
            vlBest = -MATE_VALUE; // ÕâÑù¿ÉÒÔÖªµÀ£¬ÊÇ·ñÒ»¸ö×ß·¨¶¼Ã»×ß¹ý(É±Æå)

            if (pos.InCheck())
            {
                // 4. Èç¹û±»½«¾ü£¬ÔòÉú³ÉÈ«²¿×ß·¨
                nGenMoves = pos.GenerateMoves(mvs, false);
                Array.Sort<int>(mvs, 0, 1, new CompareHistory());
            }
            else
            {
                // 5. Èç¹û²»±»½«¾ü£¬ÏÈ×ö¾ÖÃæÆÀ¼Û
                vl = pos.Evaluate();
                if (vl > vlBest)
                {
                    vlBest = vl;
                    if (vl >= vlBeta)
                    {
                        return vl;
                    }
                    if (vl > vlAlpha)
                    {
                        vlAlpha = vl;
                    }
                }

                // 6. Èç¹û¾ÖÃæÆÀ¼ÛÃ»ÓÐ½Ø¶Ï£¬ÔÙÉú³É³Ô×Ó×ß·¨
                nGenMoves = pos.GenerateMoves(mvs, true);
                Array.Sort<int>(mvs, 0, nGenMoves, new CompareMvvLva());
            }

            // 7. ÖðÒ»×ßÕâÐ©×ß·¨£¬²¢½øÐÐµÝ¹é
            for (i = 0; i < nGenMoves; i++)
            {
                if (pos.MakeMove(mvs[i]))
                {
                    vl = -SearchQuiesc(-vlBeta, -vlAlpha);
                    pos.UndoMakeMove();

                    // 8. ½øÐÐAlpha-Beta´óÐ¡ÅÐ¶ÏºÍ½Ø¶Ï
                    if (vl > vlBest)
                    {    // ÕÒµ½×î¼ÑÖµ(µ«²»ÄÜÈ·¶¨ÊÇAlpha¡¢PV»¹ÊÇBeta×ß·¨)
                        vlBest = vl;        // "vlBest"¾ÍÊÇÄ¿Ç°Òª·µ»ØµÄ×î¼ÑÖµ£¬¿ÉÄÜ³¬³öAlpha-Beta±ß½ç
                        if (vl >= vlBeta)
                        { // ÕÒµ½Ò»¸öBeta×ß·¨
                            return vl;        // Beta½Ø¶Ï
                        }
                        if (vl > vlAlpha)
                        { // ÕÒµ½Ò»¸öPV×ß·¨
                            vlAlpha = vl;     // ËõÐ¡Alpha-Beta±ß½ç
                        }
                    }
                }
            }

            // 9. ËùÓÐ×ß·¨¶¼ËÑË÷ÍêÁË£¬·µ»Ø×î¼ÑÖµ
            return vlBest == -MATE_VALUE ? pos.nDistance - MATE_VALUE : vlBest;
        }

        void RecordHash(int nFlag, int vl, int nDepth, int mv)
        {
            HashItem hsh;
            hsh = m_search.HashTable[pos.zobr.dwKey & (HASH_SIZE - 1)];
            if (hsh.ucDepth > nDepth)
            {
                return;
            }
            hsh.ucFlag = Convert.ToSByte(nFlag);
            hsh.ucDepth = Convert.ToSByte(nDepth);
            if (vl > WIN_VALUE)
            {
                if (mv == 0 && vl <= BAN_VALUE)
                {
                    return; // ¿ÉÄÜµ¼ÖÂËÑË÷µÄ²»ÎÈ¶¨ÐÔ£¬²¢ÇÒÃ»ÓÐ×î¼Ñ×Å·¨£¬Á¢¿ÌÍË³ö
                }
                hsh.svl = Convert.ToInt16(vl + pos.nDistance);
            }
            else if (vl < -WIN_VALUE)
            {
                if (mv == 0 && vl >= -BAN_VALUE)
                {
                    return; // Í¬ÉÏ
                }
                hsh.svl = Convert.ToInt16(vl - pos.nDistance);
            }
            else
            {
                hsh.svl = Convert.ToInt16(vl);
            }
            hsh.wmv = Convert.ToUInt16(mv);
            hsh.dwLock0 = pos.zobr.dwLock0;
            hsh.dwLock1 = pos.zobr.dwLock1;
            m_search.HashTable[pos.zobr.dwKey & (HASH_SIZE - 1)] = hsh;
        }

        void SetBestMove(int mv, int nDepth)
        {
            int lpmvKiller0;
            m_search.nHistoryTable[mv] += nDepth * nDepth;
            lpmvKiller0 = m_search.mvKillers[pos.nDistance, 0];
            if (lpmvKiller0 != mv)
            {
                m_search.mvKillers[pos.nDistance, 1] = lpmvKiller0;
                m_search.mvKillers[pos.nDistance, 0] = mv;
            }
        }

        /* PRIVATE API (declared here for documentation purpose) */

        int _hox2xqwlight(string sMove)
        {
            int sx = sMove[0] - '0';
            int sy = sMove[1] - '0';
            int dx = sMove[2] - '0';
            int dy = sMove[3] - '0';
            int src = (3 + sx) + (3 + sy) * 16;
            int dst = (3 + dx) + (3 + dy) * 16;
            return src | (dst << 8);
        }

        string _xqwlight2hox(int move)
        {
            int src = move & 255;
            int dst = move >> 8;
            int sx = (src % 16) - 3;
            int sy = (src / 16) - 3;
            int dx = (dst % 16) - 3;
            int dy = (dst / 16) - 3;
            return string.Format("{0}{1}{2}{3}", sx, sy, dx, dy);
        }

        const int SQUARE_SIZE = 56;
        const int BOARD_EDGE = 8;
        const int BOARD_WIDTH = BOARD_EDGE + SQUARE_SIZE * 9 + BOARD_EDGE;
        const int BOARD_HEIGHT = BOARD_EDGE + SQUARE_SIZE * 10 + BOARD_EDGE;

        // ÆåÅÌ·¶Î§
        const int RANK_TOP = 3;
        const int RANK_BOTTOM = 12;
        const int FILE_LEFT = 3;
        const int FILE_RIGHT = 11;

        // Æå×Ó±àºÅ
        const int PIECE_KING = 0;
        const int PIECE_ADVISOR = 1;
        const int PIECE_BISHOP = 2;
        const int PIECE_KNIGHT = 3;
        const int PIECE_ROOK = 4;
        const int PIECE_CANNON = 5;
        const int PIECE_PAWN = 6;

        const int MAX_GEN_MOVES = 128; // ×î´óµÄÉú³É×ß·¨Êý
        const int MAX_MOVES = 256;     // ×î´óµÄÀúÊ·×ß·¨Êý
        const int LIMIT_DEPTH = 64;    // ×î´óµÄËÑË÷Éî¶È
        const int MATE_VALUE = 10000;  // ×î¸ß·ÖÖµ£¬¼´½«ËÀµÄ·ÖÖµ
        const int BAN_VALUE = MATE_VALUE - 100; // ³¤½«ÅÐ¸ºµÄ·ÖÖµ£¬µÍÓÚ¸ÃÖµ½«²»Ð´ÈëÖÃ»»±í
        const int WIN_VALUE = MATE_VALUE - 200; // ËÑË÷³öÊ¤¸ºµÄ·ÖÖµ½çÏÞ£¬³¬³ö´ËÖµ¾ÍËµÃ÷ÒÑ¾­ËÑË÷³öÉ±ÆåÁË
        const int DRAW_VALUE = 20;     // ºÍÆåÊ±·µ»ØµÄ·ÖÊý(È¡¸ºÖµ)
        const int ADVANCED_VALUE = 3;  // ÏÈÐÐÈ¨·ÖÖµ
        const int RANDOM_MASK = 7;     // Ëæ»úÐÔ·ÖÖµ
        const int NULL_MARGIN = 400;   // ¿Õ²½²Ã¼ôµÄ×ÓÁ¦±ß½ç
        const int NULL_DEPTH = 2;      // ¿Õ²½²Ã¼ôµÄ²Ã¼ôÉî¶È
        const int HISTORY_SIZE = 65536;
        //const int HASH_SIZE = 1 << 20; // ÖÃ»»±í´óÐ¡
        const int HASH_SIZE = 1 << 10;
        const int HASH_ALPHA = 1;      // ALPHA½ÚµãµÄÖÃ»»±íÏî
        const int HASH_BETA = 2;       // BETA½ÚµãµÄÖÃ»»±íÏî
        const int HASH_PV = 3;         // PV½ÚµãµÄÖÃ»»±íÏî
        const int BOOK_SIZE = 16384;   // ¿ª¾Ö¿â´óÐ¡

        static sbyte[] cucpcStartup = new sbyte[256]  {
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0, 20, 19, 18, 17, 16, 17, 18, 19, 20,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0, 21,  0,  0,  0,  0,  0, 21,  0,  0,  0,  0,  0,
  0,  0,  0, 22,  0, 22,  0, 22,  0, 22,  0, 22,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0, 14,  0, 14,  0, 14,  0, 14,  0, 14,  0,  0,  0,  0,
  0,  0,  0,  0, 13,  0,  0,  0,  0,  0, 13,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0, 12, 11, 10,  9,  8,  9, 10, 11, 12,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
        };

        static byte[,] cucvlPiecePos = new byte[7, 256] {
  { // Ë§(½«)
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  2,  2,  2,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0, 11, 15, 11,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }, { // ÊË(Ê¿)
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0, 20,  0, 20,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0, 23,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0, 20,  0, 20,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }, { // Ïà(Ïó)
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0, 20,  0,  0,  0, 20,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0, 18,  0,  0,  0, 23,  0,  0,  0, 18,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0, 20,  0,  0,  0, 20,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }, { // Âí
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0, 90, 90, 90, 96, 90, 96, 90, 90, 90,  0,  0,  0,  0,
    0,  0,  0, 90, 96,103, 97, 94, 97,103, 96, 90,  0,  0,  0,  0,
    0,  0,  0, 92, 98, 99,103, 99,103, 99, 98, 92,  0,  0,  0,  0,
    0,  0,  0, 93,108,100,107,100,107,100,108, 93,  0,  0,  0,  0,
    0,  0,  0, 90,100, 99,103,104,103, 99,100, 90,  0,  0,  0,  0,
    0,  0,  0, 90, 98,101,102,103,102,101, 98, 90,  0,  0,  0,  0,
    0,  0,  0, 92, 94, 98, 95, 98, 95, 98, 94, 92,  0,  0,  0,  0,
    0,  0,  0, 93, 92, 94, 95, 92, 95, 94, 92, 93,  0,  0,  0,  0,
    0,  0,  0, 85, 90, 92, 93, 78, 93, 92, 90, 85,  0,  0,  0,  0,
    0,  0,  0, 88, 85, 90, 88, 90, 88, 90, 85, 88,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }, { // ³µ
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,206,208,207,213,214,213,207,208,206,  0,  0,  0,  0,
    0,  0,  0,206,212,209,216,233,216,209,212,206,  0,  0,  0,  0,
    0,  0,  0,206,208,207,214,216,214,207,208,206,  0,  0,  0,  0,
    0,  0,  0,206,213,213,216,216,216,213,213,206,  0,  0,  0,  0,
    0,  0,  0,208,211,211,214,215,214,211,211,208,  0,  0,  0,  0,
    0,  0,  0,208,212,212,214,215,214,212,212,208,  0,  0,  0,  0,
    0,  0,  0,204,209,204,212,214,212,204,209,204,  0,  0,  0,  0,
    0,  0,  0,198,208,204,212,212,212,204,208,198,  0,  0,  0,  0,
    0,  0,  0,200,208,206,212,200,212,206,208,200,  0,  0,  0,  0,
    0,  0,  0,194,206,204,212,200,212,204,206,194,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }, { // ÅÚ
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,100,100, 96, 91, 90, 91, 96,100,100,  0,  0,  0,  0,
    0,  0,  0, 98, 98, 96, 92, 89, 92, 96, 98, 98,  0,  0,  0,  0,
    0,  0,  0, 97, 97, 96, 91, 92, 91, 96, 97, 97,  0,  0,  0,  0,
    0,  0,  0, 96, 99, 99, 98,100, 98, 99, 99, 96,  0,  0,  0,  0,
    0,  0,  0, 96, 96, 96, 96,100, 96, 96, 96, 96,  0,  0,  0,  0,
    0,  0,  0, 95, 96, 99, 96,100, 96, 99, 96, 95,  0,  0,  0,  0,
    0,  0,  0, 96, 96, 96, 96, 96, 96, 96, 96, 96,  0,  0,  0,  0,
    0,  0,  0, 97, 96,100, 99,101, 99,100, 96, 97,  0,  0,  0,  0,
    0,  0,  0, 96, 97, 98, 98, 98, 98, 98, 97, 96,  0,  0,  0,  0,
    0,  0,  0, 96, 96, 97, 99, 99, 99, 97, 96, 96,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }, { // ±ø(×ä)
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  9,  9,  9, 11, 13, 11,  9,  9,  9,  0,  0,  0,  0,
    0,  0,  0, 19, 24, 34, 42, 44, 42, 34, 24, 19,  0,  0,  0,  0,
    0,  0,  0, 19, 24, 32, 37, 37, 37, 32, 24, 19,  0,  0,  0,  0,
    0,  0,  0, 19, 23, 27, 29, 30, 29, 27, 23, 19,  0,  0,  0,  0,
    0,  0,  0, 14, 18, 20, 27, 29, 27, 20, 18, 14,  0,  0,  0,  0,
    0,  0,  0,  7,  0, 13,  0, 16,  0, 13,  0,  7,  0,  0,  0,  0,
    0,  0,  0,  7,  0,  7,  0, 15,  0,  7,  0,  7,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
  }
};
        static readonly sbyte[] ccInBoard = new sbyte[256] {
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

        static readonly sbyte[] ccInFort = new sbyte[256] {
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

        static readonly sbyte[] ccLegalSpan = new sbyte[512] {
                       0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0
};


        // ¸ù¾Ý²½³¤ÅÐ¶ÏÂíÊÇ·ñõ¿ÍÈµÄÊý×é
        static readonly sbyte[] ccKnightPin = new sbyte[512] {
                              0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,-16,  0,-16,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0, -1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0, -1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0, 16,  0, 16,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
  0,  0,  0,  0,  0,  0,  0
};

        // Ë§(½«)µÄ²½³¤
        static readonly sbyte[] ccKingDelta = new sbyte[4] { -16, -1, 1, 16 };
        // ÊË(Ê¿)µÄ²½³¤
        static readonly sbyte[] ccAdvisorDelta = new sbyte[4] { -17, -15, 15, 17 };
        // ÂíµÄ²½³¤£¬ÒÔË§(½«)µÄ²½³¤×÷ÎªÂíÍÈ
        static readonly sbyte[,] ccKnightDelta = new sbyte[4, 2] { { -33, -31 }, { -18, 14 }, { -14, 18 }, { 31, 33 } };
        // Âí±»½«¾üµÄ²½³¤£¬ÒÔÊË(Ê¿)µÄ²½³¤×÷ÎªÂíÍÈ
        static readonly sbyte[,] ccKnightCheckDelta = new sbyte[4, 2] { { -33, -18 }, { -31, -14 }, { 14, 31 }, { 18, 33 } };


        // ÅÐ¶ÏÆå×ÓÊÇ·ñÔÚÆåÅÌÖÐ
        static bool IN_BOARD(int sq)
        {
            return ccInBoard[sq] != 0;
        }

        // ÅÐ¶ÏÆå×ÓÊÇ·ñÔÚ¾Å¹¬ÖÐ
        static bool IN_FORT(int sq)
        {
            return ccInFort[sq] != 0;
        }

        // »ñµÃ¸ñ×ÓµÄºá×ø±ê
        static int RANK_Y(int sq)
        {
            return sq >> 4;
        }

        // »ñµÃ¸ñ×ÓµÄ×Ý×ø±ê
        static int FILE_X(int sq)
        {
            return sq & 15;
        }

        // ¸ù¾Ý×Ý×ø±êºÍºá×ø±ê»ñµÃ¸ñ×Ó
        static int COORD_XY(int x, int y)
        {
            return x + (y << 4);
        }

        // ·­×ª¸ñ×Ó
        static int SQUARE_FLIP(int sq)
        {
            return 254 - sq;
        }

        // ×Ý×ø±êË®Æ½¾µÏñ
        static int FILE_FLIP(int x)
        {
            return 14 - x;
        }

        // ºá×ø±ê´¹Ö±¾µÏñ
        static int RANK_FLIP(int y)
        {
            return 15 - y;
        }

        // ¸ñ×ÓË®Æ½¾µÏñ
        static int MIRROR_SQUARE(int sq)
        {
            return COORD_XY(FILE_FLIP(FILE_X(sq)), RANK_Y(sq));
        }

        // ¸ñ×ÓË®Æ½¾µÏñ
        static int SQUARE_FORWARD(int sq, int sd)
        {
            return sq - 16 + (sd << 5);
        }

        // ×ß·¨ÊÇ·ñ·ûºÏË§(½«)µÄ²½³¤
        static bool KING_SPAN(int sqSrc, int sqDst)
        {
            return ccLegalSpan[sqDst - sqSrc + 256] == 1;
        }

        // ×ß·¨ÊÇ·ñ·ûºÏÊË(Ê¿)µÄ²½³¤
        static bool ADVISOR_SPAN(int sqSrc, int sqDst)
        {
            return ccLegalSpan[sqDst - sqSrc + 256] == 2;
        }

        // ×ß·¨ÊÇ·ñ·ûºÏÏà(Ïó)µÄ²½³¤
        static bool BISHOP_SPAN(int sqSrc, int sqDst)
        {
            return ccLegalSpan[sqDst - sqSrc + 256] == 3;
        }

        // Ïà(Ïó)ÑÛµÄÎ»ÖÃ
        static int BISHOP_PIN(int sqSrc, int sqDst)
        {
            return (sqSrc + sqDst) >> 1;
        }

        // ÂíÍÈµÄÎ»ÖÃ
        static int KNIGHT_PIN(int sqSrc, int sqDst)
        {
            return sqSrc + ccKnightPin[sqDst - sqSrc + 256];
        }

        // ÊÇ·ñÎ´¹ýºÓ
        static bool HOME_HALF(int sq, int sd)
        {
            return (sq & 0x80) != (sd << 7);
        }

        // ÊÇ·ñÒÑ¹ýºÓ
        static bool AWAY_HALF(int sq, int sd)
        {
            return (sq & 0x80) == (sd << 7);
        }

        // ÊÇ·ñÔÚºÓµÄÍ¬Ò»±ß
        static bool SAME_HALF(int sqSrc, int sqDst)
        {
            return ((sqSrc ^ sqDst) & 0x80) == 0;
        }

        // ÊÇ·ñÔÚÍ¬Ò»ÐÐ
        static bool SAME_RANK(int sqSrc, int sqDst)
        {
            return ((sqSrc ^ sqDst) & 0xf0) == 0;
        }

        // ÊÇ·ñÔÚÍ¬Ò»ÁÐ
        static bool SAME_FILE(int sqSrc, int sqDst)
        {
            return ((sqSrc ^ sqDst) & 0x0f) == 0;
        }

        // »ñµÃºìºÚ±ê¼Ç(ºì×ÓÊÇ8£¬ºÚ×ÓÊÇ16)
        static int SIDE_TAG(int sd)
        {
            return 8 + (sd << 3);
        }

        // »ñµÃ¶Ô·½ºìºÚ±ê¼Ç
        static int OPP_SIDE_TAG(int sd)
        {
            return 16 - (sd << 3);
        }

        // »ñµÃ×ß·¨µÄÆðµã
        static int SRC(int mv)
        {
            return mv & 255;
        }

        // »ñµÃ×ß·¨µÄÖÕµã
        static int DST(int mv)
        {
            return mv >> 8;
        }

        // ¸ù¾ÝÆðµãºÍÖÕµã»ñµÃ×ß·¨
        static int MOVE(int sqSrc, int sqDst)
        {
            return sqSrc + sqDst * 256;
        }

        // ×ß·¨Ë®Æ½¾µÏñ
        static int MIRROR_MOVE(int mv)
        {
            return MOVE(MIRROR_SQUARE(SRC(mv)), MIRROR_SQUARE(DST(mv)));
        }

        struct HashItem
        {
            public sbyte ucDepth;
            public sbyte ucFlag;
            public short svl;
            public ushort wmv;
            //public ushort wReserved;
            public uint dwLock0;
            public uint dwLock1;
        }

        // ¿ª¾Ö¿âÏî½á¹¹
        struct BookItem
        {
            public uint dwLock;
            public ushort wmv;
            public ushort wvl;
        }

        // ÓëËÑË÷ÓÐ¹ØµÄÈ«¾Ö±äÁ¿
        class Search
        {
            public int mvResult = 0;                  // µçÄÔ×ßµÄÆå
            public int[] nHistoryTable = new int[HISTORY_SIZE];      // ÀúÊ·±í
            public int[,] mvKillers = new int[LIMIT_DEPTH, 2]; // É±ÊÖ×ß·¨±í
            public HashItem[] HashTable = new HashItem[HASH_SIZE]; // ÖÃ»»±í
            public int nBookSize = 0;                 // ¿ª¾Ö¿â´óÐ¡
            public BookItem[] BookTable = null; // = new BookItem[BOOK_SIZE]; // ¿ª¾Ö¿â

            public Search()
            {
                InitializeIntArray(nHistoryTable);
                for (int i = 0; i < LIMIT_DEPTH; i++)
                {
                    mvKillers[i, 0] = new int();
                    mvKillers[i, 1] = new int();
                }
                for (int hashItem = 0; hashItem < HashTable.Length; hashItem++)
                {
                    HashTable[hashItem] = new HashItem();
                }

                //for (int bookItem = 0; bookItem < BookTable.Length; bookItem++)
                //{
                //    BookTable[bookItem] = new BookItem();
                //}
            }

            public int BinarySearch(BookItem item)
            {
                return Array.BinarySearch<BookItem>(BookTable, 0, nBookSize, item, new CompareBookItem());
            }
        }

        static Search m_search = new Search();

        // RC4ÃÜÂëÁ÷Éú³ÉÆ÷
        class RC4Struct
        {
            byte[] s = new byte[256];
            int x, y;

            public RC4Struct()
            {
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = new byte();
                }
            }

            public void InitZero()   // ÓÃ¿ÕÃÜÔ¿³õÊ¼»¯ÃÜÂëÁ÷Éú³ÉÆ÷
            {
                int i, j;
                byte uc;

                x = y = j = 0;
                for (i = 0; i < 256; i++)
                {
                    s[i] = Convert.ToByte(i);
                }

                for (i = 0; i < 256; i++)
                {
                    j = (j + s[i]) & 255;
                    uc = s[i];
                    s[i] = s[j];
                    s[j] = uc;
                }
            }

            public byte NextByte()
            {  // Éú³ÉÃÜÂëÁ÷µÄÏÂÒ»¸ö×Ö½Ú
                byte uc;
                x = (x + 1) & 255;
                y = (y + s[x]) & 255;
                uc = s[x];
                s[x] = s[y];
                s[y] = uc;
                return s[(s[x] + s[y]) & 255];
            }

            public uint NextLong()
            { // Éú³ÉÃÜÂëÁ÷µÄÏÂËÄ¸ö×Ö½Ú
                byte uc0, uc1, uc2, uc3;
                uc0 = NextByte();
                uc1 = NextByte();
                uc2 = NextByte();
                uc3 = NextByte();
                return Convert.ToUInt32(uc0) + (Convert.ToUInt32(uc1) << 8) + (Convert.ToUInt32(uc2) << 16) + (Convert.ToUInt32(uc3) << 24);
            }
        }


        // Zobrist½á¹¹
        class ZobristStruct
        {
            public uint dwKey;
            public uint dwLock0;
            public uint dwLock1;

            public void InitZero()
            {                 // ÓÃÁãÌî³äZobrist
                dwKey = dwLock0 = dwLock1 = 0;
            }

            public void InitRC4(RC4Struct rc4)
            {        // ÓÃÃÜÂëÁ÷Ìî³äZobrist
                dwKey = rc4.NextLong();
                dwLock0 = rc4.NextLong();
                dwLock1 = rc4.NextLong();
            }

            public void Xor(ZobristStruct zobr)
            { // Ö´ÐÐXOR²Ù×÷
                dwKey ^= zobr.dwKey;
                dwLock0 ^= zobr.dwLock0;
                dwLock1 ^= zobr.dwLock1;
            }

            public void Xor(ZobristStruct zobr1, ZobristStruct zobr2)
            {
                dwKey ^= zobr1.dwKey ^ zobr2.dwKey;
                dwLock0 ^= zobr1.dwLock0 ^ zobr2.dwLock0;
                dwLock1 ^= zobr1.dwLock1 ^ zobr2.dwLock1;
            }
        };

        // Zobrist±í
        class Zobrist
        {
            public ZobristStruct Player = new ZobristStruct();
            public ZobristStruct[,] Table = new ZobristStruct[14, 256];

            public Zobrist()
            {
                //Table.Initialize();
                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 256; j++)
                    {
                        Table[i, j] = new ZobristStruct();
                    }
                }

            }
        }

        static Zobrist m_zobrist = new Zobrist();

        // ÀúÊ·×ß·¨ÐÅÏ¢(Õ¼4×Ö½Ú)
        class MoveStruct
        {
            public UInt16 wmv;
            public sbyte ucpcCaptured;
            public bool ucbCheck;
            public uint dwKey;

            public void Set(int mv, sbyte pcCaptured, bool bCheck, uint dwKey_)
            {
                wmv = Convert.ToUInt16(mv);
                ucpcCaptured = pcCaptured;
                ucbCheck = bCheck;
                dwKey = dwKey_;
            }
        } // mvs

        // ¾ÖÃæ½á¹¹

        class PositionStruct
        {
            public int sdPlayer;                   // ÂÖµ½Ë­×ß£¬0=ºì·½£¬1=ºÚ·½
            public sbyte[] ucpcSquares = new sbyte[MAX_MOVES];          // ÆåÅÌÉÏµÄÆå×Ó
            public int vlWhite;
            public int vlBlack;           // ºì¡¢ºÚË«·½µÄ×ÓÁ¦¼ÛÖµ
            public int nDistance;
            public int nMoveNum;        // ¾àÀë¸ù½ÚµãµÄ²½Êý£¬ÀúÊ·×ß·¨Êý
            public MoveStruct[] mvsList = new MoveStruct[MAX_MOVES];  // ÀúÊ·×ß·¨ÐÅÏ¢ÁÐ±í

            public ZobristStruct zobr = new ZobristStruct();             // Zobrist

            public PositionStruct()
            {
                for (int i = 0; i < MAX_MOVES; i++)
                {
                    ucpcSquares[i] = 0;
                    mvsList[i] = new MoveStruct();
                }
            }

            public void ClearBoard()
            {         // Çå¿ÕÆåÅÌ
                sdPlayer = vlWhite = vlBlack = nDistance = 0;
                for (int i = 0; i < ucpcSquares.Length; i++)
                    ucpcSquares[i] = 0;
                zobr.InitZero();
            }

            public void SetIrrev()
            {           // Çå¿Õ(³õÊ¼»¯)ÀúÊ·×ß·¨ÐÅÏ¢
                mvsList[0].Set(0, 0, Checked(), zobr.dwKey);
                nMoveNum = 1;
            }

            public void Startup(sbyte[,] board) //             // ³õÊ¼»¯ÆåÅÌ
            {
                int sq;
                ClearBoard();
                if (board != null)
                {
                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 9; j++)
                        {
                            if (board[i, j] > 0)
                            {
                                sq = (3 + i) * 16 + 3 + j;
                                AddPiece(sq, board[i, j]);
                            }
                        }
                }
                else
                {
                    int pc;
                    for (sq = 0; sq < 256; sq++)
                    {
                        pc = cucpcStartup[sq];
                        if (pc != 0)
                        {
                            AddPiece(sq, pc);
                        }
                    }
                }
                SetIrrev();
            }

            public void ChangeSide()
            {         // ½»»»×ß×Ó·½
                sdPlayer = 1 - sdPlayer;
                zobr.Xor(m_zobrist.Player);
            }

            public void AddPiece(int sq, int pc)
            { // ÔÚÆåÅÌÉÏ·ÅÒ»Ã¶Æå×Ó
                ucpcSquares[sq] = Convert.ToSByte(pc);
                // ºì·½¼Ó·Ö£¬ºÚ·½(×¢Òâ"cucvlPiecePos"È¡ÖµÒªµßµ¹)¼õ·Ö
                if (pc < 16)
                {
                    vlWhite += cucvlPiecePos[pc - 8, sq];
                    zobr.Xor(m_zobrist.Table[pc - 8, sq]);
                }
                else
                {
                    vlBlack += cucvlPiecePos[pc - 16, SQUARE_FLIP(sq)];
                    zobr.Xor(m_zobrist.Table[pc - 9, sq]);
                }
            }

            public void DelPiece(int sq, int pc)
            { // ´ÓÆåÅÌÉÏÄÃ×ßÒ»Ã¶Æå×Ó
                ucpcSquares[sq] = 0;
                // ºì·½¼õ·Ö£¬ºÚ·½(×¢Òâ"cucvlPiecePos"È¡ÖµÒªµßµ¹)¼Ó·Ö
                if (pc < 16)
                {
                    vlWhite -= cucvlPiecePos[pc - 8, sq];
                    zobr.Xor(m_zobrist.Table[pc - 8, sq]);
                }
                else
                {
                    vlBlack -= cucvlPiecePos[pc - 16, SQUARE_FLIP(sq)];
                    zobr.Xor(m_zobrist.Table[pc - 9, sq]);
                }
            }

            public int Evaluate()
            {      // ¾ÖÃæÆÀ¼Ûº¯Êý
                return (sdPlayer == 0 ? vlWhite - vlBlack : vlBlack - vlWhite) + ADVANCED_VALUE;
            }

            public bool InCheck()
            {      // ÊÇ·ñ±»½«¾ü
                return mvsList[nMoveNum - 1].ucbCheck;
            }

            public bool Captured()
            {     // ÉÏÒ»²½ÊÇ·ñ³Ô×Ó
                return mvsList[nMoveNum - 1].ucpcCaptured != 0;
            }

            public sbyte MovePiece(int mv)                      // °áÒ»²½ÆåµÄÆå×Ó
            {
                int sqSrc, sqDst, pc;
                sbyte pcCaptured;
                sqSrc = SRC(mv);
                sqDst = DST(mv);
                pcCaptured = ucpcSquares[sqDst];
                if (pcCaptured != 0)
                {
                    DelPiece(sqDst, pcCaptured);
                }
                pc = ucpcSquares[sqSrc];
                DelPiece(sqSrc, pc);
                AddPiece(sqDst, pc);
                return pcCaptured;
            }

            public void UndoMovePiece(int mv, sbyte pcCaptured) // ³·Ïû°áÒ»²½ÆåµÄÆå×Ó
            {
                int sqSrc, sqDst, pc;
                sqSrc = SRC(mv);
                sqDst = DST(mv);
                pc = ucpcSquares[sqDst];
                DelPiece(sqDst, pc);
                AddPiece(sqSrc, pc);
                if (pcCaptured != 0)
                {
                    AddPiece(sqDst, pcCaptured);
                }
            }

            public bool MakeMove(int mv)                      // ×ßÒ»²½Æå
            {
                sbyte pcCaptured;
                uint dwKey;

                dwKey = zobr.dwKey;
                pcCaptured = MovePiece(mv);
                if (Checked())
                {
                    UndoMovePiece(mv, pcCaptured);
                    return false;
                }
                ChangeSide();
                mvsList[nMoveNum].Set(mv, pcCaptured, Checked(), dwKey);
                nMoveNum++;
                nDistance++;
                return true;
            }

            public void UndoMakeMove()
            {                   // ³·Ïû×ßÒ»²½Æå
                nDistance--;
                nMoveNum--;
                ChangeSide();
                UndoMovePiece(mvsList[nMoveNum].wmv, mvsList[nMoveNum].ucpcCaptured);
            }

            public void NullMove()
            {                       // ×ßÒ»²½¿Õ²½
                uint dwKey;
                dwKey = zobr.dwKey;
                ChangeSide();
                mvsList[nMoveNum].Set(0, 0, false, dwKey);
                nMoveNum++;
                nDistance++;
            }

            public void UndoNullMove()
            {                   // ³·Ïû×ßÒ»²½¿Õ²½
                nDistance--;
                nMoveNum--;
                ChangeSide();
            }

            // Éú³ÉËùÓÐ×ß·¨£¬Èç¹û"bCapture"Îª"TRUE"ÔòÖ»Éú³É³Ô×Ó×ß·¨
            public int GenerateMoves(int[] mvs, bool bCapture) //= false) ;
            {
                int i, j, nGenMoves, nDelta, sqSrc, sqDst;
                int pcSelfSide, pcOppSide, pcSrc, pcDst;
                // Éú³ÉËùÓÐ×ß·¨£¬ÐèÒª¾­¹ýÒÔÏÂ¼¸¸ö²½Öè£º

                nGenMoves = 0;
                pcSelfSide = SIDE_TAG(sdPlayer);
                pcOppSide = OPP_SIDE_TAG(sdPlayer);
                for (sqSrc = 0; sqSrc < 256; sqSrc++)
                {

                    // 1. ÕÒµ½Ò»¸ö±¾·½Æå×Ó£¬ÔÙ×öÒÔÏÂÅÐ¶Ï£º
                    pcSrc = ucpcSquares[sqSrc];
                    if ((pcSrc & pcSelfSide) == 0)
                    {
                        continue;
                    }

                    // 2. ¸ù¾ÝÆå×ÓÈ·¶¨×ß·¨
                    switch (pcSrc - pcSelfSide)
                    {
                        case PIECE_KING:
                            for (i = 0; i < 4; i++)
                            {
                                sqDst = sqSrc + ccKingDelta[i];
                                if (!IN_FORT(sqDst))
                                {
                                    continue;
                                }
                                pcDst = ucpcSquares[sqDst];
                                if (bCapture ? (pcDst & pcOppSide) != 0 : (pcDst & pcSelfSide) == 0)
                                {
                                    mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                    nGenMoves++;
                                }
                            }
                            break;
                        case PIECE_ADVISOR:
                            for (i = 0; i < 4; i++)
                            {
                                sqDst = sqSrc + ccAdvisorDelta[i];
                                if (!IN_FORT(sqDst))
                                {
                                    continue;
                                }
                                pcDst = ucpcSquares[sqDst];
                                if (bCapture ? (pcDst & pcOppSide) != 0 : (pcDst & pcSelfSide) == 0)
                                {
                                    mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                    nGenMoves++;
                                }
                            }
                            break;
                        case PIECE_BISHOP:
                            for (i = 0; i < 4; i++)
                            {
                                sqDst = sqSrc + ccAdvisorDelta[i];
                                if (!(IN_BOARD(sqDst) && HOME_HALF(sqDst, sdPlayer) && ucpcSquares[sqDst] == 0))
                                {
                                    continue;
                                }
                                sqDst += ccAdvisorDelta[i];
                                pcDst = ucpcSquares[sqDst];
                                if (bCapture ? (pcDst & pcOppSide) != 0 : (pcDst & pcSelfSide) == 0)
                                {
                                    mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                    nGenMoves++;
                                }
                            }
                            break;
                        case PIECE_KNIGHT:
                            for (i = 0; i < 4; i++)
                            {
                                sqDst = sqSrc + ccKingDelta[i];
                                if (ucpcSquares[sqDst] != 0)
                                {
                                    continue;
                                }
                                for (j = 0; j < 2; j++)
                                {
                                    sqDst = sqSrc + ccKnightDelta[i, j];
                                    if (!IN_BOARD(sqDst))
                                    {
                                        continue;
                                    }
                                    pcDst = ucpcSquares[sqDst];
                                    if (bCapture ? (pcDst & pcOppSide) != 0 : (pcDst & pcSelfSide) == 0)
                                    {
                                        mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                        nGenMoves++;
                                    }
                                }
                            }
                            break;
                        case PIECE_ROOK:
                            for (i = 0; i < 4; i++)
                            {
                                nDelta = ccKingDelta[i];
                                sqDst = sqSrc + nDelta;
                                while (IN_BOARD(sqDst))
                                {
                                    pcDst = ucpcSquares[sqDst];
                                    if (pcDst == 0)
                                    {
                                        if (!bCapture)
                                        {
                                            mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                            nGenMoves++;
                                        }
                                    }
                                    else
                                    {
                                        if ((pcDst & pcOppSide) != 0)
                                        {
                                            mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                            nGenMoves++;
                                        }
                                        break;
                                    }
                                    sqDst += nDelta;
                                }
                            }
                            break;
                        case PIECE_CANNON:
                            for (i = 0; i < 4; i++)
                            {
                                nDelta = ccKingDelta[i];
                                sqDst = sqSrc + nDelta;
                                while (IN_BOARD(sqDst))
                                {
                                    pcDst = ucpcSquares[sqDst];
                                    if (pcDst == 0)
                                    {
                                        if (!bCapture)
                                        {
                                            mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                            nGenMoves++;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    sqDst += nDelta;
                                }
                                sqDst += nDelta;
                                while (IN_BOARD(sqDst))
                                {
                                    pcDst = ucpcSquares[sqDst];
                                    if (pcDst != 0)
                                    {
                                        if ((pcDst & pcOppSide) != 0)
                                        {
                                            mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                            nGenMoves++;
                                        }
                                        break;
                                    }
                                    sqDst += nDelta;
                                }
                            }
                            break;
                        case PIECE_PAWN:
                            sqDst = SQUARE_FORWARD(sqSrc, sdPlayer);
                            if (IN_BOARD(sqDst))
                            {
                                pcDst = ucpcSquares[sqDst];
                                if (bCapture ? (pcDst & pcOppSide) != 0 : (pcDst & pcSelfSide) == 0)
                                {
                                    mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                    nGenMoves++;
                                }
                            }
                            if (AWAY_HALF(sqSrc, sdPlayer))
                            {
                                for (nDelta = -1; nDelta <= 1; nDelta += 2)
                                {
                                    sqDst = sqSrc + nDelta;
                                    if (IN_BOARD(sqDst))
                                    {
                                        pcDst = ucpcSquares[sqDst];
                                        if (bCapture ? (pcDst & pcOppSide) != 0 : (pcDst & pcSelfSide) == 0)
                                        {
                                            mvs[nGenMoves] = MOVE(sqSrc, sqDst);
                                            nGenMoves++;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                return nGenMoves;
            }

            public bool LegalMove(int mv)               // ÅÐ¶Ï×ß·¨ÊÇ·ñºÏÀí
            {
                int sqSrc, sqDst, sqPin;
                int pcSelfSide, pcSrc, pcDst, nDelta;
                // ÅÐ¶Ï×ß·¨ÊÇ·ñºÏ·¨£¬ÐèÒª¾­¹ýÒÔÏÂµÄÅÐ¶Ï¹ý³Ì£º

                // 1. ÅÐ¶ÏÆðÊ¼¸ñÊÇ·ñÓÐ×Ô¼ºµÄÆå×Ó
                sqSrc = SRC(mv);
                pcSrc = ucpcSquares[sqSrc];
                pcSelfSide = SIDE_TAG(sdPlayer);
                if ((pcSrc & pcSelfSide) == 0)
                {
                    return false;
                }

                // 2. ÅÐ¶ÏÄ¿±ê¸ñÊÇ·ñÓÐ×Ô¼ºµÄÆå×Ó
                sqDst = DST(mv);
                pcDst = ucpcSquares[sqDst];
                if ((pcDst & pcSelfSide) != 0)
                {
                    return false;
                }

                // 3. ¸ù¾ÝÆå×ÓµÄÀàÐÍ¼ì²é×ß·¨ÊÇ·ñºÏÀí
                switch (pcSrc - pcSelfSide)
                {
                    case PIECE_KING:
                        return IN_FORT(sqDst) && KING_SPAN(sqSrc, sqDst);
                    case PIECE_ADVISOR:
                        return IN_FORT(sqDst) && ADVISOR_SPAN(sqSrc, sqDst);
                    case PIECE_BISHOP:
                        return SAME_HALF(sqSrc, sqDst) && BISHOP_SPAN(sqSrc, sqDst) &&
                            ucpcSquares[BISHOP_PIN(sqSrc, sqDst)] == 0;
                    case PIECE_KNIGHT:
                        sqPin = KNIGHT_PIN(sqSrc, sqDst);
                        return sqPin != sqSrc && ucpcSquares[sqPin] == 0;
                    case PIECE_ROOK:
                    case PIECE_CANNON:
                        if (SAME_RANK(sqSrc, sqDst))
                        {
                            nDelta = (sqDst < sqSrc ? -1 : 1);
                        }
                        else if (SAME_FILE(sqSrc, sqDst))
                        {
                            nDelta = (sqDst < sqSrc ? -16 : 16);
                        }
                        else
                        {
                            return false;
                        }
                        sqPin = sqSrc + nDelta;
                        while (sqPin != sqDst && ucpcSquares[sqPin] == 0)
                        {
                            sqPin += nDelta;
                        }
                        if (sqPin == sqDst)
                        {
                            return pcDst == 0 || pcSrc - pcSelfSide == PIECE_ROOK;
                        }
                        else if (pcDst != 0 && pcSrc - pcSelfSide == PIECE_CANNON)
                        {
                            sqPin += nDelta;
                            while (sqPin != sqDst && ucpcSquares[sqPin] == 0)
                            {
                                sqPin += nDelta;
                            }
                            return sqPin == sqDst;
                        }
                        else
                        {
                            return false;
                        }
                    case PIECE_PAWN:
                        if (AWAY_HALF(sqDst, sdPlayer) && (sqDst == sqSrc - 1 || sqDst == sqSrc + 1))
                        {
                            return false;
                        }
                        return sqDst == SQUARE_FORWARD(sqSrc, sdPlayer);
                    default:
                        return false;
                }
            }

            public bool Checked()                   // ÅÐ¶ÏÊÇ·ñ±»½«¾ü
            {
                int i, j, sqSrc, sqDst;
                int pcSelfSide, pcOppSide, pcDst, nDelta;
                pcSelfSide = SIDE_TAG(sdPlayer);
                pcOppSide = OPP_SIDE_TAG(sdPlayer);
                // ÕÒµ½ÆåÅÌÉÏµÄË§(½«)£¬ÔÙ×öÒÔÏÂÅÐ¶Ï£º

                for (sqSrc = 0; sqSrc < 256; sqSrc++)
                {
                    if (ucpcSquares[sqSrc] != pcSelfSide + PIECE_KING)
                    {
                        continue;
                    }

                    // 1. ÅÐ¶ÏÊÇ·ñ±»¶Ô·½µÄ±ø(×ä)½«¾ü
                    if (ucpcSquares[SQUARE_FORWARD(sqSrc, sdPlayer)] == pcOppSide + PIECE_PAWN)
                    {
                        return true;
                    }
                    for (nDelta = -1; nDelta <= 1; nDelta += 2)
                    {
                        if (ucpcSquares[sqSrc + nDelta] == pcOppSide + PIECE_PAWN)
                        {
                            return true;
                        }
                    }

                    // 2. ÅÐ¶ÏÊÇ·ñ±»¶Ô·½µÄÂí½«¾ü(ÒÔÊË(Ê¿)µÄ²½³¤µ±×÷ÂíÍÈ)
                    for (i = 0; i < 4; i++)
                    {
                        if (ucpcSquares[sqSrc + ccAdvisorDelta[i]] != 0)
                        {
                            continue;
                        }
                        for (j = 0; j < 2; j++)
                        {
                            pcDst = ucpcSquares[sqSrc + ccKnightCheckDelta[i, j]];
                            if (pcDst == pcOppSide + PIECE_KNIGHT)
                            {
                                return true;
                            }
                        }
                    }

                    // 3. ÅÐ¶ÏÊÇ·ñ±»¶Ô·½µÄ³µ»òÅÚ½«¾ü(°üÀ¨½«Ë§¶ÔÁ³)
                    for (i = 0; i < 4; i++)
                    {
                        nDelta = ccKingDelta[i];
                        sqDst = sqSrc + nDelta;
                        while (IN_BOARD(sqDst))
                        {
                            pcDst = ucpcSquares[sqDst];
                            if (pcDst != 0)
                            {
                                if (pcDst == pcOppSide + PIECE_ROOK || pcDst == pcOppSide + PIECE_KING)
                                {
                                    return true;
                                }
                                break;
                            }
                            sqDst += nDelta;
                        }
                        sqDst += nDelta;
                        while (IN_BOARD(sqDst))
                        {
                            pcDst = ucpcSquares[sqDst];
                            if (pcDst != 0)
                            {
                                if (pcDst == pcOppSide + PIECE_CANNON)
                                {
                                    return true;
                                }
                                break;
                            }
                            sqDst += nDelta;
                        }
                    }
                    return false;
                }
                return false;
            }

            public bool IsMate()                          // ÅÐ¶ÏÊÇ·ñ±»É±
            {
                int i, nGenMoveNum;
                sbyte pcCaptured;
                int[] mvs = new int[MAX_GEN_MOVES];
                InitializeIntArray(mvs);

                nGenMoveNum = GenerateMoves(mvs, false);
                for (i = 0; i < nGenMoveNum; i++)
                {
                    pcCaptured = MovePiece(mvs[i]);
                    if (!Checked())
                    {
                        UndoMovePiece(mvs[i], pcCaptured);
                        return false;
                    }
                    else
                    {
                        UndoMovePiece(mvs[i], pcCaptured);
                    }
                }
                return true;
            }

            public int DrawValue()
            {                 // ºÍÆå·ÖÖµ
                return (nDistance & 1) == 0 ? -DRAW_VALUE : DRAW_VALUE;
            }

            public int RepStatus(int nRecur) // = 1) ;        // ¼ì²âÖØ¸´¾ÖÃæ
            {
                bool bSelfSide, bPerpCheck, bOppPerpCheck;
                MoveStruct lpmvs;

                bSelfSide = false;
                bPerpCheck = bOppPerpCheck = true;
                int index = nMoveNum - 1;
                while (index >= 0 && mvsList[index].wmv != 0 && mvsList[index].ucpcCaptured == 0)
                {
                    lpmvs = mvsList[index];
                    if (bSelfSide)
                    {
                        bPerpCheck = bPerpCheck && lpmvs.ucbCheck;
                        if (lpmvs.dwKey == zobr.dwKey)
                        {
                            nRecur--;
                            if (nRecur == 0)
                            {
                                return 1 + (bPerpCheck ? 2 : 0) + (bOppPerpCheck ? 4 : 0);
                            }
                        }
                    }
                    else
                    {
                        bOppPerpCheck = bOppPerpCheck && lpmvs.ucbCheck;
                    }
                    bSelfSide = !bSelfSide;
                    index--;
                }
                return 0;
            }

            public int RepValue(int nRepStatus)
            {        // ÖØ¸´¾ÖÃæ·ÖÖµ
                int vlReturn;
                vlReturn = ((nRepStatus & 2) == 0 ? 0 : nDistance - BAN_VALUE) +
                    ((nRepStatus & 4) == 0 ? 0 : BAN_VALUE - nDistance);
                return vlReturn == 0 ? DrawValue() : vlReturn;
            }

            public bool NullOkay()
            {                 // ÅÐ¶ÏÊÇ·ñÔÊÐí¿Õ²½²Ã¼ô
                return (sdPlayer == 0 ? vlWhite : vlBlack) > NULL_MARGIN;
            }

            public void Mirror(PositionStruct posMirror) // ¶Ô¾ÖÃæ¾µÏñ
            {
                int sq, pc;
                posMirror.ClearBoard();
                for (sq = 0; sq < 256; sq++)
                {
                    pc = ucpcSquares[sq];
                    if (pc != 0)
                    {
                        posMirror.AddPiece(MIRROR_SQUARE(sq), pc);
                    }
                }
                if (sdPlayer == 1)
                {
                    posMirror.ChangeSide();
                }
                posMirror.SetIrrev();
            }
        };

        static PositionStruct pos = new PositionStruct();

        const int PHASE_HASH = 0;
        const int PHASE_KILLER_1 = 1;
        const int PHASE_KILLER_2 = 2;
        const int PHASE_GEN_MOVES = 3;
        const int PHASE_REST = 4;

        class CompareBookItem : System.Collections.Generic.IComparer<BookItem>
        {
            public int Compare(BookItem lhs, BookItem rhs)
            {
                if (lhs.dwLock > rhs.dwLock)
                    return 1;
                else if (lhs.dwLock == rhs.dwLock)
                    return 0;
                else
                    return -1;
            }
        }

        class CompareHistory : System.Collections.Generic.IComparer<int>
        {
            public int Compare(int lhs, int rhs)
            {
                return m_search.nHistoryTable[lhs] - m_search.nHistoryTable[rhs];
            }
        }

        class CompareMvvLva : System.Collections.Generic.IComparer<int>
        {
            static sbyte[] cucMvvLva = new sbyte[24] {
              0, 0, 0, 0, 0, 0, 0, 0,
              5, 1, 1, 3, 4, 3, 2, 0,
              5, 1, 1, 3, 4, 3, 2, 0
            };

            public int Compare(int mv1, int mv2)
            {
                return MvvLva(mv1) - MvvLva(mv2);
            }

            int MvvLva(int mv)
            {
                return (cucMvvLva[pos.ucpcSquares[DST(mv)]] << 3) - cucMvvLva[pos.ucpcSquares[SRC(mv)]];
            }

        }

        class SortStruct
        {
            int mvHash, mvKiller1, mvKiller2; // ÖÃ»»±í×ß·¨ºÍÁ½¸öÉ±ÊÖ×ß·¨
            int nPhase, nIndex, nGenMoves;    // µ±Ç°½×¶Î£¬µ±Ç°²ÉÓÃµÚ¼¸¸ö×ß·¨£¬×Ü¹²ÓÐ¼¸¸ö×ß·¨
            int[] mvs = new int[MAX_GEN_MOVES];           // ËùÓÐµÄ×ß·¨

            public SortStruct()
            {
                InitializeIntArray(mvs);
            }

            public void Init(int mvHash_)
            { // ³õÊ¼»¯£¬Éè¶¨ÖÃ»»±í×ß·¨ºÍÁ½¸öÉ±ÊÖ×ß·¨
                mvHash = mvHash_;
                mvKiller1 = m_search.mvKillers[pos.nDistance, 0];
                mvKiller2 = m_search.mvKillers[pos.nDistance, 1];
                nPhase = PHASE_HASH;
            }

            public int Next() // µÃµ½ÏÂÒ»¸ö×ß·¨
            {
                int mv;
                switch (nPhase)
                {
                    // "nPhase"±íÊ¾×Å·¨Æô·¢µÄÈô¸É½×¶Î£¬ÒÀ´ÎÎª£º

                    // 0. ÖÃ»»±í×Å·¨Æô·¢£¬Íê³ÉºóÁ¢¼´½øÈëÏÂÒ»½×¶Î£»
                    case PHASE_HASH:
                        nPhase = PHASE_KILLER_1;
                        if (mvHash != 0)
                        {
                            return mvHash;
                        }
                        goto case PHASE_KILLER_1;
                    // ¼¼ÇÉ£ºÕâÀïÃ»ÓÐ"break"£¬±íÊ¾"switch"µÄÉÏÒ»¸ö"case"Ö´ÐÐÍêºó½ô½Ó×Å×öÏÂÒ»¸ö"case"£¬ÏÂÍ¬

                    // 1. É±ÊÖ×Å·¨Æô·¢(µÚÒ»¸öÉ±ÊÖ×Å·¨)£¬Íê³ÉºóÁ¢¼´½øÈëÏÂÒ»½×¶Î£»
                    case PHASE_KILLER_1:
                        nPhase = PHASE_KILLER_2;
                        if (mvKiller1 != mvHash && mvKiller1 != 0 && pos.LegalMove(mvKiller1))
                        {
                            return mvKiller1;
                        }
                        goto case PHASE_KILLER_2;

                    // 2. É±ÊÖ×Å·¨Æô·¢(µÚ¶þ¸öÉ±ÊÖ×Å·¨)£¬Íê³ÉºóÁ¢¼´½øÈëÏÂÒ»½×¶Î£»
                    case PHASE_KILLER_2:
                        nPhase = PHASE_GEN_MOVES;
                        if (mvKiller2 != mvHash && mvKiller2 != 0 && pos.LegalMove(mvKiller2))
                        {
                            return mvKiller2;
                        }
                        goto case PHASE_GEN_MOVES;

                    // 3. Éú³ÉËùÓÐ×Å·¨£¬Íê³ÉºóÁ¢¼´½øÈëÏÂÒ»½×¶Î£»
                    case PHASE_GEN_MOVES:
                        nPhase = PHASE_REST;
                        nGenMoves = pos.GenerateMoves(mvs, false);
                        Array.Sort<int>(mvs, 0, nGenMoves, new CompareHistory());
                        //qsort(mvs, nGenMoves, sizeof(int), CompareHistory);
                        nIndex = 0;
                        goto case PHASE_REST;

                    // 4. ¶ÔÊ£Óà×Å·¨×öÀúÊ·±íÆô·¢£»
                    case PHASE_REST:
                        while (nIndex < nGenMoves)
                        {
                            mv = mvs[nIndex];
                            nIndex++;
                            if (mv != mvHash && mv != mvKiller1 && mv != mvKiller2)
                            {
                                return mv;
                            }
                        }
                        return 0;

                    // 5. Ã»ÓÐ×Å·¨ÁË£¬·µ»ØÁã¡£
                    default:
                        return 0;
                }
            }
        }
    }

}

