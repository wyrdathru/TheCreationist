using System.Windows.Media;

/* The MIT License (MIT)

Copyright (c) 2013 Kyle Wernham

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

namespace ProjectVoid.TheCreationist
{
    class AnsiColours
    {
        #region fourBitColours
        /* This is the legacy set of colours provided by TinyMUX's @colours table.
         * They are compatible values from the 4-bit colour range. */
        static public Color[] fourBitColours = new Color[] 
        {
            /* White */
            (Color)ColorConverter.ConvertFromString("#FFFFFF"), // High White
            (Color)ColorConverter.ConvertFromString("#BBBBBB"), // Low White
            /* Red */
            (Color)ColorConverter.ConvertFromString("#FF0000"), // High Red
            (Color)ColorConverter.ConvertFromString("#BB0000"), // Low Red
            /* Yellow */
            (Color)ColorConverter.ConvertFromString("#FFFF55"), // High Yellow
            (Color)ColorConverter.ConvertFromString("#BBBB00"), // Low Yellow
            /* Green */
            (Color)ColorConverter.ConvertFromString("#55FF55"), // High Green
            (Color)ColorConverter.ConvertFromString("#00BB00"), // Low Green
            /* Cyam */
            (Color)ColorConverter.ConvertFromString("#55FFFF"), // High Cyan
            (Color)ColorConverter.ConvertFromString("#00BBBB"), // Low Cyan
            /* Blue */
            (Color)ColorConverter.ConvertFromString("#5555FF"), // High Blue
            (Color)ColorConverter.ConvertFromString("#0000BB"), // Low Blue
            /* Purple */
            (Color)ColorConverter.ConvertFromString("#FF55FF"), // High Purple
            (Color)ColorConverter.ConvertFromString("#800080"), // Low Purple
            /* Black */
            (Color)ColorConverter.ConvertFromString("#555555"), // High Black
            (Color)ColorConverter.ConvertFromString("#000000"), // Low Black
        }; 
        #endregion

        #region eightBitColours
        /* This is the complete set of colours provided by TinyMUX's @colours table.
         * They are compatible values from the 8-bit colour range. */
        static public Color[] eightBitColours = new Color[]
        {
            (Color)ColorConverter.ConvertFromString("#000000"),
            (Color)ColorConverter.ConvertFromString("#080808"),
            (Color)ColorConverter.ConvertFromString("#121212"),
            (Color)ColorConverter.ConvertFromString("#1C1C1C"),
            (Color)ColorConverter.ConvertFromString("#262626"),
            (Color)ColorConverter.ConvertFromString("#303030"),
            (Color)ColorConverter.ConvertFromString("#3A3A3A"),
            (Color)ColorConverter.ConvertFromString("#444444"),
            (Color)ColorConverter.ConvertFromString("#4E4E4E"),
            (Color)ColorConverter.ConvertFromString("#555555"),
            (Color)ColorConverter.ConvertFromString("#585858"),
            (Color)ColorConverter.ConvertFromString("#5F5F5F"),
            (Color)ColorConverter.ConvertFromString("#626262"),
            (Color)ColorConverter.ConvertFromString("#6C6C6C"),
            (Color)ColorConverter.ConvertFromString("#767676"),
            (Color)ColorConverter.ConvertFromString("#808080"),
            (Color)ColorConverter.ConvertFromString("#878787"),
            (Color)ColorConverter.ConvertFromString("#8A8A8A"),
            (Color)ColorConverter.ConvertFromString("#949494"),
            (Color)ColorConverter.ConvertFromString("#9E9E9E"),
            (Color)ColorConverter.ConvertFromString("#A8A8A8"),
            (Color)ColorConverter.ConvertFromString("#AFAFAF"),
            (Color)ColorConverter.ConvertFromString("#B2B2B2"),
            (Color)ColorConverter.ConvertFromString("#BBBBBB"),
            (Color)ColorConverter.ConvertFromString("#BCBCBC"),
            (Color)ColorConverter.ConvertFromString("#C6C6C6"),
            (Color)ColorConverter.ConvertFromString("#D0D0D0"),
            (Color)ColorConverter.ConvertFromString("#D7D7D7"),
            (Color)ColorConverter.ConvertFromString("#DADADA"),
            (Color)ColorConverter.ConvertFromString("#E4E4E4"),
            (Color)ColorConverter.ConvertFromString("#EEEEEE"),
            (Color)ColorConverter.ConvertFromString("#FFFFFF"),
            (Color)ColorConverter.ConvertFromString("#FFD7D7"),
            (Color)ColorConverter.ConvertFromString("#D7AFAF"),
            (Color)ColorConverter.ConvertFromString("#AF8787"),
            (Color)ColorConverter.ConvertFromString("#875F5F"),
            (Color)ColorConverter.ConvertFromString("#FFAFAF"),
            (Color)ColorConverter.ConvertFromString("#D78787"),
            (Color)ColorConverter.ConvertFromString("#AF5F5F"),
            (Color)ColorConverter.ConvertFromString("#FF8787"),
            (Color)ColorConverter.ConvertFromString("#D75F5F"),
            (Color)ColorConverter.ConvertFromString("#FF5F5F"),
            (Color)ColorConverter.ConvertFromString("#FF5555"),
            (Color)ColorConverter.ConvertFromString("#5F0000"),
            (Color)ColorConverter.ConvertFromString("#870000"),
            (Color)ColorConverter.ConvertFromString("#AF0000"),
            (Color)ColorConverter.ConvertFromString("#BB0000"),
            (Color)ColorConverter.ConvertFromString("#D70000"),
            (Color)ColorConverter.ConvertFromString("#FF0000"),
            (Color)ColorConverter.ConvertFromString("#FF875F"),
            (Color)ColorConverter.ConvertFromString("#FFAF87"),
            (Color)ColorConverter.ConvertFromString("#D7875A"),
            (Color)ColorConverter.ConvertFromString("#FF5F00"),
            (Color)ColorConverter.ConvertFromString("#D75F00"),
            (Color)ColorConverter.ConvertFromString("#FFD7AF"),
            (Color)ColorConverter.ConvertFromString("#D7AF87"),
            (Color)ColorConverter.ConvertFromString("#AF875F"),
            (Color)ColorConverter.ConvertFromString("#FFAF5F"),
            (Color)ColorConverter.ConvertFromString("#FF8700"),
            (Color)ColorConverter.ConvertFromString("#AF5F00"),
            (Color)ColorConverter.ConvertFromString("#D78700"),
            (Color)ColorConverter.ConvertFromString("#FFD787"),
            (Color)ColorConverter.ConvertFromString("#D7AF5A"),
            (Color)ColorConverter.ConvertFromString("#FFAF00"),
            (Color)ColorConverter.ConvertFromString("#875F00"),
            (Color)ColorConverter.ConvertFromString("#FFD75F"),
            (Color)ColorConverter.ConvertFromString("#AF8700"),
            (Color)ColorConverter.ConvertFromString("#D7AF00"),
            (Color)ColorConverter.ConvertFromString("#FFD700"),
            (Color)ColorConverter.ConvertFromString("#FFFFD7"),
            (Color)ColorConverter.ConvertFromString("#D7D7AF"),
            (Color)ColorConverter.ConvertFromString("#AFAF87"),
            (Color)ColorConverter.ConvertFromString("#87875F"),
            (Color)ColorConverter.ConvertFromString("#FFFFAF"),
            (Color)ColorConverter.ConvertFromString("#D7D787"),
            (Color)ColorConverter.ConvertFromString("#AFAF5F"),
            (Color)ColorConverter.ConvertFromString("#FFFF87"),
            (Color)ColorConverter.ConvertFromString("#D7D75F"),
            (Color)ColorConverter.ConvertFromString("#FFFF5F"),
            (Color)ColorConverter.ConvertFromString("#FFFF55"),
            (Color)ColorConverter.ConvertFromString("#5F5F00"),
            (Color)ColorConverter.ConvertFromString("#878700"),
            (Color)ColorConverter.ConvertFromString("#AFAF00"),
            (Color)ColorConverter.ConvertFromString("#BBBB00"),
            (Color)ColorConverter.ConvertFromString("#D7D700"),
            (Color)ColorConverter.ConvertFromString("#FFFF00"),
            (Color)ColorConverter.ConvertFromString("#D7FF00"),
            (Color)ColorConverter.ConvertFromString("#AFD700"),
            (Color)ColorConverter.ConvertFromString("#87AF00"),
            (Color)ColorConverter.ConvertFromString("#D7FF5F"),
            (Color)ColorConverter.ConvertFromString("#5F8700"),
            (Color)ColorConverter.ConvertFromString("#AFFF00"),
            (Color)ColorConverter.ConvertFromString("#D7FF87"),
            (Color)ColorConverter.ConvertFromString("#AFD75F"),
            (Color)ColorConverter.ConvertFromString("#87D700"),
            (Color)ColorConverter.ConvertFromString("#5FAF00"),
            (Color)ColorConverter.ConvertFromString("#87FF00"),
            (Color)ColorConverter.ConvertFromString("#D7FFAF"),
            (Color)ColorConverter.ConvertFromString("#AFD787"),
            (Color)ColorConverter.ConvertFromString("#87AF5F"),
            (Color)ColorConverter.ConvertFromString("#AFFF5F"),
            (Color)ColorConverter.ConvertFromString("#5FD700"),
            (Color)ColorConverter.ConvertFromString("#5FFF00"),
            (Color)ColorConverter.ConvertFromString("#87D75A"),
            (Color)ColorConverter.ConvertFromString("#AFFF87"),
            (Color)ColorConverter.ConvertFromString("#87FF5F"),
            (Color)ColorConverter.ConvertFromString("#D7FFD7"),
            (Color)ColorConverter.ConvertFromString("#AFD7AF"),
            (Color)ColorConverter.ConvertFromString("#87AF87"),
            (Color)ColorConverter.ConvertFromString("#5F875F"),
            (Color)ColorConverter.ConvertFromString("#AFFFAF"),
            (Color)ColorConverter.ConvertFromString("#87D787"),
            (Color)ColorConverter.ConvertFromString("#5FAF5F"),
            (Color)ColorConverter.ConvertFromString("#87FF87"),
            (Color)ColorConverter.ConvertFromString("#5FD75F"),
            (Color)ColorConverter.ConvertFromString("#5FFF5F"),
            (Color)ColorConverter.ConvertFromString("#55FF55"),
            (Color)ColorConverter.ConvertFromString("#005F00"),
            (Color)ColorConverter.ConvertFromString("#008700"),
            (Color)ColorConverter.ConvertFromString("#00AF00"),
            (Color)ColorConverter.ConvertFromString("#00BB00"),
            (Color)ColorConverter.ConvertFromString("#00D700"),
            (Color)ColorConverter.ConvertFromString("#00FF00"),
            (Color)ColorConverter.ConvertFromString("#5FFF87"),
            (Color)ColorConverter.ConvertFromString("#87FFAF"),
            (Color)ColorConverter.ConvertFromString("#5FD787"),
            (Color)ColorConverter.ConvertFromString("#00FF5A"),
            (Color)ColorConverter.ConvertFromString("#00D75F"),
            (Color)ColorConverter.ConvertFromString("#AFFFD7"),
            (Color)ColorConverter.ConvertFromString("#87D7AF"),
            (Color)ColorConverter.ConvertFromString("#5FAF87"),
            (Color)ColorConverter.ConvertFromString("#5FFFAF"),
            (Color)ColorConverter.ConvertFromString("#00FF87"),
            (Color)ColorConverter.ConvertFromString("#00AF5F"),
            (Color)ColorConverter.ConvertFromString("#00D787"),
            (Color)ColorConverter.ConvertFromString("#87FFD7"),
            (Color)ColorConverter.ConvertFromString("#5FD7AF"),
            (Color)ColorConverter.ConvertFromString("#00FFAF"),
            (Color)ColorConverter.ConvertFromString("#00875F"),
            (Color)ColorConverter.ConvertFromString("#5FFFD7"),
            (Color)ColorConverter.ConvertFromString("#00AF87"),
            (Color)ColorConverter.ConvertFromString("#00D7AF"),
            (Color)ColorConverter.ConvertFromString("#00FFD7"),
            (Color)ColorConverter.ConvertFromString("#008785"),
            (Color)ColorConverter.ConvertFromString("#D7FFFF"),
            (Color)ColorConverter.ConvertFromString("#AFD7D7"),
            (Color)ColorConverter.ConvertFromString("#87AFAF"),
            (Color)ColorConverter.ConvertFromString("#5F8787"),
            (Color)ColorConverter.ConvertFromString("#AFFFFF"),
            (Color)ColorConverter.ConvertFromString("#87D7D7"),
            (Color)ColorConverter.ConvertFromString("#5FAFAF"),
            (Color)ColorConverter.ConvertFromString("#87FFFF"),
            (Color)ColorConverter.ConvertFromString("#5FD7D7"),
            (Color)ColorConverter.ConvertFromString("#5FFFFF"),
            (Color)ColorConverter.ConvertFromString("#55FFFF"),
            (Color)ColorConverter.ConvertFromString("#005F5F"),
            (Color)ColorConverter.ConvertFromString("#00AFAF"),
            (Color)ColorConverter.ConvertFromString("#00BBBB"),
            (Color)ColorConverter.ConvertFromString("#00D7D7"),
            (Color)ColorConverter.ConvertFromString("#00FFFF"),
            (Color)ColorConverter.ConvertFromString("#00D7FF"),
            (Color)ColorConverter.ConvertFromString("#00AFD7"),
            (Color)ColorConverter.ConvertFromString("#0087AF"),
            (Color)ColorConverter.ConvertFromString("#5FD7FF"),
            (Color)ColorConverter.ConvertFromString("#005F87"),
            (Color)ColorConverter.ConvertFromString("#00AFFF"),
            (Color)ColorConverter.ConvertFromString("#87D7FF"),
            (Color)ColorConverter.ConvertFromString("#5FAFD7"),
            (Color)ColorConverter.ConvertFromString("#0087D7"),
            (Color)ColorConverter.ConvertFromString("#005FAF"),
            (Color)ColorConverter.ConvertFromString("#0087FF"),
            (Color)ColorConverter.ConvertFromString("#AFD7FF"),
            (Color)ColorConverter.ConvertFromString("#87AFD7"),
            (Color)ColorConverter.ConvertFromString("#5F87AF"),
            (Color)ColorConverter.ConvertFromString("#5FAFFF"),
            (Color)ColorConverter.ConvertFromString("#005FD7"),
            (Color)ColorConverter.ConvertFromString("#005FFF"),
            (Color)ColorConverter.ConvertFromString("#87AFFF"),
            (Color)ColorConverter.ConvertFromString("#5F87D7"),
            (Color)ColorConverter.ConvertFromString("#5F87FF"),
            (Color)ColorConverter.ConvertFromString("#D7D7FF"),
            (Color)ColorConverter.ConvertFromString("#AFAFD7"),
            (Color)ColorConverter.ConvertFromString("#8787AF"),
            (Color)ColorConverter.ConvertFromString("#5F5F87"),
            (Color)ColorConverter.ConvertFromString("#AFAFFF"),
            (Color)ColorConverter.ConvertFromString("#8787D7"),
            (Color)ColorConverter.ConvertFromString("#5F5FAF"),
            (Color)ColorConverter.ConvertFromString("#8787FF"),
            (Color)ColorConverter.ConvertFromString("#5F5FD7"),
            (Color)ColorConverter.ConvertFromString("#5F5FFF"),
            (Color)ColorConverter.ConvertFromString("#5555FF"),
            (Color)ColorConverter.ConvertFromString("#00005F"),
            (Color)ColorConverter.ConvertFromString("#000087"),
            (Color)ColorConverter.ConvertFromString("#0000AF"),
            (Color)ColorConverter.ConvertFromString("#0000BB"),
            (Color)ColorConverter.ConvertFromString("#0000D7"),
            (Color)ColorConverter.ConvertFromString("#0000FF"),
            (Color)ColorConverter.ConvertFromString("#875FFF"),
            (Color)ColorConverter.ConvertFromString("#AF87FF"),
            (Color)ColorConverter.ConvertFromString("#875FD7"),
            (Color)ColorConverter.ConvertFromString("#5F00FF"),
            (Color)ColorConverter.ConvertFromString("#5F00D7"),
            (Color)ColorConverter.ConvertFromString("#D7AFFF"),
            (Color)ColorConverter.ConvertFromString("#AF87D7"),
            (Color)ColorConverter.ConvertFromString("#875FAF"),
            (Color)ColorConverter.ConvertFromString("#AF5FFF"),
            (Color)ColorConverter.ConvertFromString("#8700FF"),
            (Color)ColorConverter.ConvertFromString("#5F00AF"),
            (Color)ColorConverter.ConvertFromString("#8700D7"),
            (Color)ColorConverter.ConvertFromString("#D787FF"),
            (Color)ColorConverter.ConvertFromString("#AF5FD7"),
            (Color)ColorConverter.ConvertFromString("#AF00FF"),
            (Color)ColorConverter.ConvertFromString("#5F0087"),
            (Color)ColorConverter.ConvertFromString("#D75FFF"),
            (Color)ColorConverter.ConvertFromString("#8700AF"),
            (Color)ColorConverter.ConvertFromString("#AF00D7"),
            (Color)ColorConverter.ConvertFromString("#D700FF"),
            (Color)ColorConverter.ConvertFromString("#FFD7FF"),
            (Color)ColorConverter.ConvertFromString("#D7AFD7"),
            (Color)ColorConverter.ConvertFromString("#AF87AF"),
            (Color)ColorConverter.ConvertFromString("#875F87"),
            (Color)ColorConverter.ConvertFromString("#FFAFFF"),
            (Color)ColorConverter.ConvertFromString("#D787D7"),
            (Color)ColorConverter.ConvertFromString("#AF5FAF"),
            (Color)ColorConverter.ConvertFromString("#FF87FF"),
            (Color)ColorConverter.ConvertFromString("#D75FD7"),
            (Color)ColorConverter.ConvertFromString("#FF5FFF"),
            (Color)ColorConverter.ConvertFromString("#FF55FF"),
            (Color)ColorConverter.ConvertFromString("#5F005F"),
            (Color)ColorConverter.ConvertFromString("#870087"),
            (Color)ColorConverter.ConvertFromString("#AF00AF"),
            (Color)ColorConverter.ConvertFromString("#BB00BB"),
            (Color)ColorConverter.ConvertFromString("#D700D7"),
            (Color)ColorConverter.ConvertFromString("#FF00FF"),
            (Color)ColorConverter.ConvertFromString("#FF00D7"),
            (Color)ColorConverter.ConvertFromString("#D700AF"),
            (Color)ColorConverter.ConvertFromString("#AF0087"),
            (Color)ColorConverter.ConvertFromString("#FF5FD7"),
            (Color)ColorConverter.ConvertFromString("#87005F"),
            (Color)ColorConverter.ConvertFromString("#FF00AF"),
            (Color)ColorConverter.ConvertFromString("#FF87D7"),
            (Color)ColorConverter.ConvertFromString("#D75FAF"),
            (Color)ColorConverter.ConvertFromString("#D70087"),
            (Color)ColorConverter.ConvertFromString("#AF005F"),
            (Color)ColorConverter.ConvertFromString("#FF0087"),
            (Color)ColorConverter.ConvertFromString("#FFAFD7"),
            (Color)ColorConverter.ConvertFromString("#D787AF"),
            (Color)ColorConverter.ConvertFromString("#AF5F87"),
            (Color)ColorConverter.ConvertFromString("#FF5FAF"),
            (Color)ColorConverter.ConvertFromString("#D7005F"),
            (Color)ColorConverter.ConvertFromString("#FF005F"),
            (Color)ColorConverter.ConvertFromString("#FF87AF"),
            (Color)ColorConverter.ConvertFromString("#D75F87"),
            (Color)ColorConverter.ConvertFromString("#FF5F87"),
        }; 
        #endregion
    }
}