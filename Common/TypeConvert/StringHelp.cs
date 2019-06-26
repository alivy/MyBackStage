/*************************************************************************************
* 代码:Jarqi
* 时间:2015.08.12
* 说明:字符串处理公共基类
* 其他:
* 修改人：
* 修改时间：
* 修改说明：
************************************************************************************/
using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

namespace Common
{
    public class StringHelp
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public StringHelp() { }

        /// <summary>
        /// 判断参数字符是否为空
        /// </summary>
        /// <param name="sourceString">输入字符串</param>
        /// <returns>True:为空;falsh:不为空</returns>
        public static bool IsNull(string sourceString)
        {
            if (String.Equals(sourceString, null) || String.Equals(sourceString, ""))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 字符串保留尾数及星符数
        /// </summary>
        /// <param name="sourceString">字符串</param>
        /// <param name="starI">星符位数最少一位</param>
        /// <param name="StrI">字符串保留位数</param>
        /// <returns></returns>
        public static string IsStrTen(string sourceString, int starI, int StrI)
        {
            try
            {
                if (sourceString.Length >= StrI)
                {
                    sourceString = "*".PadLeft(starI, '*') + sourceString.Substring(sourceString.Length - StrI, StrI);
                }
                return sourceString;
            }
            catch
            {
                return sourceString;
            }
        }


        /// <summary>
        /// 获取操作字符串长度
        /// </summary>
        /// <param name="sourceString">输入字符串</param>
        /// <returns>返回长度</returns>
        public static int MaxLength(string sourceString)
        {
            return sourceString.Length;
        }

        /// <summary>
        /// 双向截取一定长度字符串
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <param name="leftOrRight">截取方向True:Left;Flash:Right</param>
        /// <param name="size">要截取的大小</param>
        /// <returns>返回截取后的字符串</returns>
        public static string InterceptString(string sourceString, bool leftOrRight, int size)
        {
            if (IsNull(sourceString))
            { throw new Exception("操作字符串参数不能为空！"); }
            int maxLength = MaxLength(sourceString);
            if (size >= maxLength)
            {
                return sourceString;
            }
            if (leftOrRight)
            {
                return sourceString.Substring(0, size);
            }
            return sourceString.Substring(maxLength - size, size);
        }

        /// <summary>
        /// 浅去除Html 
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回操作后字符串</returns>
        public static string WipeOffHtml(string sourceString)
        {
            return Regex.Replace(sourceString, "<[^>]*>", "");
        }

        /// <summary>
        /// 深去除Html包括脚本等
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回操作后字符串</returns>
        public static string ScriptHtml(string sourceString)
        {
            string[] aryReg = {
             @"<script[^>]*?>.*?</script>",
             @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
             @"([\r\n])[\s]+",
             @"&(quot|#34);",
             @"&(amp|#38);",
             @"&(lt|#60);",
             @"&(gt|#62);",
             @"&(nbsp|#160);",
             @"&(iexcl|#161);",
             @"&(cent|#162);",
             @"&(pound|#163);",
             @"&(copy|#169);",
             @"&#(\d+);",
             @"-->",
             @"<!--.*\n"
         };

            string[] aryRep = {
             "",
             "",
             "",
             "\"",
             "&",
             "<",
             ">",
             " ",
             "\xa1",//chr(161),
             "\xa2",//chr(162),
             "\xa3",//chr(163),
             "\xa9",//chr(169),
             "",
             "\r\n",
             ""
         };
            string newReg = aryReg[0];

            string strOutput = sourceString;

            for (int i = 0; i < aryReg.Length; i++)
            {

                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);

                strOutput = regex.Replace(strOutput, aryRep[i]);

            }

            strOutput.Replace("<", "");

            strOutput.Replace(">", "");

            strOutput.Replace("\r\n", "");

            return strOutput;
        }

        /// <summary>
        /// 输入HTML中的ImgUrl
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回干净的Url</returns>
        public static string GetImgUrl(string sourceString)
        {
            string str = string.Empty;
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>",
            RegexOptions.Compiled);
            Match m = r.Match(sourceString.ToLower());
            if (m.Success)
                str = m.Result("${url}");
            return str;
        }


        /// <summary> 
        /// 过滤SQL注入 
        /// </summary> 
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回安全的SQL</returns> 
        public static string FilterSql(string source)
        {
            string str = source;
            str = str.Replace("'", "''");
            str = str.Replace("<", "<");
            str = str.Replace(">", ">");
            if (String.IsNullOrEmpty(source))
            {
                return "";
            }
            //string _pattern = "exec |insert |select |delete |'|update |chr(|mid(|master |truncate |char(|declare | and |--";
            //if (Regex.IsMatch(source, _pattern, RegexOptions.IgnoreCase))
            //{
            //    source = Regex.Replace(source, _pattern, "", RegexOptions.IgnoreCase);
            //}
            return source;
        }

        /// <summary> 
        /// 移除非法或不友好字符 
        /// </summary> 
        /// <param name="keyWord">非法或不友好字符以|隔开</param> 
        /// <param name="chkStr">要处理的字符串</param> 
        /// <returns>处理后的字符串</returns> 
        public static string FilterBadWords(string keyWord, string sourceString)
        {
            if (sourceString == "")
            {
                return "";
            }
            string[] bwords = keyWord.Split('|');
            int i, j;
            string str;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < bwords.Length; i++)
            {
                str = bwords[i].ToString().Trim();
                string regStr, toStr;
                regStr = str;
                Regex r = new Regex(regStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                Match m = r.Match(sourceString);
                if (m.Success)
                {
                    j = m.Value.Length;
                    sb.Insert(0, "*", j);
                    toStr = sb.ToString();
                    sourceString = Regex.Replace(sourceString, regStr, toStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                }
                sb.Remove(0, sb.Length);
            }
            return sourceString;
        }

        /// <summary>
        /// 判断是否是合法IPV4
        /// </summary>
        /// <param name="sourceString">要处理的字符串</param>
        /// <returns>返回结果</returns>
        public static bool IsIPV4(string sourceString)
        {
            Regex rx = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            return (rx.IsMatch(sourceString));
        }

        /// <summary>
        /// IPV4转数字
        /// </summary>
        /// <param name="sourceString">要处理的字符串</param>
        /// <returns>返回结果</returns>
        public static double IPV4Convert(string sourceString)
        {
            if (!IsIPV4(sourceString))
            {
                throw new Exception("IP不合法!");
            }

            string[] listIP = sourceString.Split('.');

            StringBuilder intIP = new StringBuilder();

            for (int i = 0; i < listIP.Length; i++)
            {
                string ipPart = listIP[i].ToString();

                if (ipPart.Length == 3)
                {
                    intIP.Append(ipPart);
                }
                if (ipPart.Length == 2)
                {
                    intIP.Append("0" + ipPart);
                }
                if (ipPart.Length == 1)
                {
                    intIP.Append("00" + ipPart);
                }
            }
            return Convert.ToDouble(intIP.ToString());
        }

        /// <summary>
        /// 正则表达式判断字符串格式
        /// </summary>
        /// <param name="sourceString">要处理的字符串</param>
        /// <param name="strVerdict">正则表达式</param>
        /// <returns>返回结果</returns>
        public static bool FormatVerdict(string sourceString, string strVerdict)
        {
            Regex rx = new Regex(strVerdict);
            return (rx.IsMatch(sourceString));
        }

        /// <summary>
        /// 从字符串中的尾部删除指定的字符串
        /// </summary>
        /// <param name="sourceString">需要处理的字符串</param>
        /// <param name="removedString">删除匹配字符串</param>
        /// <returns>处理后字符串</returns>
        public static string Remove(string sourceString, string removedString)
        {
            try
            {
                if (sourceString.IndexOf(removedString) < 0)
                    throw new Exception("原字符串中不包含移除字符串！");
                string result = sourceString;
                int lengthOfSourceString = sourceString.Length;
                int lengthOfRemovedString = removedString.Length;
                int startIndex = lengthOfSourceString - lengthOfRemovedString;
                string tempSubString = sourceString.Substring(startIndex);
                if (tempSubString.ToUpper() == removedString.ToUpper())
                {
                    result = sourceString.Remove(startIndex, lengthOfRemovedString);
                }
                return result;
            }
            catch
            {
                return sourceString;
            }
        }

        /// <summary>
        /// 获取拆分符右边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string RightSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[tempString.Length - 1].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取拆分符左边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string LeftSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[0].ToString();
            }
            return result;
        }

        /// <summary>
        /// 去掉最后一个逗号
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static string DelLastComma(string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }

        /// <summary>
        /// 获取某一字符串在字符串数组中出现的次数
        /// </summary>
        /// <param name="stringArray">输入要查询的字符串</param>
        /// <param name="findString">输入操作字符串</param>
        /// <returns>返回出现次数</returns>
        public static int GetStringCount(string[] stringArray, string findString)
        {
            int count = -1;
            string totalString = GetArrayString(stringArray);
            string subString = totalString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = totalString.Substring(subString.IndexOf(findString));
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 获取某一字符串在字符串中出现的次数
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <param name="findString">要查询的字符</param>
        /// <returns>返回出现次数</returns>
        public static int GetStringCount(string sourceString, string findString)
        {
            int count = 0;
            int findStringLength = findString.Length;
            string subString = sourceString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString) + findStringLength);
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 截取从startString开始到原字符串结尾的所有字符
        /// </summary>
        /// <param name="sourceString">需要操作的字符串</param>
        /// <param name="startString">开始字符</param>
        /// <returns>返回结果</returns>
        public static string GetSubString(string sourceString, string startString)
        {
            try
            {
                int index = sourceString.ToUpper().IndexOf(startString);
                if (index > 0)
                {
                    return sourceString.Substring(index);
                }
                return sourceString;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 删除从beginRemovedString到 endRemovedString的字符串
        /// </summary>
        /// <param name="sourceString">需要操作的字符串</param>
        /// <param name="beginRemovedString">开始字符</param>
        /// <param name="endRemovedString">结束字符</param>
        /// <returns>返回结果</returns>
        public static string GetSubString(string sourceString, string beginRemovedString, string endRemovedString)
        {
            try
            {
                if (sourceString.IndexOf(beginRemovedString) != 0)
                    beginRemovedString = "";

                if (sourceString.LastIndexOf(endRemovedString, sourceString.Length - endRemovedString.Length) < 0)
                    endRemovedString = "";

                int startIndex = beginRemovedString.Length;
                int length = sourceString.Length - beginRemovedString.Length - endRemovedString.Length;
                if (length > 0)
                {
                    return sourceString.Substring(startIndex, length);
                }
                return sourceString;
            }
            catch
            {
                return sourceString; ;
            }
        }

        /// <summary>
        /// 按字节数取出字符串的长度
        /// </summary>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字符串的字节数</returns>
        public static int GetByteCount(string strTmp)
        {
            int intCharCount = 0;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intCharCount = intCharCount + 2;
                }
                else
                {
                    intCharCount = intCharCount + 1;
                }
            }
            return intCharCount;
        }

        /// <summary>
        /// 按字节数要在字符串的位置
        /// </summary>
        /// <param name="intIns">字符串的位置</param>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字节的位置</returns>
        public static int GetByteIndex(int intIns, string strTmp)
        {
            int intReIns = 0;
            if (strTmp.Trim() == "")
            {
                return intIns;
            }
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intReIns = intReIns + 2;
                }
                else
                {
                    intReIns = intReIns + 1;
                }
                if (intReIns >= intIns)
                {
                    intReIns = i + 1;
                    break;
                }
            }
            return intReIns;
        }

        /// <summary>
        /// 截取输入最大的字符串
        /// </summary>
        /// <param name="text">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>返回操作结果</returns>
        public static string InputText(string sourceString, int maxLength, string endWith = "...")
        {
            sourceString = sourceString.Trim();
            if (string.IsNullOrEmpty(sourceString))
                return string.Empty;
            if (sourceString.Length > maxLength)
                sourceString = sourceString.Substring(0, maxLength) + endWith;
            sourceString = Regex.Replace(sourceString, "[\\s]{2,}", " "); //two or more spaces
            sourceString = Regex.Replace(sourceString, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n"); //<br>
            sourceString = Regex.Replace(sourceString, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " "); // 
            sourceString = Regex.Replace(sourceString, "<(.|\\n)*?>", string.Empty); //any other tags
            sourceString = sourceString.Replace("'", "''");
            return sourceString;
        }

        /// <summary>
        /// 转义操作
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回操作结果</returns>
        public static string Encode(string sourceString)
        {
            sourceString = sourceString.Replace("'", "＇");
            sourceString = sourceString.Replace("\"", "\"");
            sourceString = sourceString.Replace("<", "<");
            sourceString = sourceString.Replace(">", ">");
            return sourceString;
        }

        /// <summary>
        /// 反义操作
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回操作结果</returns>
        public static string Decode(string sourceString)
        {
            sourceString = sourceString.Replace(">", ">");
            sourceString = sourceString.Replace("<", "<");
            sourceString = sourceString.Replace(" ", " ");
            sourceString = sourceString.Replace("\"", "\"");
            return sourceString;
        }

        /// <summary>
        /// 字符传的转换 用在查询 登陆时 防止恶意的盗取密码
        /// </summary>
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>返回操作结果</returns>
        public static string TBCode(string sourceString)
        {
            sourceString = sourceString.Replace("!", "");
            sourceString = sourceString.Replace("@", "");
            sourceString = sourceString.Replace("#", "");
            sourceString = sourceString.Replace("$", "");
            sourceString = sourceString.Replace("%", "");
            sourceString = sourceString.Replace("^", "");
            sourceString = sourceString.Replace("&", "");
            sourceString = sourceString.Replace("*", "");
            sourceString = sourceString.Replace("(", "");
            sourceString = sourceString.Replace(")", "");
            sourceString = sourceString.Replace("_", "");
            sourceString = sourceString.Replace("+", "");
            sourceString = sourceString.Replace("|", "");
            sourceString = sourceString.Replace("?", "");
            sourceString = sourceString.Replace("/", "");
            sourceString = sourceString.Replace(".", "");
            sourceString = sourceString.Replace(">", "");
            sourceString = sourceString.Replace("<", "");
            sourceString = sourceString.Replace("{", "");
            sourceString = sourceString.Replace("}", "");
            sourceString = sourceString.Replace("[", "");
            sourceString = sourceString.Replace("]", "");
            sourceString = sourceString.Replace("-", "");
            sourceString = sourceString.Replace("=", "");
            sourceString = sourceString.Replace(",", "");
            GetStarStr(leftCount: 2, source: "");
            return sourceString;
        }

        #region 将字符串转换为带*号的字符串
        /// <summary>
        /// 将字符串转换为带*号的字符串
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="leftCount">左边正常字符个数</param>
        /// <param name="rightCount">右边正常字符个数</param>
        /// <param name="startCount">星号字符个数</param>
        /// <param name="replaceChar">作为星号的字符，默认为:*</param>
        /// <returns></returns>
        public static string GetStarStr(string source = null, int leftCount = 2,
            int rightCount = 2, int startCount = 3, char replaceChar = '*')
        {
            if (string.IsNullOrEmpty(source)) return "";
            var len = source.Length - leftCount - rightCount;
            if (len < 0 || startCount < 0)
            {
                return source;
            }
            StringBuilder m_starChar = new StringBuilder();
            for (int i = 0; i < startCount; i++)
            {
                m_starChar.Append(replaceChar);
            }
            return String.Format("{0}{1}{2}", source.Substring(0, leftCount), m_starChar, source.Substring(source.Length - rightCount, rightCount));
        }
        #endregion


        #region 匹配是否为Id集合 (1,2,3,41,)
        /// <summary> 
        /// 匹配是否为Id集合 (1,2,3,41,)
        /// </summary> 
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>是否正确</returns> 
        public static bool MatchIds(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                string _pattern = @"^((\d+)\,){1,30}$";
                if (Regex.IsMatch(source, _pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 根据字符串(,1,2,)返回数组
        /// <summary> 
        /// 根据字符串(,1,2,)返回数组
        /// </summary> 
        /// <param name="sourceString">输入操作字符串</param>
        /// <returns>Id数组</returns> 
        public static string[] GetIdsArrByStr(string source)
        {
            string[] retAttr = null;
            string _pattern = @"^(\,((\d+)\,){1,30}|(\d+))$";  //验证:  (,1,2,3,41,)
            if (!String.IsNullOrEmpty(source) && Regex.IsMatch(source, _pattern, RegexOptions.IgnoreCase))
            {
                if (source.IndexOf(",") >= 0)
                {
                    var temp = source.Substring(1, source.Length - 2);
                    retAttr = temp.Split(',');
                }
                else
                {
                    retAttr = new string[] { source };
                }
            }
            return retAttr;
        }
        #endregion

        #region 金额转大写
        /// <summary>
        /// 金额转大写
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public static string ToUpper(decimal money)
        {
            //将小写金额转换成大写金额 
            String[] MyScale = { "分", "角", "圆", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
            String[] MyBase = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            String moneyStr = "";
            bool isPoint = false;
            string moneyDigital = Convert.ToDecimal(money.ToString("F2")).ToString();
            if (moneyDigital.IndexOf(".") != -1)
            {
                moneyDigital = moneyDigital.Remove(moneyDigital.IndexOf("."), 1);
                isPoint = true;
            }
            for (int i = moneyDigital.Length; i > 0; i--)
            {
                int MyData = Convert.ToInt16(moneyDigital[moneyDigital.Length - i].ToString());
                moneyStr += MyBase[MyData];
                if (isPoint == true)
                {
                    moneyStr += MyScale[i - 1];
                }
                else
                {
                    moneyStr += MyScale[i + 1];
                }
            }
            while (moneyStr.Contains("零零"))
                moneyStr = moneyStr.Replace("零零", "零");
            moneyStr = moneyStr.Replace("零亿", "亿");
            moneyStr = moneyStr.Replace("亿万", "亿");
            moneyStr = moneyStr.Replace("零万", "万");
            moneyStr = moneyStr.Replace("零仟", "零");
            moneyStr = moneyStr.Replace("零佰", "零");
            moneyStr = moneyStr.Replace("零拾", "零");
            while (moneyStr.Contains("零零"))
                moneyStr = moneyStr.Replace("零零", "零");
            moneyStr = moneyStr.Replace("零圆", "圆");
            moneyStr = moneyStr.Replace("零角", "");
            moneyStr = moneyStr.Replace("零分", "");
            moneyStr = moneyStr + "整";
            return moneyStr;
        }
        #endregion

        /// <summary>
        /// 字符转整型
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static List<int> ChartToInteger(IEnumerable<string> score)
        {
            var ints = new List<int>();
            foreach (var item in score)
            {
                int.TryParse(item, out int num);
                ints.Add(num);
            }
            return ints;
        }
    }
}