using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheCreationist.Core.Helpers
{
    public enum LookupTypes
    {
        Index,
        Hex,
        Rgb555
    }

    public struct LookupEntry
    {
        public string Index;
        public string Hex;
        public string Rgb555;
    }

    public class XtermHelper
    {
        public const int R_BIT_VALUE = 36;
        public const int G_BIT_VALUE = 6;
        public const int B_BIT_VALUE = 1;

        public const int LOW_COLOR_COUNT = 8;
        public const int HIGH_COLOR_COUNT = 8;
        public const int OTHER_COLOR_COUNT = 216;
        public const int GRAYSCALE_COLOR_COUNT = 24;

        public List<LookupEntry> XTERM_MAP = new List<LookupEntry>()
        {
            // Basic - low
            //new LookupEntry { Index = "000", Hex = "000000", Rgb555 = "000" },
            //new LookupEntry { Index = "001", Hex = "800000", Rgb555 = "300" },
            //new LookupEntry { Index = "002", Hex = "008000", Rgb555 = "030" },
            //new LookupEntry { Index = "003", Hex = "808000", Rgb555 = "330" },
            //new LookupEntry { Index = "004", Hex = "000080", Rgb555 = "003" },
            //new LookupEntry { Index = "005", Hex = "800080", Rgb555 = "303" },
            //new LookupEntry { Index = "006", Hex = "008080", Rgb555 = "030" },
            //new LookupEntry { Index = "007", Hex = "c0c0c0", Rgb555 = "444" },

            // Basic - high
            //new LookupEntry { Index = "008", Hex = "808080", Rgb555 = "333" },
            //new LookupEntry { Index = "009", Hex = "ff0000", Rgb555 = "500" },
            //new LookupEntry { Index = "010", Hex = "00ff00", Rgb555 = "050" },
            //new LookupEntry { Index = "011", Hex = "ffff00", Rgb555 = "550" },
            //new LookupEntry { Index = "012", Hex = "0000ff", Rgb555 = "005" },
            //new LookupEntry { Index = "013", Hex = "ff00ff", Rgb555 = "505" },
            //new LookupEntry { Index = "014", Hex = "00ffff", Rgb555 = "055" },
            //new LookupEntry { Index = "015", Hex = "ffffff", Rgb555 = "555" },

            // Extended
            new LookupEntry { Index = "016", Hex = "000000", Rgb555 = "000" },
            new LookupEntry { Index = "017", Hex = "00005f", Rgb555 = "001" },
            new LookupEntry { Index = "018", Hex = "000087", Rgb555 = "002" },
            new LookupEntry { Index = "019", Hex = "0000af", Rgb555 = "003" },
            new LookupEntry { Index = "020", Hex = "0000d7", Rgb555 = "004" },
            new LookupEntry { Index = "021", Hex = "0000ff", Rgb555 = "005" },
            new LookupEntry { Index = "022", Hex = "005f00", Rgb555 = "000" },
            new LookupEntry { Index = "023", Hex = "005f5f", Rgb555 = "001" },
            new LookupEntry { Index = "024", Hex = "005f87", Rgb555 = "002" },
            new LookupEntry { Index = "025", Hex = "005faf", Rgb555 = "003" },
            new LookupEntry { Index = "026", Hex = "005fd7", Rgb555 = "004" },
            new LookupEntry { Index = "027", Hex = "005fff", Rgb555 = "005" },
            new LookupEntry { Index = "028", Hex = "008700", Rgb555 = "010" },
            new LookupEntry { Index = "029", Hex = "00875f", Rgb555 = "011" },
            new LookupEntry { Index = "030", Hex = "008787", Rgb555 = "012" },
            new LookupEntry { Index = "031", Hex = "0087af", Rgb555 = "013" },
            new LookupEntry { Index = "032", Hex = "0087d7", Rgb555 = "014" },
            new LookupEntry { Index = "033", Hex = "0087ff", Rgb555 = "015" },
            new LookupEntry { Index = "034", Hex = "00af00", Rgb555 = "020" },
            new LookupEntry { Index = "035", Hex = "00af5f", Rgb555 = "021" },
            new LookupEntry { Index = "036", Hex = "00af87", Rgb555 = "022" },
            new LookupEntry { Index = "037", Hex = "00afaf", Rgb555 = "023" },
            new LookupEntry { Index = "038", Hex = "00afd7", Rgb555 = "024" },
            new LookupEntry { Index = "039", Hex = "00afff", Rgb555 = "025" },
            new LookupEntry { Index = "040", Hex = "00d700", Rgb555 = "030" },
            new LookupEntry { Index = "041", Hex = "00d75f", Rgb555 = "031" },
            new LookupEntry { Index = "042", Hex = "00d787", Rgb555 = "032" },
            new LookupEntry { Index = "043", Hex = "00d7af", Rgb555 = "033" },
            new LookupEntry { Index = "044", Hex = "00d7d7", Rgb555 = "034" },
            new LookupEntry { Index = "045", Hex = "00d7ff", Rgb555 = "035" },
            new LookupEntry { Index = "046", Hex = "00ff00", Rgb555 = "040" },
            new LookupEntry { Index = "047", Hex = "00ff5f", Rgb555 = "041" },
            new LookupEntry { Index = "048", Hex = "00ff87", Rgb555 = "042" },
            new LookupEntry { Index = "049", Hex = "00ffaf", Rgb555 = "043" },
            new LookupEntry { Index = "050", Hex = "00ffd7", Rgb555 = "044" },
            new LookupEntry { Index = "051", Hex = "00ffff", Rgb555 = "045" },
            new LookupEntry { Index = "052", Hex = "5f0000", Rgb555 = "050" },
            new LookupEntry { Index = "053", Hex = "5f005f", Rgb555 = "051" },
            new LookupEntry { Index = "054", Hex = "5f0087", Rgb555 = "052" },
            new LookupEntry { Index = "055", Hex = "5f00af", Rgb555 = "053" },
            new LookupEntry { Index = "056", Hex = "5f00d7", Rgb555 = "054" },
            new LookupEntry { Index = "057", Hex = "5f00ff", Rgb555 = "055" },
            new LookupEntry { Index = "058", Hex = "5f5f00", Rgb555 = "100" },
            new LookupEntry { Index = "059", Hex = "5f5f5f", Rgb555 = "101" },
            new LookupEntry { Index = "060", Hex = "5f5f87", Rgb555 = "102" },
            new LookupEntry { Index = "061", Hex = "5f5faf", Rgb555 = "103" },
            new LookupEntry { Index = "062", Hex = "5f5fd7", Rgb555 = "104" },
            new LookupEntry { Index = "063", Hex = "5f5fff", Rgb555 = "105" },
            new LookupEntry { Index = "064", Hex = "5f8700", Rgb555 = "110" },
            new LookupEntry { Index = "065", Hex = "5f875f", Rgb555 = "111" },
            new LookupEntry { Index = "066", Hex = "5f8787", Rgb555 = "112" },
            new LookupEntry { Index = "067", Hex = "5f87af", Rgb555 = "113" },
            new LookupEntry { Index = "068", Hex = "5f87d7", Rgb555 = "114" },
            new LookupEntry { Index = "069", Hex = "5f87ff", Rgb555 = "115" },
            new LookupEntry { Index = "070", Hex = "5faf00", Rgb555 = "120" },
            new LookupEntry { Index = "071", Hex = "5faf5f", Rgb555 = "121" },
            new LookupEntry { Index = "072", Hex = "5faf87", Rgb555 = "122" },
            new LookupEntry { Index = "073", Hex = "5fafaf", Rgb555 = "123" },
            new LookupEntry { Index = "074", Hex = "5fafd7", Rgb555 = "124" },
            new LookupEntry { Index = "075", Hex = "5fafff", Rgb555 = "125" },
            new LookupEntry { Index = "076", Hex = "5fd700", Rgb555 = "130" },
            new LookupEntry { Index = "077", Hex = "5fd75f", Rgb555 = "131" },
            new LookupEntry { Index = "078", Hex = "5fd787", Rgb555 = "132" },
            new LookupEntry { Index = "079", Hex = "5fd7af", Rgb555 = "133" },
            new LookupEntry { Index = "080", Hex = "5fd7d7", Rgb555 = "134" },
            new LookupEntry { Index = "081", Hex = "5fd7ff", Rgb555 = "135" },
            new LookupEntry { Index = "082", Hex = "5fff00", Rgb555 = "140" },
            new LookupEntry { Index = "083", Hex = "5fff5f", Rgb555 = "141" },
            new LookupEntry { Index = "084", Hex = "5fff87", Rgb555 = "142" },
            new LookupEntry { Index = "085", Hex = "5fffaf", Rgb555 = "143" },
            new LookupEntry { Index = "086", Hex = "5fffd7", Rgb555 = "144" },
            new LookupEntry { Index = "087", Hex = "5fffff", Rgb555 = "145" },
            new LookupEntry { Index = "088", Hex = "870000", Rgb555 = "150" },
            new LookupEntry { Index = "089", Hex = "87005f", Rgb555 = "151" },
            new LookupEntry { Index = "090", Hex = "870087", Rgb555 = "152" },
            new LookupEntry { Index = "091", Hex = "8700af", Rgb555 = "153" },
            new LookupEntry { Index = "092", Hex = "8700d7", Rgb555 = "154" },
            new LookupEntry { Index = "093", Hex = "8700ff", Rgb555 = "155" },
            new LookupEntry { Index = "094", Hex = "875f00", Rgb555 = "200" },
            new LookupEntry { Index = "095", Hex = "875f5f", Rgb555 = "201" },
            new LookupEntry { Index = "096", Hex = "875f87", Rgb555 = "202" },
            new LookupEntry { Index = "097", Hex = "875faf", Rgb555 = "203" },
            new LookupEntry { Index = "098", Hex = "875fd7", Rgb555 = "204" },
            new LookupEntry { Index = "099", Hex = "875fff", Rgb555 = "205" },
            new LookupEntry { Index = "100", Hex = "878700", Rgb555 = "210" },
            new LookupEntry { Index = "101", Hex = "87875f", Rgb555 = "211" },
            new LookupEntry { Index = "102", Hex = "878787", Rgb555 = "212" },
            new LookupEntry { Index = "103", Hex = "8787af", Rgb555 = "213" },
            new LookupEntry { Index = "104", Hex = "8787d7", Rgb555 = "214" },
            new LookupEntry { Index = "105", Hex = "8787ff", Rgb555 = "215" },
            new LookupEntry { Index = "106", Hex = "87af00", Rgb555 = "220" },
            new LookupEntry { Index = "107", Hex = "87af5f", Rgb555 = "221" },
            new LookupEntry { Index = "108", Hex = "87af87", Rgb555 = "222" },
            new LookupEntry { Index = "109", Hex = "87afaf", Rgb555 = "223" },
            new LookupEntry { Index = "110", Hex = "87afd7", Rgb555 = "224" },
            new LookupEntry { Index = "111", Hex = "87afff", Rgb555 = "225" },
            new LookupEntry { Index = "112", Hex = "87d700", Rgb555 = "230" },
            new LookupEntry { Index = "113", Hex = "87d75f", Rgb555 = "231" },
            new LookupEntry { Index = "114", Hex = "87d787", Rgb555 = "232" },
            new LookupEntry { Index = "115", Hex = "87d7af", Rgb555 = "233" },
            new LookupEntry { Index = "116", Hex = "87d7d7", Rgb555 = "234" },
            new LookupEntry { Index = "117", Hex = "87d7ff", Rgb555 = "235" },
            new LookupEntry { Index = "118", Hex = "87ff00", Rgb555 = "240" },
            new LookupEntry { Index = "119", Hex = "87ff5f", Rgb555 = "241" },
            new LookupEntry { Index = "120", Hex = "87ff87", Rgb555 = "242" },
            new LookupEntry { Index = "121", Hex = "87ffaf", Rgb555 = "243" },
            new LookupEntry { Index = "122", Hex = "87ffd7", Rgb555 = "244" },
            new LookupEntry { Index = "123", Hex = "87ffff", Rgb555 = "245" },
            new LookupEntry { Index = "124", Hex = "af0000", Rgb555 = "250" },
            new LookupEntry { Index = "125", Hex = "af005f", Rgb555 = "251" },
            new LookupEntry { Index = "126", Hex = "af0087", Rgb555 = "252" },
            new LookupEntry { Index = "127", Hex = "af00af", Rgb555 = "253" },
            new LookupEntry { Index = "128", Hex = "af00d7", Rgb555 = "254" },
            new LookupEntry { Index = "129", Hex = "af00ff", Rgb555 = "255" },
            new LookupEntry { Index = "130", Hex = "af5f00", Rgb555 = "300" },
            new LookupEntry { Index = "131", Hex = "af5f5f", Rgb555 = "311" },
            new LookupEntry { Index = "132", Hex = "af5f87", Rgb555 = "312" },
            new LookupEntry { Index = "133", Hex = "af5faf", Rgb555 = "313" },
            new LookupEntry { Index = "134", Hex = "af5fd7", Rgb555 = "314" },
            new LookupEntry { Index = "135", Hex = "af5fff", Rgb555 = "315" },
            new LookupEntry { Index = "136", Hex = "af8700", Rgb555 = "320" },
            new LookupEntry { Index = "137", Hex = "af875f", Rgb555 = "321" },
            new LookupEntry { Index = "138", Hex = "af8787", Rgb555 = "322" },
            new LookupEntry { Index = "139", Hex = "af87af", Rgb555 = "323" },
            new LookupEntry { Index = "140", Hex = "af87d7", Rgb555 = "324" },
            new LookupEntry { Index = "141", Hex = "af87ff", Rgb555 = "325" },
            new LookupEntry { Index = "142", Hex = "afaf00", Rgb555 = "330" },
            new LookupEntry { Index = "143", Hex = "afaf5f", Rgb555 = "331" },
            new LookupEntry { Index = "144", Hex = "afaf87", Rgb555 = "332" },
            new LookupEntry { Index = "145", Hex = "afafaf", Rgb555 = "333" },
            new LookupEntry { Index = "146", Hex = "afafd7", Rgb555 = "334" },
            new LookupEntry { Index = "147", Hex = "afafff", Rgb555 = "335" },
            new LookupEntry { Index = "148", Hex = "afd700", Rgb555 = "340" },
            new LookupEntry { Index = "149", Hex = "afd75f", Rgb555 = "341" },
            new LookupEntry { Index = "150", Hex = "afd787", Rgb555 = "342" },
            new LookupEntry { Index = "151", Hex = "afd7af", Rgb555 = "343" },
            new LookupEntry { Index = "152", Hex = "afd7d7", Rgb555 = "344" },
            new LookupEntry { Index = "153", Hex = "afd7ff", Rgb555 = "345" },
            new LookupEntry { Index = "154", Hex = "afff00", Rgb555 = "350" },
            new LookupEntry { Index = "155", Hex = "afff5f", Rgb555 = "351" },
            new LookupEntry { Index = "156", Hex = "afff87", Rgb555 = "352" },
            new LookupEntry { Index = "157", Hex = "afffaf", Rgb555 = "353" },
            new LookupEntry { Index = "158", Hex = "afffd7", Rgb555 = "354" },
            new LookupEntry { Index = "159", Hex = "afffff", Rgb555 = "355" },
            new LookupEntry { Index = "160", Hex = "d70000", Rgb555 = "400" },
            new LookupEntry { Index = "161", Hex = "d7005f", Rgb555 = "401" },
            new LookupEntry { Index = "162", Hex = "d70087", Rgb555 = "402" },
            new LookupEntry { Index = "163", Hex = "d700af", Rgb555 = "403" },
            new LookupEntry { Index = "164", Hex = "d700d7", Rgb555 = "404" },
            new LookupEntry { Index = "165", Hex = "d700ff", Rgb555 = "405" },
            new LookupEntry { Index = "166", Hex = "d75f00", Rgb555 = "410" },
            new LookupEntry { Index = "167", Hex = "d75f5f", Rgb555 = "411" },
            new LookupEntry { Index = "168", Hex = "d75f87", Rgb555 = "412" },
            new LookupEntry { Index = "169", Hex = "d75faf", Rgb555 = "413" },
            new LookupEntry { Index = "170", Hex = "d75fd7", Rgb555 = "414" },
            new LookupEntry { Index = "171", Hex = "d75fff", Rgb555 = "415" },
            new LookupEntry { Index = "172", Hex = "d78700", Rgb555 = "420" },
            new LookupEntry { Index = "173", Hex = "d7875f", Rgb555 = "421" },
            new LookupEntry { Index = "174", Hex = "d78787", Rgb555 = "422" },
            new LookupEntry { Index = "175", Hex = "d787af", Rgb555 = "423" },
            new LookupEntry { Index = "176", Hex = "d787d7", Rgb555 = "424" },
            new LookupEntry { Index = "177", Hex = "d787ff", Rgb555 = "425" },
            new LookupEntry { Index = "178", Hex = "d7af00", Rgb555 = "430" },
            new LookupEntry { Index = "179", Hex = "d7af5f", Rgb555 = "431" },
            new LookupEntry { Index = "180", Hex = "d7af87", Rgb555 = "432" },
            new LookupEntry { Index = "181", Hex = "d7afaf", Rgb555 = "433" },
            new LookupEntry { Index = "182", Hex = "d7afd7", Rgb555 = "434" },
            new LookupEntry { Index = "183", Hex = "d7afff", Rgb555 = "435" },
            new LookupEntry { Index = "184", Hex = "d7d700", Rgb555 = "440" },
            new LookupEntry { Index = "185", Hex = "d7d75f", Rgb555 = "441" },
            new LookupEntry { Index = "186", Hex = "d7d787", Rgb555 = "442" },
            new LookupEntry { Index = "187", Hex = "d7d7af", Rgb555 = "443" },
            new LookupEntry { Index = "188", Hex = "d7d7d7", Rgb555 = "444" },
            new LookupEntry { Index = "189", Hex = "d7d7ff", Rgb555 = "445" },
            new LookupEntry { Index = "190", Hex = "d7ff00", Rgb555 = "450" },
            new LookupEntry { Index = "191", Hex = "d7ff5f", Rgb555 = "451" },
            new LookupEntry { Index = "192", Hex = "d7ff87", Rgb555 = "452" },
            new LookupEntry { Index = "193", Hex = "d7ffaf", Rgb555 = "453" },
            new LookupEntry { Index = "194", Hex = "d7ffd7", Rgb555 = "454" },
            new LookupEntry { Index = "195", Hex = "d7ffff", Rgb555 = "455" },
            new LookupEntry { Index = "196", Hex = "ff0000", Rgb555 = "500" },
            new LookupEntry { Index = "197", Hex = "ff005f", Rgb555 = "501" },
            new LookupEntry { Index = "198", Hex = "ff0087", Rgb555 = "502" },
            new LookupEntry { Index = "199", Hex = "ff00af", Rgb555 = "503" },
            new LookupEntry { Index = "200", Hex = "ff00d7", Rgb555 = "504" },
            new LookupEntry { Index = "201", Hex = "ff00ff", Rgb555 = "505" },
            new LookupEntry { Index = "202", Hex = "ff5f00", Rgb555 = "510" },
            new LookupEntry { Index = "203", Hex = "ff5f5f", Rgb555 = "511" },
            new LookupEntry { Index = "204", Hex = "ff5f87", Rgb555 = "512" },
            new LookupEntry { Index = "205", Hex = "ff5faf", Rgb555 = "513" },
            new LookupEntry { Index = "206", Hex = "ff5fd7", Rgb555 = "514" },
            new LookupEntry { Index = "207", Hex = "ff5fff", Rgb555 = "515" },
            new LookupEntry { Index = "208", Hex = "ff8700", Rgb555 = "520" },
            new LookupEntry { Index = "209", Hex = "ff875f", Rgb555 = "521" },
            new LookupEntry { Index = "210", Hex = "ff8787", Rgb555 = "522" },
            new LookupEntry { Index = "211", Hex = "ff87af", Rgb555 = "523" },
            new LookupEntry { Index = "212", Hex = "ff87d7", Rgb555 = "524" },
            new LookupEntry { Index = "213", Hex = "ff87ff", Rgb555 = "525" },
            new LookupEntry { Index = "214", Hex = "ffaf00", Rgb555 = "530" },
            new LookupEntry { Index = "215", Hex = "ffaf5f", Rgb555 = "531" },
            new LookupEntry { Index = "216", Hex = "ffaf87", Rgb555 = "532" },
            new LookupEntry { Index = "217", Hex = "ffafaf", Rgb555 = "533" },
            new LookupEntry { Index = "218", Hex = "ffafd7", Rgb555 = "534" },
            new LookupEntry { Index = "219", Hex = "ffafff", Rgb555 = "535" },
            new LookupEntry { Index = "220", Hex = "ffd700", Rgb555 = "540" },
            new LookupEntry { Index = "221", Hex = "ffd75f", Rgb555 = "541" },
            new LookupEntry { Index = "222", Hex = "ffd787", Rgb555 = "542" },
            new LookupEntry { Index = "223", Hex = "ffd7af", Rgb555 = "543" },
            new LookupEntry { Index = "224", Hex = "ffd7d7", Rgb555 = "544" },
            new LookupEntry { Index = "225", Hex = "ffd7ff", Rgb555 = "545" },
            new LookupEntry { Index = "226", Hex = "ffff00", Rgb555 = "550" },
            new LookupEntry { Index = "227", Hex = "ffff5f", Rgb555 = "551" },
            new LookupEntry { Index = "228", Hex = "ffff87", Rgb555 = "552" },
            new LookupEntry { Index = "229", Hex = "ffffaf", Rgb555 = "553" },
            new LookupEntry { Index = "230", Hex = "ffffd7", Rgb555 = "554" },
            new LookupEntry { Index = "231", Hex = "ffffff", Rgb555 = "555" },

            // Grayscale
            //new LookupEntry { Index = "232", Hex = "080808", Rgb555 = "000" },
            //new LookupEntry { Index = "233", Hex = "121212", Rgb555 = "000" },
            //new LookupEntry { Index = "234", Hex = "1c1c1c", Rgb555 = "000" },
            //new LookupEntry { Index = "235", Hex = "262626", Rgb555 = "000" },
            //new LookupEntry { Index = "236", Hex = "303030", Rgb555 = "000" },
            //new LookupEntry { Index = "237", Hex = "3a3a3a", Rgb555 = "000" },
            //new LookupEntry { Index = "238", Hex = "444444", Rgb555 = "000" },
            //new LookupEntry { Index = "239", Hex = "4e4e4e", Rgb555 = "000" },
            //new LookupEntry { Index = "240", Hex = "585858", Rgb555 = "000" },
            //new LookupEntry { Index = "241", Hex = "626262", Rgb555 = "000" },
            //new LookupEntry { Index = "242", Hex = "6c6c6c", Rgb555 = "000" },
            //new LookupEntry { Index = "243", Hex = "767676", Rgb555 = "000" },
            //new LookupEntry { Index = "244", Hex = "808080", Rgb555 = "000" },
            //new LookupEntry { Index = "245", Hex = "8a8a8a", Rgb555 = "000" },
            //new LookupEntry { Index = "246", Hex = "949494", Rgb555 = "000" },
            //new LookupEntry { Index = "247", Hex = "9e9e9e", Rgb555 = "000" },
            //new LookupEntry { Index = "248", Hex = "a8a8a8", Rgb555 = "000" },
            //new LookupEntry { Index = "249", Hex = "b2b2b2", Rgb555 = "000" },
            //new LookupEntry { Index = "250", Hex = "bcbcbc", Rgb555 = "000" },
            //new LookupEntry { Index = "251", Hex = "c6c6c6", Rgb555 = "000" },
            //new LookupEntry { Index = "252", Hex = "d0d0d0", Rgb555 = "000" },
            //new LookupEntry { Index = "253", Hex = "dadada", Rgb555 = "000" },
            //new LookupEntry { Index = "254", Hex = "e4e4e4", Rgb555 = "000" },
            //new LookupEntry { Index = "255", Hex = "eeeeee", Rgb555 = "000" },
        };

        public string ConvertHexToRgb555(string hex)
        {
            return XTERM_MAP.First((x) => x.Hex.ToUpper() == hex.ToUpper()).Rgb555;
        }

        public string ConvertRgb55ToHex(string rgb555)
        {
            return XTERM_MAP.First((x) => x.Rgb555 == rgb555).Hex.ToUpper();
        }

        //public int ConvertRgbToRgb555(int r, int g, int b)
        //{
        //    var red = (XtermHelper.R_BIT_VALUE * r);
        //    var green = (XtermHelper.G_BIT_VALUE * g);
        //    var blue = (XtermHelper.B_BIT_VALUE * b);

        //    return (XtermHelper.LOW_COLOR_COUNT + XtermHelper.HIGH_COLOR_COUNT) + r + g + b;
        //}

        //public string ConvertRgb555ToIndex(char[] rgb)
        //{
        //    var r = (XtermHelper.R_BIT_VALUE * char.GetNumericValue(rgb[0]));
        //    var g = (XtermHelper.G_BIT_VALUE * char.GetNumericValue(rgb[1]));
        //    var b = (XtermHelper.B_BIT_VALUE * char.GetNumericValue(rgb[2]));

        //    var value = (XtermHelper.LOW_COLOR_COUNT + XtermHelper.HIGH_COLOR_COUNT) + r + g + b;

        //    var x = Math.Floor(value / 100);
        //    var y = Math.Floor((value % 100) / 10);
        //    var z = value % 10;

        //    return String.Format("{0} {1} {2}", x, y, z);
        //}
    }
}
