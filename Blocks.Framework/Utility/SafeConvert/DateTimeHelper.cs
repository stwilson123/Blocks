using System;

namespace Blocks.Framework.Utility.SafeConvert
{
    /// <summary>
    /// 获取的时间类型
    /// </summary>
    public enum TimeFormatEnum
    {
        /// <summary>
        /// 时间格式yyyy-MM-dd
        /// </summary>
        Date = 0,

        /// <summary>
        /// 时间格式yyyy-MM-dd HH:mm:ss
        /// </summary>
        DateTime = 1,

        /// <summary>
        /// 时间格式yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        Milliseconds = 2,

        /// <summary>
        /// 时间格式HH:mm:ss
        /// </summary>
        Time = 3,

        /// <summary>
        /// 时间格式yyMMdd
        /// </summary>
        DateName = 4,

        /// <summary>
        /// 时间格式yy
        /// </summary>
        Year = 5,

        /// <summary>
        /// 时间格式yyyyMMdd
        /// </summary>
        DateTimeName = 6,

        /// <summary>
        /// 时间格式HHmmss
        /// </summary>
        ShortTime = 7,

        /// <summary>
        /// 时间格式yyyyMM
        /// </summary>
        DateMonthName = 8,

        /// <summary>
        /// yyMM
        /// </summary>
        ShortYearAndMonth = 9,

        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        DateTimeFileName = 10,
        /// <summary>
        /// yyyyMMddHHmmssfff
        /// </summary>
        DateTimeVersionName = 11,

        Month =12,
    }

    public class DateTimeHelper
    {
        /// <summary>
        /// 将DateTime类型转换为String类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeString(DateTime dTime, TimeFormatEnum timeFormatEnum)
        {

            string strDatetime = string.Empty;
            if (dTime != null && !dTime.Equals(DateTime.MaxValue) && !dTime.Equals(DateTime.MinValue))
            {
                strDatetime = dTime.ToString(SafeConvert.DateTimeFormat[(Int32)(timeFormatEnum)]);
            }
            else
            {
                strDatetime = string.Empty;
            }

            return strDatetime;
        }

        /// <summary>
        /// 将DateTime类型转换为String类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeStringByFormat(DateTime dTime, string format)
        {

            string strDatetime = string.Empty;
            if (dTime != null && !dTime.Equals(DateTime.MaxValue) && !dTime.Equals(DateTime.MinValue))
            {
                strDatetime = dTime.ToString(format);
            }
            else
            {
                strDatetime = string.Empty;
            }

            return strDatetime;
        }

        /// <summary>
        /// 将DateTime类型转换为String类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeStringByFormat(DateTime? dTime, string format)
        {

            string strDatetime = string.Empty;
            if (dTime.HasValue)
            {
                strDatetime = dTime.Value.ToString(format);
            }
            else
            {
                strDatetime = string.Empty;
            }

            return strDatetime;
        }


        /// <summary>
        /// 将DateTime类型转换为String类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeString(string dTime, TimeFormatEnum timeFormatEnum)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), timeFormatEnum);
        }

        /// <summary>
        /// 将DateTime类型转换为String类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeString(DateTime? dTime, TimeFormatEnum timeFormatEnum)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), timeFormatEnum);
        }
        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd HH:mm:ss类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTime(DateTime dTime)
        {
            return ToDateTimeString(dTime, TimeFormatEnum.DateTime);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd HH:mm:ss类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTime(string dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.DateTime);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd HH:mm:ss类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTime(DateTime? dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.DateTime);
        }


        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDate(DateTime dTime)
        {
            return ToDateTimeString(dTime, TimeFormatEnum.Date);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDate(string dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Date);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDate(DateTime? dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Date);
        }


        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToMonth(DateTime dTime)
        {
            return ToDateTimeString(dTime, TimeFormatEnum.Month);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToMonth(string dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Month);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToMonth(DateTime? dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Month);
        }
        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd HH:mm:ss.fff类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeWithMilliseconds(DateTime dTime)
        {
            return ToDateTimeString(dTime, TimeFormatEnum.Milliseconds);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd HH:mm:ss.fff类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeWithMilliseconds(string dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Milliseconds);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyy-MM-dd HH:mm:ss.fff类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToDateTimeWithMilliseconds(DateTime? dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Milliseconds);
        }

        /// <summary>
        /// 将DateTime类型转换为HH:mm:ss类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToTime(DateTime dTime)
        {
            return ToDateTimeString(dTime, TimeFormatEnum.Time);
        }

        /// <summary>
        /// 将DateTime类型转换为HH:mm:ss类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToTime(string dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Time);
        }

        /// <summary>
        /// 将DateTime类型转换为HH:mm:ss类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime">待转换DateTime</param>
        /// <returns>返回String类型值，如果传入的DateTime不能转换为String，返回空字符串</returns>
        public static string ToTime(DateTime? dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.Time);
        }

        /// <summary>
        /// 将DateTime类型转换为yyyyMMdd类型，如果传入的DateTime不能转换为String，返回空字符串
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(string dTime)
        {
            return ToDateTimeString(SafeConvert.ToDateTime(dTime), TimeFormatEnum.DateTimeName);
        }
    }
}
