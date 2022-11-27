﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2019
{
    class Day8 : IDay
    {

        private const string input = "112222222222222212222122220212222222202222222222222122222222202100222222120222101222222122222222221222220102222222222020222222212221222222222222221222102222222222222212222222220222222222202222222222222122222222222100222222120222121222222122222222221222221002222222222222222222202221222222222222222222022222222222222212222222220222222222202222222222222222222222222201222222120222010222222022222222221222220012222222222021222222212222222222222222221222112222222222222222222022222212222222222222222222222022222222202000222222021222021222222222222222221222220202222222222222222222202221222222222222222222212222222222222202222222222222222222222222222222222222222222212021222222021222001222222222222222222222222122222222222222222222222221222222222222221222222222222222222202222122222222222222202222212222222122222202212110222222222222220222222022222222220222221012222222222020222222222221222222222222220222022222222222222202222222221222222222222222212222202122222202202012222222222222220222222122222222222222222122222222222220222222222222222222222212220222112222222222222222222122221212222222222222202222212222222222222111222222121222121222222122222222220222222100222222222221222222212221222222222222221222022222222222222212222022222212222222212222202222222222222212122011222222222222002222222122222222222222220012222222222021222222222220222222222222220222022222222222220212222022222202222222202222212222201122222202222212222222222222102222222222222222222222222212222222222222222222212220222222222202221222002222222222222222222022220212222222212222212222220122222212022201222222121222100222222122222222220222222122222222222122222222202222222222222212222222002222222222222222222122222212222222222222222222222222222222102110222222221222101222222222222222220222221212222222222021222222202222222222222222221222202220222222220212222222222222222222212222202222211022222202022221222222222222001222222122222222021222220012222222222122222222212221222222222222222222212222222222221202222022221222222222222222212222200222222212012101222222021222200222222022222222120222222210222222222021222222212222222222222222220222002222222222221212222222220212222222202222222222212222222222202202222222221222100222222022222222220222222000222222222121222222212220222222222212221222112220222222221222222222222222222222212222212222222122222212122212212222221222010222222022222222022222222002222222222120222222202222222222222202222222222222222222221222222022221222222222212222202222212122222222122201212222120222101222222222222222222222221212222222222120222222222220222222222202220222022221222222222212222122220222222222212222212222212022222202222012222222120222000222222222222222121222222221222022222220222222222222222222222202221222002221222222222222222122222212222222202222012222211222222212112111212222122222020222222022222222022222221011222122222220222222212221222220222211221222112222222222221222222122221222222222222222102222211222222212002101202222020222111222222222022222122222222022222222222122222222222222222220222220222222202222222222220222222122221212222222202222222222222022222222202200222221021222220222221222122222122222221011222022222222222222202220222220222201222222112222222222221202222122222222222222222222012222210122222212012000202222121222011222221022220222020222220002222122222222222222222220222222222202020222002220222222222222222122221022222222202222222222220122222212102201222221022222221222221022022222222222220100202222222220222222202220222221202201221222022221222222220222222022220002222222202222202222210222222202022101202222220222120222221220021222021222221000212122222220222222212222222220222211122222002222222222220202222222221212222222222222102222221222222212002001202222021222011222220021121222222222220201212122222021222222212220222222222221220220022220222222221212222122222222222222222222102222222022222202112000222021022222011222222121220222121222220101222022222122222222212221222222212212122222022220222222121222222222220202222222222222012222210222222212222120222220221222222222221121121222220222220022212222222222222222222220222220202220020222202221212222020222222222221022222222212222112222202022222212212021202222021222202222220122221222120222220020112122222022222222200222222222222210120222022222202222021212222022222002202222222222222222221122222202112201212221020222200220221020120222022222220012212022222120222222212222222222212211122222012222212222020222222122221002212222212222102222222222222210002010212221221222211222221220121222222222220211102122222022222222220221222221222220122221222221212222022202222122222122222222202222012222211022222202222110222121122222112221221222222222121222220202122022222022222222220220222222212200020221202220222222221202222222220202222222222222022222211122222222222120212022221222120222222221120222122222222100222022222021222222201222222220222220220222022222212222120211222122220222212222202222222222202222222212212221202221221222021221220021220222022222222002112122222021222222211220222221222200122222212021202222122200222122220022202222202222222222211222222200122122212122220222112221220122122222120222222222102022222122202222220221222221202202220222102222212222022200222112220222212222212222212222220022222212012020202121120222212221222020220222120222221101122222222022212222210220222221202211222220022021202222222220222120222222202222202222012222210222222210212021212021120222102220222222220222120222221000122122222220202222222020222222212222021222112022212222221202222021222102220222212222022222221022222222202102212021121222101222222120021222122222222212122122222021202222210022222222222212220222112021202222122201222101222212202222202222102222211022222210012010212120222222121221222121021222022222222002112022222222212202200222222222222210222220012121212222120202222012221102212222201222212222212122222220122101212221222222212222222020222222122222221122212022222121212222211221222222222220120222222220222222120221222200221112211222212222112222211122222220222010222121220222211220220120021222022222220021222222222020212212210221222221202222122222012220202222221211222212220202211222201222112222212122222202112222222121122222220222222120120222021222222001212122222021212212200121222221212200022222222020202222020222222100220112221222200222022222221222222200012010212221120222200222222220121222221222222211022222222222202222221122222221212210221220222120212222121200222100220202211222200222022122212122222222122100212121120122212221221021122222020222222102202222222022202222210121222220212200220222122221222222222200222110222012201222222222002022211122222211222001222021022122101220220221120222021222222200112122222020212212210020222220222212122221222220202222020212222010221122202222220222022222220022222221122221212120021122222221222220020222021222220010012222222022202212221121222220222201122220212221212222020211122202220112200222201222112022201222222202102022212021020122021222220222221222021222221000102022222120212222220021222221222222121220102022202222020202222000220002211222221222002022220022222201022112222122121222102220221120121222120222221201212122222222212202220022222222221200022222222222222222022212122211221112222222222222012022222222222220122120212122021022202220222220220222122222222210212122222122212222212021222200220201021222202120222222022221222212220222210222212222202122211222222212002001202022222022200220220222120222021222221222002122222122212222221221222221201211221221012220222221220200122212212102210222210222212122221122222212122101212122022022211221221122122222222222222200012102222020212212221221222202210211221221022021202220022220222101202222211222221222002122211222222202122100212221122022001220222122221222121222221020212002222122222212202222222201222210221220001221212221022221222201211222211222202222122222211222222222122202212121121122010220221222121222021222220201012212222220212212211220222211212202220222221022202222220220122101202022221222221222122222201022222200002202212220021222000222222021220222102222222202202102222120222212211222222201211210021222211120222221021221122120220222220222210222012222201022222221202121222020122122201220021120221222211222220122212122222220212222201221222210200211220222111020212221222211122000220002210222212222202002201222222201222202222222020222011221021120222222211222221120002222222022202222210222222201210201122220001222222221020202122020202022212222201202002012202222222200002101212122121222012220022022120222120222220110102002222122212202211022222220210200220221222121222222220212122020212112220222221212102102212022222210212221202122221022021220021021221222010222222011102222222221202212212020222222202201221221211222202221020201022120202102201222210212202222212122222201222011212121222122201222120220220222121222221210002112222220222202222022222211212221121222101121222222120200222202201012222222201212002012210222222221002111202021221122000222220020121222020222220122202022222020222212211121122211222222220222020220202221120221222111212022221222201212112122222022222221022100222122122122222222122022220222222222220002202212222222212202221222022210221222022220212121222222022222122201222122212222202202212202221122222211102112202222020022002222121221022022200220221020011202222121212212200121122200210202222222111220222220222200022112212002211222200222122202211022222212120211222221220022020220022221021222011222221120011002222021212212222222222210220221121221020222212221022220122000202102201222221212002122212122222001012121202122222022001222220022121022201222020200212112222020202222210021122212212210221222000221222221222202022122200222200222221222222212220222222200000012212120021122210222020120221222202221021000022102222220212112201122022222202201022222211222212222221222122221212112222222220212112212212022222112212011222122220122120222221220022022110221020111000112222120202222202021022221202200022220000220222220222221122122222002222222212212022112221122222201112210212220122222212222020222112222022221122122010222222120222012220121122211212200122220212222202220020202022210212022210222201222222122222022222011002011202120222122211221221222211102101221121202220102222122212212221222122210200202220221010122210220020201022020212122202222212202222212222222222001022201212022020222102220021122201221011221222212010002222220202002200021222201210221121220110221210221020211222012202102201222200212102212222122222212221001202021022122211222020122111221110220021212222012222221212212210020222212201200122221200222211222120212122122201222210222202222222002202222222222001222222121221022020222221022121021110220122211000122222121222011211022222221221111020222011120220220020221022020222212212222222212002212200022222221002221212220021022021222220022222000200222020100202002222200212002200220022220202201220222222021222220221210122101222102220222202212212102201122222002020012222022222202012221221222122121120221021112221212212001222000211220022222212002121220010121212220220201222020220012212222210222102022221022222221101021212021021122122222121222222101101222120110110112212010102111220222222202212011020222212222111222221210122020221212220222211202110022221222222102011120212120020102120221222222111121220222120011112002202010122212222220222201200112021200112121120220020201022110200002202222211222200022220122222200220111212021122012012221221222000211012221021201122212222022022012212022022200202012222212201022210221220211022102210112212222220222200012201222222011120101212022121022002220122121212122002221122101110202212110112012210122022201212210112212110121221222021212022200201102200222211202200022220222222000210201202020021010002222020021201101220220122201220202222102102021212120122211201001112212020122110221022220122122220012212222211202200212212022222022202222222121221202122221220222011000012222221212020212202222212202200221022201221111000200000220002220200210122221222012202222211212000202222222222021201101202122122020222220120221112200121210020210101122212201012021210221122200211102212200012020200220220222022111212202211222222222222212202122222111202022222222121021111220220120001220120222021211200002222000022000202221222210210220101220102021110220112200221122210212212222220222012112220122222112020110222220121121211202020022111000100211022112020212212121102022212222122202222220201212002022012222002202020010222212212202202202222212212022222010210101202122020002101222022122202022121222120220100102212120012020220020222201201000211211120120112222202212222021021112220202202202120022212022222021111220202222022222121201022120201010212211122121202002212121222222222022022202221021010201111201200220112201122220010222210222202222102012222222222112020002222020121220221221021221211212021202121021100022212020212210212122122201220120000211202201121222101222220202122002210222210202221222202222222221210001212022120221222211020022022001002220220222000012202021002020200222022210221011220200101202012220220202221201020122220222222202202112220222222102121221212122021100121210020122112201210220021120012002202101002002220220222212201022100221122220011210011222021020201022211222200212121112200022022002210100212222022122000200021120110212011200220001020202212022122122202220222211002022220200122020122200001212021200110202212222212212001212222022222000200101222010221210201211021022111100001221120221201212212222202211220121022221001121111221222212011200110202120121001202200212222202212202212122022100010012222211121110001202022222120022020221222002202002222011102212200220022210111202010210122212200211000102021001010202220222202202100222220122022100112200212211122012202011020220102021210202020221000222202020212201210022122202012211212211010011200211002020122110021022202202222222211112211222222000102010222222221112210211122122022112122222020001002212222101102221201021222210201010220221011022102202110221221022010222222222221212022102221222022120111002202210022121121100221121221000120212022212121112212220112010212222222211020222010202112201010212110001220102211222220222210202210112221122212201222200212112221120220212020221201100210210120111020112212100112201211020122222020211220200002101012201211122221022201012201202212222102112221022102021100112212201021102002011222120220200200202022110012122202000212202220221122211020011012210022000121200121200122202011212220212212212222222202022022000220022212102221100111101221120002111210000122201220212202100002202202022022201110201021222110120202222000012021211021122212221220202201022201122122001201022212210220121002000020121212020022021221222111012202210102211201121022210002120001220212020012210020222021120121222220221201222102002202222022000201001222122220222210010222022102200120202220002201102212001112012210200022220101122102202111121121210100220221020012212200210212202111212201022012201010020202201020202110002122220211200202212021001110022202001022022220201122212012010111210101002020222011112222111210122200220012220220002210222222001012120222112121012001112021121202212121112121012112112212210002010210222222202111200210211202121012002102000101211111010100120221012202200121110120121110120020121100021222100011001001200112000212100001000010102120220120002000122100001220102";
        private (int, int) dimension = (25, 6);

       // private const string input = "0222112222120000";
        //private (int, int) dimension = (2, 2);

        //private const string input = "123456789012";
        //private (int, int) dimension = (3, 2);

        public long Part1()
        {
            var layers = Convert(input, dimension);

            var minZeroDigits = int.MaxValue;
            var sumOneTwo = 0;

            foreach (var layer in layers)
            {
                var zerodigits = layer.Count(d => d == 0);
                if (zerodigits <= minZeroDigits)
                {
                    sumOneTwo = layer.Count(d => d == 1) * layer.Count(d => d == 2);
                    minZeroDigits = zerodigits;
                }
            }

            return sumOneTwo;
        }

        private IEnumerable<IEnumerable<byte>> Convert(string input, (int, int) dimension)
        {
            var bytes = input.ToCharArray().Select(c => byte.Parse(c.ToString()));
            var layers = new List<IEnumerable<byte>>();

            var layersize = dimension.Item1 * dimension.Item2;

            int start = 0;

            while (start < bytes.Count())
            {
                var layer = bytes.Skip(start).Take(layersize);
                layers.Add(layer);
                start += layersize;
            }
            return layers;
        }

        public long Part2()
        {
            var layers = Convert(input, dimension).Reverse();
            var layersize = dimension.Item1 * dimension.Item2;
            var image = layers.First().ToArray();

            foreach (var layer in layers.Skip(1))
            {
                var layerbytes = layer.ToArray();
                for (int pixel = 0; pixel < layersize; pixel++)
                {
                    var value = layerbytes[pixel];
                    if (value != 2)
                    {
                        image[pixel] = value;
                    }
                }
            }

            var rows = dimension.Item2;
            var columns = dimension.Item1;
            var color = Console.BackgroundColor;
            Console.WriteLine();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var pixel = (row * columns) + column;
                    var value = image[pixel];
                    if (value == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        
                    }
                    if (value == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    if (value == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(' ');
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = color;

            return 0;
        }
    }
}
