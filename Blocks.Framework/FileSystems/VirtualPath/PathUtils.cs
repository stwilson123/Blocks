using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace Blocks.Framework.FileSystems.VirtualPath
{
    internal static class PathUtils
    {
        private static readonly char[] _invalidFileNameChars = ((IEnumerable<char>) Path.GetInvalidFileNameChars()).Where<char>((Func<char, bool>) (c =>
        {
            if ((int) c != (int) Path.DirectorySeparatorChar)
                return (int) c != (int) Path.AltDirectorySeparatorChar;
            return false;
        })).ToArray<char>();
        private static readonly char[] _invalidFilterChars = ((IEnumerable<char>) PathUtils._invalidFileNameChars).Where<char>((Func<char, bool>) (c =>
        {
            if (c != '*' && c != '|')
                return c != '?';
            return false;
        })).ToArray<char>();
        private static readonly char[] _pathSeparators = new char[2]
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        internal static bool HasInvalidPathChars(string path)
        {
            return path.IndexOfAny(PathUtils._invalidFileNameChars) != -1;
        }

        internal static bool HasInvalidFilterChars(string path)
        {
            return path.IndexOfAny(PathUtils._invalidFilterChars) != -1;
        }

        internal static string EnsureTrailingSlash(string path)
        {
            if (!string.IsNullOrEmpty(path) && (int) path[path.Length - 1] != (int) Path.DirectorySeparatorChar)
                return path + Path.DirectorySeparatorChar.ToString();
            return path;
        }

        internal static bool PathNavigatesAboveRoot(string path)
        {
            StringTokenizer stringTokenizer = new StringTokenizer(path, PathUtils._pathSeparators);
            int num = 0;
            foreach (StringSegment stringSegment in stringTokenizer)
            {
                if (!stringSegment.Equals(".") && !stringSegment.Equals(""))
                {
                    if (stringSegment.Equals(".."))
                    {
                        --num;
                        if (num == -1)
                            return true;
                    }
                    else
                        ++num;
                }
            }
            return false;
        }
    }
}